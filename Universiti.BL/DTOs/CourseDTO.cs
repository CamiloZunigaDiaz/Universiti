using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universiti.BL.DTOs
{
    public class CourseDTO
    {
        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Credits")]
        public int Credits { get; set; }
    }
}
