using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class PriceModel
    {
        public int PriceId { get; set; }
        public double PriceTotal { get; set; }
        public double PriceHotel { get; set; }
        public double PriceTransport { get; set; }
        public double PriceService { get; set; }
        public double PriceOther { get; set; }
        public string PriceNote { get; set; }
    }
}