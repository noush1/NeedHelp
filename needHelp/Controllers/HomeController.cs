using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace needHelp.Controllers
{
    public class HomeController : Controller
    {
        private GeneralModel _db = new GeneralModel();

        public ActionResult Index()
        {
            _db.cities.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}