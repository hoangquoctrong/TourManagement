using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTravelGroupViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding

        #region Information
        private string _TravelGroup_Name;
        public string TravelGroup_Name { get => _TravelGroup_Name; set { _TravelGroup_Name = value; OnPropertyChanged(); } }

        private string _TravelGroup_Type;
        public string TravelGroup_Type { get => _TravelGroup_Type; set { _TravelGroup_Type = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_TourList;
        public ObservableCollection<ComboBoxModel> CB_TourList { get => _CB_TourList; set { _CB_TourList = value; OnPropertyChanged(); } }

        private ComboBoxModel _CB_TourSelected;
        public ComboBoxModel CB_TourSelected { get => _CB_TourSelected; set { _CB_TourSelected = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_TourInformationList;
        public ObservableCollection<ComboBoxModel> CB_TourInformationList { get => _CB_TourInformationList; set { _CB_TourInformationList = value; OnPropertyChanged(); } }

        private ComboBoxModel _CB_TourInformationSelected;
        public ComboBoxModel CB_TourInformationSelected { get => _CB_TourInformationSelected; set { _CB_TourInformationSelected = value; OnPropertyChanged(); } }
        #endregion Travel Information

        #region Traveller
        private int _TravellerCount;
        public int TravellerCount { get => _TravellerCount; set { _TravellerCount = value; OnPropertyChanged(); } }

        private BindableCollection<TravellerModel> _TravellerList;
        public BindableCollection<TravellerModel> TravellerList { get => _TravellerList; set { _TravellerList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxTravellerModel> _CB_TravellerList;
        public BindableCollection<ComboBoxTravellerModel> CB_TravellerList { get => _CB_TravellerList; set { _CB_TravellerList = value; OnPropertyChanged(); } }
        #endregion Traveller

        #region Staff
        private int _StaffCount;
        public int StaffCount { get => _StaffCount; set { _StaffCount = value; OnPropertyChanged(); } }

        private BindableCollection<StaffDetailModel> _StaffList;
        public BindableCollection<StaffDetailModel> StaffList { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxStaffModel> _CB_StaffList;
        public BindableCollection<ComboBoxStaffModel> CB_StaffList { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxMissionModel> _CB_MissionList;
        public BindableCollection<ComboBoxMissionModel> CB_MissionList { get => _CB_MissionList; set { _CB_MissionList = value; OnPropertyChanged(); } }
        #endregion Staff

        #endregion Data Binding
        public AddTravelGroupViewModel(int user_id)
        {
            User_ID = user_id;

            TravellerList = new BindableCollection<TravellerModel>();
            TravellerCount = 0;

            StaffList = new BindableCollection<StaffDetailModel>();
            StaffCount = 0;
        }

        #region Command Information
        private void SetTourComboBox()
        {

        }
        #endregion Command Information

        #region Command Staff
        private ICommand _SearchStaffCommand;
        public ICommand SearchStaffCommand
        {
            get
            {
                if (_SearchStaffCommand == null)
                {
                    _SearchStaffCommand = null;
                }
                return _SearchStaffCommand;
            }
        }

        private ICommand _AddStaffCommand;
        public ICommand AddStaffCommand
        {
            get
            {
                if (_AddStaffCommand == null)
                {
                    _AddStaffCommand = new RelayCommand<object>(null, p => AddStaff());
                }
                return _AddStaffCommand;
            }
        }
        private void AddStaff()
        {
            StaffList.Add(new StaffDetailModel()
            {
                StaffName = "",
                MissionResponsibility = "",
                Staff_Notify = ""
            });
            StaffCount++;
        }

        private ICommand _RemoveStaffCommand;
        public ICommand RemoveStaffCommand
        {
            get
            {
                if (_RemoveStaffCommand == null)
                {
                    _RemoveStaffCommand = new RelayCommand<object>(p => TravellerCount > 0, p => RemoveStaff());
                }
                return _RemoveStaffCommand;
            }
        }

        private void RemoveStaff()
        {
            StaffList.RemoveAt(StaffCount - 1);
            StaffCount--;
        }
        #endregion Command Staff

        #region Command Traveller
        private ICommand _SearchTravellerCommand;
        public ICommand SearchTravellerCommand
        {
            get
            {
                if (_SearchTravellerCommand == null)
                {
                    _SearchTravellerCommand = null;
                }
                return _SearchTravellerCommand;
            }
        }

        private ICommand _AddTravellerCommand;
        public ICommand AddTravellerCommand
        {
            get
            {
                if (_AddTravellerCommand == null)
                {
                    _AddTravellerCommand = new RelayCommand<object>(p => TravellerCount <= 20, p => AddTraveller());
                }
                return _AddTravellerCommand;
            }
        }
        private void AddTraveller()
        {
            TravellerList.Add(new TravellerModel()
            {
                Traveller_Name = "",
                Traveller_Address = "",
                Traveller_Birth = DateTime.Now,
                Traveller_CitizenIdentity = "",
                Traveller_Notify = "",
                Traveller_PhoneNumber = "",
                Traveller_Sex = "",
                Traveller_Type = ""
            });
            TravellerCount++;
        }

        private ICommand _RemoveTravellerCommand;
        public ICommand RemoveTravellerCommand
        {
            get
            {
                if (_RemoveTravellerCommand == null)
                {
                    _RemoveTravellerCommand = new RelayCommand<object>(p => TravellerCount > 0, p => RemoveTraveller());
                }
                return _RemoveTravellerCommand;
            }
        }

        private void RemoveTraveller()
        {
            TravellerList.RemoveAt(TravellerCount - 1);
            TravellerCount--;
        }
        #endregion Command Traveller

        private ICommand _AddTravelGroupCommand;
        public ICommand AddTravelGroupCommand
        {
            get
            {
                if (_AddTravelGroupCommand == null)
                {

                }

                return _AddTravelGroupCommand;
            }
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new TravelGroupViewModel(User_ID));
                }

                return _CancelCommand;
            }
        }

    }
}
