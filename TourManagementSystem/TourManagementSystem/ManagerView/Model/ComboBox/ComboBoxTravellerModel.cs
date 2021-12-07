using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class ComboBoxTravellerModel : ComboBoxModel
    {
        private TravellerModel _TravellerItem;
        public TravellerModel TravellerItem { get => _TravellerItem; set { _TravellerItem = value; OnPropertyChanged(); } }
        public ComboBoxTravellerModel(string cb_name, bool is_selected, TravellerModel traveller) : base(cb_name, is_selected)
        {
            this.TravellerItem = traveller;
        }

        public ComboBoxTravellerModel(string cb_name, int id, bool is_selected, TravellerModel traveller) : base(cb_name, id, is_selected)
        {
            this.TravellerItem = traveller;
        }
    }
}
