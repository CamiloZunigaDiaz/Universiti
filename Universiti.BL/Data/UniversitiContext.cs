using System.Data.Entity;
using Universiti.BL.Models;

namespace Universiti.BL.Data
{
    public class UniversitiContext : DbContext
    {
        public UniversitiContext() : base("DefaultConnection")
        {

        } 

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<OfficeAssignment> officeAssignments { get; set; }

    }
}
