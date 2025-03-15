using EFCoreInterceptor.Logging;
using Microsoft.EntityFrameworkCore;
using EFCoreInterceptor.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EFCoreInterceptor.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=EPLVRIGW000F\\SQLEXPRESS;Database=EfCoreInterceptor;Trusted_Connection=True;TrustServerCertificate=True;");

            // Register Interceptor
            //optionsBuilder.AddInterceptors(new CommandInterceptor());
            //optionsBuilder.AddInterceptors(new EFSaveChangesInterceptor());

            // Enable EF Logging
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); // Logs Queries to Console
            optionsBuilder.EnableSensitiveDataLogging(); // Logs Parameters

            // Filter logging to log only queries
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);

        }
    }
}
