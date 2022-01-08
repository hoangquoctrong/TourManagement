using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class RatingModel
    {
        
        public int RatingID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int GroupID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string TravellerName { get; set; }
        [Required(ErrorMessage = "Required")]
        public int TravellerID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 characters required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}