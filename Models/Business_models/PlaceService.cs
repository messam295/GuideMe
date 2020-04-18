using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMe.Models.Business_models
{
    public class PlaceService
    {
        [Key,Column(Order =0)]
        public int PlaceID { get; set; }

        [Key, Column(Order = 1)]
        public int ServiceID { get; set; }

        public virtual Place Place { get; set; }
        public virtual Service Service { get; set; }
    }
}
