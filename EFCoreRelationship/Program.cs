// See https://aka.ms/new-console-template for more information


using EFCoreRelationship.Data;
using EFCoreRelationship.Models;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

//SeedOneToOneData(context);
//FetchOneToOneData(context);

//SeedOneToManyData(context);
//FetchOneToManyData(context);

SeedManyToManyData(context);

static void SeedOneToOneData(AppDbContext context)
{
    if (!context.Students.Any())
    {
        var student = new Student
        {
            Name = "Alice Johnson",
            Age = 22,
            Email = "alice@example.com",
            Address = new Address { Street = "123 Main St", City = "New York" }
        };

        context.Students.Add(student);
        context.SaveChanges();
        Console.WriteLine("One-to-One data seeded.");
    }
}

static void FetchOneToOneData(AppDbContext context)
{
    var students = context.Students.Include(s => s.Address).ToList();

    Console.WriteLine("\nFetching Students with Addresses:");
    foreach (var student in students)
    {
        Console.WriteLine($"Student: {student.Name}, Address: {student.Address.Street}, {student.Address.City}");
    }
}


//static void SeedOneToManyData(AppDbContext context)
//{
//    var student = new Student
//    {
//        Name = "John Doe",
//        Age = 24,
//        Email = "john@example.com",
//        Courses = new List<Course>
//            {
//                new Course { Title = "Math 101" },
//                new Course { Title = "Physics 101" }
//            }
//    };

//    context.Students.Add(student);
//    context.SaveChanges();
//    Console.WriteLine("One-to-Many data seeded.");
//}

//static void FetchOneToManyData(AppDbContext context)
//{
//    var students = context.Students.Include(s => s.Courses).ToList();

//    Console.WriteLine("\nFetching Students with Courses:");
//    foreach (var student in students)
//    {
//        Console.WriteLine($"Student: {student.Name}");
//        foreach (var course in student.Courses)
//        {
//            Console.WriteLine($"  Course: {course.Title}");
//        }
//    }
//}


static void SeedManyToManyData(AppDbContext context)
{
    var student1 = new Student { Name = "Alice Johnson", Age = 22, Email = "alice@example.com" };
    var student2 = new Student { Name = "Bob Smith", Age = 24, Email = "bob@example.com" };

    var course1 = new Course { Title = "Math 101" };
    var course2 = new Course { Title = "Physics 101" };

    context.AddRange(student1, student2, course1, course2);
    context.SaveChanges();

    var studentCourses = new List<StudentCourse>
        {
            new StudentCourse { StudentId = student1.Id, CourseId = course1.Id },
            new StudentCourse { StudentId = student1.Id, CourseId = course2.Id },
            new StudentCourse { StudentId = student2.Id, CourseId = course1.Id }
        };

    context.AddRange(studentCourses);
    context.SaveChanges();
    Console.WriteLine("Many-to-Many data seeded.");
}


