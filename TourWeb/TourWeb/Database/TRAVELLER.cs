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
    
    public partial class TRAVELLER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAVELLER()
        {
            this.TRAVELLER_ACCOUNT = new HashSet<TRAVELLER_ACCOUNT>();
            this.TRAVELLER_DETAIL = new HashSet<TRAVELLER_DETAIL>();
        }
    
        public int TRAVELLER_ID { get; set; }
        public string TRAVELLER_NAME { get; set; }
        public string TRAVELLER_CITIZEN_IDENTITY { get; set; }
        public string TRAVELLER_SEX { get; set; }
        public Nullable<System.DateTime> TRAVELLER_BIRTH { get; set; }
        public string TRAVELLER_ADDRESS { get; set; }
        public string TRAVELLER_PHONE_NUMBER { get; set; }
        public string TRAVELLER_TYPE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAVELLER_ACCOUNT> TRAVELLER_ACCOUNT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAVELLER_DETAIL> TRAVELLER_DETAIL { get; set; }
    }
}
