using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class PlaceDetailModel : BaseViewModel
    {
        private int _Place_Detail_ID;

        public int Place_Detail_ID
        { get => _Place_Detail_ID; set { _Place_Detail_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;

        public int Tour_ID
        { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private int _Place_ID;

        public int Place_ID
        { get => _Place_ID; set { _Place_ID = value; OnPropertyChanged(); } }
    }
}