// See https://aka.ms/new-console-template for more information

using EFCoreHeirarchyTPH.Data;
using EFCoreHeirarchyTPH.Models;

using var context = new AppDbContext();

context.People.Add(new Student { Name = "Alice Johnson", Grade = 10 });
context.People.Add(new Teacher { Name = "Bob Smith", Subject = "Math" });
context.SaveChanges();

Console.WriteLine("\nFetching People:");
foreach (var person in context.People)
{
    Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, Type: {person.GetType().Name}");
}