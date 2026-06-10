using EF_Code_First_Approach_Demo.Models;
using EF_Code_First_Approach_Demo.Services;

StudentService service = new StudentService();

while (true)
{
Console.WriteLine("\n1. Add Student");
Console.WriteLine("2. View Students");
Console.WriteLine("3. Exit");


Console.Write("Enter Choice: ");

int choice = Convert.ToInt32(Console.ReadLine());

switch (choice)
{
    case 1:

        Student student = new Student();

        Console.Write("Enter Name: ");
        student.StudentName = Console.ReadLine();

        Console.Write("Enter Email: ");
        student.Email = Console.ReadLine();

        service.AddStudent(student);

        break;

    case 2:

        var students = service.GetAllStudents();

        foreach (var s in students)
        {
            Console.WriteLine(
                $"{s.StudentId} - {s.StudentName} - {s.Email}");
        }

        break;

    case 3:

        return;
}


}

