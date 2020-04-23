using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuideMe.ViewModels
{
    public class RecomendationVM
    {
        ApplicationDbContext Context=new ApplicationDbContext();

        public IEnumerable<City> GetCities() => Context.Cities.ToList();
        public IEnumerable<Place> GetHotels(int cityID, int Day, int Budget) => Context.Prices.Where(p => p.price <= Budget/(Day)).Select(p => p.Place).Where(p=>p.Category.Name== "Hotel" && p.CityID==cityID).ToList();
        public IEnumerable<Place> GetResturants(int cityID, int day,int budget)=> Context.Prices.Where(p => p.price <= budget/(day)).Select(p => p.Place).Where(p => p.Category.Name == "Restaurant" && p.CityID == cityID).ToList();

        public List<Place> Resturants { get; set; }
        public List<Place> Hotels { get; set; }

        [Required,Range(1,365)]
        public int Day { set; get; }
        [Required,Range(100,1000000)]
        public int Budget { get; set; }
    }
}