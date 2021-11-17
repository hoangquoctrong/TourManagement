using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddHotelViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding
        private string _Hotel_Name;
        public string Hotel_Name { get => _Hotel_Name; set { _Hotel_Name = value; OnPropertyChanged(); } }

        private string _Hotel_Address;
        public string Hotel_Address { get => _Hotel_Address; set { _Hotel_Address = value; OnPropertyChanged(); } }

        private string _Hotel_Phone_Number;
        public string Hotel_Phone_Number { get => _Hotel_Phone_Number; set { _Hotel_Phone_Number = value; OnPropertyChanged(); } }

        private string _Hotel_Type;
        public string Hotel_Type { get => _Hotel_Type; set { _Hotel_Type = value; OnPropertyChanged(); } }

        private string _Hotel_Description;
        public string Hotel_Description { get => _Hotel_Description; set { _Hotel_Description = value; OnPropertyChanged(); } }

        private string _Hotel_Note_Remove;
        public string Hotel_Note_Remove { get => _Hotel_Note_Remove; set { _Hotel_Note_Remove = value; OnPropertyChanged(); } }

        private string _Hotel_Email;
        public string Hotel_Email { get => _Hotel_Email; set { _Hotel_Email = value; OnPropertyChanged(); } }

        private float _Hotel_Price;
        public float Hotel_Price { get => _Hotel_Price; set { _Hotel_Price = value; OnPropertyChanged(); } }

        private string _Hotel_Is_Restaurant;
        public string Hotel_Is_Restaurant { get => _Hotel_Is_Restaurant; set { _Hotel_Is_Restaurant = value; OnPropertyChanged(); } }

        private byte[] _Hotel_Image_Byte_Source;
        public byte[] Hotel_Image_Byte_Source { get => _Hotel_Image_Byte_Source; set { _Hotel_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Hotel_Image_Source;
        public BitmapImage Hotel_Image_Source { get => _Hotel_Image_Source; set { _Hotel_Image_Source = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_PlaceList;
        public ObservableCollection<ComboBoxModel> CB_PlaceList { get => _CB_PlaceList; set { _CB_PlaceList = value; OnPropertyChanged(); } }

        private ComboBoxModel _CB_PlaceSelected;
        public ComboBoxModel CB_PlaceSelected { get => _CB_PlaceSelected; set { _CB_PlaceSelected = value; OnPropertyChanged(); } }
        #endregion
        public AddHotelViewModel(int user_id)
        {
            User_ID = user_id;
            LoadPlaceCombobox();
        }

        private void LoadPlaceCombobox()
        {
            CB_PlaceList = new ObservableCollection<ComboBoxModel>();
            foreach (var item in PlaceHandleModel.GetPlace())
            {
                ComboBoxModel cbm = new ComboBoxModel(item.PLACE_NAME, item.PLACE_ID, false);

                CB_PlaceList.Add(cbm);
            }

            CB_PlaceSelected = CB_PlaceList.FirstOrDefault();
        }

        private ICommand _AddHotelCommand;
        public ICommand AddHotelCommand
        {
            get
            {
                if (_AddHotelCommand == null)
                {
                    _AddHotelCommand = new RelayCommand<ContentControl>(p => IsExcuteAddHotelCommand(), p => ExcuteAddHotelCommand(p));
                }
                return _AddHotelCommand;
            }
        }

        private void ExcuteAddHotelCommand(ContentControl p)
        {
            HotelModel hotel = InsertHotelModel();
            if (HotelHandleModel.InsertHotel(hotel, User_ID))
            {
                MessageBox.Show("Add successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new HotelViewModel(User_ID);
            }
            else
            {
                MessageBox.Show("Add failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteAddHotelCommand()
        {
            return !string.IsNullOrEmpty(Hotel_Name) &&
                    !string.IsNullOrEmpty(Hotel_Phone_Number) &&
                    !string.IsNullOrEmpty(Hotel_Address) &&
                    !string.IsNullOrEmpty(Hotel_Type) &&
                    !string.IsNullOrEmpty(Hotel_Email) &&
                    !string.IsNullOrEmpty(Hotel_Is_Restaurant) &&
                    CB_PlaceSelected != null;
        }

        private HotelModel InsertHotelModel()
        {
            return new HotelModel()
            {
                HOTEL_ADDRESS = Hotel_Address,
                HOTEL_DESCRIPTION = Hotel_Description,
                HOTEL_EMAIL = Hotel_Email,
                HOTEL_IMAGE_BYTE_SOURCE = Hotel_Image_Byte_Source,
                HOTEL_IS_DELETE = false,
                HOTEL_IS_RESTAURANT = Hotel_Is_Restaurant,
                HOTEL_NAME = Hotel_Name,
                HOTEL_PHONE_NUMBER = Hotel_Phone_Number,
                HOTEL_PRICE = Hotel_Price,
                HOTEL_TYPE = Hotel_Type,
                PLACE_ID = CB_PlaceSelected.CB_ID,
                PLACE_NAME = CB_PlaceSelected.CB_Name
            };
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new HotelViewModel(User_ID));
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
                Hotel_Image_Byte_Source = File.ReadAllBytes(string_File_Name);
                Hotel_Image_Source = bitmap_Image;
            }
        }
    }
}
