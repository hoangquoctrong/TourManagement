using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TravellerDetailModel : BaseViewModel
    {
        private int _TravellerDetail_ID;

        public int TravellerDetail_ID
        { get => _TravellerDetail_ID; set { _TravellerDetail_ID = value; OnPropertyChanged(); } }

        private int _TravelGroup_ID;

        public int TravelGroup_ID
        { get => _TravelGroup_ID; set { _TravelGroup_ID = value; OnPropertyChanged(); } }

        private int _Traveller_ID;

        public int Traveller_ID
        { get => _Traveller_ID; set { _Traveller_ID = value; OnPropertyChanged(); } }

        private int _TravellerDetailStar_ID;

        public int TravellerDetailStar_ID
        { get => _TravellerDetailStar_ID; set { _TravellerDetailStar_ID = value; OnPropertyChanged(); } }
    }
}