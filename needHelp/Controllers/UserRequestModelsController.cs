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
    public class UserRequestModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        public ActionResult Details(int? activityId, int? volunteerId, int? partial)
        {
            if (activityId == null || volunteerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var requests = from s in db.user_requests
                           where s.volunteerId == volunteerId && s.activityId == activityId
                           select s;

            if (requests == null)
            {
                return HttpNotFound();
            }

            if (partial != null)
            {
                return PartialView("SendRespond", requests.First());
            }

            return View("SendRespond", requests.First());
        }
    }
}