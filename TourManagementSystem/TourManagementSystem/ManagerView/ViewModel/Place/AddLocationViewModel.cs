using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddLocationViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding
        private int _Place_ID;
        public int Place_ID { get => _Place_ID; set { _Place_ID = value; OnPropertyChanged(); } }

        private string _Location_Name;
        public string Location_Name { get => _Location_Name; set { _Location_Name = value; OnPropertyChanged(); } }

        private string _Location_Address;
        public string Location_Address { get => _Location_Address; set { _Location_Address = value; OnPropertyChanged(); } }

        private string _Location_Description;
        public string Location_Description { get => _Location_Description; set { _Location_Description = value; OnPropertyChanged(); } }

        private string _Place_Name;
        public string Place_Name { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        #endregion Data Binding
        public AddLocationViewModel(int user_id, int place_id)
        {
            User_ID = user_id;
            Place_ID = place_id;
            Place_Name = PlaceHandleModel.GetPlaceName(Place_ID);
        }

        private ICommand _AddLocationCommand;
        public ICommand AddLocationCommand
        {
            get
            {
                if (_AddLocationCommand == null)
                {
                    _AddLocationCommand = new RelayCommand<ContentControl>(p => IsExcuteAddCommand(), p => ExcuteAddCommand(p));
                }
                return _AddLocationCommand;
            }
        }

        private void ExcuteAddCommand(ContentControl p)
        {
            LocationModel location = InsertLocationModel();
            if (PlaceHandleModel.InsertLocation(location, User_ID))
            {
                MessageWindow messageWindow = new MessageWindow("Add Location successfully!", MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
                //MessageBox.Show("Add Location successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new ShowPlaceViewModel(User_ID, Place_ID, Visibility.Visible);
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Add Location failed! Please try again!", MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                //MessageBox.Show("Add Location failed! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteAddCommand()
        {
            return !string.IsNullOrEmpty(Location_Name) &&
                    !string.IsNullOrEmpty(Location_Address);
        }

        private LocationModel InsertLocationModel()
        {
            return new LocationModel()
            {
                LOCATION_NAME = Location_Name,
                LOCATION_ADDRESS = Location_Address,
                LOCATION_DESCRIPTION = string.IsNullOrEmpty(Location_Description) ? "" : Location_Description,
                PLACE_ID = Place_ID
            };
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new ShowPlaceViewModel(User_ID, Place_ID, Visibility.Visible));
                }
                return _CancelCommand;
            }
        }

    }
}
