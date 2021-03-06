﻿using needHelp.Common;
using needHelp.LearningAlgorithm;
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
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        public ActionResult Index()
        {
            VolunteerModels volunteer = _cache.volunteers.First(user => user.email.Equals(User.Identity.Name));

            ActivitiesSuggestion suggestion = new ActivitiesSuggestion();
            ViewBag.sugestedActivities = suggestion.SuggestActivities(volunteer);

            return View(volunteer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VolunteerModels volunteerModels = _cache.volunteers.Find(id);
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
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        public ActionResult RemoveFromActivity(int? id, int? activityId)
        {
            if (id == null || activityId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VolunteerModels volunteerModels = db.volunteers.Find(id);
            var request = (from s in db.user_requests
                           where s.volunteerId == id && s.activityId == activityId
                           select s).First();
            if (volunteerModels == null || request == null)
            {
                return HttpNotFound();
            }

            request.isDeletedByUser = true;
            db.SaveChanges();
            _cache.UpdateCache();

            ViewBag.sugestedActivities = _cache.activities.ToList();

            return View("Index", volunteerModels);
        }
    }
}