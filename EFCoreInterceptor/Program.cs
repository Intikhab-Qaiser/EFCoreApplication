// See https://aka.ms/new-console-template for more information

using EFCoreInterceptor.Data;
using EFCoreInterceptor.Models;

using var context = new AppDbContext();

SeedData(context);

static void SeedData(AppDbContext context)
{
    if (!context.Students.Any())
    {
        Console.WriteLine("Seeding students...");
        var students = new[]
        {
                new Student { Name = "Alice Johnson", Age = 22, Email = "alice@datacamp.com" },
                new Student { Name = "Bob Smith", Age = 24, Email = "bob@datacamp.com" },
                new Student { Name = "Charlie Brown", Age = 20, Email = "charlie@datacamp.com" },
                new Student { Name = "David Miller", Age = 23, Email = "david@datacamp.com" },
                new Student { Name = "Emily Davis", Age = 21, Email = "emily@datacamp.com" }
        };

        context.Students.AddRange(students);
        context.SaveChanges();
        Console.WriteLine("Data seeded successfully!");
    }
}
