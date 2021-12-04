using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TransportModel : BaseViewModel
    {
        private int _TRANSPORT_ID;
        public int TRANSPORT_ID { get => _TRANSPORT_ID; set { _TRANSPORT_ID = value; OnPropertyChanged(); } }

        private string _TRANSPORT_NAME;
        public string TRANSPORT_NAME { get => _TRANSPORT_NAME; set { _TRANSPORT_NAME = value; OnPropertyChanged(); } }

        private int _TRANSPORT_AMOUNT_MAX;
        public int TRANSPORT_AMOUNT_MAX { get => _TRANSPORT_AMOUNT_MAX; set { _TRANSPORT_AMOUNT_MAX = value; OnPropertyChanged(); } }

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

        private double _TRANSPORT_PRICE;
        public double TRANSPORT_PRICE { get => _TRANSPORT_PRICE; set { _TRANSPORT_PRICE = value; OnPropertyChanged(); } }

        private string _TRANSPORT_TYPETRANS;
        public string TRANSPORT_TYPETRANS { get => _TRANSPORT_TYPETRANS; set { _TRANSPORT_TYPETRANS = value; OnPropertyChanged(); } }

        private bool _TRANSPORT_IS_DELETE;
        public bool TRANSPORT_IS_DELETE { get => _TRANSPORT_IS_DELETE; set { _TRANSPORT_IS_DELETE = value; OnPropertyChanged(); } }

        private int _TRANSPORT_AMOUNT;
        public int TRANSPORT_AMOUNT { get => _TRANSPORT_AMOUNT; set { _TRANSPORT_AMOUNT = value; OnPropertyChanged(); } }

        private bool _TRANSPORT_IS_AMOUNT;
        public bool TRANSPORT_IS_AMOUNT { get => _TRANSPORT_IS_AMOUNT; set { _TRANSPORT_IS_AMOUNT = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_TransportAmount;
        public ObservableCollection<ComboBoxModel> CB_TransportAmount { get => _CB_TransportAmount; set { _CB_TransportAmount = value; OnPropertyChanged(); } }
    }
}
