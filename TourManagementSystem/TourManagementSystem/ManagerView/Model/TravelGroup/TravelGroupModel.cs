using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TravelGroupModel : BaseViewModel
    {
        private int _TravelGroup_ID;
        public int TravelGroup_ID { get => _TravelGroup_ID; set { _TravelGroup_ID = value; OnPropertyChanged(); } }

        private int _TourInformation_ID;
        public int TourInformation_ID { get => _TourInformation_ID; set { _TourInformation_ID = value; OnPropertyChanged(); } }

        private string _TravelGroup_Name;
        public string TravelGroup_Name { get => _TravelGroup_Name; set { _TravelGroup_Name = value; OnPropertyChanged(); } }

        private string _TravelGroup_Type;
        public string TravelGroup_Type { get => _TravelGroup_Type; set { _TravelGroup_Type = value; OnPropertyChanged(); } }

        private string _Tour_Name;
        public string Tour_Name { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private string _Tour_StartString;
        public string Tour_StartString { get => _Tour_StartString; set { _Tour_StartString = value; OnPropertyChanged(); } }

        private DateTime _Tour_Start = DateTime.Now;
        public DateTime Tour_Start { get => _Tour_Start; set { _Tour_Start = value; OnPropertyChanged(); } }

        private string _Tour_EndString;
        public string Tour_EndString { get => _Tour_EndString; set { _Tour_EndString = value; OnPropertyChanged(); } }

        private DateTime _Tour_End = DateTime.Now;
        public DateTime Tour_End { get => _Tour_End; set { _Tour_End = value; OnPropertyChanged(); } }

        private bool _IsDelete;
        public bool IsDelete { get => _IsDelete; set { _IsDelete = value; OnPropertyChanged(); } }

        private string _Group_Status;
        public string Group_Status { get => _Group_Status; set { _Group_Status = value; OnPropertyChanged(); } }

        private double _TourInformation_Price;
        public double TourInformation_Price { get => _TourInformation_Price; set { _TourInformation_Price = value; OnPropertyChanged(); } }
    }
}
