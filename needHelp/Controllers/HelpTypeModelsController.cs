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
    public class HelpTypeModelsController : Controller
    {
        private GeneralModel db = GeneralModel.Instance();
        private Cache _cache = Cache.Instance();

        // GET: HelpTypeModels
        public ActionResult Index()
        {
            return View(_cache.help_types.ToList());
        }

        // GET: HelpTypeModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpTypeModels helpTypeModels = _cache.help_types.Find(id);
            if (helpTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(helpTypeModels);
        }

        // GET: HelpTypeModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HelpTypeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,typeName,imagePath")] HelpTypeModels helpTypeModels)
        {
            if (ModelState.IsValid)
            {
                db.help_types.Add(helpTypeModels);
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }

            return View(helpTypeModels);
        }

        // GET: HelpTypeModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpTypeModels helpTypeModels = _cache.help_types.Find(id);
            if (helpTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(helpTypeModels);
        }

        // POST: HelpTypeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,typeName,imagePath")] HelpTypeModels helpTypeModels)
        {
            HelpTypeModels helpType = db.help_types.First(h => h.id == helpTypeModels.id);

            if (ModelState.IsValid)
            {
                helpType.imagePath = helpTypeModels.imagePath;
                helpType.typeName = helpTypeModels.typeName;

                db.Entry(helpType).State = EntityState.Modified;
                db.SaveChanges();
                _cache.UpdateCache();
                return RedirectToAction("Index");
            }
            return View(helpType);
        }

        // GET: HelpTypeModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpTypeModels helpTypeModels = _cache.help_types.Find(id);
            if (helpTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(helpTypeModels);
        }

        // POST: HelpTypeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HelpTypeModels helpTypeModels = db.help_types.Find(id);
            db.help_types.Remove(helpTypeModels);
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
