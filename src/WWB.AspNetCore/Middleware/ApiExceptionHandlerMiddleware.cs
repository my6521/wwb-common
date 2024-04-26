using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WWB.Common;
using WWB.Common.Dtos;
using WWB.Common.Extensions;

namespace WWB.AspNetCore.Middleware
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Exception exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (exception != null)
                {
                    await HandleExceptionAsync(context, exception);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "全局异常！！！");

            var errorResult = new ErrorApiResult();
            if (ex is FriendlyException appFriendlyException)
            {
                errorResult.Code = appFriendlyException.ErrorCode;
                errorResult.Message = appFriendlyException.Message;
            }
            else
            {
                errorResult.Code = context.Response.StatusCode;
                errorResult.Message = ex.Message;
            }
            await WriteResponse(context, errorResult);
        }

        private async Task WriteResponse(HttpContext context, ErrorApiResult errorResult)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(errorResult.ToJson());
        }
    }
}