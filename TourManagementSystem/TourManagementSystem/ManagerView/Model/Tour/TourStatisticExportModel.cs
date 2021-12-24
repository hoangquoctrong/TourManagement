using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model.Tour
{
    internal class TourStatisticExportModel : BaseViewModel
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

        public TourStatisticExportModel(int id, string name, int numGroup, int numTraveller, double totalCost)
        {
            _Tour_ID = id;
            _Tour_Name = name;
            _Tour_NumberVisitGroup = numGroup;
            _Tour_NumberVisitTraveller = numTraveller;
            _Tour_TotalCost = totalCost;
        }
    }
}