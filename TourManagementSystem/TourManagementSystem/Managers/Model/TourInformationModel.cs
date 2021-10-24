using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.Model
{
    public class TourInformationModel : BaseViewModel
    {
        private int _TOUR_ID;
        public int TOUR_ID { get => _TOUR_ID; set { _TOUR_ID = value; OnPropertyChanged(); } }

        private int _INFORMATION_ID;
        public int INFORMATION_ID { get => _INFORMATION_ID; set { _INFORMATION_ID = value; OnPropertyChanged(); } }

        private TourTimeModel _INFORMATION_TIME;
        public TourTimeModel INFORMATION_TIME { get => _INFORMATION_TIME; set { _INFORMATION_TIME = value; OnPropertyChanged(); } }

        private TourPriceModel _INFORMATION_PRICE;
        public TourPriceModel INFORMATION_PRICE { get => _INFORMATION_PRICE; set { _INFORMATION_PRICE = value; OnPropertyChanged(); } }

        private ObservableCollection<TourScheduleModel> _INFORMATION_SCHEDULE_LIST;
        public ObservableCollection<TourScheduleModel> INFORMATION_SCHEDULE_LIST { get => _INFORMATION_SCHEDULE_LIST; set { _INFORMATION_SCHEDULE_LIST = value; OnPropertyChanged(); } }

        private ObservableCollection<PlaceModel> _INFORMATION_PLACE_LIST;
        public ObservableCollection<PlaceModel> INFORMATION_PLACE_LIST { get => _INFORMATION_PLACE_LIST; set { _INFORMATION_PLACE_LIST = value; OnPropertyChanged(); } }
    }
}
