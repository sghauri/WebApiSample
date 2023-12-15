using WebApiSample.Middlewares;

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

            builder.Services.AddTransient<FactoryBasedMiddleware>();
            
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