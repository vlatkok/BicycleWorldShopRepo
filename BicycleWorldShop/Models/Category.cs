using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BicycleWorldShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(45,ErrorMessage = "The category name can be maximum 45 characters long")]
        public string Name { get; set; }

        [Display(Name = "SubCategory")]
        [Required(ErrorMessage = "SubCategory name is required")]
        [MaxLength(60, ErrorMessage = "The SubCategory name can be maximum 60 characters long")]
        public string SubCategoryName { get; set; }

        public virtual  ICollection<Product> Products { get; set; } 
    }
}