using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace needHelp.Controllers
{
    public class OrgProfileModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        public ActionResult Index()
        {
            OrganizationModels org = db.organizations.First(user => user.email.Equals(User.Identity.Name));
            
            /*
            var result = from s in db.activities
                         where (s.organizationId == org.id)
                         select s;

            ViewBag.organizationActivities = result.ToList();
            */
            return View(org);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrganizationModels org = db.organizations.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }

            return View(org);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userId,name,contactName,contactPhone,email,website")] OrganizationModels org)
        {
            OrganizationModels organizationModels = db.organizations.First(user => user.email == org.email);
            organizationModels.name = org.name;
            organizationModels.website = org.website;
            organizationModels.email = org.email;
            organizationModels.contactName = org.contactName;
            organizationModels.contactPhone = org.contactPhone;

            if (ModelState.IsValid)
            {
                db.Entry(organizationModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizationModels);
        }
    }
}