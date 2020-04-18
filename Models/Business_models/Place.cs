using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMe.Models.Business_models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Index("IX_UniquePlace", 1, IsUnique = true)]
        public string Name { get; set; }

        [ForeignKey("PlaceCategory")]
        public int PlaceCategoryID { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }

        [StringLength(100)]
        [Index("IX_UniquePlace",2,IsUnique =true)]
        public string Address { get; set; }

        public string GoogleLocation { get; set; } // gooel map Link

        public string Image { get; set; }

        public Category PlaceCategory { get; set; }
        public City City { get; set; }

        public ICollection<PlaceService> PlaceServices { get; set; }
    }
}
