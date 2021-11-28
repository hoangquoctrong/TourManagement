using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowHotelViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private HotelModel _HotelSelected;
        public HotelModel HotelSelected { get => _HotelSelected; set { _HotelSelected = value; OnPropertyChanged(); } }

        #region Data Binding
        private int _Hotel_ID;
        public int Hotel_ID { get => _Hotel_ID; set { _Hotel_ID = value; OnPropertyChanged(); } }

        private string _Hotel_Name;
        public string Hotel_Name { get => _Hotel_Name; set { _Hotel_Name = value; OnPropertyChanged(); } }

        private string _Hotel_Address;
        public string Hotel_Address { get => _Hotel_Address; set { _Hotel_Address = value; OnPropertyChanged(); } }

        private string _Hotel_Phone_Number;
        public string Hotel_Phone_Number { get => _Hotel_Phone_Number; set { _Hotel_Phone_Number = value; OnPropertyChanged(); } }

        private bool _Hotel_Is_Delete;
        public bool Hotel_Is_Delete { get => _Hotel_Is_Delete; set { _Hotel_Is_Delete = value; OnPropertyChanged(); } }

        private string _Hotel_Type;
        public string Hotel_Type { get => _Hotel_Type; set { _Hotel_Type = value; OnPropertyChanged(); } }

        private string _Hotel_Description;
        public string Hotel_Description { get => _Hotel_Description; set { _Hotel_Description = value; OnPropertyChanged(); } }

        private string _Hotel_Email;
        public string Hotel_Email { get => _Hotel_Email; set { _Hotel_Email = value; OnPropertyChanged(); } }

        private double _Hotel_Price;
        public double Hotel_Price { get => _Hotel_Price; set { _Hotel_Price = value; OnPropertyChanged(); } }

        private int _Place_ID;
        public int Place_ID { get => _Place_ID; set { _Place_ID = value; OnPropertyChanged(); } }

        private string _Place_Name;
        public string Place_Name { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        private string _Hotel_Is_Restaurant;
        public string Hotel_Is_Restaurant { get => _Hotel_Is_Restaurant; set { _Hotel_Is_Restaurant = value; OnPropertyChanged(); } }

        private byte[] _Hotel_Image_Byte_Source;
        public byte[] Hotel_Image_Byte_Source { get => _Hotel_Image_Byte_Source; set { _Hotel_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Hotel_Image_Source;
        public BitmapImage Hotel_Image_Source { get => _Hotel_Image_Source; set { _Hotel_Image_Source = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_PlaceList;
        public ObservableCollection<ComboBoxModel> CB_PlaceList { get => _CB_PlaceList; set { _CB_PlaceList = value; OnPropertyChanged(); } }

        private ComboBoxModel _CB_PlaceSelected;
        public ComboBoxModel CB_PlaceSelected
        {
            get => _CB_PlaceSelected;
            set
            {
                _CB_PlaceSelected = value;
                Place_ID = CB_PlaceSelected.CB_ID;
                Place_Name = CB_PlaceSelected.CB_Name;
                OnPropertyChanged();
            }
        }
        #endregion

        public ShowHotelViewModel(int user_id, HotelModel hotel)
        {
            User_ID = user_id;
            HotelSelected = hotel;
            SetHotelInView(hotel);
            LoadPlaceCombobox(Place_ID);
            LoadTourHotelDetailComboBox();
            ObservableCollection<TourHotelDetailModel> tourhoteldetailItems = TourDetailHandleModel.GetTourHotelDetailList();
            TourHotelDetailItemsCollection = new CollectionViewSource { Source = tourhoteldetailItems };
            TourHotelDetailItemsCollection.Filter += TourHotelDetailItem_Filter;
        }

        private void SetHotelInView(HotelModel hotel)
        {
            Hotel_ID = hotel.HOTEL_ID;
            Hotel_Name = hotel.HOTEL_NAME;
            Hotel_Address = hotel.HOTEL_ADDRESS;
            Hotel_Phone_Number = hotel.HOTEL_PHONE_NUMBER;
            Hotel_Is_Restaurant = hotel.HOTEL_IS_RESTAURANT;
            Hotel_Type = hotel.HOTEL_TYPE;
            Hotel_Email = hotel.HOTEL_EMAIL;
            Hotel_Description = hotel.HOTEL_DESCRIPTION;
            Hotel_Price = hotel.HOTEL_PRICE;
            Hotel_Is_Delete = !hotel.HOTEL_IS_DELETE;
            Place_ID = hotel.PLACE_ID;
            Place_Name = hotel.PLACE_NAME;
            Hotel_Image_Byte_Source = hotel.HOTEL_IMAGE_BYTE_SOURCE;
            Hotel_Image_Source = Hotel_Image_Byte_Source == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Hotel.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Hotel_Image_Byte_Source);
        }

        private void LoadPlaceCombobox(int place_id)
        {
            CB_PlaceList = new ObservableCollection<ComboBoxModel>();
            foreach (var item in PlaceHandleModel.GetPlaceList())
            {
                ComboBoxModel cbm = new ComboBoxModel(item.PLACE_NAME, item.PLACE_ID, false);

                CB_PlaceList.Add(cbm);
            }

            CB_PlaceSelected = CB_PlaceList.FirstOrDefault(x => x.CB_ID == place_id);
        }

        private ICommand _ModifyImageCommand;
        public ICommand ModifyImageCommand
        {
            get
            {
                if (_ModifyImageCommand == null)
                {
                    _ModifyImageCommand = new RelayCommand<object>(p => !Hotel_Is_Delete, p => ExcuteAddImageCommand());
                }

                return _ModifyImageCommand;
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

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(p => true, p => p.Content = new HotelViewModel(User_ID));
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
            HotelSelected = InsertDataToHotelSelect();
            if (HotelHandleModel.UpdateHotel(HotelSelected, User_ID))
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
            if (Hotel_Name != HotelSelected.HOTEL_NAME)
            {
                return true;
            }
            if (Hotel_Address != HotelSelected.HOTEL_ADDRESS)
            {
                return true;
            }
            if (Hotel_Phone_Number != HotelSelected.HOTEL_PHONE_NUMBER)
            {
                return true;
            }
            if (Hotel_Type != HotelSelected.HOTEL_TYPE)
            {
                return true;
            }
            if (Hotel_Email != HotelSelected.HOTEL_EMAIL)
            {
                return true;
            }
            if (Hotel_Description != HotelSelected.HOTEL_DESCRIPTION)
            {
                return true;
            }
            if (Hotel_Is_Restaurant != HotelSelected.HOTEL_IS_RESTAURANT)
            {
                return true;
            }
            if (Hotel_Price != HotelSelected.HOTEL_PRICE)
            {
                return true;
            }
            if (Hotel_Image_Byte_Source != HotelSelected.HOTEL_IMAGE_BYTE_SOURCE)
            {
                return true;
            }
            if (CB_PlaceSelected.CB_ID != HotelSelected.PLACE_ID)
            {
                return true;
            }

            return false;
        }

        private HotelModel InsertDataToHotelSelect()
        {
            return new HotelModel()
            {
                HOTEL_ADDRESS = Hotel_Address,
                HOTEL_DESCRIPTION = Hotel_Description,
                HOTEL_EMAIL = Hotel_Email,
                HOTEL_ID = Hotel_ID,
                HOTEL_IMAGE_BYTE_SOURCE = Hotel_Image_Byte_Source,
                HOTEL_IS_RESTAURANT = Hotel_Is_Restaurant,
                HOTEL_NAME = Hotel_Name,
                HOTEL_PHONE_NUMBER = Hotel_Phone_Number,
                HOTEL_PRICE = Hotel_Price,
                PLACE_ID = Place_ID,
                PLACE_NAME = Place_Name,
                HOTEL_TYPE = Hotel_Type
            };
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand<ContentControl>(p => Hotel_Is_Delete, p => ExcuteDeleteCommand(p));
                }
                return _DeleteCommand;
            }
        }

        private void ExcuteDeleteCommand(ContentControl p)
        {
            if (HotelHandleModel.DeleteHotel(Hotel_ID, User_ID))
            {
                MessageBox.Show("Delete hotel successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new HotelViewModel(User_ID);
            }
            else
            {
                MessageBox.Show("Delete hotel failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private CollectionViewSource TourHotelDetailItemsCollection;

        public ICollectionView TourHotelDetailCollection => TourHotelDetailItemsCollection.View;


        private ObservableCollection<ComboBoxModel> _CB_TourHotelDetailList;
        public ObservableCollection<ComboBoxModel> CB_TourHotelDetailList { get => _CB_TourHotelDetailList; set { _CB_TourHotelDetailList = value; OnPropertyChanged("CB_HistoryList"); } }

        private ComboBoxModel _CB_TourHotelDetailSelected;
        public ComboBoxModel CB_TourHotelDetailSelected { get => _CB_TourHotelDetailSelected; set { _CB_TourHotelDetailSelected = value; OnPropertyChanged("CB_HistorySelected"); } }

        private void LoadTourHotelDetailComboBox()
        {
            CB_TourHotelDetailList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Tour Name", true),
                new ComboBoxModel("Travel Group Name", false),
                new ComboBoxModel("Start Date", false),
                new ComboBoxModel("End Date", false)
            };
            CB_TourHotelDetailSelected = CB_TourHotelDetailList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TourHotelDetailFilterText;
        public string TourHotelDetailFilterText
        {
            get => _TourHotelDetailFilterText;
            set
            {
                _TourHotelDetailFilterText = value;
                TourHotelDetailItemsCollection.View.Refresh();
                OnPropertyChanged("TourHotelDetailFilterText");
            }
        }

        private void TourHotelDetailItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(TourHotelDetailFilterText))
            {
                e.Accepted = true;
                return;
            }

            TourHotelDetailModel _items = e.Item as TourHotelDetailModel;
            switch (CB_TourHotelDetailSelected.CB_Name)
            {
                case "Tour Name":
                    if (_items.TOUR_NAME.IndexOf(TourHotelDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Travel Group Name":
                    if (_items.TRAVEL_GROUP_NAME.IndexOf(TourHotelDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Start Date":
                    if (_items.STRING_START_DATE.IndexOf(TourHotelDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "End Date":
                    if (_items.STRING_END_DATE.IndexOf(TourHotelDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
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
