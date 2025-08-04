using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic; // 需要引入这个
using System.Threading;
using System.Threading.Tasks;
using MQWebApplication.Controllers;
using MQWebApplication.Models;

namespace MQWebApplication;

public class DailyMqTaskService : BackgroundService
{
    private readonly ILogger<DailyMqTaskService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    public DailyMqTaskService(IServiceProvider serviceProvider, ILogger<DailyMqTaskService> logger, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // _logger.LogInformation("Daily Concurrent MQ Task Service is starting.");

        // while (!stoppingToken.IsCancellationRequested)
        //     try
        //     {
        //         // 计算下一次执行时间（第二天的午夜0点）
        //         var now = DateTime.Now;
        //         var nextRunTime = now.Date.AddDays(1); // .Date 获取日期部分（时间为00:00:00）
        //         // 计算需要延迟的时间
        //         var delay = nextRunTime - now;

        //         _logger.LogInformation("Next daily concurrent MQ task will run at: {runTime}", nextRunTime);
        //         await Task.Delay(delay, stoppingToken);

        //         _logger.LogInformation("Starting concurrent MQ tasks at {time}", DateTime.Now);

        //         // 使用 Task.WhenAll 并发执行任务
        //         await ExecuteConcurrentTasks(stoppingToken);

        //         _logger.LogInformation("All concurrent MQ tasks finished successfully.");
        //     }
        //     catch (TaskCanceledException)
        //     {
        //         break;
        //     }
        //     catch (Exception ex)
        //     {
        //         // 如果 Task.WhenAll 中的任何一个任务失败，异常会在这里被捕获
        //         _logger.LogError(ex, "An error occurred during concurrent task execution.");
        //         await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        //     }

        // _logger.LogInformation("Daily Concurrent MQ Task Service is stopping.");
        _logger.LogInformation("定时任务服务正在启动...");

        while (!stoppingToken.IsCancellationRequested)
            try
            {
                // 计算下一次执行时间（第二天的午夜0点）
                var now = DateTime.Now;
                // var nextRunTime = now.Date.AddDays(1); // .Date 获取日期部分（时间为00:00:00）
                var executionTime = TimeSpan.Parse(_configuration["DailyExecutionTime"] ?? "00:00:00");

                // 计算下一次执行时间
                var nextRunTime = now.Date.Add(executionTime);
                if (now >= nextRunTime) nextRunTime = nextRunTime.AddDays(1);
                // 计算需要延迟的时间
                var delay = nextRunTime - now;

                _logger.LogInformation("下一次定时任务将在{runTime}执行", nextRunTime.ToString("yyyy-MM-dd HH:mm:ss"));
                await Task.Delay(delay, stoppingToken);

                _logger.LogInformation("开始执行定时任务，当前时间: {time}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                // 使用 Task.WhenAll 并发执行任务
                await ExecuteConcurrentTasks(stoppingToken);

                _logger.LogInformation("所有定时任务已成功完成");
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                // 如果 Task.WhenAll 中的任何一个任务失败，异常会在这里被捕获
                _logger.LogError(ex, "定时任务执行过程中发生错误");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }

        _logger.LogInformation("定时任务服务正在停止");
    }

    private async Task ExecuteConcurrentTasks(CancellationToken stoppingToken)
    {

        // var ksData = new PutMsgDto();
        // var ryData = new PutMsgDto();
        // var bqData = new PutMsgDto();
        // 1. 创建一个任务列表
        var tasks = new List<Task>();

        // 2. 使用 Task.Run 将每个同步方法包装成一个任务，并添加到列表
        //    Task.Run 会从线程池中获取一个线程来执行你的代码
        tasks.Add(Task.Run(() =>
    {
        try
        {
            // 创建一个新的依赖注入作用域
            using var scope = _serviceProvider.CreateScope();
            var controller = scope.ServiceProvider.GetRequiredService<MQController>();
            _logger.LogInformation("开始推送科室信息...");
            controller.ComposePutAndGetMsgks(new PutMsgDto());
            _logger.LogInformation("科室信息推送完成。");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "科室信息推送失败");
            throw;
        }
    }, stoppingToken));

        tasks.Add(Task.Run(() =>
        {
            try
            {
                // 创建一个新的依赖注入作用域
                using var scope = _serviceProvider.CreateScope();
                var controller = scope.ServiceProvider.GetRequiredService<MQController>();
                _logger.LogInformation("开始推送员工信息...");
                controller.ComposePutAndGetMsgry(new PutMsgDto());
                _logger.LogInformation("员工信息推送完成。");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "员工信息推送失败");
                throw;
            }
        }, stoppingToken));

        tasks.Add(Task.Run(() =>
        {
            try
            {
                // 创建一个新的依赖注入作用域
                using var scope = _serviceProvider.CreateScope();
                var controller = scope.ServiceProvider.GetRequiredService<MQController>();
                _logger.LogInformation("开始推送病区信息...");
                controller.ComposePutAndGetMsgbq(new PutMsgDto());
                _logger.LogInformation("病区信息推送完成。");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "病区信息推送失败");
                throw;
            }
        }, stoppingToken));

        // 3. 等待所有任务完成
        await Task.WhenAll(tasks);
    }
}