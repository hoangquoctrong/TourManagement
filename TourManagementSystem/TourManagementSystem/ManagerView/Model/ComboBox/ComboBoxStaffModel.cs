using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class ComboBoxStaffModel : ComboBoxModel
    {
        private StaffModel _StaffItem;

        public StaffModel StaffItem
        { get => _StaffItem; set { _StaffItem = value; OnPropertyChanged(); } }

        public ComboBoxStaffModel(string cb_name, bool is_selected, StaffModel staff) : base(cb_name, is_selected)
        {
            this.StaffItem = staff;
        }

        public ComboBoxStaffModel(string cb_name, int id, bool is_selected, StaffModel staff) : base(cb_name, id, is_selected)
        {
            this.StaffItem = staff;
        }
    }
}