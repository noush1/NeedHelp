using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using needHelp.Models;

namespace needHelp.Controllers
{
    public class OrganizationModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        // GET: OrganizationModels
        public ActionResult Index()
        {
            return View(db.organizations.ToList());
        }

        // GET: OrganizationModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = db.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // GET: OrganizationModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationModels/Create
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
                return RedirectToAction("Index");
            }

            return View(organizationModels);
        }

        // GET: OrganizationModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = db.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // POST: OrganizationModels/Edit/5
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
                return RedirectToAction("Index");
            }
            return View(organizationModels);
        }

        // GET: OrganizationModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationModels organizationModels = db.organizations.Find(id);
            if (organizationModels == null)
            {
                return HttpNotFound();
            }
            return View(organizationModels);
        }

        // POST: OrganizationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizationModels organizationModels = db.organizations.Find(id);
            db.organizations.Remove(organizationModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
