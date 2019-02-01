using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace BicycleWorldShop.Models
{
    public class Product
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }    
        
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100, ErrorMessage = "The maximum length must be upto 100 characters only")]
        public string Name { get; set; }

    //    [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Has to be decimal with two decimal points")]
    //    [Range(0,7,ErrorMessage = "The maximum possible value should be up to 7 digits")]
        public Decimal Price { get;set; }


        public string Description { get; set; }
        public string Feature { get; set; }
        public string Video { get; set; }
    //  [Required(ErrorMessage = "PartNumbers of the product is required")]
        public string PartNumber { get; set; }
        public string Color { get; set; }

      //  [Required(ErrorMessage = "Product availability is required")]
     //   [MaxLength(3, ErrorMessage = "The maximum length must be up to 3 characters only")]
        public string Availability { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Product brand is required")]
        [MaxLength(100, ErrorMessage = "The maximum length must be upto 100 characters only")]
        public string Brand { get; set; }


       // [RegularExpression(@"^\d+.\d{0,1}$", ErrorMessage = "Has to be decimal with one decimal points")]
     //   [Range(0, 3, ErrorMessage = "The maximum possible value should be upto 3 digits")]
        public string Size { get; set; }

        [MaxLength(6, ErrorMessage = "The maximum length must be up to 6 characters only")]
        public string Gender { get; set; }


        [Display(Name = "Updated At")]
        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdated { get; set; }


       [Column(Order = 1)]
        public int Category_Id { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}