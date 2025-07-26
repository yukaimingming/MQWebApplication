using MQWebApplication.DBhelpler;
using Scalar.AspNetCore;
using Serilog;
using System.Runtime.InteropServices;
using MQWebApplication;
using MQWebApplication.Controllers;

//serilog初始化设置
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
// 确保 MQSDK 能找到配置文件
var baseDir = AppDomain.CurrentDomain.BaseDirectory;
var configPath = Path.Combine(baseDir, "SDKConfig.properties");

// 确保 MQSDK.dll 存在
var dllPath = Path.Combine(baseDir, "MQSDK.dll");
if (!File.Exists(dllPath)) throw new FileNotFoundException($"MQSDK.dll 文件不存在: {dllPath}");


// 检查配置文件是否存在
if (!File.Exists(configPath)) throw new FileNotFoundException($"MQSDK配置文件不存在: {configPath}");
// Add services to the container.
Console.WriteLine($"Base Directory: {AppContext.BaseDirectory}");
Console.WriteLine($"DLL Path: {Path.Combine(AppContext.BaseDirectory, "MQSDK.dll")}");
Console.WriteLine($"Config Path: {Path.Combine(AppContext.BaseDirectory, "SDKConfig.properties")}");
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//数据库服务注册
builder.Services.AddSingleton(new SqlServerDatabase(builder.Configuration.GetConnectionString("connect_his")!));
////服务依赖注册
builder.Services.AddTransient<IQueryHelperSqlserver, QueryHelperSqlserver>();
builder.Services.AddTransient<IGetxml, Getxml.ESBXmlGenerator>();
builder.Services.AddHostedService<DailyMqTaskService>();
builder.Services.AddScoped<MQController>(); // 注册控制器以便依赖注入

//首先初始化 Serilog 的缺点是，来自 ASP.NET Core 主机的服务（包括配置和依赖项注入）尚不可用。appsettings.json
//为了解决这个问题，Serilog 支持两阶段初始化。初始“引导”记录器在程序启动时立即配置，一旦主机加载，该记录器将被完全配置的记录器取代。
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File($"Logs/.log", rollingInterval: RollingInterval.Day,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}"));


var app = builder.Build();
Log.Information("服务启动:Services Starting...");
Log.Information($"运行环境:MQ接口服务正在运行!!! 请勿随意关闭接口服务，避免造成数据丢失!!! Powered by {RuntimeInformation.FrameworkDescription} 强力驱动 on Kestrel");

app.MapOpenApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.MapScalarApiReference(); //scalar api reference
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();