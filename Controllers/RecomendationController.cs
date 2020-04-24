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
    public class RecomendationController : Controller
    {
        ApplicationDbContext Context = new ApplicationDbContext();
        // GET: Recomendation
        public ActionResult Index()
        {
            ViewData["City"] = new SelectList(Context.Cities.ToList(), "ID", "Name");
            ViewData["Categories"] = new SelectList(Context.Categories.ToList(), "ID", "Name");
            RecomendationVM VM = new RecomendationVM();
            return View(VM);
        }

        public ActionResult FilteredPlaces(int CityID,int Day,int Budget)
        {
            RecomendationVM recomendationVM = new RecomendationVM();
            recomendationVM.Hotels = recomendationVM.GetHotels(CityID,Day, Budget).ToList();
            recomendationVM.Resturants = recomendationVM.GetResturants(CityID,Day, Budget).ToList();
            ViewBag.CityID = CityID;
            ViewBag.Day = Day;
            ViewBag.Budget = Budget;
            return View(recomendationVM);
        }

        public ActionResult RecomendPlaces(int CityID, int Day, int Budget)
        {
            ViewBag.CityID = CityID;
            ViewBag.Day = Day;
            ViewBag.Budget = Budget;
            RecomendationVM recomendationVM = new RecomendationVM();
            recomendationVM.Day = Day;
            recomendationVM.Budget = Budget;
            return View(recomendationVM);
        }
    }
}