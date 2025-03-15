// See https://aka.ms/new-console-template for more information
using EFCoreApplication.Data;
using EFCoreApplication.Model;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

//********************************************************************************
///// Call Advanced Queries
//********************************************************************************
// SeedDataForAdvancedQueries(context);

//FilterStudentsByAge(context, 21);

//Console.WriteLine("\n2. Fetch Only Names and Emails:");
//ProjectStudentData(context);

//Console.WriteLine("\n3. Fetch Sorted Students (Descending by Age):");
//FetchSortedStudents(context);

//FetchPaginatedStudents(context, pageNumber: 1, pageSize: 2);

//ExecuteRawSqlQuery(context);

//FetchAllActiveStudents(context);

//FetchAllStudents(context);


//********************************************************************************
///// Call Performance Optimization Queries
//********************************************************************************
//FetchStudentsWithNoTracking(context);

//FetchStudentsWithLazyLoading(context);

//FetchStudentsWithEagerLoading(context);

//FetchStudentsWithExplicitLoading(context);

//FetchStudentsWithQuerySplitting(context);

//FetchStudentByIdNonCompiled(context, 3);

//FetchStudentByIdCompiled(context, 3);



//********************************************************************************
///// Call Concurrency Queries
//********************************************************************************
//SimulateOptimisticConcurrency(context);

//SimulatePessimisticConcurrency(context);

//PerformTransaction(context);

PerformTransactionWithSavepoints(context);



//********************************************************************************
///// Advanced Queries
//********************************************************************************
static void SeedDataForAdvancedQueries(AppDbContext context)
{
    if (!context.Students.Any())
    {
        Console.WriteLine("Seeding students...");
        var students = new[]
        {
                new Student { Name = "Alice Johnson", Age = 22, Email = "alice@example.com" },
                new Student { Name = "Bob Smith", Age = 24, Email = "bob@example.com" },
                new Student { Name = "Charlie Brown", Age = 20, Email = "charlie@example.com" },
                new Student { Name = "David Miller", Age = 23, Email = "david@example.com" },
                new Student { Name = "Emily Davis", Age = 21, Email = "emily@example.com" }
        };

        context.Students.AddRange(students);
        context.SaveChanges();
        Console.WriteLine("Data seeded successfully!");
    }
}

