using MQWebApplication.Models;
using Newtonsoft.Json;

namespace MQWebApplication.middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _env; //开发环境

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning(ex, "响应已开始，无法返回异常数据");
                return;
            }

            // 日志（完整异常）
            _logger.LogError(ex, "未处理异常: {Message}", ex.Message);

            // 响应头
            context.Response.Clear();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = ex switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            // 构造返回
            var errorResponse = new Result
            {
                resultCode = context.Response.StatusCode.ToString(),
                resultDesc = _env.IsDevelopment()
                    ? $"{ex.Message}\n{ex.StackTrace}"
                    : "系统异常，请联系管理员"
            };

            // 返回前端
            var json = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}