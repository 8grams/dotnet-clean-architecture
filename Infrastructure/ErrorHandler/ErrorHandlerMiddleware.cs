using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Application.Exceptions;

namespace WebApi.Infrastructure.ErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorMsg = ex.Message;

            if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

            if (ex is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                errorMsg = ex.Message;

            }
            var result = JsonConvert.SerializeObject(new {
                success = false,
                message = errorMsg
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
