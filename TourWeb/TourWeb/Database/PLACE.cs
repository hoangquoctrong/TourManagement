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
    
    public partial class PLACE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLACE()
        {
            this.TOUR_HOTEL = new HashSet<TOUR_HOTEL>();
            this.TOUR_LOCATION = new HashSet<TOUR_LOCATION>();
            this.TOUR_PLACE_DETAILED = new HashSet<TOUR_PLACE_DETAILED>();
        }
    
        public int PLACE_ID { get; set; }
        public string PLACE_NAME { get; set; }
        public string PLACE_NATION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_HOTEL> TOUR_HOTEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_LOCATION> TOUR_LOCATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_PLACE_DETAILED> TOUR_PLACE_DETAILED { get; set; }
    }
}
