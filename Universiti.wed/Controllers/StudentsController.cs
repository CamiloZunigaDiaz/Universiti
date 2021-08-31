using System.Collections.Generic;
using System.Web.Mvc;
using Universiti.wed.Models;

namespace Universiti.wed.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        //cuando no tienes un decorador es como si tuvieras este decorador [HttpGet] 
        [HttpGet]
        public ActionResult Index()
        {
            var students = new List<Student>(); //tamaño no fijo

            students.Add(new Student{ ID = 1, FirstMidName = "Camilo", LastName = "Zuñiga", EnrollmentDate = System.DateTime.Now});
            students.Add(new Student{ ID = 2, FirstMidName = "Jensel", LastName = "Granobles", EnrollmentDate = System.DateTime.Now});
            students.Add(new Student { ID = 3, FirstMidName = "Michael", LastName = "Vega", EnrollmentDate = System.DateTime.Now });

            ViewBag.Data = "Data de Prueba"; 
            ViewBag.Message = "Mensaje de Prueba"; 

            return View(students); //Si no pones nada en medio de los paretecis redireciconas a una vista igual al nombre de la clase
                                   //return View("Index"); vista que se redirecciona por el nombre que esta dentro de los parentecis
                                   //return View("Account/Login"); si quieres redireccionar a una vista de otra carpeta escribes el nombre de la carpeta y luego el nombre de la vista

        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student) 
        {
            var id = student.ID; 
            var lastname = student.LastName;
            var firstmidname = student.FirstMidName;
            var enrollmentdate = student.EnrollmentDate;
            
            

            return View();
        }

    }
}