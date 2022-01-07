using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class TourInformation
    {
        public int TourInformationID { get; set; }
        public PriceModel Price { get; set; }
        public TimeModel Time { get; set; }
        public List<ScheduleModel> Schedule { get; set; }
    }
}