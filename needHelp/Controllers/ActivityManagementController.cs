﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using needHelp.Models;
using needHelp.Common;

namespace needHelp.Controllers
{
    public class ActivityManagementController : Controller
    {
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        // GET: ActivityManagement
        public ActionResult Index()
        {
            bool needToUpdateDB = false;
            if (User.Identity.IsAuthenticated)
            {
                OrganizationModels org = _cache.organizations.First(user => user.email.Equals(User.Identity.Name));
                org.org_activities = org.org_activities.OrderBy(d => d.date.Ticks).ToList();

                IQueryable<UserRequestModels> deletedRequestsByUser = from r in _cache.user_requests
                                                                      where r.activity.organizationId == org.id && r.isDeletedByUser == true
                                                                      select r;
                ICollection<UserRequestModels> acceptedRequests = new LinkedList<UserRequestModels>();

                foreach (UserRequestModels request in deletedRequestsByUser)
                {
                    if (request.isAccepted) 
                    {
                        acceptedRequests.Add(request);
                    }
                    else if (!request.isAccepted) 
                    {
                        needToUpdateDB = true;
                        _cache.user_requests.Remove(request);
                    }
                }

                int acceptedRequestsAmount = acceptedRequests.Count();

                // check if to build an alert message
                if (acceptedRequestsAmount > 0)
                {
                    string alertString;

                    if (acceptedRequestsAmount == 1)
                    {
                        UserRequestModels request = acceptedRequests.First();
                        alertString = "המתנדב " + request.volunteer.firstName + " " + request.volunteer.lastName
                                             + " ביטל את השתתפות בפעילות " + request.activity.name;
                    }
                    else
                    {
                        alertString = "מספר מתנדבים ביטלו את השתתפותם מפעילויות האירגון: ";
                        foreach (UserRequestModels request in acceptedRequests)
                        {
                            alertString = alertString + "\\n " + request.volunteer.firstName + " " + request.volunteer.lastName + " - " + request.activity.name;
                        }
                    }

                    ViewBag.alertString = alertString;
                }
                else
                {
                    ViewBag.alertString = null;
                }
                

                // check if there are deleted requests from the DB
                if (needToUpdateDB)
                {
                    db.SaveChanges();
                    _cache.UpdateCache();
                }

                return View(org);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ActivityManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = _cache.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // GET: ActivityManagement/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(_cache.cities, "id", "name");
            ViewBag.typeId = new SelectList(_cache.help_types, "id", "typeName");
            return View();
        }

        // POST: ActivityManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (ModelState.IsValid)
            {
                db.activities.Add(activityModels);
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(_cache.cities, "id", "name", activityModels.cityId);
            ViewBag.typeId = new SelectList(_cache.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = _cache.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(_cache.cities, "id", "name", activityModels.cityId);
            ViewBag.typeId = new SelectList(_cache.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // POST: ActivityManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            ActivityModels activity = db.activities.First(a => a.id == activityModels.id);

            if (ModelState.IsValid)
            {
                activity.type = db.help_types.Find(activityModels.typeId);
                activity.address = activityModels.address;
                activity.cityId = activityModels.cityId;
                activity.date = activityModels.date;
                activity.description = activityModels.description;
                activity.name = activityModels.name;

                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(_cache.cities, "id", "name", activity.cityId);
            ViewBag.typeId = new SelectList(_cache.help_types, "id", "typeName", activity.typeId);
            return View(activity);
        }

        // GET: ActivityManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = _cache.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // POST: ActivityManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityModels activityModels = db.activities.Find(id);
            db.activities.Remove(activityModels);
            db.SaveChanges();
            _cache.UpdateCache();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Respond([Bind(Include = "activityId,volunteerId,isDeletedByOrganization,isDeletedByUser,isAccepted,replyMessage")] UserRequestModels request)
        {
            if (ModelState.IsValid)
            {
                request.isAnswered = true;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }

            return Index();
        }

        [HttpPost]
        public ActionResult RemoveRequest()
        {
            ViewBag.alertString = null;
            if (User.Identity.IsAuthenticated)
            {
                OrganizationModels org = db.organizations.First(user => user.email.Equals(User.Identity.Name));

                if (org != null)
                {
                    // delete the confirmed requests
                    db.user_requests.RemoveRange(db.user_requests.Where(r => r.activity.organizationId == org.id && r.isDeletedByUser));
                    db.SaveChanges();
                    _cache.UpdateCache();
                }

                // return Index
                return RedirectToAction("Index", "ActivityManagement");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
