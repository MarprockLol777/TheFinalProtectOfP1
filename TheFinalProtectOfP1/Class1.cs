using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TheFinalProtectOfP1;



var context = new MyDataContex();
var student = new List<Student>();


bool running = true;

while (running)
{
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Show Students");
    Console.WriteLine("3. Exit");
    Console.Write("Choose option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            AddStudent();
            break;

        case 2:
            ShowStudents();
            break;

        case 3:
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}

void AddStudent()
{

    var student = new Student{ };

    Console.Write("Name: ");
    student.Name = Console.ReadLine();

    Console.Write("Last Name: ");
    student.LastName = Console.ReadLine();

    Console.Write("Age: ");
    student.Age = Convert.ToInt32(Console.ReadLine());

    Console.Write("Sex (1=Male, 2=Female): ");
    student.Sex = Convert.ToInt32(Console.ReadLine());

    Console.Write("Address: ");
    student.Address = Console.ReadLine();

    Console.Write("Score: ");
    student.Scores = Convert.ToInt32(Console.ReadLine());

    Console.Write("Overall Score: ");
    student.Overall_score = Convert.ToInt32(Console.ReadLine());


    context.Students.Add(student);
    context.SaveChanges();
    

    Console.WriteLine("Saved successfully!");
}

void ShowStudents()
{
    var student = context.Students.ToList();

    foreach (var s in student)
    {
        Console.WriteLine($"ID: {s.Id} | {s.Name} {s.LastName} | Score: {s.Scores} | Overall: {s.Overall_score}");
    }
}