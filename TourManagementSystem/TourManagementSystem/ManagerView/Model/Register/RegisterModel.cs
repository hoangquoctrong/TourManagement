using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model.Register
{
    public class RegisterModel : BaseViewModel
    {
        private int _Register_ID;

        public int Register_ID
        { get => _Register_ID; set { _Register_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;

        public int Tour_ID
        { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private string _Register_Name;

        public string Register_Name
        { get => _Register_Name; set { _Register_Name = value; OnPropertyChanged(); } }

        private string _Register_Address;

        public string Register_Address
        { get => _Register_Address; set { _Register_Address = value; OnPropertyChanged(); } }

        private string  _Register_PhoneNumber;

        public string Register_PhoneNumber
        { get => _Register_PhoneNumber; set { _Register_PhoneNumber = value; OnPropertyChanged(); } }

        private string _Register_Emai;

        public string Register_Email
        { get => _Register_Emai; set { _Register_Emai = value; OnPropertyChanged(); } }

        private string _Register_Detail;

        public string Register_Detail
        { get => _Register_Detail; set { _Register_Detail = value; OnPropertyChanged(); } }
    }
}
