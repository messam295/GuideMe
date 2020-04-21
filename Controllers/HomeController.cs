using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuideMe.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext ctx;

        public HomeController()
        {
            ctx = new ApplicationDbContext();
        }
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult _cities() 
        {
            var cities = ctx.Cities.Include("Places").OrderByDescending(c=>c.Places.Count).ToList();
            
            return PartialView(cities);
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

        public ActionResult _allPlaces()
        {
            List<Place> places = ctx.Places.ToList();
            return PartialView(places);
        }
    }
}
