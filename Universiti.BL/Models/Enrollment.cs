using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Universiti.BL.Models
{
    [Table("Enrollment", Schema = "dbo")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

    }
}
