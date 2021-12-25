using System.Collections.ObjectModel;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourStatisticModel : BaseViewModel
    {
        private int _Tour_ID;

        public int Tour_ID
        { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private string _Tour_Name;

        public string Tour_Name
        { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private int _Tour_NumberVisitGroup;

        public int Tour_NumberVisitGroup
        { get => _Tour_NumberVisitGroup; set { _Tour_NumberVisitGroup = value; OnPropertyChanged(); } }

        private int _Tour_NumberVisitTraveller;

        public int Tour_NumberVisitTraveller
        { get => _Tour_NumberVisitTraveller; set { _Tour_NumberVisitTraveller = value; OnPropertyChanged(); } }

        private double _Tour_TotalCost;

        public double Tour_TotalCost
        { get => _Tour_TotalCost; set { _Tour_TotalCost = value; OnPropertyChanged(); } }

        private ObservableCollection<TourDetailStatisticModel> _DetailStatistic;

        public ObservableCollection<TourDetailStatisticModel> DetailStatistic
        { get => _DetailStatistic; set { _DetailStatistic = value; OnPropertyChanged(); } }
    }
}