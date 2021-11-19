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
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTransportViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding
        private string _Transport_Name;
        public string Transport_Name { get => _Transport_Name; set { _Transport_Name = value; OnPropertyChanged(); } }

        private string _Transport_License_Plate;
        public string Transport_License_Plate { get => _Transport_License_Plate; set { _Transport_License_Plate = value; OnPropertyChanged(); } }

        private string _Transport_Company;
        public string Transport_Company { get => _Transport_Company; set { _Transport_Company = value; OnPropertyChanged(); } }

        private string _Transport_Type;
        public string Transport_Type { get => _Transport_Type; set { _Transport_Type = value; OnPropertyChanged(); } }

        private string _Transport_Description;
        public string Transport_Description { get => _Transport_Description; set { _Transport_Description = value; OnPropertyChanged(); } }

        private bool _Transport_TypeTrans_Choose;
        public bool Transport_TypeTrans_Choose { get => _Transport_TypeTrans_Choose; set { _Transport_TypeTrans_Choose = value; OnPropertyChanged(); } }

        private string _Transport_TypeTrans;
        public string Transport_TypeTrans
        {
            get
            {
                if (_Transport_TypeTrans == null)
                {
                    _Transport_TypeTrans = "";
                }
                return _Transport_TypeTrans;
            }
            set
            {
                _Transport_TypeTrans = value;
                OnPropertyChanged();
                if (Transport_TypeTrans.Contains("Road"))
                {
                    Transport_TypeTrans_Choose = true;
                }
                else
                {
                    Transport_TypeTrans_Choose = false;
                }
            }
        }

        private double _Transport_Price;
        public double Transport_Price { get => _Transport_Price; set { _Transport_Price = value; OnPropertyChanged(); } }

        private byte[] _Transport_Image_Byte_Source;
        public byte[] Transport_Image_Byte_Source { get => _Transport_Image_Byte_Source; set { _Transport_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Transport_Image_Source;
        public BitmapImage Transport_Image_Source { get => _Transport_Image_Source; set { _Transport_Image_Source = value; OnPropertyChanged(); } }
        #endregion Data Binding
        public AddTransportViewModel(int user_id)
        {
            User_ID = user_id;
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
                Transport_Image_Byte_Source = File.ReadAllBytes(string_File_Name);
                Transport_Image_Source = bitmap_Image;
            }
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TransportViewModel(User_ID));
                }

                return _CancelCommand;
            }
        }

        private ICommand _AddTransportCommand;
        public ICommand AddTransportCommand
        {
            get
            {
                if (_AddTransportCommand == null)
                {
                    _AddTransportCommand = new RelayCommand<ContentControl>(p => IsExcuteAddTransportCommand(), p => ExcuteAddTransportCommand(p));
                }
                return _AddTransportCommand;
            }
        }

        private void ExcuteAddTransportCommand(ContentControl p)
        {
            TransportModel transport = InsertTransportModel();
            if (TransportHandleModel.InsertTransport(transport, User_ID))
            {
                MessageBox.Show("Add successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Content = new TransportViewModel(User_ID);
            }
            else
            {
                MessageBox.Show("Add failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteAddTransportCommand()
        {
            if (Transport_TypeTrans.Contains("Road"))
            {
                return !string.IsNullOrEmpty(Transport_Name) &&
                    !string.IsNullOrEmpty(Transport_Company) &&
                    !string.IsNullOrEmpty(Transport_License_Plate) &&
                    !string.IsNullOrEmpty(Transport_Type) &&
                    Transport_Price > 0;
            }
            else if (Transport_TypeTrans.Contains("Water") || Transport_TypeTrans.Contains("Air"))
            {
                return !string.IsNullOrEmpty(Transport_Name) &&
                    !string.IsNullOrEmpty(Transport_Company) &&
                    !string.IsNullOrEmpty(Transport_Type) &&
                    Transport_Price > 0;
            }
            return false;
        }

        private TransportModel InsertTransportModel()
        {
            return new TransportModel()
            {
                TRANSPORT_NAME = Transport_Name,
                TRANSPORT_TYPE = Transport_Type,
                TRANSPORT_TYPETRANS = Transport_TypeTrans,
                TRANSPORT_LICENSE_PLATE = Transport_TypeTrans_Choose ? Transport_License_Plate : "",
                TRANSPORT_PRICE = Transport_Price,
                TRANSPORT_COMPANY = Transport_Company,
                TRANSPORT_DATE = DateTime.Now,
                TRANSPORT_DESCRIPTION = string.IsNullOrEmpty(Transport_Description) ? "" : Transport_Description,
                TRANSPORT_STRING_DATE = DateTime.Now.ToString("dd/MM/yyyy"),
                TRANSPORT_IS_DELETE = false,
                TRANSPORT_IMAGE_BYTE_SOURCE = Transport_Image_Byte_Source
            };
        }
    }
}
