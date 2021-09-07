using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universiti.BL.DTOs
{
    public class OfficeAssignmentDTO
    {
        [Required]
        public int InstructorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        public InstructorDTO Instructor { get; set; }

        
    }
}
