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
    
    public partial class TOUR_TRANSPORT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOUR_TRANSPORT()
        {
            this.TOUR_TRANSPORT_DETAIL = new HashSet<TOUR_TRANSPORT_DETAIL>();
        }
    
        public int TOUR_TRANSPORT_ID { get; set; }
        public string TOUR_TRANSPORT_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOUR_TRANSPORT_DETAIL> TOUR_TRANSPORT_DETAIL { get; set; }
    }
}