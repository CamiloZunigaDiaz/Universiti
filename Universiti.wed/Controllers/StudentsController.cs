using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Universiti.BL.Models;
using Universiti.BL.DTOs;
using Universiti.BL.Data;
using System.Linq;
using PagedList;

namespace Universiti.wed.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversitiContext context = new UniversitiContext();

        // GET: Students
        //cuando no tienes un decorador es como si tuvieras este decorador [HttpGet] 
        [HttpGet]
        public ActionResult Index(int? studentId, int ? pageSize, int ? page)
        {
            //SELECT * FROM Students
            var query = context.Students.ToList();

            //forma 1
            //var students = (from q in query
            //               where q.EnrollmentDate < DateTime.Now
            //               select new StudentDTO 
            //               { 
            //                  ID = q.ID,
            //                  LastName = q.LastName,
            //                  FirstMidName = q.FirstMidName,
            //                  EnrollmentDate = q.EnrollmentDate
            //               }).ToList();

            //forma 2 de hacer una consulta con Linq
            var students = query.Where(x => x.EnrollmentDate < DateTime.Now)
                            .Select(x => new StudentDTO
                            {
                                ID = x.ID,
                                LastName = x.LastName,
                                FirstMidName = x.FirstMidName,
                                EnrollmentDate = x.EnrollmentDate
                            }).ToList();
            #region cursos relacionados al estudiante
            if (studentId != null)
            {

                var courses =  (from q in context.Enrollments
                               join r in context.Courses on q.CourseID equals r.CourseID
                               where q.StudentID == studentId
                               select new CourseDTO
                               {
                                   CourseID = r.CourseID,
                                   Title = r.Title,
                                   Credits = r.Credits

                               }).ToList();

                ViewBag.Courses = courses;

            }
            #endregion
            ViewBag.Message = "Mensaje de Prueba";
            ViewBag.Data = "Data de Prueba";

            #region paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;
            #endregion
            return View(students.ToPagedList(page.Value, pageSize.Value)); //Si no pones nada en medio de los paretecis redireciconas a una vista igual al nombre de la clase
                                   //return View("Index"); vista que se redirecciona por el nombre que esta dentro de los parentecis
                                   //return View("Account/Login"); si quieres redireccionar a una vista de otra carpeta escribes el nombre de la carpeta y luego el nombre de la vista

        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentDTO student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(student);
                if (student.EnrollmentDate > DateTime.Now)
                    throw new Exception("La fecha de la matricula no puede ser mayor a la fecha actual");


                context.Students.Add(new Student
                {
                    FirstMidName = student.FirstMidName,
                    LastName = student.LastName, 
                    EnrollmentDate = student.EnrollmentDate
                });
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(student);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var query = context.Students.Find(id);
            var student = context.Students.Where(x => x.ID == id)
                           .Select(x => new StudentDTO
                           {
                               ID = x.ID,
                               LastName = x.LastName,
                               FirstMidName = x.FirstMidName,
                               EnrollmentDate = x.EnrollmentDate
                           }).FirstOrDefault();



            return View(student);
        }


        [HttpPost]
        public ActionResult Edit(StudentDTO student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(student);

                if (student.EnrollmentDate > DateTime.Now)
                    throw new Exception("La fecha de la matricula no puede ser mayor a la fecha actual");


                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var studentModel = context.Students.FirstOrDefault(x => x.ID == student.ID);

                //campos que se van a modificar
                //sobreescribir las propiedades del modelo de base de datos
                studentModel.LastName = student.LastName;
                studentModel.FirstMidName = student.FirstMidName;
                studentModel.EnrollmentDate = student.EnrollmentDate;

                //aplicar los cambios en base de datos
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(student);


        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!context.Enrollments.Any(x => x.StudentID == id))
            {
                var studentModel = context.Students.FirstOrDefault(x => x.ID == id);
                context.Students.Remove(studentModel);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
