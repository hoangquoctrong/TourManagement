using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class StaffSatisticExportModel : BaseViewModel
    {
        private int _Staff_ID;
        public int Staff_ID
        { get => _Staff_ID; set { _Staff_ID = value; OnPropertyChanged(); } }

        private string _Staff_Name;
        public string Staff_Name
        { get => _Staff_Name; set { _Staff_Name = value; OnPropertyChanged(); } }

        private int _Staff_Tour;
        public int Staff_Tour
        { get => _Staff_Tour; set { _Staff_Tour = value; OnPropertyChanged(); } }

        public StaffSatisticExportModel(int id, string name, int numtour)
        {
            Staff_ID = id;
            Staff_Name = name;
            Staff_Tour = numtour;
        }
    }
}