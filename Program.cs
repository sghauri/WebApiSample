/* BISMILLAH AR-RAHMAN AR-RAHEEM */
/* In the Name of Allah (SWT) Most Gracious, Most Merciful */

using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiSample.Middlewares;
using WebApiSample.Services;

namespace WebApiSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // factory based middleware can have singleton, transient and scoped lifetime
            // convention based middleware is singleton
            builder.Services.AddTransient<FactoryBasedMiddleware>();

            /// add logging
            //builder.Services.AddSingleton<ICustomLogger, SimpleLogger>();
            //builder.Services.AddSingleton<ICustomLogger, DetailedLogger>();
            
            /// This code is intended to demo the injection of multiple services implmenting the same interface:
            /// following method will result in both implmentations being injected
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ICustomLogger, SimpleLogger>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ICustomLogger, DetailedLogger>());
                        
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            /// inline middleware
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() => {

                    context.Response.Headers.Add("X-Date-Info", DateTime.Now.Date.ToString("dddd dd, MMMM yyyy"));
                    return Task.CompletedTask;
                });

                await next();
            });

            /// convention based middleware
            app.UseMiddleware<ConventionBasedMiddleware>();
            /// factory baed middleware
            app.UseMiddleware<FactoryBasedMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}