using System;
using System.ComponentModel.DataAnnotations;

namespace Universiti.BL.DTOs
{
    public class StudentDTO
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "El campo LastName es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string LastName { get; set; }

        [Display(Name = "FirstMidName")]
        [Required(ErrorMessage = "El campo FirstMidName es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string FirstMidName { get; set; }

        [Display(Name = "EnrollmentDate")]
        [Required(ErrorMessage = "El campo EnrollmentDate es requerido")]
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstMidName);
                //return $"{LastName} {FirstMidName}";
                //return LastName+ "" + FirstMidName; Interpolacion en una cadena
            }
        }
    }
}
