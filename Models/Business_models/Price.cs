using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMe.Models.Business_models
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("Place")]
        public int PlaceID { get; set; }
        public int price { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }
    }
}
