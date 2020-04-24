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
        public IEnumerable<Place> GetHotels(int cityID, int Day, int Budget) => Context.Prices.Where(p => p.price <= Budget/(Day*3)).Select(p => p.Place).Where(p=>p.Category.Name== "Hotel" && p.CityID==cityID).ToList();
        public IEnumerable<Place> GetResturants(int cityID, int day,int budget)=> Context.Prices.Where(p => p.price <= budget/(day*3)).Select(p => p.Place).Where(p => p.Category.Name == "Restaurant" && p.CityID == cityID).ToList();
        public Price GetPrice(int placeID) => Context.Prices.FirstOrDefault(p => p.PlaceID == placeID);
        public Place GetRandomHotel(int cityID, int Day, int Budget)
        {
            List<Place> Hotels = this.GetHotels(cityID, Day, Budget).ToList();
            Random randomHotel = new Random();
            return Hotels[randomHotel.Next(Hotels.Count)];
        }
        public List<Place> GetRandomResturants(int cityID, int Day, int Budget)
        {
            Random randomResturant = new Random();
            List<int> indexes = new List<int>();
            List<Place> Resturants = this.GetResturants(cityID, Day, Budget).ToList();
            List<Place> newRestaurants = new List<Place>();
            while(indexes.Count < Resturants.Count)
            {
                int randnum = randomResturant.Next(0,Resturants.Count);
                if (!indexes.Contains(randnum))
                {
                    indexes.Add(randnum);
                }
            }
            for (int i = 0; i < indexes.Count; i++)
            {
                newRestaurants.Add(Resturants[indexes[i]]);
            }

            return newRestaurants;
        }

        public List<Place> Resturants { get; set; }
        public List<Place> Hotels { get; set; }

        [Required,Range(1,365)]
        public int Day { set; get; }
        [Required,Range(100,1000000)]
        public int Budget { get; set; }
    }
}