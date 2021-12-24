using System;
using System.Collections.ObjectModel;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TravellerModel : BaseViewModel
    {
        private int _Traveller_ID;
        public int Traveller_ID
        { get => _Traveller_ID; set { _Traveller_ID = value; OnPropertyChanged(); } }

        private string _Traveller_Name;
        public string Traveller_Name
        { get => _Traveller_Name; set { _Traveller_Name = value; OnPropertyChanged(); Traveller_Check = true; } }

        private string _Traveller_CitizenIdentity;
        public string Traveller_CitizenIdentity
        { get => _Traveller_CitizenIdentity; set { _Traveller_CitizenIdentity = value; OnPropertyChanged(); } }

        private string _Traveller_Sex;
        public string Traveller_Sex
        { get => _Traveller_Sex; set { _Traveller_Sex = value; OnPropertyChanged("Traveller_Sex"); } }

        private string _Traveller_BirthString;
        public string Traveller_BirthString
        { get => _Traveller_BirthString; set { _Traveller_BirthString = value; OnPropertyChanged(); } }

        private DateTime _Traveller_Birth = DateTime.Now;
        public DateTime Traveller_Birth
        { get => _Traveller_Birth; set { _Traveller_Birth = value; OnPropertyChanged(); } }

        private string _Traveller_PhoneNumber;
        public string Traveller_PhoneNumber
        { get => _Traveller_PhoneNumber; set { _Traveller_PhoneNumber = value; OnPropertyChanged(); } }

        private string _Traveller_Address;
        public string Traveller_Address
        { get => _Traveller_Address; set { _Traveller_Address = value; OnPropertyChanged(); } }

        private string _Traveller_Type;
        public string Traveller_Type
        { get => _Traveller_Type; set { _Traveller_Type = value; OnPropertyChanged("Traveller_Type"); } }

        private int _Traveller_Number_Tour;
        public int Traveller_Number_Tour
        { get => _Traveller_Number_Tour; set { _Traveller_Number_Tour = value; OnPropertyChanged(); } }

        #region Parameter for binding Item Control

        private int _Traveller_Star;
        public int Traveller_Star
        { get => _Traveller_Star; set { _Traveller_Star = value; OnPropertyChanged(); Traveller_CheckCommand = true; } }

        private bool _Traveller_StarEnable;
        public bool Traveller_StarEnable
        { get => _Traveller_StarEnable; set { _Traveller_StarEnable = value; OnPropertyChanged(); } }

        private int _Traveller_Index;
        public int Traveller_Index
        { get => _Traveller_Index; set { _Traveller_Index = value; OnPropertyChanged(); } }

        private bool _Traveller_Check;
        public bool Traveller_Check
        { get => _Traveller_Check; set { _Traveller_Check = value; OnPropertyChanged(); } }

        private bool _Traveller_Enable;
        public bool Traveller_Enable
        { get => _Traveller_Enable; set { _Traveller_Enable = value; OnPropertyChanged(); } }

        private bool _Traveller_CheckCommand;
        public bool Traveller_CheckCommand
        { get => _Traveller_CheckCommand; set { _Traveller_CheckCommand = value; OnPropertyChanged(); } }

        private int _Traveller_Select;
        public int Traveller_Select
        { get => _Traveller_Select; set { _Traveller_Select = value; OnPropertyChanged(); } }

        private string _Traveller_Notify = "";
        public string Traveller_Notify
        { get => _Traveller_Notify; set { _Traveller_Notify = value; OnPropertyChanged(); } }

        private ObservableCollection<TravellerModel> _TravelSearchList;

        public ObservableCollection<TravellerModel> TravelSearchList
        {
            get
            {
                if (_TravelSearchList == null)
                {
                    _TravelSearchList = new ObservableCollection<TravellerModel>();
                }
                return _TravelSearchList;
            }

            set
            {
                _TravelSearchList = value;
                OnPropertyChanged();
            }
        }

        #endregion Parameter for binding Item Control
    }
}