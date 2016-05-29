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
    public class ActivityManagementController : Controller
    {
        private GeneralModel db = new GeneralModel();

        // GET: ActivityManagement
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                OrganizationModels org = db.organizations.First(user => user.email.Equals(User.Identity.Name));
                org.org_activities = org.org_activities.OrderBy(d => d.date.Ticks).ToList();

                if (ViewBag.requestsToDelete != null) 
                {
                    List<UserRequestModels> requests = ViewBag.requestsToDelete;
                    foreach (UserRequestModels requestToDelete in requests)
                    {
                        db.user_requests.Remove(requestToDelete);
                    }

                    ViewBag.requestsToDelete = null;
                    ViewBag.showAlert = null;
                    db.SaveChanges();
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
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // GET: ActivityManagement/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
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
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityManagement/Edit/5
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
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // POST: ActivityManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityManagement/Delete/5
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

        // POST: ActivityManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityModels activityModels = db.activities.Find(id);
            db.activities.Remove(activityModels);
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Respond([Bind(Include = "activityId,volunteerId,isDeletedByOrganization,isDeletedByUser,isAccepted,replyMessage")] UserRequestModels request)
        {
            if (ModelState.IsValid)
            {
                request.isAnswered = true;
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Index();
        }
    }
}
