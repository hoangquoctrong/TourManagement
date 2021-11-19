using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TransportModel : BaseViewModel
    {
        private int _TRANSPORT_ID;
        public int TRANSPORT_ID { get => _TRANSPORT_ID; set { _TRANSPORT_ID = value; OnPropertyChanged(); } }

        private string _TRANSPORT_NAME;
        public string TRANSPORT_NAME { get => _TRANSPORT_NAME; set { _TRANSPORT_NAME = value; OnPropertyChanged(); } }

        private string _TRANSPORT_LICENSE_PLATE;
        public string TRANSPORT_LICENSE_PLATE { get => _TRANSPORT_LICENSE_PLATE; set { _TRANSPORT_LICENSE_PLATE = value; OnPropertyChanged(); } }

        private string _TRANSPORT_COMPANY;
        public string TRANSPORT_COMPANY { get => _TRANSPORT_COMPANY; set { _TRANSPORT_COMPANY = value; OnPropertyChanged(); } }

        private string _TRANSPORT_TYPE;
        public string TRANSPORT_TYPE { get => _TRANSPORT_TYPE; set { _TRANSPORT_TYPE = value; OnPropertyChanged(); } }

        private string _TRANSPORT_DESCRIPTION;
        public string TRANSPORT_DESCRIPTION { get => _TRANSPORT_DESCRIPTION; set { _TRANSPORT_DESCRIPTION = value; OnPropertyChanged(); } }

        private string _TRANSPORT_STRING_DATE;
        public string TRANSPORT_STRING_DATE { get => _TRANSPORT_STRING_DATE; set { _TRANSPORT_STRING_DATE = value; OnPropertyChanged(); } }

        private DateTime _TRANSPORT_DATE = DateTime.Now;
        public DateTime TRANSPORT_DATE { get => _TRANSPORT_DATE; set { _TRANSPORT_DATE = value; OnPropertyChanged(); } }

        private byte[] _TRANSPORT_IMAGE_BYTE_SOURCE;
        public byte[] TRANSPORT_IMAGE_BYTE_SOURCE { get => _TRANSPORT_IMAGE_BYTE_SOURCE; set { _TRANSPORT_IMAGE_BYTE_SOURCE = value; OnPropertyChanged(); } }

        private double _TRANSPORT_PRICE;
        public double TRANSPORT_PRICE { get => _TRANSPORT_PRICE; set { _TRANSPORT_PRICE = value; OnPropertyChanged(); } }

        private string _TRANSPORT_TYPETRANS;
        public string TRANSPORT_TYPETRANS { get => _TRANSPORT_TYPETRANS; set { _TRANSPORT_TYPETRANS = value; OnPropertyChanged(); } }

        private bool _TRANSPORT_IS_DELETE;
        public bool TRANSPORT_IS_DELETE { get => _TRANSPORT_IS_DELETE; set { _TRANSPORT_IS_DELETE = value; OnPropertyChanged(); } }
    }
}
