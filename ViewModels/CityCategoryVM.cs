using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuideMe.ViewModels
{
    
    public class CityCategoryVM
    {
        ApplicationDbContext Context = new ApplicationDbContext();

        public IEnumerable<City> GetCities() => Context.Cities.ToList();
        public IEnumerable<Category> GetCategories() => Context.Categories.ToList();
        public IEnumerable<Place> GetPlaces() => Context.Places.ToList();
        public IEnumerable<Service> GetServices() => Context.Services.ToList();
        public IEnumerable<Price> GetPrices() => Context.Prices.ToList();
        public IEnumerable<PlaceService> GetPlacesServices() => Context.PlaceServices.ToList();
    }
}