// Advanced Query Examples
static void FilterStudentsByAge(AppDbContext context, int age)
{
    var students = context.Students
        .Where(s => s.Age > age)
        .ToList();

    Console.WriteLine("\n1. Filtering Students Over Age 21:");
    foreach (var student in students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
}

static void ProjectStudentData(AppDbContext context)
{
    var studentData = context.Students
        .Select(s => new { s.Name, s.Email })
        .ToList();

    foreach (var student in studentData)
    {
        Console.WriteLine($"Name: {student.Name}, Email: {student.Email}");
    }
}

static void FetchSortedStudents(AppDbContext context)
{
    var sortedStudents = context.Students
        .OrderByDescending(s => s.Age)
        .ToList();

    foreach (var student in sortedStudents)
    {
        Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
    }
}

static void FetchPaginatedStudents(AppDbContext context, int pageNumber, int pageSize)
{
    var paginatedStudents = context.Students
        .OrderBy(s => s.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    Console.WriteLine("\nFetch Students with Pagination (Page 1, Page Size 2):");
    foreach (var student in paginatedStudents)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
    }
}

static void ExecuteRawSqlQuery(AppDbContext context)
{
    var students = context.Students
        .FromSqlRaw("SELECT * FROM Students WHERE Age > @p0", 21)
        .ToList();

    Console.WriteLine("\n5. Execute Raw SQL Query:");
    foreach (var student in students)
    {
        Console.WriteLine($"Raw SQL - Name: {student.Name}, Age: {student.Age}");
    }
}

static void FetchAllActiveStudents(AppDbContext context)
{
    var students = context.Students
        .ToList();

    Console.WriteLine("\nExecute SQL Query With Global Filter:");
    foreach (var student in students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
}

static void FetchAllStudents(AppDbContext context)
{
    var students = context.Students
        .IgnoreQueryFilters()
        .ToList();

    Console.WriteLine("\nExecute SQL Query Without Global Filter:");
    foreach (var student in students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
}





//********************************************************************************
///// Performance Optimization
//********************************************************************************
static void FetchStudentsWithNoTracking(AppDbContext context)
{
    var students = context.Students.AsNoTracking().ToList();

    Console.WriteLine("\nFetching Students with No Tracking:");
    foreach (var student in students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
    }
}

static void FetchStudentsWithLazyLoading(AppDbContext context)
{
    var students = context.Students.ToList();

    Console.WriteLine("\nLazy Loading:");
    foreach (var student in students)
    {
        foreach (var course in student.Courses) // Lazy load occurs here
        {
            Console.WriteLine($"Student: {student.Name}, Course: {course.Title}");
        }
    }
}

static void FetchStudentsWithEagerLoading(AppDbContext context)
{
    var students = context.Students.Include(s => s.Courses).ToList();

    Console.WriteLine("\nEager Loading:");
    foreach (var student in students)
    {
        foreach (var course in student.Courses)
        {
            Console.WriteLine($"Student: {student.Name}, Course: {course.Title}");
        }
    }
}

static void FetchStudentsWithExplicitLoading(AppDbContext context)
{
    var students = context.Students.ToList();

    Console.WriteLine("\nExplicit Loading:");
    foreach (var student in students)
    {
        context.Entry(student).Collection(s => s.Courses).Load(); // Explicit Load

        foreach (var course in student.Courses)
        {
            Console.WriteLine($"Student: {student.Name}, Course: {course.Title}");
        }
    }
}

static void FetchStudentsWithQuerySplitting(AppDbContext context)
{
    var students = context.Students.Include(s => s.Courses).AsSplitQuery().ToList();

    Console.WriteLine("\nQuery Splitting:");
    foreach (var student in students)
    {
        foreach (var course in student.Courses)
        {
            Console.WriteLine($"Student: {student.Name}, Course: {course.Title}");
        }
    }
}

static void FetchStudentByIdCompiled(AppDbContext context, int studentId)
{
    var student = AppDbContext.GetStudentByIdCompiled(context, studentId);

    Console.WriteLine("\nExecute Compiled SQL Query:");
    if (student != null)
        Console.WriteLine($"Compiled Query - Name: {student.Name}, Age: {student.Age}");
}

static void FetchStudentByIdNonCompiled(AppDbContext context, int studentId)
{
    var student = context.Students
        .FirstOrDefault(s => s.Id == studentId);

    Console.WriteLine("\nExecute Non-Compiled SQL Query:");
    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
}



//********************************************************************************
///// Concurrency
//********************************************************************************
static void SimulateOptimisticConcurrency(AppDbContext context)
{
    Console.WriteLine("\nSimulating Optimistic Concurrency...");

    var student1 = context.Students.FirstOrDefault();
    if (student1 == null) return;

    using var secondContext = new AppDbContext(); // Simulating another user
    var student2 = secondContext.Students.FirstOrDefault(s => s.Id == student1.Id);

    if (student2 == null) return;

    student1.Age += 1; // First user updates age
    context.SaveChanges();

    student2.Age += 2; // Second user also tries to update age
    try
    {
        secondContext.SaveChanges(); // This should throw a DbUpdateConcurrencyException
    }
    catch (DbUpdateConcurrencyException)
    {
        Console.WriteLine("Concurrency conflict detected! Changes were not saved.");
    }
}

static void SimulatePessimisticConcurrency(AppDbContext context)
{
    Console.WriteLine("\nSimulating Pessimistic Concurrency...");

    var student = context.Students.FirstOrDefault();
    if (student == null) return;

    using var transaction = context.Database.BeginTransaction();

    var lockedStudent = context.Students.FromSqlRaw("SELECT * FROM Students WITH (UPDLOCK) WHERE Id = {0}", student.Id).FirstOrDefault();

    if (lockedStudent != null)
    {
        Console.WriteLine($"Student {lockedStudent.Name} is locked for updates.");
        lockedStudent.Age += 1;
        context.SaveChanges();
    }

    transaction.Commit();
}

static void PerformTransaction(AppDbContext context)
{
    Console.WriteLine("\nStarting Transaction...");

    using var transaction = context.Database.BeginTransaction();

    try
    {
        var student1 = new Student { Name = "Emma Watson", Age = 25, Email = "emma@example.com" };
        var student2 = new Student { Name = "John Doe", Age = 26, Email = "john@example.com" };

        context.Students.AddRange(student1, student2);
        context.SaveChanges();

        Console.WriteLine("Students added. Committing transaction...");
        transaction.Commit();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}. Rolling back transaction...");
        transaction.Rollback();
    }
}

static void PerformTransactionWithSavepoints(AppDbContext context)
{
    Console.WriteLine("\nStarting Transaction with Savepoints...");

    using var transaction = context.Database.BeginTransaction();

    try
    {
        var student1 = new Student { Name = "Lily Evans", Age = 23, Email = "lily@example.com" };
        context.Students.Add(student1);
        context.SaveChanges();

        // Create a Savepoint
        transaction.CreateSavepoint("AfterFirstInsert");

        var student2 = new Student { Name = "James Potter", Age = 24, Email = "james@example.com" };
        context.Students.Add(student2);

        // Simulate an error
        throw new Exception("Something went wrong!");

        context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}. Rolling back to savepoint...");
        transaction.RollbackToSavepoint("AfterFirstInsert");
        transaction.Commit();
    }
}