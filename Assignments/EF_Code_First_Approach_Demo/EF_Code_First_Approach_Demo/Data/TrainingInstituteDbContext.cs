using EF_Code_First_Approach_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Code_First_Approach_Demo.Data
{
internal class TrainingInstituteDbContext : DbContext
{
public DbSet<Student> Students { get; set; }


    public DbSet<Course> Courses { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "SERVER=localhost;Database=TrainingInstituteDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}


}
