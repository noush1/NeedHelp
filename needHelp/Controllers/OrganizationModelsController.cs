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
    public class OrganizationModelsController : Controller
    {
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        // GET: OrganizationModels
        public ActionResult Index()
        {
            return View(_cache.organizations.ToList());
        }

        // GET: OrganizationModels/Details/5
        public ActionResult Details(int? id, int? partial)
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

            if (partial == null)
            {
                return View(organizationModels);
            }
            else
            {
                return PartialView(organizationModels);
            }
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
                _cache.UpdateCache();
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
            OrganizationModels organizationModels = _cache.organizations.Find(id);
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
            OrganizationModels org = db.organizations.First(o => o.id == organizationModels.id);

            if (ModelState.IsValid)
            {
                org.contactName = organizationModels.contactName;
                org.contactPhone = organizationModels.contactPhone;
                org.email = organizationModels.email;
                org.name = organizationModels.name;
                org.website = organizationModels.website;

                db.Entry(org).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }
            return View(org);
        }

        // GET: OrganizationModels/Delete/5
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

        // POST: OrganizationModels/Delete/5
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
