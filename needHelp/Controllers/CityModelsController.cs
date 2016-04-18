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
    public class CityModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        // GET: CityModels
        public ActionResult Index()
        {
            return View(db.cities.ToList());
        }

        // GET: CityModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityModels cityModels = db.cities.Find(id);
            if (cityModels == null)
            {
                return HttpNotFound();
            }
            return View(cityModels);
        }

        // GET: CityModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] CityModels cityModels)
        {
            if (ModelState.IsValid)
            {
                db.cities.Add(cityModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityModels);
        }

        // GET: CityModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityModels cityModels = db.cities.Find(id);
            if (cityModels == null)
            {
                return HttpNotFound();
            }
            return View(cityModels);
        }

        // POST: CityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] CityModels cityModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityModels);
        }

        // GET: CityModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityModels cityModels = db.cities.Find(id);
            if (cityModels == null)
            {
                return HttpNotFound();
            }
            return View(cityModels);
        }

        // POST: CityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CityModels cityModels = db.cities.Find(id);
            db.cities.Remove(cityModels);
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
