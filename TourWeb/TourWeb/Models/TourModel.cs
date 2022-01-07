using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class TourModel
    {
        public int TourID { get; set; }
        public String TourName { get; set; }
        public String Tour_Character { get; set; }
        public double Tour_Star { get; set; }
        public string imagesData { get; set; }
    }
}