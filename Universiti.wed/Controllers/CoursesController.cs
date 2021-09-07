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
        public ActionResult Index( int? pageSize, int? page)
        {

            var query = context.Courses.ToList();

            var courses = query.Select(x => new CourseDTO
            {
                CourseID = x.CourseID,
                Title = x.Title,
                Credits = x.Credits

            }).ToList();
          

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
    }
}