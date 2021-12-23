using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    public class ShowPlaceViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private PlaceModel _PlaceSelected;
        public PlaceModel PlaceSelected { get => _PlaceSelected; set { _PlaceSelected = value; OnPropertyChanged(); } }

        private Visibility _IsVisibility;
        public Visibility IsVisibility { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        #region Data Binding
        private int _Place_ID;
        public int Place_ID { get => _Place_ID; set { _Place_ID = value; OnPropertyChanged(); } }

        private string _Place_Name;
        public string Place_Name { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        private string _Place_Nation;
        public string Place_Nation { get => _Place_Nation; set { _Place_Nation = value; OnPropertyChanged(); } }

        private int _Place_Location;
        public int Place_Location { get => _Place_Location; set { _Place_Location = value; OnPropertyChanged(); } }
        #endregion Data Binding

        public ShowPlaceViewModel(int user_id, int place_id, Visibility visibility)
        {
            IsVisibility = visibility;
            User_ID = user_id;
            PlaceSelected = PlaceHandleModel.GetPlace(place_id);
            SetPlaceInView(PlaceSelected);
            LoadLocationComboBox();
            LocationItems = PlaceHandleModel.GetLocationList(place_id);
            Refresh_LocationItems = PlaceHandleModel.GetLocationList(place_id);
        }

        private void SetPlaceInView(PlaceModel place)
        {
            Place_ID = place.PLACE_ID;
            Place_Name = place.PLACE_NAME;
            Place_Location = place.PLACE_LOCATION;
            Place_Nation = place.PLACE_NATION;

            IsEnable = IsVisibility == Visibility.Visible;
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(p => true, p => p.Content = new PlaceViewModel(User_ID, IsVisibility));
                }
                return _CancelCommand;
            }
        }

        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand
        {
            get
            {
                if (_SaveChangeCommand == null)
                {
                    _SaveChangeCommand = new RelayCommand<object>(p => IsExcuteSaveChangeCommand(), p => ExcuteSaveChangeCommand());
                }
                return _SaveChangeCommand;
            }
        }

        private void ExcuteSaveChangeCommand()
        {
            PlaceModel place = InsertDataToPlaceSelected();
            if (PlaceHandleModel.UpdatePlace(place, User_ID))
            {
                string messageDisplay = string.Format("Update Place Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
            else
            {
                string messageDisplay = string.Format("Update Place Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();                
            }
        }

        private bool IsExcuteSaveChangeCommand()
        {
            if (Place_Name != PlaceSelected.PLACE_NAME)
            {
                return true;
            }

            if (Place_Nation != PlaceSelected.PLACE_NATION)
            {
                return true;
            }

            return false;
        }

        private PlaceModel InsertDataToPlaceSelected()
        {
            return new PlaceModel()
            {
                PLACE_ID = Place_ID,
                PLACE_NAME = Place_Name,
                PLACE_NATION = Place_Nation,
                PLACE_LOCATION = Place_Location
            };
        }

        private ObservableCollection<LocationModel> _LocationItems;
        public ObservableCollection<LocationModel> LocationItems { get => _LocationItems; set { _LocationItems = value; OnPropertyChanged("LocationItems"); } }

        private ObservableCollection<LocationModel> _Refresh_LocationItems;
        public ObservableCollection<LocationModel> Refresh_LocationItems { get => _Refresh_LocationItems; set { _Refresh_LocationItems = value; OnPropertyChanged("Refresh_LocationItems"); } }

        private LocationModel _LocationSelected;
        public LocationModel LocationSelected { get => _LocationSelected; set { _LocationSelected = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_LocationList;
        public ObservableCollection<ComboBoxModel> CB_LocationList { get => _CB_LocationList; set { _CB_LocationList = value; OnPropertyChanged("CB_LocationList"); } }

        private ComboBoxModel _CB_LocationSelected;
        public ComboBoxModel CB_LocationSelected { get => _CB_LocationSelected; set { _CB_LocationSelected = value; OnPropertyChanged("CB_LocationSelected"); } }

        private void LoadLocationComboBox()
        {
            CB_LocationList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Address", false)
            };
            CB_LocationSelected = CB_LocationList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _LocationFilterText;
        public string LocationFilterText
        {
            get => _LocationFilterText;
            set
            {
                _LocationFilterText = value;
                OnPropertyChanged("LocationFilterText");
                LocationItem_Filter();
            }
        }

        private void LocationItem_Filter()
        {
            LocationItems = Refresh_LocationItems;
            if (!string.IsNullOrEmpty(LocationFilterText))
            {
                switch (CB_LocationSelected.CB_Name)
                {
                    case "Name":
                        LocationItems = new ObservableCollection<LocationModel>(LocationItems.Where(x => x.LOCATION_NAME.Contains(LocationFilterText) ||
                                                                                                            x.LOCATION_NAME.ToLower().Contains(LocationFilterText) ||
                                                                                                            x.LOCATION_NAME.ToUpper().Contains(LocationFilterText)));
                        break;
                    case "Address":
                        LocationItems = new ObservableCollection<LocationModel>(LocationItems.Where(x => x.LOCATION_ADDRESS.Contains(LocationFilterText) ||
                                                                                                            x.LOCATION_ADDRESS.ToLower().Contains(LocationFilterText) ||
                                                                                                            x.LOCATION_ADDRESS.ToUpper().Contains(LocationFilterText)));
                        break;
                }
            }

        }

        private ICommand _AddLocationCommand;
        public ICommand AddLocationCommand
        {
            get
            {
                if (_AddLocationCommand == null)
                {
                    _AddLocationCommand = new RelayCommand<ContentControl>(null, p => p.Content = new AddLocationViewModel(User_ID, Place_ID));
                }
                return _AddLocationCommand;
            }
        }

        private ICommand _ShowDetailLocationCommand;
        public ICommand ShowDetailLocationCommand
        {
            get
            {
                if (_ShowDetailLocationCommand == null)
                {
                    _ShowDetailLocationCommand = new RelayCommand<ContentControl>(null, p => p.Content = new ShowLocationViewModel(User_ID, Place_ID, LocationSelected, IsVisibility));
                }
                return _ShowDetailLocationCommand;
            }
        }
    }
}
