using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddPlaceViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding

        private string _Place_Name;
        public string Place_Name
        { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        private string _Place_Nation;
        public string Place_Nation
        { get => _Place_Nation; set { _Place_Nation = value; OnPropertyChanged(); } }

        private int _Place_Location;
        public int Place_Location
        { get => _Place_Location; set { _Place_Location = value; OnPropertyChanged(); } }

        #endregion Data Binding

        public AddPlaceViewModel(int user_id)
        {
            User_ID = user_id;
        }

        private ICommand _AddPlaceCommand;

        public ICommand AddPlaceCommand
        {
            get
            {
                if (_AddPlaceCommand == null)
                {
                    _AddPlaceCommand = new RelayCommand<ContentControl>(p => IsExcuteAddPlaceCommand(), p => ExcuteAddPlaceCommand(p));
                }
                return _AddPlaceCommand;
            }
        }

        private void ExcuteAddPlaceCommand(ContentControl p)
        {
            PlaceModel place = InsertPlaceModel();
            if (PlaceHandleModel.InsertPlace(place, User_ID))
            {
                string messageDisplay = string.Format("Add Place Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
                p.Content = new PlaceViewModel(User_ID, Visibility.Visible);
            }
            else
            {
                string messageDisplay = string.Format("Add Place Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
        }

        private bool IsExcuteAddPlaceCommand()
        {
            return !string.IsNullOrEmpty(Place_Name) &&
                    !string.IsNullOrEmpty(Place_Nation);
        }

        private PlaceModel InsertPlaceModel()
        {
            return new PlaceModel()
            {
                PLACE_NATION = Place_Nation,
                PLACE_NAME = Place_Name
            };
        }

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new PlaceViewModel(User_ID, Visibility.Visible));
                }

                return _CancelCommand;
            }
        }
    }
}