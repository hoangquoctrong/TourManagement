using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.Model
{
    public class HotelModel : BaseViewModel
    {
        private int _HOTEL_ID;
        public int HOTEL_ID { get => _HOTEL_ID; set { _HOTEL_ID = value; OnPropertyChanged(); } }

        private string _HOTEL_NAME;
        public string HOTEL_NAME { get => _HOTEL_NAME; set { _HOTEL_NAME = value; OnPropertyChanged(); } }

        private string _HOTEL_ADDRESS;
        public string HOTEL_ADDRESS { get => _HOTEL_ADDRESS; set { _HOTEL_ADDRESS = value; OnPropertyChanged(); } }

        private string _HOTEL_PHONE_NUMBER;
        public string HOTEL_PHONE_NUMBER { get => _HOTEL_PHONE_NUMBER; set { _HOTEL_PHONE_NUMBER = value; OnPropertyChanged(); } }

        private string _HOTEL_IS_RESTAURANT;
        public string HOTEL_IS_RESTAURANT { get => _HOTEL_IS_RESTAURANT; set { _HOTEL_IS_RESTAURANT = value; OnPropertyChanged(); } }

        private string _HOTEL_TYPE;
        public string HOTEL_TYPE { get => _HOTEL_TYPE; set { _HOTEL_TYPE = value; OnPropertyChanged(); } }

        private string _HOTEL_EMAIL;
        public string HOTEL_EMAIL { get => _HOTEL_EMAIL; set { _HOTEL_EMAIL = value; OnPropertyChanged(); } }

        private string _HOTEL_DESCRIPTION;
        public string HOTEL_DESCRIPTION { get => _HOTEL_DESCRIPTION; set { _HOTEL_DESCRIPTION = value; OnPropertyChanged(); } }

        private float _HOTEL_PRICE;
        public float HOTEL_PRICE { get => _HOTEL_PRICE; set { _HOTEL_PRICE = value; OnPropertyChanged(); } }

        private byte[] _HOTEL_IMAGE_BYTE_SOURCE;
        public byte[] HOTEL_IMAGE_BYTE_SOURCE { get => _HOTEL_IMAGE_BYTE_SOURCE; set { _HOTEL_IMAGE_BYTE_SOURCE = value; OnPropertyChanged(); } }

    }
}
