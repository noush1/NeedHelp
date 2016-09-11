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
    public class CityModelsController : Controller
    {
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        // GET: CityModels
        public ActionResult Index()
        {
            return View(_cache.cities.ToList());
        }

        // GET: CityModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityModels cityModels = _cache.cities.Find(id);
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
                _cache.UpdateCache();
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
            CityModels cityModels = _cache.cities.Find(id);
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
            CityModels city = db.cities.First(c => c.id == cityModels.id);

            if (ModelState.IsValid)
            {
                city.name = cityModels.name;

                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: CityModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityModels cityModels = _cache.cities.Find(id);
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
