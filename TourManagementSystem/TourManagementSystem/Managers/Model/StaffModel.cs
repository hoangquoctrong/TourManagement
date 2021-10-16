using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.Model
{
    public class StaffModel : BaseViewModel
    {
        private string _STAFF_USERNAME;
        public string STAFF_USERNAME { get => _STAFF_USERNAME; set { _STAFF_USERNAME = value; OnPropertyChanged(); } }

        private string _STAFF_PASSWORD;
        public string STAFF_PASSWORD { get => _STAFF_PASSWORD; set { _STAFF_PASSWORD = value; OnPropertyChanged(); } }

        private int _STAFF_ID;
        public int STAFF_ID { get => _STAFF_ID; set { _STAFF_ID = value; OnPropertyChanged(); } }

        private string _STAFF_NAME;
        public string STAFF_NAME { get => _STAFF_NAME; set { _STAFF_NAME = value; OnPropertyChanged(); } }

        private string _STAFF_ROLE;
        public string STAFF_ROLE { get => _STAFF_ROLE; set { _STAFF_ROLE = value; OnPropertyChanged(); } }

        private string _STAFF_CITIZEN_CARD;
        public string STAFF_CITIZEN_CARD { get => _STAFF_CITIZEN_CARD; set { _STAFF_CITIZEN_CARD = value; OnPropertyChanged(); } }

        private DateTime _STAFF_CITIZEN_CARD_DATE = DateTime.Now;
        public DateTime STAFF_CITIZEN_CARD_DATE { get => _STAFF_CITIZEN_CARD_DATE; set { _STAFF_CITIZEN_CARD_DATE = value; OnPropertyChanged(); } }

        private string _STAFF_STRING_CITIZEN_CARD_DATE;
        public string STAFF_STRING_CITIZEN_CARD_DATE { get => _STAFF_STRING_CITIZEN_CARD_DATE; set { _STAFF_STRING_CITIZEN_CARD_DATE = value; OnPropertyChanged(); } }

        private string _STAFF_CITIZEN_CARD_PLACE;
        public string STAFF_CITIZEN_CARD_PLACE { get => _STAFF_CITIZEN_CARD_PLACE; set { _STAFF_CITIZEN_CARD_PLACE = value; OnPropertyChanged(); } }

        private DateTime _STAFF_BIRTH_DATE = DateTime.Now;
        public DateTime STAFF_BIRTH_DATE { get => _STAFF_BIRTH_DATE; set { _STAFF_BIRTH_DATE = value; OnPropertyChanged(); } }

        private string _STAFF_STRING_BIRTH_DATE;
        public string STAFF_STRING_BIRTH_DATE { get => _STAFF_STRING_BIRTH_DATE; set { _STAFF_STRING_BIRTH_DATE = value; OnPropertyChanged(); } }

        private string _STAFF_BIRTH_PLACE;
        public string STAFF_BIRTH_PLACE { get => _STAFF_BIRTH_PLACE; set { _STAFF_BIRTH_PLACE = value; OnPropertyChanged(); } }

        private string _STAFF_ADDRESS;
        public string STAFF_ADDRESS { get => _STAFF_ADDRESS; set { _STAFF_ADDRESS = value; OnPropertyChanged(); } }

        private string _STAFF_GENDER;
        public string STAFF_GENDER { get => _STAFF_GENDER; set { _STAFF_GENDER = value; OnPropertyChanged(); } }

        private string _STAFF_ACADEMIC_LEVEL;
        public string STAFF_ACADEMIC_LEVEL { get => _STAFF_ACADEMIC_LEVEL; set { _STAFF_ACADEMIC_LEVEL = value; OnPropertyChanged(); } }

        private string _STAFF_EMAIL;
        public string STAFF_EMAIL { get => _STAFF_EMAIL; set { _STAFF_EMAIL = value; OnPropertyChanged(); } }

        private string _STAFF_NOTE;
        public string STAFF_NOTE { get => _STAFF_NOTE; set { _STAFF_NOTE = value; OnPropertyChanged(); } }

        private string _STAFF_PHONE_NUMBER;
        public string STAFF_PHONE_NUMBER { get => _STAFF_PHONE_NUMBER; set { _STAFF_PHONE_NUMBER = value; OnPropertyChanged(); } }

        private DateTime _STAFF_START_DATE = DateTime.Now;
        public DateTime STAFF_START_DATE { get => _STAFF_START_DATE; set { _STAFF_START_DATE = value; OnPropertyChanged(); } }

        private string _STAFF_STRING_START_DATE;
        public string STAFF_STRING_START_DATE { get => _STAFF_STRING_START_DATE; set { _STAFF_STRING_START_DATE = value; OnPropertyChanged(); } }

        private byte[] _STAFF_IMAGE_BYTE_SOURCE;
        public byte[] STAFF_IMAGE_BYTE_SOURCE { get => _STAFF_IMAGE_BYTE_SOURCE; set { _STAFF_IMAGE_BYTE_SOURCE = value; OnPropertyChanged(); } }
    }
}
