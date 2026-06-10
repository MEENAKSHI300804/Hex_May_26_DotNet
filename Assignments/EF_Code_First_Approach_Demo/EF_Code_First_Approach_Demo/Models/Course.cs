namespace EF_Code_First_Approach_Demo.Models
{
public class Course
{
public int CourseId { get; set; }


    public string CourseName { get; set; }

    public decimal Fees { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}


}
