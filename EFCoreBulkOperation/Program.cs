// See https://aka.ms/new-console-template for more information
//Console.WriteLine("hello");

using EFCoreBulkOperation.Data;
using EFCoreBulkOperation.Models;
using System.Diagnostics;

using var context = new AppDbContext();

//Console.WriteLine("\nSeeding students with BulkInsert...");
//BulkInsertStudents(context);

Console.WriteLine("\nSeeding students with SaveChangesAsync...");
BatchInsertStudents(context);

static void BulkInsertStudents(AppDbContext context)
{
    var students = new List<Student>();

    for (int i = 1; i <= 1000; i++)
    {
        students.Add(new Student
        {
            Name = $"Student {i}",
            Age = new Random().Next(18, 30),
            Email = $"student{i}@atomcamp.com",
            IsDeleted = false
        });
    }

    var stopwatch = Stopwatch.StartNew();
    context.BulkInsert(students);
    stopwatch.Stop();

    Console.WriteLine($"Inserted {students.Count} students with BulkInsert in {stopwatch.ElapsedMilliseconds} ms.");
}

static void BatchInsertStudents(AppDbContext context)
{
    var students = new List<Student>();

    for (int i = 1; i <= 1000; i++)
    {
        students.Add(new Student
        {
            Name = $"Student {i}",
            Age = new Random().Next(18, 30),
            Email = $"student{i}@example.com",
            IsDeleted = false
        });
    }

    var stopwatch = Stopwatch.StartNew();
    context.Students.AddRange(students);
    context.SaveChanges();
    stopwatch.Stop();

    Console.WriteLine($"Batch Insert (EF Core Default): Inserted {students.Count} students in {stopwatch.ElapsedMilliseconds} ms.");
}

static void BulkUpdateStudents(AppDbContext context)
{
    var students = context.Students.Take(500).ToList();
    foreach (var student in students)
    {
        student.Age += 1; // Simulate an update
    }

    var stopwatch = Stopwatch.StartNew();
    context.BulkUpdate(students);
    stopwatch.Stop();

    Console.WriteLine($"Updated {students.Count} students in {stopwatch.ElapsedMilliseconds} ms.");
}

static void BulkDeleteStudents(AppDbContext context)
{
    var students = context.Students.Where(s => s.Age > 25).ToList();

    var stopwatch = Stopwatch.StartNew();
    context.BulkDelete(students);
    stopwatch.Stop();

    Console.WriteLine($"Deleted {students.Count} students in {stopwatch.ElapsedMilliseconds} ms.");
}

static void BulkMergeStudents(AppDbContext context)
{
    var students = context.Students.Take(500).ToList();
    foreach (var student in students)
    {
        student.Age += 2; // Simulating an update
    }

    var newStudents = new List<Student>();
    for (int i = 1001; i <= 1500; i++)
    {
        newStudents.Add(new Student
        {
            Name = $"Student {i}",
            Age = new Random().Next(18, 30),
            Email = $"student{i}@example.com",
            IsDeleted = false
        });
    }

    students.AddRange(newStudents);

    var stopwatch = Stopwatch.StartNew();
    context.BulkMerge(students);
    stopwatch.Stop();

    Console.WriteLine($"Bulk Merge: Inserted/Updated {students.Count} students in {stopwatch.ElapsedMilliseconds} ms.");
}


