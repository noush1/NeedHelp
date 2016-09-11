using System;
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
    public class OrganizationCalendarController : Controller
    {
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        // GET: OrganizationCalendar
        public ActionResult Index()
        {
            List<ActivityAsEvent> events = new List<ActivityAsEvent>();
            OrganizationModels org = _cache.organizations.First(user => user.email.Equals(User.Identity.Name));
            List<ActivityModels> activities = org.org_activities.OrderBy(d => d.date.Ticks).ToList();

            foreach(ActivityModels act in activities)
            {
                ActivityAsEvent to_add = new ActivityAsEvent();
                to_add.title = String.Concat(act.name,"-",act.city.name);
                to_add.description = act.description;
                to_add.datetime = String.Format("{0:s}", act.date);
                events.Add(to_add);
            }

            return View("Index",this.Json(events,JsonRequestBehavior.AllowGet));
        }

        // GET: OrganizationCalendar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = _cache.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // GET: OrganizationCalendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationCalendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userId,name,contactName,contactPhone,email,website")] OrganizationModels organizationModels)
        {
            if (ModelState.IsValid)
            {
                db.organizations.Add(organizationModels);
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }

            return View(organizationModels);
        }

        // GET: OrganizationCalendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = _cache.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // POST: OrganizationCalendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userId,name,contactName,contactPhone,email,website")] OrganizationModels organizationModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationModels).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }
            return View(organizationModels);
        }

        // GET: OrganizationCalendar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = _cache.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // POST: OrganizationCalendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationModels organizationModels = db.organizations.Find(id);
            db.organizations.Remove(organizationModels);
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
    }
}
