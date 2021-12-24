using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class ComboBoxInformationModel : ComboBoxModel
    {
        private TourInformationModel _InformationItem;

        public ComboBoxInformationModel(string cb_name, bool is_selected, TourInformationModel information) : base(cb_name, is_selected)
        {
            this.InformationItem = information;
        }

        public ComboBoxInformationModel(string cb_name, int id, bool is_selected, TourInformationModel information) : base(cb_name, id, is_selected)
        {
            this.InformationItem = information;
        }

        public TourInformationModel InformationItem
        { get => _InformationItem; set { _InformationItem = value; OnPropertyChanged(); } }
    }
}