using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class ComboBoxTourModel : ComboBoxModel
    {
        public ComboBoxTourModel(string cb_name, bool is_selected, TourModel tour) : base(cb_name, is_selected)
        {
            this.TourItem = tour;
        }

        public ComboBoxTourModel(string cb_name, int id, bool is_selected, TourModel tour) : base(cb_name, id, is_selected)
        {
            this.TourItem = tour;
        }

        private TourModel _TourItem;

        public TourModel TourItem
        { get => _TourItem; set { _TourItem = value; OnPropertyChanged(); } }
    }
}