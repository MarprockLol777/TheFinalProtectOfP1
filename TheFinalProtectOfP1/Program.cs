using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TheFinalProtectOfP1;

var context = new MyDataContex();
bool running = true;

while (running)
{
    Console.WriteLine("====== Student Score Calculator ======");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Show Students");
    Console.WriteLine("3. Update Student");
    Console.WriteLine("4. Delete Student");
    Console.WriteLine("5. Exit");
    Console.Write("Choose option: ");

    int choice;
    try
    {
        choice = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        Console.WriteLine("Invalid option. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1: AddStudent(); break;
        case 2: ShowStudents(); break;
        case 3: UpdateStudent(); break;
        case 4: DeleteStudent(); break;
        case 5: running = false; break;
        default: Console.WriteLine("Invalid option."); break;
    }
}

// ─────────────────────────────────────────────
//  ADD
// ─────────────────────────────────────────────
void AddStudent()
{
    var student = new Student();

    Console.Write("Name: ");
    student.Name = Console.ReadLine();

    Console.Write("Last Name: ");
    student.LastName = Console.ReadLine();

    Console.Write("Age: ");
    try { student.Age = Convert.ToInt32(Console.ReadLine()); }
    catch { Console.WriteLine("Invalid age, saving as 0."); student.Age = 0; }

    Console.Write("Sex (1=Male, 2=Female): ");
    try { student.Sex = Convert.ToInt32(Console.ReadLine()); }
    catch { Console.WriteLine("Invalid input, saving as 1."); student.Sex = 1; }

    Console.Write("Address: ");
    student.Address = Console.ReadLine();

    // ── Recolectar notas ──────────────────────
    var scores = new List<int>();
    bool addingScores = true;

    while (addingScores)
    {
        // Pedir nota con try/catch
        int score = 0;
        bool validScore = false;
        while (!validScore)
        {
            Console.Write("Enter a score: ");
            try
            {
                score = Convert.ToInt32(Console.ReadLine());
                validScore = true;
            }
            catch
            {
                Console.WriteLine("Invalid score. Please enter a number.");
            }
        }
        scores.Add(score);

        // Preguntar si agrega otra nota
        bool validChoice = false;
        while (!validChoice)
        {
            Console.WriteLine("Do you want to add another score?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            Console.Write("Option: ");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    validChoice = true;               // sigue el while exterior
                }
                else if (option == 2)
                {
                    validChoice = true;
                    addingScores = false;             // sale del while exterior
                }
                else
                {
                    Console.WriteLine("Please enter 1 or 2.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }
        }
    }

    // Guardar notas como "85,90,78" y calcular promedio
    student.Scores = string.Join(",", scores);
    student.Overall_score = scores.Count > 0 ? (int)scores.Average() : 0;

    context.Students.Add(student);
    context.SaveChanges();
    Console.WriteLine("Student saved successfully!");
}

// ─────────────────────────────────────────────
//  SHOW
// ─────────────────────────────────────────────
void ShowStudents()
{
    var students = context.Students.ToList();

    if (!students.Any())
    {
        Console.WriteLine("No students found.");
        return;
    }

    foreach (var s in students)
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine($"ID      : {s.Id}");
        Console.WriteLine($"Name    : {s.Name} {s.LastName}");
        Console.WriteLine($"Age     : {s.Age}");
        Console.WriteLine($"Sex     : {(s.Sex == 1 ? "Male" : "Female")}");
        Console.WriteLine($"Address : {s.Address}");
        Console.WriteLine($"Scores  : {s.Scores}");           // "85,90,78"
        Console.WriteLine($"Average : {s.Overall_score}");
        Console.WriteLine("-------------------------------");
    }
}

// ─────────────────────────────────────────────
//  UPDATE
// ─────────────────────────────────────────────
void UpdateStudent()
{
    ShowStudents();

    Console.Write("Enter the ID of the student to update: ");
    int id;
    try { id = Convert.ToInt32(Console.ReadLine()); }
    catch { Console.WriteLine("Invalid ID."); return; }

    var student = context.Students.Find(id);
    if (student == null) { Console.WriteLine("Student not found."); return; }

    // Cada campo: si el usuario no escribe nada, se conserva el valor actual
    Console.Write($"Name ({student.Name}): ");
    string input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input)) student.Name = input;

    Console.Write($"Last Name ({student.LastName}): ");
    input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input)) student.LastName = input;

    Console.Write($"Age ({student.Age}): ");
    input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input))
    {
        try { student.Age = Convert.ToInt32(input); }
        catch { Console.WriteLine("Invalid age, keeping old value."); }
    }

    Console.Write($"Sex ({student.Sex}): ");
    input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input))
    {
        try { student.Sex = Convert.ToInt32(input); }
        catch { Console.WriteLine("Invalid input, keeping old value."); }
    }

    Console.Write($"Address ({student.Address}): ");
    input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input)) student.Address = input;

    // Preguntar si quiere actualizar las notas
    Console.WriteLine("Do you want to update scores?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");

    bool updateScores = false;
    bool validChoice = false;
    while (!validChoice)
    {
        try
        {
            int opt = Convert.ToInt32(Console.ReadLine());
            if (opt == 1) { updateScores = true; validChoice = true; }
            else if (opt == 2) { validChoice = true; }
            else Console.WriteLine("Enter 1 or 2.");
        }
        catch { Console.WriteLine("Invalid input. Enter 1 or 2."); }
    }

    if (updateScores)
    {
        var scores = new List<int>();
        bool addingScores = true;

        while (addingScores)
        {
            bool validScore = false;
            while (!validScore)
            {
                Console.Write("Enter a score: ");
                try
                {
                    scores.Add(Convert.ToInt32(Console.ReadLine()));
                    validScore = true;
                }
                catch { Console.WriteLine("Invalid score. Please enter a number."); }
            }

            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Do you want to add another score?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.Write("Option: ");
                try
                {
                    int opt = Convert.ToInt32(Console.ReadLine());
                    if (opt == 1) { valid = true; }
                    else if (opt == 2) { valid = true; addingScores = false; }
                    else Console.WriteLine("Please enter 1 or 2.");
                }
                catch { Console.WriteLine("Invalid input. Please enter 1 or 2."); }
            }
        }

        student.Scores = string.Join(",", scores);
        student.Overall_score = scores.Count > 0 ? (int)scores.Average() : 0;
    }

    context.SaveChanges();
    Console.WriteLine("Student updated successfully!");
}

// ─────────────────────────────────────────────
//  DELETE
// ─────────────────────────────────────────────
void DeleteStudent()
{
    ShowStudents();

    Console.Write("Enter the ID of the student to delete: ");
    int id;
    try { id = Convert.ToInt32(Console.ReadLine()); }
    catch { Console.WriteLine("Invalid ID."); return; }

    var student = context.Students.Find(id);
    if (student == null) { Console.WriteLine("Student not found."); return; }

    Console.WriteLine($"Are you sure you want to delete {student.Name} {student.LastName}?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");

    bool validChoice = false;
    while (!validChoice)
    {
        try
        {
            int opt = Convert.ToInt32(Console.ReadLine());
            if (opt == 1)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                Console.WriteLine("Student deleted successfully!");
                validChoice = true;
            }
            else if (opt == 2)
            {
                Console.WriteLine("Delete cancelled.");
                validChoice = true;
            }
            else Console.WriteLine("Enter 1 or 2.");
        }
        catch { Console.WriteLine("Invalid input. Enter 1 or 2."); }
    }
}