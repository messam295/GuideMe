using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMe.Models.Business_models
{
    public class Place
    {
        private double rate = 10;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Index("IX_UniquePlace", 1, IsUnique = true)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }

        [StringLength(100)]
        [Index("IX_UniquePlace",2,IsUnique =true)]
        public string Address { get; set; }
        [DisplayName("Google location")]
        public string GoogleLocation { get; set; } // google map Link

        public string Image { get; set; }

        public Category Category { get; set; }
        [Range(0, 10)]
        public double Rate { get { return rate; } set { rate = value; } }
        public City City { get; set; }

        public ICollection<PlaceService> PlaceServices { get; set; }
    }
}
