using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourPriceModel : BaseViewModel
    {
        private int _PRICE_ID;

        public int PRICE_ID
        { get => _PRICE_ID; set { _PRICE_ID = value; OnPropertyChanged(); } }

        private double _PRICE_COST_TOTAL;

        public double PRICE_COST_TOTAL
        { get => _PRICE_COST_TOTAL; set { _PRICE_COST_TOTAL = value; OnPropertyChanged(); } }

        private double _PRICE_COST_HOTEL;

        public double PRICE_COST_HOTEL
        { get => _PRICE_COST_HOTEL; set { _PRICE_COST_HOTEL = value; OnPropertyChanged(); } }

        private double _PRICE_COST_TRANSPORT;

        public double PRICE_COST_TRANSPORT
        { get => _PRICE_COST_TRANSPORT; set { _PRICE_COST_TRANSPORT = value; OnPropertyChanged(); } }

        private double _PRICE_COST_SERVICE;

        public double PRICE_COST_SERVICE
        { get => _PRICE_COST_SERVICE; set { _PRICE_COST_SERVICE = value; OnPropertyChanged(); } }

        private string _PRICE_NOTE;

        public string PRICE_NOTE
        { get => _PRICE_NOTE; set { _PRICE_NOTE = value; OnPropertyChanged(); } }
    }
}