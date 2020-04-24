using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuideMe.ViewModels
{
    public class PlacePriceVM
    {
        ApplicationDbContext c = new ApplicationDbContext();
        public List<Place> PlaceList { get; set; }
        public List<Price> PriceList { get; set; }
    }
}