using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class PlaceModel : BaseViewModel
    {
        private int _PLACE_ID;
        public int PLACE_ID { get => _PLACE_ID; set { _PLACE_ID = value; OnPropertyChanged(); } }

        private string _PLACE_NAME;
        public string PLACE_NAME { get => _PLACE_NAME; set { _PLACE_NAME = value; OnPropertyChanged(); } }

        private string _PLACE_NATION;
        public string PLACE_NATION { get => _PLACE_NATION; set { _PLACE_NATION = value; OnPropertyChanged(); } }

        private int _PLACE_LOCATION;
        public int PLACE_LOCATION { get => _PLACE_LOCATION; set { _PLACE_LOCATION = value; OnPropertyChanged(); } }
    }
}
