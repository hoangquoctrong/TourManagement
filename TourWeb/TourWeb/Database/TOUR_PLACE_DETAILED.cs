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
    
    public partial class TOUR_PLACE_DETAILED
    {
        public int TOUR_PLACE_DETAILED_ID { get; set; }
        public int PLACE_ID { get; set; }
        public int TOUR_ID { get; set; }
    
        public virtual PLACE PLACE { get; set; }
        public virtual TOUR TOUR { get; set; }
    }
}
