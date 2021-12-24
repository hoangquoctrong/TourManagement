using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class LocationDetailModel : BaseViewModel
    {
        private int _Location_Detail_ID;
        public int Location_Detail_ID
        { get => _Location_Detail_ID; set { _Location_Detail_ID = value; OnPropertyChanged(); } }

        private int _Tour_Information_ID;
        public int Tour_Information_ID
        { get => _Tour_Information_ID; set { _Tour_Information_ID = value; OnPropertyChanged(); } }

        private int _Location_ID;
        public int Location_ID
        { get => _Location_ID; set { _Location_ID = value; OnPropertyChanged(); } }
    }
}