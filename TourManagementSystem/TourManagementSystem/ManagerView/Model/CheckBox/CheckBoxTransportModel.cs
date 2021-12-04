using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class CheckBoxTransportModel : CheckBoxModel
    {
        private double _Transport_Price;
        public double Transport_Price { get => _Transport_Price; set { _Transport_Price = value; OnPropertyChanged(); } }

        private TransportModel _TransportSelect;
        public TransportModel TransportSelect { get => _TransportSelect; set { _TransportSelect = value; OnPropertyChanged(); } }

        public CheckBoxTransportModel(string name, double price, int id, bool is_selected, TransportModel transport)
            : base(name, id, is_selected)
        {
            this.Transport_Price = price;
            this.TransportSelect = transport;
        }
    }
}
