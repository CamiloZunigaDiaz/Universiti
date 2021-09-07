using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universiti.BL.Data;
using Universiti.BL.DTOs;

namespace Universiti.wed.Controllers
{
    public class OfficeAssignmentController : Controller
    {
        private readonly UniversitiContext context = new UniversitiContext();
        // GET: OfficeAssignment
        public ActionResult Index()
        {
            var query = context.officeAssignments.Include("Instructor").ToList();
            var offices = query.Select(x => new OfficeAssignmentDTO
            {
                InstructorID = x.InstructorID,
                Location = x.Location,
                Instructor = new InstructorDTO
                {

                    FirstMidName = x.Instructor.FirstMidName,
                    LastName = x.Instructor.LastName


                }
            });

            return View(offices);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadData(); 

            return View();

        }

        [HttpPost]
        public ActionResult Create(OfficeAssignmentDTO office)
        {

            LoadData();


            if (!ModelState.IsValid)
                return View(ModelState);

            context.officeAssignments.Add(new BL.Models.OfficeAssignment
            {
                InstructorID = office.InstructorID,
                Location = office.Location
            });
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        private void LoadData()
        {
            var instructors = context.Instructors.Select(x => new InstructorDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,

            }).ToList();

            ViewData["Instructor"] = new SelectList(instructors, "ID", "FullName");
        }

    }
}