using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ECommerce.API.Middleware
{
    public class CorrelationIdMiddleware
    {
        public const string HeaderName = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderName, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString("N");
                context.Request.Headers[HeaderName] = correlationId;
            }

            context.Response.Headers[HeaderName] = correlationId.ToString();
            await _next(context);
        }
    }
}
