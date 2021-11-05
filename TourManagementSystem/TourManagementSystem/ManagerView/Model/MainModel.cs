


/// <summary>
/// Model [ "The Content Creator" ]
/// The model class holds the data. The model can be referred to as the data file for the front-end of the application.
/// </summary>


namespace TourManagementSystem.ManagerView.Model
{
    //Main Menu Item
    public class MenuItems
    {
        public string MenuName { get; set; }
        public string MenuImage { get; set; }
    }

    // Dashboard
    public class DashboardItems
    {
        public string DashboardName { get; set; }
        public string DashboardImage { get; set; }
    }

    //Tour
    public class TourItems
    {
        public string TourName { get; set; }
        public string TourImage { get; set; }
    }

    //TravelGroup
    public class TravelGroupItems
    {
        public string TravelGroupName { get; set; }
        public string TravelGroupImage { get; set; }
    }

    //Place
    public class PlaceItems
    {
        public string PlaceName { get; set; }
        public string PlaceImage { get; set; }
    }

    //Staff
    public class StaffItems
    {
        public string StaffName { get; set; }
        public string StaffImage { get; set; }
    }

    //Hotel
    public class HotelItems
    {
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
    }

    //Transport
    public class TransportItems
    {
        public string TransportName { get; set; }
        public string TransportImage { get; set; }
    }

    //Account
    public class AccountItems
    {
        public string AccountName { get; set; }
        public string AccountImage { get; set; }
    }
}