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
        [RegularExpression(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Not a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 characters required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}