using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPMVCWeb_Application.Models;
using System.Data.Entity;

namespace ASPMVCWeb_Application.Controllers
{
    public class HomeController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        public ActionResult Index()
        {

            var model = (from student in db.Students
                         select student).ToList();
            return View(model);

        }

        public ActionResult Details(int id)
        {
            Student student = db.Students.First(x => x.StudentID == id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            return View("Details", student);
        }

        public ActionResult Edit(int id)
        {
            Student student = db.Students.First(x => x.StudentID == id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Student student = db.Students.First(x => x.StudentID == id);

                if (TryUpdateModel(student))
                {
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Edit failure, see inner exception");
                return View();
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Student student = new Student();
                if (ModelState.IsValid)
                {
                    student.FirstName = collection["FirstName"].ToString();
                    student.LastName = collection["LastName"].ToString();
                    student.MiddleName = collection["MiddleName"].ToString();
                    student.Email = collection["Email"].ToString();
                    student.Phone = collection["Phone"].ToString();
                    db.Students.Add(student);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(student);
                }

            }
            catch
            {
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            Student student = db.Students.SingleOrDefault(x => x.StudentID == id);
            if (student != null)
            {
                return View("Delete", student);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            Student student = db.Students.SingleOrDefault(s => s.StudentID == id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}