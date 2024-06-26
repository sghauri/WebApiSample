using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;

namespace WebApiSample.DataContext
{
    public class TodoDbContext : DbContext
    {
        private string _connectionString = "Data Source=sqlite.db";

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) 
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionString)
                        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                        .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem 
                    { 
                        Id = 1, 
                        Title = "First Item", 
                        CreatedDate = DateTime.Now, 
                        Status = "Not Started", 
                        Description = "First to do item added via seed method of EF core." 
                    }    
            );
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
