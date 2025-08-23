using MQWebApplication.Models;
using Newtonsoft.Json;

namespace MQWebApplication.middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // 如果响应已经开始，就无法再写入数据，只记录日志
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(ex, "响应已开始，无法返回异常数据");
                    return;
                }

                // 设置返回类型和状态码
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // 构造返回对象
                var errorResponse = new Result
                {
                    resultCode = "500",
                    resultDesc = ex.Message // 后端可用的异常信息
                };

                // 记录完整日志（包含堆栈）
                _logger.LogError(ex, "捕获异常: {0}", JsonConvert.SerializeObject(errorResponse));

                // 返回数据给后端
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
