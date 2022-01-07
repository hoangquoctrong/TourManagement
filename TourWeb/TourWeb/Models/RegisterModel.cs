using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class RegisterModel
    {
        public int RegisterID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Detail { get; set; }
    }
}