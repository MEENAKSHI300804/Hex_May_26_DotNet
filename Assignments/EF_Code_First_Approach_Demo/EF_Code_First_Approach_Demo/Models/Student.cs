namespace EF_Code_First_Approach_Demo.Models
{
public class Student
{
public int StudentId { get; set; }


    public string StudentName { get; set; }

    public string Email { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}


}
