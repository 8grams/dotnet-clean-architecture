using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.Application.Interfaces.Authorization;
using WebApi.Application.Interfaces;

namespace WebApi.Infrastructure.Authorization
{
    public class AuthmeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthmeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IWebApiDbContext dbContext, IAuthUser authUser)
        {
            var authFilterCtx = context.Request;
            var request = authFilterCtx.HttpContext.Request;

            if (request.Path.ToString().StartsWith("/auth", StringComparison.CurrentCultureIgnoreCase)
            || request.Path.ToString().StartsWith("/static", StringComparison.CurrentCultureIgnoreCase)
            || request.Path.ToString().StartsWith("/analytics", StringComparison.CurrentCultureIgnoreCase)) 
            {
                return _next.Invoke(context);
            }

            // do auth here

            return _next.Invoke(context);
        }
    }
}
