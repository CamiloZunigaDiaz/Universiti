using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universiti.BL.DTOs
{
    public class DepartmentDTO
    {
        [Display(Name = "DepartmentID")]
        public int DepartmentID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Budget")]
        public decimal Budget { get; set; }

        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }
    }
}
