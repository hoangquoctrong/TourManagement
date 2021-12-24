using System.Collections.ObjectModel;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class StaffStatisticModel : BaseViewModel
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

        private ObservableCollection<TourTimeModel> _TimeList;
        public ObservableCollection<TourTimeModel> TimeList
        { get => _TimeList; set { _TimeList = value; OnPropertyChanged(); } }
    }
}