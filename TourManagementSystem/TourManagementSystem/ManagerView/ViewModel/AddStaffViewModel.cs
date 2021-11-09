using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ManagerView.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddStaffViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding
        private string _Staff_Name;
        public string Staff_Name { get => _Staff_Name; set { _Staff_Name = value; OnPropertyChanged(); } }

        private string _Staff_Role;
        public string Staff_Role { get => _Staff_Role; set { _Staff_Role = value; OnPropertyChanged(); } }

        private string _Staff_ID_Card;
        public string Staff_ID_Card { get => _Staff_ID_Card; set { _Staff_ID_Card = value; OnPropertyChanged(); } }

        private string _Staff_ID_Card_Place;
        public string Staff_ID_Card_Place { get => _Staff_ID_Card_Place; set { _Staff_ID_Card_Place = value; OnPropertyChanged(); } }

        private DateTime _Staff_ID_Card_Date = DateTime.Now;
        public DateTime Staff_ID_Card_Date { get => _Staff_ID_Card_Date; set { _Staff_ID_Card_Date = value; OnPropertyChanged(); } }

        private string _Staff_String_ID_Card_Date;
        public string Staff_String_ID_Card_Date { get => _Staff_String_ID_Card_Date; set { _Staff_String_ID_Card_Date = value; OnPropertyChanged(); } }

        private DateTime _Staff_Birthday = DateTime.Now;
        public DateTime Staff_Birthday { get => _Staff_Birthday; set { _Staff_Birthday = value; OnPropertyChanged(); } }

        private string _Staff_String_Birthday;
        public string Staff_String_Birthday { get => _Staff_String_Birthday; set { _Staff_String_Birthday = value; OnPropertyChanged(); } }

        private string _Staff_Birth_Place;
        public string Staff_Birth_Place { get => _Staff_Birth_Place; set { _Staff_Birth_Place = value; OnPropertyChanged(); } }

        private string _Staff_Address;
        public string Staff_Address { get => _Staff_Address; set { _Staff_Address = value; OnPropertyChanged(); } }

        private string _Staff_Gender;
        public string Staff_Gender { get => _Staff_Gender; set { _Staff_Gender = value; OnPropertyChanged(); } }

        private string _Staff_Academic_Level;
        public string Staff_Academic_Level { get => _Staff_Academic_Level; set { _Staff_Academic_Level = value; OnPropertyChanged(); } }

        private string _Staff_Email;
        public string Staff_Email { get => _Staff_Email; set { _Staff_Email = value; OnPropertyChanged(); } }

        private string _Staff_Phone_Number;
        public string Staff_Phone_Number { get => _Staff_Phone_Number; set { _Staff_Phone_Number = value; OnPropertyChanged(); } }

        private string _Staff_Username;
        public string Staff_Username { get => _Staff_Username; set { _Staff_Username = value; OnPropertyChanged(); } }

        private string _Staff_Password;
        public string Staff_Password { get => _Staff_Password; set { _Staff_Password = value; OnPropertyChanged(); } }

        private string _Staff_Note;
        public string Staff_Note { get => _Staff_Note; set { _Staff_Note = value; OnPropertyChanged(); } }

        private DateTime _Staff_Start_Date = DateTime.Now;
        public DateTime Staff_Start_Date { get => _Staff_Start_Date; set { _Staff_Start_Date = value; OnPropertyChanged(); } }

        private byte[] _Staff_Image_Byte_Source;
        public byte[] Staff_Image_Byte_Source { get => _Staff_Image_Byte_Source; set { _Staff_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Staff_Image_Source;
        public BitmapImage Staff_Image_Source { get => _Staff_Image_Source; set { _Staff_Image_Source = value; OnPropertyChanged(); } }
        #endregion Data Binding
        public AddStaffViewModel(int user_id)
        {
            User_ID = user_id;
        }

        private ICommand _AddStaffCommand;

        public ICommand AddStaffCommand
        {
            get
            {
                if (_AddStaffCommand == null)
                {
                    _AddStaffCommand = new RelayCommand<ContentControl>(_ => IsExcuteAddStaffCommand(), p =>
                    {
                        ExcuteAddStaffCommand(p);
                    });
                }
                return _AddStaffCommand;
            }
        }

        private bool IsExcuteAddStaffCommand()
        {
            if (string.IsNullOrEmpty(Staff_Name) ||
                string.IsNullOrEmpty(Staff_Role) ||
                string.IsNullOrEmpty(Staff_ID_Card) ||
                string.IsNullOrEmpty(Staff_ID_Card_Place) ||
                string.IsNullOrEmpty(Staff_Birth_Place) ||
                string.IsNullOrEmpty(Staff_Address) ||
                string.IsNullOrEmpty(Staff_Gender) ||
                string.IsNullOrEmpty(Staff_Academic_Level) ||
                string.IsNullOrEmpty(Staff_Email) ||
                string.IsNullOrEmpty(Staff_Phone_Number) ||
                string.IsNullOrEmpty(Staff_Username) ||
                string.IsNullOrEmpty(Staff_Password))
            {
                return false;
            }

            return StaffHandleModel.CheckAccountStaff(Staff_Username);
        }

        private void ExcuteAddStaffCommand(ContentControl p)
        {
            StaffModel staff = InsertStaffModel();
            if (StaffHandleModel.InsertStaff(staff, User_ID))
            {
                MessageBox.Show("Add successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new StaffViewModel(User_ID);
            }
            else
            {
                MessageBox.Show("Add failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private StaffModel InsertStaffModel()
        {
            return new StaffModel()
            {
                STAFF_NAME = Staff_Name,
                STAFF_ROLE = Staff_Role,
                STAFF_CITIZEN_CARD = Staff_ID_Card,
                STAFF_CITIZEN_CARD_DATE = Staff_ID_Card_Date,
                STAFF_CITIZEN_CARD_PLACE = Staff_ID_Card_Place,
                STAFF_BIRTH_DATE = Staff_Birthday,
                STAFF_BIRTH_PLACE = Staff_Birth_Place,
                STAFF_GENDER = Staff_Gender,
                STAFF_ACADEMIC_LEVEL = Staff_Academic_Level,
                STAFF_ADDRESS = Staff_Address,
                STAFF_EMAIL = Staff_Email,
                STAFF_PHONE_NUMBER = Staff_Phone_Number,
                STAFF_USERNAME = Staff_Username,
                STAFF_PASSWORD = Staff_Password,
                STAFF_NOTE = Staff_Note
            };
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new StaffViewModel(User_ID));
                }

                return _CancelCommand;
            }
        }

        private ICommand _AddImageCommand;
        public ICommand AddImageCommand
        {
            get
            {
                if (_AddImageCommand == null)
                {
                    _AddImageCommand = new RelayCommand<object>(p => true, p => ExcuteAddImageCommand());
                }

                return _AddImageCommand;
            }
        }

        private void ExcuteAddImageCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

            if (ofd.ShowDialog() == true)
            {
                string string_File_Name = ofd.FileName;
                BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                Staff_Image_Byte_Source = File.ReadAllBytes(string_File_Name);
                Staff_Image_Source = bitmap_Image;
            }
        }
    }
}
