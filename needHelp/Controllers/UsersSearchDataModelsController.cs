using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace needHelp.Controllers
{
    public class UsersSearchDataModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        public ActionResult Index()
        {
            var usersSearchData = db.search_data;

            foreach (UserSearchDataModels usd in usersSearchData.ToList())
            {
                usd.org = db.organizations.Find(usd.organizationId);
            }

            return View(usersSearchData.ToList());
        }
    }
}