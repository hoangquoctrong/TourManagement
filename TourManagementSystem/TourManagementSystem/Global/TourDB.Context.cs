﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Tour_Mangement_DatabaseEntities : DbContext
    {
        public Tour_Mangement_DatabaseEntities()
            : base("name=Tour_Mangement_DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PLACE> PLACE { get; set; }
        public virtual DbSet<TOUR> TOUR { get; set; }
        public virtual DbSet<TOUR_LOCATION> TOUR_LOCATION { get; set; }
        public virtual DbSet<TOUR_LOCATION_DETAILED> TOUR_LOCATION_DETAILED { get; set; }
        public virtual DbSet<TOUR_PRICE> TOUR_PRICE { get; set; }
        public virtual DbSet<TOUR_HOTEL> TOUR_HOTEL { get; set; }
        public virtual DbSet<TOUR_HOTEL_DETAIL> TOUR_HOTEL_DETAIL { get; set; }
        public virtual DbSet<TOUR_MISSION> TOUR_MISSION { get; set; }
        public virtual DbSet<TOUR_STAFF> TOUR_STAFF { get; set; }
        public virtual DbSet<TOUR_TRANSPORT> TOUR_TRANSPORT { get; set; }
        public virtual DbSet<TOUR_TRANSPORT_DETAIL> TOUR_TRANSPORT_DETAIL { get; set; }
        public virtual DbSet<TRAVEL_COST> TRAVEL_COST { get; set; }
        public virtual DbSet<TRAVEL_GROUP> TRAVEL_GROUP { get; set; }
        public virtual DbSet<TRAVELLER> TRAVELLER { get; set; }
        public virtual DbSet<TRAVELLER_DETAIL> TRAVELLER_DETAIL { get; set; }
        public virtual DbSet<TOUR_ACCOUNT> TOUR_ACCOUNT { get; set; }
        public virtual DbSet<TOUR_RECORD> TOUR_RECORD { get; set; }
        public virtual DbSet<TOUR_STAFF_DELETE> TOUR_STAFF_DELETE { get; set; }
        public virtual DbSet<TRAVELLER_ACCOUNT> TRAVELLER_ACCOUNT { get; set; }
    }
}