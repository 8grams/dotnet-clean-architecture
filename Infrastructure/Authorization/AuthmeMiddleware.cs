using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Infrastructure.Authorization
{
    public class AuthmeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthmeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, ISFDDBContext dbContext, IAuthUser authUser, IAuthAdmin authAdmin)
        {
            var authFilterCtx = context.Request;
            var request = authFilterCtx.HttpContext.Request;

            // handle admin
            if (request.Path.ToString().StartsWith("/admin", StringComparison.CurrentCultureIgnoreCase))
            {
                return this.handleAdminRoutes(context, dbContext, authAdmin);
            }

            if (request.Path.ToString().StartsWith("/auth", StringComparison.CurrentCultureIgnoreCase)
            || request.Path.ToString().StartsWith("/static", StringComparison.CurrentCultureIgnoreCase)
            || request.Path.ToString().StartsWith("/analytics", StringComparison.CurrentCultureIgnoreCase)) 
            {
                return _next.Invoke(context);
            }

            string authHeader = "";
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                authHeader = authToken.SingleOrDefault();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            if (string.IsNullOrEmpty(authHeader)) throw new UnauthorizedAccessException();

            var token = authHeader.Replace("Bearer", "").Trim();
            var auth = dbContext.AccessTokens.Where(e => e.AuthToken.Equals(token))
                .Where(e => e.ExpiresAt > DateTime.Now)
                .Include(e => e.User)
                .Include(e => e.User.SalesmanData)
                .FirstOrDefault();

            if (auth == null) throw new UnauthorizedAccessException();

            authUser.Name = auth.User.Salesman.SalesmanName;
            authUser.SalesCode = auth.User.Salesman.SalesmanCode;
            authUser.UserId = auth.User.Id;

            return _next.Invoke(context);
        }

        private Task handleAdminRoutes(HttpContext context, ISFDDBContext dbContext, IAuthAdmin authAdmin)
        {
            var authFilterCtx = context.Request;
            var request = authFilterCtx.HttpContext.Request;

            if (request.Path.ToString().StartsWith("/admin/auth", StringComparison.CurrentCultureIgnoreCase)) 
            {
                return _next.Invoke(context);
            }

            string authHeader = "";
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                authHeader = authToken.SingleOrDefault();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            if (string.IsNullOrEmpty(authHeader)) throw new UnauthorizedAccessException();

            var token = authHeader.Replace("Bearer", "").Trim();
            var auth = dbContext.AdminTokens.Where(e => e.AuthToken.Equals(token))
                .Where(e => e.ExpiresAt > DateTime.Now)
                .Include(e => e.Admin)
                .FirstOrDefault();

            if (auth == null) throw new UnauthorizedAccessException();

            authAdmin.Name = auth.Admin.Name;
            return _next.Invoke(context);
        }
    }
}
