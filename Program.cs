/* BISMILLAH AR-RAHMAN AR-RAHEEM */
/* In the Name of ALLAH Most Gracious, Most Merciful */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DAL.DataContext;
using DAL.Repositories;
using WebApiSample.Middlewares;
using WebApiSample.Services;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using Domain.Entities;
using Microsoft.AspNetCore.OData;

namespace WebApiSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddControllers().AddOData(options => options
                                                                .AddRouteComponents("odata", GetEdmModel())
                                                                .Select()
                                                                .Filter()
                                                                .OrderBy()
                                                                .SetMaxTop(20)
                                                                .Count()
                                                                .Expand());

            builder.Services.AddDbContext<TodoDbContext>(options => 
                                                            options.UseSqlite(builder.Configuration.GetConnectionString("TodoDbContext") ?? 
                                                                    throw new InvalidOperationException("Connection string 'TodoDbContext' not found.")));

            builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
                        
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // factory based middleware can have singleton, transient and scoped lifetime
            // whereas convention based middleware is singleton
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

            /// factory baed middleware can be added like this as well
            //app.UseMiddleware<FactoryBasedMiddleware>();

            app.MapControllers();

            app.Run();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<TodoItem>("TodoItemV2");
            return builder.GetEdmModel();
        }
    }
}