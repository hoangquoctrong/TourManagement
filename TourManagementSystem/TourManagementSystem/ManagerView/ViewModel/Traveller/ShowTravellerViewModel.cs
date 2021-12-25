using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowTravellerViewModel : BaseViewModel
    {
        private int _User_ID;

        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _IsVisibility;

        public Visibility IsVisibility
        { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private bool _IsEnable;

        public bool IsEnable
        { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        private TravellerModel _TravellerSelected;

        public TravellerModel TravellerSelected
        { get => _TravellerSelected; set { _TravellerSelected = value; OnPropertyChanged(); } }

        public ShowTravellerViewModel(int user_id, TravellerModel traveller, Visibility visibility)
        {
            User_ID = user_id;
            IsVisibility = visibility;
            TravellerSelected = traveller;
            SetTraveller(traveller);

            LoadTravellerDetailComboBox();
            ObservableCollection<TravelGroupModel> travellerdetailItems = TravelGroupHandleModel.GetTravelGroupListWithTravellerID(traveller.Traveller_ID);
            TravellerDetailItemsCollection = new CollectionViewSource { Source = travellerdetailItems };
            TravellerDetailItemsCollection.Filter += TravellerDetailItem_Filter;
        }

        #region Data Binding

        private int _Traveller_ID;

        public int Traveller_ID
        { get => _Traveller_ID; set { _Traveller_ID = value; OnPropertyChanged(); } }

        private string _Traveller_Name;

        public string Traveller_Name
        { get => _Traveller_Name; set { _Traveller_Name = value; OnPropertyChanged(); } }

        private string _Traveller_Type;

        public string Traveller_Type
        { get => _Traveller_Type; set { _Traveller_Type = value; OnPropertyChanged(); } }

        private string _Traveller_Address;

        public string Traveller_Address
        { get => _Traveller_Address; set { _Traveller_Address = value; OnPropertyChanged(); } }

        private string _Traveller_PhoneNumber;

        public string Traveller_PhoneNumber
        { get => _Traveller_PhoneNumber; set { _Traveller_PhoneNumber = value; OnPropertyChanged(); } }

        private string _Traveller_CitizenIdentity;

        public string Traveller_CitizenIdentity
        { get => _Traveller_CitizenIdentity; set { _Traveller_CitizenIdentity = value; OnPropertyChanged(); } }

        private string _Traveller_Sex;

        public string Traveller_Sex
        { get => _Traveller_Sex; set { _Traveller_Sex = value; OnPropertyChanged(); } }

        private DateTime _Traveller_Birth = DateTime.Now;

        public DateTime Traveller_Birth
        { get => _Traveller_Birth; set { _Traveller_Birth = value; OnPropertyChanged(); } }

        private string _Traveller_BirthString;

        public string Traveller_BirthString
        { get => _Traveller_BirthString; set { _Traveller_BirthString = value; OnPropertyChanged(); } }

        private int _NumberOfTour;

        public int NumberOfTour
        { get => _NumberOfTour; set { _NumberOfTour = value; OnPropertyChanged(); } }

        #endregion Data Binding

        private async void SetTraveller(TravellerModel traveller)
        {
            await Task.Delay(1000);
            Traveller_ID = traveller.Traveller_ID;
            Traveller_Name = traveller.Traveller_Name;
            Traveller_Address = traveller.Traveller_Address;
            Traveller_Type = traveller.Traveller_Type;
            Traveller_PhoneNumber = traveller.Traveller_PhoneNumber;
            Traveller_Sex = traveller.Traveller_Sex;
            Traveller_CitizenIdentity = traveller.Traveller_CitizenIdentity;
            Traveller_Birth = traveller.Traveller_Birth;
            Traveller_BirthString = Traveller_Birth.ToString("dd/MM/yyyy");
            NumberOfTour = traveller.Traveller_Number_Tour;

            IsEnable = IsVisibility == Visibility.Visible;
        }

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TravellerViewModel(User_ID, IsVisibility));
                }

                return _CancelCommand;
            }
        }

        private ICommand _SaveTravellerCommand;

        public ICommand SaveTravellerCommand
        {
            get
            {
                if (_SaveTravellerCommand == null)
                {
                    _SaveTravellerCommand = new RelayCommand<object>(p => IsExcuteTravellerCommand(), p => ExcuteTravellerCommand());
                }
                return _SaveTravellerCommand;
            }
        }

        private void ExcuteTravellerCommand()
        {
            TravellerSelected = UpdateTraveller();
            if (TravelGroupHandleModel.UpdateTraveller(TravellerSelected, User_ID, true))
            {
                string messageDisplay = string.Format("Update Traveller Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
            else
            {
                string messageDisplay = string.Format("Update Traveller Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
        }

        private bool IsExcuteTravellerCommand()
        {
            if (string.IsNullOrEmpty(Traveller_Name))
            {
                return false;
            }

            if (Traveller_Name != TravellerSelected.Traveller_Name)
            {
                return true;
            }

            if (Traveller_Type != TravellerSelected.Traveller_Type)
            {
                return true;
            }

            if (Traveller_Sex != TravellerSelected.Traveller_Sex)
            {
                return true;
            }

            if (Traveller_PhoneNumber != TravellerSelected.Traveller_PhoneNumber)
            {
                return true;
            }

            if (Traveller_Address != TravellerSelected.Traveller_Address)
            {
                return true;
            }

            if (Traveller_CitizenIdentity != TravellerSelected.Traveller_CitizenIdentity)
            {
                return true;
            }

            if (Traveller_Birth != TravellerSelected.Traveller_Birth)
            {
                return true;
            }

            return false;
        }

        private TravellerModel UpdateTraveller()
        {
            return new TravellerModel()
            {
                Traveller_ID = TravellerSelected.Traveller_ID,
                Traveller_Name = Traveller_Name,
                Traveller_Type = Traveller_Type,
                Traveller_Address = Traveller_Address,
                Traveller_PhoneNumber = Traveller_PhoneNumber,
                Traveller_CitizenIdentity = Traveller_CitizenIdentity,
                Traveller_Sex = Traveller_Sex,
                Traveller_Birth = Traveller_Birth,
                Traveller_BirthString = Traveller_Birth.ToString("dd/MM/yyyy")
            };
        }

        private CollectionViewSource TravellerDetailItemsCollection;

        public ICollectionView TravellerDetailCollection => TravellerDetailItemsCollection.View;

        private ObservableCollection<ComboBoxModel> _CB_TravellerDetailList;

        public ObservableCollection<ComboBoxModel> CB_TravellerDetailList
        { get => _CB_TravellerDetailList; set { _CB_TravellerDetailList = value; OnPropertyChanged("CB_HistoryList"); } }

        private ComboBoxModel _CB_TravellerDetailSelected;

        public ComboBoxModel CB_TravellerDetailSelected
        { get => _CB_TravellerDetailSelected; set { _CB_TravellerDetailSelected = value; OnPropertyChanged("CB_HistorySelected"); } }

        private void LoadTravellerDetailComboBox()
        {
            CB_TravellerDetailList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Tour Name", true),
                new ComboBoxModel("Travel Group Name", false),
                new ComboBoxModel("Start Date", false),
                new ComboBoxModel("End Date", false)
            };
            CB_TravellerDetailSelected = CB_TravellerDetailList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TravellerDetailFilterText;

        public string TravellerDetailFilterText
        {
            get => _TravellerDetailFilterText;
            set
            {
                _TravellerDetailFilterText = value;
                TravellerDetailItemsCollection.View.Refresh();
                OnPropertyChanged("TravellerDetailFilterText");
            }
        }

        private void TravellerDetailItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(TravellerDetailFilterText))
            {
                e.Accepted = true;
                return;
            }

            TravelGroupModel _items = e.Item as TravelGroupModel;
            switch (CB_TravellerDetailSelected.CB_Name)
            {
                case "Tour Name":
                    if (_items.Tour_Name.IndexOf(TravellerDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "Travel Group Name":
                    if (_items.TravelGroup_Name.IndexOf(TravellerDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "Start Date":
                    if (_items.Tour_StartString.IndexOf(TravellerDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "End Date":
                    if (_items.Tour_EndString.IndexOf(TravellerDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
            }
        }
    }
}