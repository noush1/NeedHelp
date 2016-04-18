using System;
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

            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
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
            int organizationID = 0, typeID = 0, cityID = 0;
            DateTime defaultDate = new DateTime(1777,1,1);
            DateTime startDate = defaultDate;
            DateTime endDate = defaultDate;

            string req = Request["organizationId"];
            if (req != null && req != String.Empty)
            {
               organizationID = Int32.Parse(req.ToString());
            }
            req = Request["typeId"];
            if (req != null && req != String.Empty)
            {
               typeID = Int32.Parse(req.ToString());
            }
            req = Request["cityId"];
            if (req != null && req != String.Empty)
            {
                cityID = Int32.Parse(req.ToString());
            }

            req = Request["txtStartDate"];
            if (req != null && req != String.Empty)
            {
                DateTime temp = new DateTime();
                if (DateTime.TryParse(req.ToString(), out temp))
                {
                    startDate = temp;
                }
            }

            req = Request["txtEndDate"];
            if (req != null && req != String.Empty)
            {
                DateTime temp = new DateTime();
                if (DateTime.TryParse(req.ToString(),out temp))
                {
                    endDate = temp;
                }
            }
           
            var result = from s in db.activities
                         where s.name.Contains(activityName) &&
                         (cityID == 0 || s.cityId == cityID) &&
                         (typeID == 0 || s.typeId == typeID) &&
                         (organizationID == 0 || s.organizationId == organizationID) &&
                         (startDate.Equals(defaultDate) || startDate <= s.date) &&
                         (endDate.Equals(defaultDate) || endDate >= s.date)
                         select s;

            //return RedirectToAction("Index","Series");
            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
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
