using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourLocationModel : BaseViewModel
    {
        private int _TOUR_ID;
        public int TOUR_ID { get => _TOUR_ID; set { _TOUR_ID = value; OnPropertyChanged(); } }

        private int _LOACATION_ID;
        public int LOCATION_ID { get => _LOACATION_ID; set { _LOACATION_ID = value; OnPropertyChanged(); } }

        private string _TOUR_NAME;
        public string TOUR_NAME { get => _TOUR_NAME; set { _TOUR_NAME = value; OnPropertyChanged(); } }

        private string _TOUR_STRING_DATE;
        public string TOUR_STRING_DATE { get => _TOUR_STRING_DATE; set { _TOUR_STRING_DATE = value; OnPropertyChanged(); } }

        private DateTime _TOUR_DATE = DateTime.Now;
        public DateTime TOUR_DATE { get => _TOUR_DATE; set { _TOUR_DATE = value; OnPropertyChanged(); } }
    }
}
