using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class CheckBoxHotelModel : CheckBoxModel
    {
        private double _Hotel_Price;
        public double Hotel_Price { get => _Hotel_Price; set { _Hotel_Price = value; OnPropertyChanged(); } }
        private HotelModel _HotelSelect;
        public HotelModel HotelSelect { get => _HotelSelect; set { _HotelSelect = value; OnPropertyChanged(); } }

        public CheckBoxHotelModel(string name, double price, int id, bool is_selected, HotelModel hotel)
            : base(name, id, is_selected)
        {
            this.Hotel_Price = price;
            this.HotelSelect = hotel;
        }
    }
}
