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
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowLocationViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private LocationModel _LocationSelected;
        public LocationModel LocationSelected { get => _LocationSelected; set { _LocationSelected = value; OnPropertyChanged(); } }

        #region Data Binding
        private int _Place_ID;
        public int Place_ID { get => _Place_ID; set { _Place_ID = value; OnPropertyChanged(); } }

        private int _Location_ID;
        public int Location_ID { get => _Location_ID; set { _Location_ID = value; OnPropertyChanged(); } }

        private string _Location_Name;
        public string Location_Name { get => _Location_Name; set { _Location_Name = value; OnPropertyChanged(); } }

        private string _Location_Address;
        public string Location_Address { get => _Location_Address; set { _Location_Address = value; OnPropertyChanged(); } }

        private string _Location_Description;
        public string Location_Description { get => _Location_Description; set { _Location_Description = value; OnPropertyChanged(); } }

        private string _Place_Name;
        public string Place_Name { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        #endregion Data Binding

        public ShowLocationViewModel(int user_id, int place_id, LocationModel location)
        {
            User_ID = user_id;
            Place_ID = place_id;
            LocationSelected = location;
            SetLocationInView(location);
            LoadTourLocationComboBox();
            ObservableCollection<TourLocationModel> tourlocationItems = PlaceHandleModel.GetTourLocationList(location.LOCATION_ID);
            TourLocationItemsCollection = new CollectionViewSource { Source = tourlocationItems };
            TourLocationItemsCollection.Filter += TourLocationItem_Filter;
        }

        private void SetLocationInView(LocationModel location)
        {
            Place_ID = location.PLACE_ID;
            Place_Name = PlaceHandleModel.GetPlaceName(Place_ID);
            Location_ID = location.LOCATION_ID;
            Location_Name = location.LOCATION_NAME;
            Location_Address = location.LOCATION_ADDRESS;
            Location_Description = location.LOCATION_DESCRIPTION;
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new ShowPlaceViewModel(User_ID, Place_ID));
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
            LocationModel location = InsertDataToLocationSelected();
            if (PlaceHandleModel.UpdateLocation(location, User_ID))
            {
                MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveChangeCommand()
        {
            if (Location_Name != LocationSelected.LOCATION_NAME)
            {
                return true;
            }

            if (Location_Address != LocationSelected.LOCATION_ADDRESS)
            {
                return true;
            }

            if (Location_Description != LocationSelected.LOCATION_DESCRIPTION)
            {
                return true;
            }

            return false;
        }

        private LocationModel InsertDataToLocationSelected()
        {
            return new LocationModel()
            {
                PLACE_ID = Place_ID,
                LOCATION_ID = Location_ID,
                LOCATION_NAME = Location_Name,
                LOCATION_ADDRESS = Location_Address,
                LOCATION_DESCRIPTION = Location_Description
            };
        }

        private CollectionViewSource TourLocationItemsCollection;

        public ICollectionView TourLocationCollection => TourLocationItemsCollection.View;

        private ObservableCollection<ComboBoxModel> _CB_TourLocationList;
        public ObservableCollection<ComboBoxModel> CB_TourLocationList { get => _CB_TourLocationList; set { _CB_TourLocationList = value; OnPropertyChanged("CB_TourLocationList"); } }

        private ComboBoxModel _CB_TourLocationSelected;
        public ComboBoxModel CB_TourLocationSelected { get => _CB_TourLocationSelected; set { _CB_TourLocationSelected = value; OnPropertyChanged("CB_TourLocationSelected"); } }

        private void LoadTourLocationComboBox()
        {
            CB_TourLocationList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Time", false)
            };
            CB_TourLocationSelected = CB_TourLocationList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TourLocationFilterText;
        public string TourLocationFilterText
        {
            get => _TourLocationFilterText;
            set
            {
                _TourLocationFilterText = value;
                TourLocationItemsCollection.View.Refresh();
                OnPropertyChanged("TourLocationFilterText");
            }
        }

        private void TourLocationItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(TourLocationFilterText))
            {
                e.Accepted = true;
                return;
            }

            TourLocationModel _items = e.Item as TourLocationModel;
            switch (CB_TourLocationSelected.CB_Name)
            {
                case "Name":
                    if (_items.TOUR_NAME.IndexOf(TourLocationFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Time":
                    if (_items.TOUR_STRING_DATE.IndexOf(TourLocationFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
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
