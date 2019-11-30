using LeoProject.Infrastructure.Controllers.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeoProject.Infrastructure.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //context.Session[""]
            // Do something with context near the beginning of request processing.
            var token = context.Request.Headers["token"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                var apiResult = new ApiResult(ResultCode.Forbidden_ERROR, "没有权限访问");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResult));
                return;
            }
            await _next.Invoke(context);

            // Clean up.
        }
    }
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
