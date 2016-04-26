using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace needHelp.Controllers
{
    public class ProfileModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        public ActionResult Index()
        {
            VolunteerModels volunteer = db.volunteers.First(user => user.email == User.Identity.Name);
            ViewBag.sugestedActivities = db.activities.ToList();

            return View(volunteer);
        }
    }
}