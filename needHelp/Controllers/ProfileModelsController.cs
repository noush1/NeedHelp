using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace needHelp.Controllers
{
    public class ProfileModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        public ActionResult Index()
        {
            VolunteerModels volunteer = db.volunteers.First(user => user.email.Equals(User.Identity.Name));
            ViewBag.sugestedActivities = db.activities.ToList();

            return View(volunteer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VolunteerModels volunteerModels = db.volunteers.Find(id);
            if (volunteerModels == null)
            {
                return HttpNotFound();
            }

            return View(volunteerModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "firstName,lastName,email,phone")] VolunteerModels volunteer)
        {
            VolunteerModels volunteerModels = db.volunteers.First(user => user.email == volunteer.email);
            volunteerModels.firstName = volunteer.firstName;
            volunteerModels.lastName = volunteer.lastName;
            volunteerModels.email = volunteer.email;
            volunteerModels.phone = volunteer.phone;

            if (ModelState.IsValid)
            {
                db.Entry(volunteerModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }
    }
}