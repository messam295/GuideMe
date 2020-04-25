using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using GuideMe.ViewModels;
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
            ViewData["City"] = new SelectList(ctx.Cities.ToList(), "ID", "Name");
            ViewData["Categories"] = new SelectList(ctx.Categories.ToList(), "ID", "Name");
            CityCategoryVM vM = new CityCategoryVM();
            return View(vM);
        }
        
        public ActionResult GetFilterdPlaces(int CityId, int CategoryId, int PriceRange)
        {
            List<Place> places = ctx.Places.Where(p => p.CityID == CityId && p.CategoryID == CategoryId).ToList();
            return View(places);
        }

        //[HttpPost]
        //public ActionResult GetFilterdPlaces(CityCategoryVM VM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View(VM);
        //    }
        //    return RedirectToAction("Index");
        //}

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
            PlacePriceVM BigModel = new PlacePriceVM();
            BigModel.PlaceList = ctx.Places.ToList();
            BigModel.PriceList = ctx.Prices.ToList();
            
            return PartialView(BigModel);
        }
        public ActionResult _Restaurants()
        {
            List<Place> RestaurantsList = ctx.Places.Where(p => p.CategoryID == 2).OrderByDescending(r=>r.Rate).ToList();
            return PartialView(RestaurantsList);
        }
    }
}
