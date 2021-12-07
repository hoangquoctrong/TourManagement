using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class StaffDetailModel : BaseViewModel
    {
        private int _StaffDetailID;
        public int StaffDetailID { get => _StaffDetailID; set { _StaffDetailID = value; OnPropertyChanged(); } }

        private int _Staff_ID;
        public int Staff_ID { get => _Staff_ID; set { _Staff_ID = value; OnPropertyChanged(); } }

        private int _MissionID;
        public int MissionID { get => _MissionID; set { _MissionID = value; OnPropertyChanged(); } }

        private int _TravelGroupID;
        public int TravelGroupID { get => _TravelGroupID; set { _TravelGroupID = value; OnPropertyChanged(); } }

        private string _StaffName;
        public string StaffName { get => _StaffName; set { _StaffName = value; OnPropertyChanged(); Staff_Check = true; Staff_Notify = ""; } }

        private string _MissionResponsibility;
        public string MissionResponsibility { get => _MissionResponsibility; set { _MissionResponsibility = value; OnPropertyChanged(); } }

        private string _Staff_Notify;
        public string Staff_Notify { get => _Staff_Notify; set { _Staff_Notify = value; OnPropertyChanged(); } }

        private bool _Staff_Check = true;
        public bool Staff_Check { get => _Staff_Check; set { _Staff_Check = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxStaffModel> _CB_StaffList;
        public BindableCollection<ComboBoxStaffModel> CB_StaffList { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged(); } }
    }
}
