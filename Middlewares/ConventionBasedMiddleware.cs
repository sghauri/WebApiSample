using Microsoft.Extensions.Primitives;

namespace WebApiSample.Middlewares
{
    public class ConventionBasedMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionBasedMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            if (context.Request.Headers.TryGetValue("Host", out StringValues value))
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Host-Info", value);
                    return Task.CompletedTask;
                });
            }
            await _next(context);
        }
    }
}
