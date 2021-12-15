using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourInformationModel : BaseViewModel
    {
        private int _TOUR_ID;
        public int TOUR_ID { get => _TOUR_ID; set { _TOUR_ID = value; OnPropertyChanged(); } }

        private int _INFORMATION_ID;
        public int INFORMATION_ID { get => _INFORMATION_ID; set { _INFORMATION_ID = value; OnPropertyChanged(); } }

        private bool _INFORMATION_ENABLE;
        public bool INFORMATION_ENABLE { get => _INFORMATION_ENABLE; set { _INFORMATION_ENABLE = value; OnPropertyChanged(); } }

        private string _INFORMATION_STATUS;
        public string INFORMATION_STATUS { get => _INFORMATION_STATUS; set { _INFORMATION_STATUS = value; OnPropertyChanged(); } }

        private TourTimeModel _INFORMATION_TIME;
        public TourTimeModel INFORMATION_TIME { get => _INFORMATION_TIME; set { _INFORMATION_TIME = value; OnPropertyChanged(); } }

        private TourPriceModel _INFORMATION_PRICE;
        public TourPriceModel INFORMATION_PRICE { get => _INFORMATION_PRICE; set { _INFORMATION_PRICE = value; OnPropertyChanged(); } }

        private ObservableCollection<TourScheduleModel> _INFORMATION_SCHEDULE_LIST;
        public ObservableCollection<TourScheduleModel> INFORMATION_SCHEDULE_LIST { get => _INFORMATION_SCHEDULE_LIST; set { _INFORMATION_SCHEDULE_LIST = value; OnPropertyChanged(); } }

        private ObservableCollection<LocationModel> _INFORMATION_LOCATION_LIST;
        public ObservableCollection<LocationModel> INFORMATION_LOCATION_LIST { get => _INFORMATION_LOCATION_LIST; set { _INFORMATION_LOCATION_LIST = value; OnPropertyChanged(); } }

        private ObservableCollection<HotelModel> _INFORMATION_HOTEL_LIST;
        public ObservableCollection<HotelModel> INFORMATION_HOTEL_LIST { get => _INFORMATION_HOTEL_LIST; set { _INFORMATION_HOTEL_LIST = value; OnPropertyChanged(); } }

        private ObservableCollection<TransportModel> _INFORMATION_TRANSPORT_LIST;
        public ObservableCollection<TransportModel> INFORMATION_TRANSPORT_LIST { get => _INFORMATION_TRANSPORT_LIST; set { _INFORMATION_TRANSPORT_LIST = value; OnPropertyChanged(); } }

        private ObservableCollection<MissionModel> _INFORMATION_MISSION_LIST;
        public ObservableCollection<MissionModel> INFORMATION_MISSION_LIST { get => _INFORMATION_MISSION_LIST; set { _INFORMATION_MISSION_LIST = value; OnPropertyChanged(); } }
    }
}
