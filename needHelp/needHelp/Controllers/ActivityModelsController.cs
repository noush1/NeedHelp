﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using needHelp.Models;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace needHelp.Controllers
{
    public class ActivityModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        // GET: ActivityModels
        public ActionResult Index()
        {
            var activities = db.activities.Include(a => a.city);
            return View(activities.ToList());
        }

        // GET: ActivityModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // GET: ActivityModels/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
            return View();
        }

        // POST: ActivityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (ModelState.IsValid)
            {
                activityModels.org = db.organizations.Find(activityModels.organizationId);
                activityModels.type = db.help_types.Find(activityModels.typeId);
                db.activities.Add(activityModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // POST: ActivityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (ModelState.IsValid)
            {
                activityModels.org = db.organizations.Find(activityModels.organizationId);
                activityModels.type = db.help_types.Find(activityModels.typeId);
                db.Entry(activityModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // POST: ActivityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityModels activityModels = db.activities.Find(id);
            db.activities.Remove(activityModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SearchActivities()
        {
            string activityName = Request["txtActivityName"].ToString();

            var result = from s in db.activities
                         where s.name.Contains(activityName)
                         select s;

            //return RedirectToAction("Index","Series");
            return View("Index", result.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public HttpResponseMessage Get()
        {
             var result = db.activities.Include(a => a.city).ToList();
             HttpResponseMessage msg = new HttpResponseMessage();
             msg.Content = new ObjectContent<object>(result, new System.Net.Http.Formatting.JsonMediaTypeFormatter());



             msg.StatusCode = HttpStatusCode.OK;
             return msg;
        }
    }
}
