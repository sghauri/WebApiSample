namespace WebApiSample.Middlewares
{
    public class FactoryBasedMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate requestDelegate)
        {
            context.Response.OnStarting(() => {
                context.Response.Headers.Add("X-Time-Info", DateTime.Now.ToString("HH:mm:ss"));
                return Task.CompletedTask;
            });
            await requestDelegate(context);
        }

    }
}
