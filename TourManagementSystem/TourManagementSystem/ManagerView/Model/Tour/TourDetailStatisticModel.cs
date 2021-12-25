using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourDetailStatisticModel : BaseViewModel
    {
        private DateTime _Tour_Department = DateTime.Now;

        public DateTime Tour_Department
        { get => _Tour_Department; set { _Tour_Department = value; OnPropertyChanged(); } }

        private DateTime _Tour_End = DateTime.Now;

        public DateTime Tour_End
        { get => _Tour_End; set { _Tour_End = value; OnPropertyChanged(); } }

        private double _Tour_Cost;

        public double Tour_Cost
        { get => _Tour_Cost; set { _Tour_Cost = value; OnPropertyChanged(); } }

        private int _Tour_NumberTraveller;

        public int Tour_NumberTraveller
        { get => _Tour_NumberTraveller; set { _Tour_NumberTraveller = value; OnPropertyChanged(); } }
    }
}