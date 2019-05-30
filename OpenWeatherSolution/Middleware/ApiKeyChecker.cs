using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OpenWeatherSolution.Models;

namespace OpenWeatherSolution.Middleware
{
    public class ApiKeyChecker
    {
        private const string AppId = "appid";

        private readonly RequestDelegate _next;
        private readonly AppConfig _config;

        public ApiKeyChecker(
            RequestDelegate next,
            AppConfig config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey(AppId))
            {
                var appIdVal = (string) context.Request.Query[AppId];
                if (appIdVal.Equals(_config.AppId, StringComparison.InvariantCultureIgnoreCase))
                {
                    await _next(context);
                }
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Missed or wrong AppId");
            }
        }
    }

    public static class ApiKeyMwEx
    {
        public static IApplicationBuilder UseApiKeyChecker(this IApplicationBuilder app)
            => app.UseMiddleware<ApiKeyChecker>();
    }
}