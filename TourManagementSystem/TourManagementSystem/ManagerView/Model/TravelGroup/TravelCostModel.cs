using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TravelCostModel : BaseViewModel
    {
        private int _TravelGroup_ID;

        public int TravelGroup_ID
        { get => _TravelGroup_ID; set { _TravelGroup_ID = value; OnPropertyChanged(); } }

        private int _TravelCost_ID;

        public int TravelCost_ID
        { get => _TravelCost_ID; set { _TravelCost_ID = value; OnPropertyChanged(); } }

        private double _HotelPrice;

        public double HotelPrice
        { get => _HotelPrice; set { _HotelPrice = value; OnPropertyChanged(); } }

        private double _TransportPrice;

        public double TransportPrice
        { get => _TransportPrice; set { _TransportPrice = value; OnPropertyChanged(); } }

        private double _ServicePrice;

        public double ServicePrice
        { get => _ServicePrice; set { _ServicePrice = value; OnPropertyChanged(); } }

        private double _AnotherPrice;

        public double AnotherPrice
        { get => _AnotherPrice; set { _AnotherPrice = value; OnPropertyChanged(); } }

        private double _TotalPrice;

        public double TotalPrice
        { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private string _TravelCostDescription;

        public string TravelCostDescription
        { get => _TravelCostDescription; set { _TravelCostDescription = value; OnPropertyChanged(); } }
    }
}