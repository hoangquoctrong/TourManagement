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
    
    public partial class TOUR_INFORMATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOUR_INFORMATION()
        {
            this.TOUR_LOCATION_DETAIL = new HashSet<TOUR_LOCATION_DETAIL>();
            this.TOUR_MISSION = new HashSet<TOUR_MISSION>();
            this.TOUR_SCHEDULE = new HashSet<TOUR_SCHEDULE>();
        }
    
        public int TOUR_INFORMATION_ID { get; set; }
        public int TOUR_ID { get; set; }
        public int TOUR_HOTEL_ID { get; set; }
        public int TOUR_TRANSPORT_ID { get; set; }
        public int TOUR_TIME_ID { get; set; }
        public int TOUR_PRICE_ID { get; set; }
    
        public virtual TOUR TOUR { get; set; }
        public virtual TOUR_HOTEL TOUR_HOTEL { get; set; }
        public virtual TOUR_TRANSPORT TOUR_TRANSPORT { get; set; }
        public virtual TOUR_TIME TOUR_TIME { get; set; }
        public virtual TOUR_PRICE TOUR_PRICE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_LOCATION_DETAIL> TOUR_LOCATION_DETAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_MISSION> TOUR_MISSION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_SCHEDULE> TOUR_SCHEDULE { get; set; }
    }
}