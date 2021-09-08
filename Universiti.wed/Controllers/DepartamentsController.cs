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
    public class DepartamentsController : Controller
    {
        private readonly UniversitiContext context = new UniversitiContext();


        [HttpGet]
        public ActionResult Index(int? pageSize, int? page)
        {

            var query = context.Departments.ToList();

            var department = query.Select(x => new DepartmentDTO
            {
                DepartmentID = x.DepartmentID,
                Name = x.Name,
                Budget = x.Budget,
                StartDate = x.StartDate


            }).ToList();


            #region paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;
            #endregion
            return View(department.ToPagedList(page.Value, pageSize.Value));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentDTO department)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(department);



                context.Departments.Add(new Department
                {
                    DepartmentID = department.DepartmentID,
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate

                });
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var department = context.Departments.Where(x => x.DepartmentID == id)
                           .Select(x => new DepartmentDTO
                           {
                               DepartmentID = x.DepartmentID,
                               Name = x.Name,
                               Budget = x.Budget,
                               StartDate = x.StartDate

                           }).FirstOrDefault();



            return View(department);
        }


        [HttpPost]
        public ActionResult Edit(DepartmentDTO department)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(department);


                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var departmentModel = context.Departments.FirstOrDefault(x => x.DepartmentID == department.DepartmentID);

                //campos que se van a modificar
                //sobreescribir las propiedades del modelo de base de datos

                departmentModel.Name = department.Name;
                departmentModel.Budget = department.Budget;

                //aplicar los cambios en base de datos
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);


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