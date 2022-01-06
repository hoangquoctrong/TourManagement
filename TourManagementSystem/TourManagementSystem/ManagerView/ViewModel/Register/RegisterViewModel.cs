using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model.Register;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    internal class RegisterViewModel : BaseViewModel
    {
        private int _User_ID;

        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private Visibility _IsVisibility;

        public Visibility IsVisibility
        { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private Visibility _WaitingVisbility;

        public Visibility WaitingVisbility
        { get => _WaitingVisbility; set { _WaitingVisbility = value; OnPropertyChanged("WaitingVisbility"); } }

        private ObservableCollection<RegisterModel> _RegisterItems;

        public ObservableCollection<RegisterModel> RegisterItems
        { get => _RegisterItems; set { _RegisterItems = value; OnPropertyChanged("RegisterItems"); } }

        private ObservableCollection<RegisterModel> _Refresh_RegisterItems;

        public ObservableCollection<RegisterModel> Refresh_RegisterItems
        { get => _Refresh_RegisterItems; set { _Refresh_RegisterItems = value; OnPropertyChanged("Refresh_RegisterItems"); } }

        private RegisterModel _RegisterSelected;

        public RegisterModel RegisterSelected
        { get => _RegisterSelected; set { _RegisterSelected = value; OnPropertyChanged("RegisterSelected"); } }

        private Visibility _ProgressBarVisbility;

        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        public RegisterViewModel(int user_id, Visibility visibility)
        {
            User_ID = user_id;
            IsVisibility = visibility;
            WaitingVisbility = Visibility.Collapsed;
            LoadRegisterComboBox();
            ProgressBarVisbility = Visibility.Visible;
            LoadRegisterData();
        }

        private async void LoadRegisterData()
        {
            await Task.Delay(3000);
            RegisterItems = RegisterHandleModel.GetRegisterList();
            Refresh_RegisterItems = RegisterHandleModel.GetRegisterList();
            ProgressBarVisbility = Visibility.Hidden;
            if (RegisterItems.Count == 0)
            {
                WaitingVisbility = Visibility.Visible;
            }
            else
            {
                WaitingVisbility = Visibility.Collapsed | Visibility.Visible;
            }
        }

        private ObservableCollection<ComboBoxModel> _CB_RegisterList;

        public ObservableCollection<ComboBoxModel> CB_RegisterList
        { get => _CB_RegisterList; set { _CB_RegisterList = value; OnPropertyChanged("CB_RegisterList"); } }

        private ComboBoxModel _CB_RegisterSelected;

        public ComboBoxModel CB_RegisterSelected
        { get => _CB_RegisterSelected; set { _CB_RegisterSelected = value; OnPropertyChanged("CB_RegisterSelected"); } }

        private void LoadRegisterComboBox()
        {
            CB_RegisterList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Email", false)
            };
            CB_RegisterSelected = CB_RegisterList.FirstOrDefault(x => x.IsSelected);
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
                RegisterItems_Filter();
            }
        }

        private void RegisterItems_Filter()
        {
            RegisterItems = Refresh_RegisterItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (_CB_RegisterSelected.CB_Name)
                {
                    case "Name":
                        RegisterItems = new ObservableCollection<RegisterModel>(RegisterItems.Where(x => x.Register_Name.Contains(FilterText) ||
                                                                                                            x.Register_Name.ToLower().Contains(FilterText) ||
                                                                                                            x.Register_Name.ToUpper().Contains(FilterText)));
                        break;

                    case "Email":
                        RegisterItems = new ObservableCollection<RegisterModel>(RegisterItems.Where(x => x.Register_Email.Contains(FilterText) ||
                                                                                                            x.Register_Email.ToLower().Contains(FilterText) ||
                                                                                                            x.Register_Email.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _DeleteRegisterCommand;

        public ICommand DeleteRegisterCommand
        {
            get
            {
                if (_DeleteRegisterCommand == null)
                {
                    _DeleteRegisterCommand = new RelayCommand<object>(p => RegisterSelected != null, p => deleteRegister());
                }
                return _DeleteRegisterCommand;
            }
        }

        public void deleteRegister()
        {
            bool? Result = new MessageWindow("Do you want to delete this register?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result == true)
            {
                if (RegisterHandleModel.DeleteRegister(RegisterSelected, User_ID))
                {
                    string message = string.Format("Delete This Register Successfully!");
                    MessageWindow messageWindow = new MessageWindow(message, MessageType.Success, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    RegisterSelected = null;
                }
                else
                {
                    string message = string.Format("Delete This Register Failed!");
                    MessageWindow messageWindow = new MessageWindow(message, MessageType.Error, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                }
                ProgressBarVisbility = Visibility.Visible;
                LoadRegisterData();
            }
        }
    }
}