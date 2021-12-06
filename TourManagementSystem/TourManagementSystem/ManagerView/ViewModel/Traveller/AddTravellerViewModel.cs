using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTravellerViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        public AddTravellerViewModel(int user_id)
        {
            User_ID = user_id;
        }

        #region Data Binding
        private string _Traveller_Name;
        public string Traveller_Name { get => _Traveller_Name; set { _Traveller_Name = value; OnPropertyChanged(); } }

        private string _Traveller_Type;
        public string Traveller_Type { get => _Traveller_Type; set { _Traveller_Type = value; OnPropertyChanged(); } }

        private string _Traveller_Address;
        public string Traveller_Address { get => _Traveller_Address; set { _Traveller_Address = value; OnPropertyChanged(); } }

        private string _Traveller_PhoneNumber;
        public string Traveller_PhoneNumber { get => _Traveller_PhoneNumber; set { _Traveller_PhoneNumber = value; OnPropertyChanged(); } }

        private string _Traveller_CitizenIdentity;
        public string Traveller_CitizenIdentity { get => _Traveller_CitizenIdentity; set { _Traveller_CitizenIdentity = value; OnPropertyChanged(); } }

        private string _Traveller_Sex;
        public string Traveller_Sex { get => _Traveller_Sex; set { _Traveller_Sex = value; OnPropertyChanged(); } }

        private DateTime _Traveller_Birth = DateTime.Now;
        public DateTime Traveller_Birth { get => _Traveller_Birth; set { _Traveller_Birth = value; OnPropertyChanged(); } }
        #endregion Data Binding

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TravellerViewModel(User_ID));
                }

                return _CancelCommand;
            }
        }

        private ICommand _AddTravellerCommand;
        public ICommand AddTravellerCommand
        {
            get
            {
                if (_AddTravellerCommand == null)
                {
                    _AddTravellerCommand = new RelayCommand<ContentControl>(p => IsExcuteAddTravellerCommand(), p => ExcuteAddTravellerCommand(p));
                }
                return _AddTravellerCommand;
            }
        }

        private void ExcuteAddTravellerCommand(ContentControl p)
        {
            TravellerModel traveller = InsertTravellerModel();
            if (TravelGroupHandleModel.InsertTraveller(traveller, User_ID, true))
            {
                MessageBox.Show("Add successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new TravellerViewModel(User_ID);
            }
            else
            {
                MessageBox.Show("Add failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteAddTravellerCommand()
        {
            return !string.IsNullOrEmpty(Traveller_Name) &&
                    !string.IsNullOrEmpty(Traveller_Type) &&
                    !string.IsNullOrEmpty(Traveller_Sex) &&
                    !string.IsNullOrEmpty(Traveller_Address) &&
                    !string.IsNullOrEmpty(Traveller_CitizenIdentity) &&
                    !string.IsNullOrEmpty(Traveller_PhoneNumber) &&
                    Traveller_Birth < DateTime.Now;
        }

        private TravellerModel InsertTravellerModel()
        {
            return new TravellerModel()
            {
                Traveller_Name = Traveller_Name,
                Traveller_Type = Traveller_Type,
                Traveller_Sex = Traveller_Sex,
                Traveller_Address = Traveller_Address,
                Traveller_CitizenIdentity = Traveller_CitizenIdentity,
                Traveller_PhoneNumber = Traveller_PhoneNumber,
                Traveller_Birth = Traveller_Birth,
                Traveller_BirthString = Traveller_Birth.ToString("dd/MM/yyyy")
            };
        }
    }
}
