//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourManagementSystem.Global
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRAVELLER_DETAIL
    {
        public int TRAVELLER_DETAIL_ID { get; set; }
        public int TRAVEL_GROUP_ID { get; set; }
        public int TRAVELLER_ID { get; set; }
        public Nullable<int> TRAVELLER_DETAIL_STAR { get; set; }
    
        public virtual TRAVEL_GROUP TRAVEL_GROUP { get; set; }
        public virtual TRAVELLER TRAVELLER { get; set; }
    }
}