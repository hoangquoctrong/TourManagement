using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;
using StaffModel = TourManagementSystem.ManagerView.Model.StaffModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        private ObservableCollection<StaffModel> _StaffItems;
        public ObservableCollection<StaffModel> StaffItems { get => _StaffItems; set { _StaffItems = value; OnPropertyChanged("staffItems"); } }

        private ObservableCollection<StaffModel> _Refresh_StaffItems;
        public ObservableCollection<StaffModel> Refresh_StaffItems { get => _Refresh_StaffItems; set { _Refresh_StaffItems = value; OnPropertyChanged("staffItems"); } }

        private StaffModel _StaffSelected;
        public StaffModel StaffSelected { get => _StaffSelected; set { _StaffSelected = value; OnPropertyChanged("StaffSelected"); } }

        public StaffViewModel(int user_id)
        {
            User_ID = user_id;
            ProgressBarVisbility = Visibility.Hidden;
            LoadStaffComboBox();
            Checkbox_DisplayAllStaff = false;
        }

        private ObservableCollection<ComboBoxModel> _CB_StaffList;
        public ObservableCollection<ComboBoxModel> CB_StaffList { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged("CB_StaffList"); } }

        private ComboBoxModel _CB_StaffSelected;
        public ComboBoxModel CB_StaffSelected { get => _CB_StaffSelected; set { _CB_StaffSelected = value; OnPropertyChanged("CB_StaffSelected"); } }

        private void LoadStaffComboBox()
        {
            CB_StaffList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Role", false),
                new ComboBoxModel("Gender", false),
                new ComboBoxModel("Citizen Identity", false),
                new ComboBoxModel("Address", false),
                new ComboBoxModel("Phone Number", false)
            };
            CB_StaffSelected = CB_StaffList.FirstOrDefault(x => x.IsSelected);
        }

        private bool _Checkbox_DisplayAllStaff;
        public bool Checkbox_DisplayAllStaff
        {
            get => _Checkbox_DisplayAllStaff;
            set
            {
                _Checkbox_DisplayAllStaff = value;
                OnPropertyChanged("Checkbox_DisplayAllStaff");
                ProgressBarVisbility = Visibility.Visible;
                CheckboxDisplayStaff_Handle(Checkbox_DisplayAllStaff);
            }
        }

        private async void CheckboxDisplayStaff_Handle(bool checkboxDisplay)
        {

            if (StaffItems == null)
            {
                await Task.Delay(3000);
                FilterText = "";
                StaffItems = new ObservableCollection<StaffModel>();
                Refresh_StaffItems = new ObservableCollection<StaffModel>();
                StaffItems = StaffHandleModel.GetStaffList(checkboxDisplay);
                Refresh_StaffItems = StaffHandleModel.GetStaffList(checkboxDisplay);
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                await Task.Delay(2000);
                FilterText = "";
                StaffItems = StaffHandleModel.GetStaffList(checkboxDisplay);
                Refresh_StaffItems = StaffHandleModel.GetStaffList(checkboxDisplay);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        //Text Search Filter
        private string _FilterText;
        public string FilterText
        {
            get => _FilterText;
            set
            {
                _FilterText = value;
                OnPropertyChanged("FilterText");
                StaffItems_Filter();
            }
        }

        private void StaffItems_Filter()
        {
            StaffItems = Refresh_StaffItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_StaffSelected.CB_Name)
                {
                    case "Name":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_NAME.Contains(FilterText) ||
                                                                                                            x.STAFF_NAME.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_NAME.ToUpper().Contains(FilterText)));
                        break;
                    case "Role":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_ROLE.Contains(FilterText) ||
                                                                                                            x.STAFF_ROLE.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_ROLE.ToUpper().Contains(FilterText)));
                        break;
                    case "Gender":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_GENDER.Contains(FilterText) ||
                                                                                                            x.STAFF_GENDER.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_GENDER.ToUpper().Contains(FilterText)));
                        break;
                    case "Citizen Identity":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_CITIZEN_CARD.Contains(FilterText) ||
                                                                                                            x.STAFF_CITIZEN_CARD.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_CITIZEN_CARD.ToUpper().Contains(FilterText)));
                        break;
                    case "Address":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_ADDRESS.Contains(FilterText) ||
                                                                                                            x.STAFF_ADDRESS.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_ADDRESS.ToUpper().Contains(FilterText)));
                        break;
                    case "Phone Number":
                        StaffItems = new ObservableCollection<StaffModel>(StaffItems.Where(x => x.STAFF_PHONE_NUMBER.Contains(FilterText) ||
                                                                                                            x.STAFF_PHONE_NUMBER.ToLower().Contains(FilterText) ||
                                                                                                            x.STAFF_PHONE_NUMBER.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailStaffCommand;
        public ICommand ShowDetailStaffCommand
        {
            get
            {
                if (_ShowDetailStaffCommand == null)
                {
                    _ShowDetailStaffCommand = new RelayCommand<ContentControl>(_ => StaffSelected != null, p => p.Content = new ShowStaffViewModel(User_ID, StaffSelected));
                }
                return _ShowDetailStaffCommand;
            }
        }

        private ICommand _AddStaffCommand;
        public ICommand AddStaffCommand
        {
            get
            {
                if (_AddStaffCommand == null)
                {
                    _AddStaffCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddStaffViewModel(User_ID));
                }
                return _AddStaffCommand;
            }
        }

        private ICommand _StatisticStaffCommand;
        public ICommand StatisticStaffCommand
        {
            get
            {
                if (_StatisticStaffCommand == null)
                {
                    _StatisticStaffCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new StatisticStaffViewModel(User_ID));
                }
                return _StatisticStaffCommand;
            }
        }
    }
}
