using EF_Code_First_Approach_Demo.Data;
using EF_Code_First_Approach_Demo.Models;

namespace EF_Code_First_Approach_Demo.Services
{
public class StudentService
{
private readonly TrainingInstituteDbContext _context;


    public StudentService()
    {
        _context = new TrainingInstituteDbContext();
    }

    public void AddStudent(Student student)
    {
        _context.Students.Add(student);

        _context.SaveChanges();

        Console.WriteLine("Student Added Successfully");
    }

    public List<Student> GetAllStudents()
    {
        return _context.Students.ToList();
    }
}


}
