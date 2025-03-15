// See https://aka.ms/new-console-template for more information

using EFCoreInterceptor.Data;
using EFCoreInterceptor.Models;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

SeedData(context);
FetchAllStudents(context);

static void SeedData(AppDbContext context)
{
    if (!context.Students.Any())
    {
        Console.WriteLine("Seeding students...");
        var students = new[]
        {
                new Student { Name = "Alice Johnson", Age = 22, Email = "alice@datacamp.com", HeightInCm = 5.6m, IsEnrolled=true,Status=StudentStatus.Active, SSN="8383" },
                new Student { Name = "Bob Smith", Age = 24, Email = "bob@datacamp.com" , HeightInCm = 5.9m, IsEnrolled=true,Status=StudentStatus.Active, SSN="566" },
                new Student { Name = "Charlie Brown", Age = 20, Email = "charlie@datacamp.com" , HeightInCm = 6.6m, IsEnrolled=false,Status=StudentStatus.Inactive, SSN="2222" },
                new Student { Name = "David Miller", Age = 23, Email = "david@datacamp.com" , HeightInCm = 5.2m, IsEnrolled=true,Status=StudentStatus.Active, SSN="888888" },
                new Student { Name = "Emily Davis", Age = 21, Email = "emily@datacamp.com" , HeightInCm = 6.1m, IsEnrolled=false,Status=StudentStatus.Graduated, SSN="121333" },
        };

        context.Students.AddRange(students);
        context.SaveChanges();
        Console.WriteLine("Data seeded successfully!");
    }
}

static void FetchAllStudents(AppDbContext context)
{
    var students = context.Students
        .ToList();

    Console.WriteLine("\nGet all students:");
    foreach (var student in students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, SSN: {student.SSN}, IsEnrolled: {student.IsEnrolled}, Status: {student.Status}");
    }
}
