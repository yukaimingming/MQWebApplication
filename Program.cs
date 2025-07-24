using MQWebApplication.DBhelpler;
using Scalar.AspNetCore;
using Serilog;
using System.Runtime.InteropServices;
using MQWebApplication;
using MQWebApplication.Controllers;

//serilog��ʼ������
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
// ȷ�� MQSDK ���ҵ������ļ�
var baseDir = AppDomain.CurrentDomain.BaseDirectory;
var configPath = Path.Combine(baseDir, "SDKConfig.properties");

// ȷ�� MQSDK.dll ����
var dllPath = Path.Combine(baseDir, "MQSDK.dll");
if (!File.Exists(dllPath)) throw new FileNotFoundException($"MQSDK.dll �ļ�������: {dllPath}");


// ��������ļ��Ƿ����
if (!File.Exists(configPath)) throw new FileNotFoundException($"MQSDK�����ļ�������: {configPath}");
// Add services to the container.
Console.WriteLine($"Base Directory: {AppContext.BaseDirectory}");
Console.WriteLine($"DLL Path: {Path.Combine(AppContext.BaseDirectory, "MQSDK.dll")}");
Console.WriteLine($"Config Path: {Path.Combine(AppContext.BaseDirectory, "SDKConfig.properties")}");
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//���ݿ����ע��
builder.Services.AddSingleton(new SqlServerDatabase(builder.Configuration.GetConnectionString("connect_his")!));
////��������ע��
builder.Services.AddTransient<IQueryHelperSqlserver, QueryHelperSqlserver>();
builder.Services.AddTransient<IGetxml, Getxml.ESBXmlGenerator>();
builder.Services.AddHostedService<DailyMqTaskService>();
builder.Services.AddScoped<MQController>(); // ע��������Ա�����ע��

//���ȳ�ʼ�� Serilog ��ȱ���ǣ����� ASP.NET Core �����ķ��񣨰������ú�������ע�룩�в����á�appsettings.json
//Ϊ�˽��������⣬Serilog ֧�����׶γ�ʼ������ʼ����������¼���ڳ�������ʱ�������ã�һ���������أ��ü�¼��������ȫ���õļ�¼��ȡ����
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File($"Logs/.log", rollingInterval: RollingInterval.Day,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}"));


var app = builder.Build();
Log.Information("����������{0}", "Services Starting...");
Log.Information("���л���: {0}",
    $"MQ�ӿڷ�����������!!! ��������رսӿڷ��񣬱���������ݶ�ʧ!!! Powered by {RuntimeInformation.FrameworkDescription} ǿ������ on Kestrel");

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