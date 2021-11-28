using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowTourInformationViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;
        public int Tour_ID { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }


        public ShowTourInformationViewModel(int user_id, int tour_id, TourInformationModel tourInformation, ObservableCollection<PlaceModel> places)
        {

        }
    }
}
