using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Global.Model
{
    public class ComboBoxModel : BaseViewModel
    {
        /*
         * IsSelected is bool value to define what object is displayed in combobox
         */
        private bool _IsSelected;
        public bool IsSelected
        { get => _IsSelected; set { _IsSelected = value; OnPropertyChanged(); } }

        /*
         * Datagrid Combobox
         */
        private string _CB_Name;
        public string CB_Name
        { get => _CB_Name; set { _CB_Name = value; OnPropertyChanged(); } }

        /*
         * ID Combobox
         */
        private int _CB_ID;
        public int CB_ID
        { get => _CB_ID; set { _CB_ID = value; OnPropertyChanged(); } }

        public ComboBoxModel(string cb_name, bool is_selected)
        {
            this.IsSelected = is_selected;
            this.CB_Name = cb_name;
        }

        public ComboBoxModel(string cb_name, int id, bool is_selected)
        {
            this.IsSelected = is_selected;
            this.CB_ID = id;
            this.CB_Name = cb_name;
        }
    }
}