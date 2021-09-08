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
    public class CoursesController : Controller
    {

        private readonly UniversitiContext context = new UniversitiContext();


        [HttpGet]
        public ActionResult Index(int? courseid, int? pageSize, int? page)
        {

            var query = context.Courses.ToList();

            var courses = query.Select(x => new CourseDTO
            {
                CourseID = x.CourseID,
                Title = x.Title,
                Credits = x.Credits

            }).ToList();

            if (courseid != null)
            {

                var instructors = (from c in context.CourseInstructors
                               join i in context.Instructors on c.InstructorID equals i.ID
                               where c.CourseID == courseid
                               select new InstructorDTO
                               {
                                   ID = i.ID,
                                   LastName = i.LastName,
                                   FirstMidName = i.FirstMidName

                               }).ToList();

                ViewBag.Instructors = instructors;

            }


            #region paginacion
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;
            #endregion
            return View(courses.ToPagedList(page.Value, pageSize.Value)); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseDTO course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(course);
               


                context.Courses.Add(new Course
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Credits = course.Credits

                });
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(course);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var course = context.Courses.Where(x => x.CourseID == id)
                           .Select(x => new CourseDTO
                           {
                               CourseID = x.CourseID,
                               Title = x.Title,
                               Credits = x.Credits

                           }).FirstOrDefault();



            return View(course);
        }


        [HttpPost]
        public ActionResult Edit(CourseDTO course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(course);

                
                //var studentModel = context.Students.Where(x => x.ID == student.ID).Select(x => x).FirstOrDefault();
                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == course.CourseID);

                //campos que se van a modificar
                //sobreescribir las propiedades del modelo de base de datos
                
                courseModel.Title = course.Title;
                courseModel.Credits = course.Credits;

                //aplicar los cambios en base de datos
                context.SaveChanges();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(course);


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!context.Enrollments.Any(x => x.CourseID == id) && !context.CourseInstructors.Any(j => j.CourseID == id))
            {
                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == id);
                context.Courses.Remove(courseModel);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}