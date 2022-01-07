using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class TimeModel
    {
        public int TimeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string TimeStartString { get; set; }
        public string TimeEndString { get; set; }
        public int TimeDay { get; set; }
        public int TimeNight { get; set; }
        public string TimeString { get; set; }
    }
}