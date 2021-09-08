using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universiti.BL.Data;
using Universiti.BL.DTOs;
using Universiti.BL.Models;

namespace Universiti.wed.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversitiContext context = new UniversitiContext();


        [HttpGet]
        public ActionResult Index(int ? instructorid, int? pageSize, int? page)
        {

            var query = context.Instructors.ToList();

            var instructors = query.Select(x => new InstructorDTO
            {
                ID = x.ID,
                LastName = x.LastName,
                FirstMidName = x.FirstMidName,
                HireDate = x.HireDate


            }).ToList();

            if (instructorid != null)
            {

                var departments = (from d in context.Departments
                               join i in context.Instructors on d.InstructorID equals i.ID
                               where d.DepartmentID == instructorid
                               select new DepartmentDTO
                               {
                                   DepartmentID = d.DepartmentID,
                                   Name = d.Name                                  

                               }).ToList();

                ViewBag.Departments = departments;

            }

            #region paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;
            #endregion
            return View(instructors.ToPagedList(page.Value, pageSize.Value));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(InstructorDTO instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(instructor);



                context.Instructors.Add(new Instructor
                {
                    ID= instructor.ID,
                    LastName = instructor.LastName,
                    FirstMidName = instructor.FirstMidName,
                    HireDate = instructor.HireDate                   

                });
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructor);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var instructor = context.Instructors.Where(x => x.ID == id)
                           .Select(x => new InstructorDTO
                           {
                               ID = x.ID,
                               LastName = x.LastName,
                               FirstMidName = x.FirstMidName,
                               HireDate = x.HireDate

                           }).FirstOrDefault();



            return View(instructor);
        }


        [HttpPost]
        public ActionResult Edit(InstructorDTO instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(instructor);


                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var instructorModel = context.Instructors.FirstOrDefault(x => x.ID == instructor.ID);

                //campos que se van a modificar
                //sobreescribir las propiedades del modelo de base de datos

                instructorModel.LastName = instructor.LastName;
                instructorModel.FirstMidName = instructor.FirstMidName;

                //aplicar los cambios en base de datos
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructor);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!context.Departments.Any(x => x.InstructorID == id) && !context.CourseInstructors.Any(j => j.InstructorID == id) && !context.officeAssignments.Any(i => i.InstructorID == id))
            {
                var instructorModel = context.Instructors.FirstOrDefault(x => x.ID == id);
                context.Instructors.Remove(instructorModel);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}