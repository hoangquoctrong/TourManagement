using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.Model
{
    public class LocationModel : BaseViewModel
    {
        private int _LOCATION_ID;
        public int LOCATION_ID { get => _LOCATION_ID; set { _LOCATION_ID = value; OnPropertyChanged(); } }

        private string _LOCATION_NAME;
        public string LOCATION_NAME { get => _LOCATION_NAME; set { _LOCATION_NAME = value; OnPropertyChanged(); } }

        private string _LOCATION_ADDRESS;
        public string LOCATION_ADDRESS { get => _LOCATION_ADDRESS; set { _LOCATION_ADDRESS = value; OnPropertyChanged(); } }

        private string _LOCATION_DESCRIPTION;
        public string LOCATION_DESCRIPTION { get => _LOCATION_DESCRIPTION; set { _LOCATION_DESCRIPTION = value; OnPropertyChanged(); } }

        private int _PLACE_ID;
        public int PLACE_ID { get => _PLACE_ID; set { _PLACE_ID = value; OnPropertyChanged(); } }
    }
}
