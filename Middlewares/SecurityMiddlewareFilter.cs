using Microsoft.Extensions.Primitives;

namespace WebApiSample.Middlewares
{
    /// <summary>
    /// This middlware is intended to be applied to any action or controller as a filter middleware.
    /// This middleware implements a scenario of online assessment.
    /// Middleware will disallow access to an endpoint if request header does not contain pre-defined
    /// header key and value.
    /// </summary>
    public class SecurityMiddlewareFilter
    {
        private const string _headerKey = "accessKey";
        private const string _headerValue = "key313";

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                if (IsSecurityKeyPresent(context)) 
                {
                    await next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            });
        }

        private bool IsSecurityKeyPresent(HttpContext context) 
        { 
            var result = false;
            if (context.Request.Headers.TryGetValue(_headerKey, out StringValues value))
            {
                if (value.Equals(_headerValue)) 
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
