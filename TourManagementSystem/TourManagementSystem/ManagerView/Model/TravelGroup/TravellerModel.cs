using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TravellerModel : BaseViewModel
    {
        private int _Traveller_ID;
        public int Traveller_ID { get => _Traveller_ID; set { _Traveller_ID = value; OnPropertyChanged(); } }

        private string _Traveller_Name;
        public string Traveller_Name { get => _Traveller_Name; set { _Traveller_Name = value; OnPropertyChanged(); } }

        private string _Traveller_CitizenIdentity;
        public string Traveller_CitizenIdentity { get => _Traveller_CitizenIdentity; set { _Traveller_CitizenIdentity = value; OnPropertyChanged(); } }

        private string _Traveller_Sex;
        public string Traveller_Sex { get => _Traveller_Sex; set { _Traveller_Sex = value; OnPropertyChanged(); } }

        private string _Traveller_BirthString;
        public string Traveller_BirthString { get => _Traveller_BirthString; set { _Traveller_BirthString = value; OnPropertyChanged(); } }

        private DateTime _Traveller_Birth = DateTime.Now;
        public DateTime Traveller_Birth { get => _Traveller_Birth; set { _Traveller_Birth = value; OnPropertyChanged(); } }

        private string _Traveller_PhoneNumber;
        public string Traveller_PhoneNumber { get => _Traveller_PhoneNumber; set { _Traveller_PhoneNumber = value; OnPropertyChanged(); } }

        private string _Traveller_Address;
        public string Traveller_Address { get => _Traveller_Address; set { _Traveller_Address = value; OnPropertyChanged(); } }

        private string _Traveller_Type;
        public string Traveller_Type { get => _Traveller_Type; set { _Traveller_Type = value; OnPropertyChanged(); } }

        private string _Traveller_Notify;
        public string Traveller_Notify { get => _Traveller_Notify; set { _Traveller_Notify = value; OnPropertyChanged(); } }
    }
}
