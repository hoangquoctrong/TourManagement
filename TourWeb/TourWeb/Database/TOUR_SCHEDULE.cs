//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourWeb.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class TOUR_SCHEDULE
    {
        public int TOUR_SCHEDULE_ID { get; set; }
        public string TOUR_SCHEDULE_DAY { get; set; }
        public string TOUR_SCHEDULE_CONTENT { get; set; }
        public int TOUR_INFORMATION_ID { get; set; }
    
        public virtual TOUR_INFORMATION TOUR_INFORMATION { get; set; }
    }
}