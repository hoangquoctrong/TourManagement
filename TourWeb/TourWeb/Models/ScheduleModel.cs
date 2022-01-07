using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class ScheduleModel
    {
        public int ScheduleId { get; set; }
        public string ScheduleDay { get; set; }
        public string ScheduleContent { get; set; }
    }
}