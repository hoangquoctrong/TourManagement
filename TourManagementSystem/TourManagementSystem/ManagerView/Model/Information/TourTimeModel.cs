using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourTimeModel : BaseViewModel
    {
        private int _TIME_ID;
        public int TIME_ID
        { get => _TIME_ID; set { _TIME_ID = value; OnPropertyChanged(); } }

        private DateTime _TIME_DEPARTMENT_TIME = DateTime.Now;
        public DateTime TIME_DEPARTMENT_TIME
        { get => _TIME_DEPARTMENT_TIME; set { _TIME_DEPARTMENT_TIME = value; OnPropertyChanged(); } }

        private string _TIME_DEPARTMENT_STRING;
        public string TIME_DEPARTMENT_STRING
        { get => _TIME_DEPARTMENT_STRING; set { _TIME_DEPARTMENT_STRING = value; OnPropertyChanged(); } }

        private DateTime _TIME_END_TIME = DateTime.Now;
        public DateTime TIME_END_TIME
        { get => _TIME_END_TIME; set { _TIME_END_TIME = value; OnPropertyChanged(); } }

        private string _TIME_END_STRING;
        public string TIME_END_STRING
        { get => _TIME_END_STRING; set { _TIME_END_STRING = value; OnPropertyChanged(); } }

        private int _TIME_DAY;
        public int TIME_DAY
        { get => _TIME_DAY; set { _TIME_DAY = value; OnPropertyChanged(); } }

        private int _TIME_NIGHT;
        public int TIME_NIGHT
        { get => _TIME_NIGHT; set { _TIME_NIGHT = value; OnPropertyChanged(); } }

        private string _TIME_STRING;
        public string TIME_STRING
        { get => _TIME_STRING; set { _TIME_STRING = value; OnPropertyChanged(); } }
    }
}