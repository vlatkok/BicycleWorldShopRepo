using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BicycleWorldShop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product ID")]
        public int  ProductId { get; set; }
        [Required(ErrorMessage = "Name of the headline is required")]
        [MaxLength(100, ErrorMessage = "The maximum length must be upto 100 characters only")]
        public string Headline { get; set; }

      
        public int Rating { get; set; }


        public string Pros { get; set; }
        public string Cons { get; set; }

        [Display(Name = "Best Uses")]
        public string BestUses { get; set; }
        [Display(Name = "Describe Yourself")]
        public string DescribeYourself { get; set; }
        [Display(Name = "Cycling Style")]
        public string CyclingStyle { get; set; }
        public string Gift { get; set; }
        [Required(ErrorMessage = "Comment text is required")]
        [MaxLength(1000, ErrorMessage = "The maximum length must be upto 1000 characters only")]

        public string Comments { get; set; }
        [Display(Name = "Bottom Line")]
        public string BottomLine { get; set; }
        [Required(ErrorMessage = "Nickname is required")]
        [MaxLength(60, ErrorMessage = "The maximum length must be upto 60 characters only")]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [MaxLength(100, ErrorMessage = "The maximum length must be upto 100 characters only")]
        public string Location { get; set; }

       
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
           ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]       
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
    }
}