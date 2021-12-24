using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourScheduleModel : BaseViewModel
    {
        private int _SCHEDULE_ID;
        public int SCHEDULE_ID
        { get => _SCHEDULE_ID; set { _SCHEDULE_ID = value; OnPropertyChanged(); } }

        private string _SCHEDULE_DAY;
        public string SCHEDULE_DAY
        { get => _SCHEDULE_DAY; set { _SCHEDULE_DAY = value; OnPropertyChanged(); } }

        private string _SCHEDULE_CONTENT;
        public string SCHEDULE_CONTENT
        { get => _SCHEDULE_CONTENT; set { _SCHEDULE_CONTENT = value; OnPropertyChanged(); } }
    }
}