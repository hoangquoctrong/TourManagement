using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTourViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        #region Data Binding 
        private string _Tour_Name;
        public string Tour_Name { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private string _Tour_Type;
        public string Tour_Type { get => _Tour_Type; set { _Tour_Type = value; OnPropertyChanged(); } }

        private string _Tour_Description;
        public string Tour_Description { get => _Tour_Description; set { _Tour_Description = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_1;
        public byte[] Tour_Image_Byte_Source_1 { get => _Tour_Image_Byte_Source_1; set { _Tour_Image_Byte_Source_1 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_1;
        public BitmapImage Tour_Image_Source_1 { get => _Tour_Image_Source_1; set { _Tour_Image_Source_1 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_2;
        public byte[] Tour_Image_Byte_Source_2 { get => _Tour_Image_Byte_Source_2; set { _Tour_Image_Byte_Source_2 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_2;
        public BitmapImage Tour_Image_Source_2 { get => _Tour_Image_Source_2; set { _Tour_Image_Source_2 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_3;
        public byte[] Tour_Image_Byte_Source_3 { get => _Tour_Image_Byte_Source_3; set { _Tour_Image_Byte_Source_3 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_3;
        public BitmapImage Tour_Image_Source_3 { get => _Tour_Image_Source_3; set { _Tour_Image_Source_3 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_4;
        public byte[] Tour_Image_Byte_Source_4 { get => _Tour_Image_Byte_Source_4; set { _Tour_Image_Byte_Source_4 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_4;
        public BitmapImage Tour_Image_Source_4 { get => _Tour_Image_Source_4; set { _Tour_Image_Source_4 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_5;
        public byte[] Tour_Image_Byte_Source_5 { get => _Tour_Image_Byte_Source_5; set { _Tour_Image_Byte_Source_5 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_5;
        public BitmapImage Tour_Image_Source_5 { get => _Tour_Image_Source_5; set { _Tour_Image_Source_5 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_6;
        public byte[] Tour_Image_Byte_Source_6 { get => _Tour_Image_Byte_Source_6; set { _Tour_Image_Byte_Source_6 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_6;
        public BitmapImage Tour_Image_Source_6 { get => _Tour_Image_Source_6; set { _Tour_Image_Source_6 = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxModel> _PlaceList;
        public BindableCollection<CheckBoxModel> PlaceList { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        public BindableCollection<PlaceModel> PlaceSelectedList { get; set; }

        private BindableCollection<CheckBoxModel> _RefreshPlaceList;
        public BindableCollection<CheckBoxModel> RefreshPlaceList { get => _RefreshPlaceList; set { _RefreshPlaceList = value; OnPropertyChanged(); } }
        #endregion Data Binding


        public AddTourViewModel(int user_id)
        {
            User_ID = user_id;
            ProgressBarVisbility = Visibility.Hidden;
            SetTourImageInView();
            SetPlaceData();
            PlaceSelectedList = new BindableCollection<PlaceModel>();
        }

        private async void SetPlaceData()
        {
            PlaceList = await GetPlaceList();
            RefreshPlaceList = PlaceList;
        }

        private void SetTourImageInView()
        {
            Tour_Image_Source_1 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

            Tour_Image_Source_2 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

            Tour_Image_Source_3 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

            Tour_Image_Source_4 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

            Tour_Image_Source_5 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

            Tour_Image_Source_6 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));
        }

        private async Task<BindableCollection<CheckBoxModel>> GetPlaceList()
        {
            await Task.Delay(2000);
            ObservableCollection<PlaceModel> PlaceItems = PlaceHandleModel.GetPlaceList();
            BindableCollection<CheckBoxModel> placeList = new BindableCollection<CheckBoxModel>();
            foreach (PlaceModel item in PlaceItems)
            {
                placeList.Add(new CheckBoxModel(item.PLACE_NAME, item.PLACE_NATION, item.PLACE_ID, false));
            }

            return placeList;
        }

        private ICommand _PlaceItemCheckCommand;

        public ICommand PlaceItemCheckCommand
        {
            get
            {
                if (_PlaceItemCheckCommand == null)
                {
                    _PlaceItemCheckCommand = new RelayCommand<int>(null, p =>
                    {
                        CheckBoxModel checkBoxModel = PlaceList.Where(x => x.CB_ID == p).FirstOrDefault();
                        if (checkBoxModel.IsSelected)
                        {
                            PlaceSelectedList.Add(new PlaceModel()
                            {
                                PLACE_ID = checkBoxModel.CB_ID,
                                PLACE_NAME = checkBoxModel.CB_Name,
                                PLACE_NATION = checkBoxModel.CB_Sub_Name
                            });
                        }
                        else
                        {
                            PlaceModel placeModel = PlaceSelectedList.Where(x => x.PLACE_ID == checkBoxModel.CB_ID).FirstOrDefault();
                            int index = PlaceSelectedList.IndexOf(placeModel);
                            PlaceSelectedList.RemoveAt(index);
                        }

                        RefreshPlaceList = PlaceList;
                    });
                }
                return _PlaceItemCheckCommand;
            }
        }

        private string _FilterPlaceText;
        public string FilterPlaceText
        {
            get => _FilterPlaceText;
            set
            {
                _FilterPlaceText = value;
                OnPropertyChanged("FilterPlaceText");
                PlaceItems_Filter();
            }
        }

        private void PlaceItems_Filter()
        {
            PlaceList = RefreshPlaceList;
            if (!string.IsNullOrEmpty(FilterPlaceText))
            {
                PlaceList = new BindableCollection<CheckBoxModel>(PlaceList.Where(x => x.CB_Name.Contains(FilterPlaceText) ||
                                                            x.CB_Name.ToLower().Contains(FilterPlaceText) ||
                                                            x.CB_Name.ToUpper().Contains(FilterPlaceText)));
            }
        }

        private ICommand _AddTourCommand;

        public ICommand AddTourCommand
        {
            get
            {
                if (_AddTourCommand == null)
                {
                    _AddTourCommand = new RelayCommand<ContentControl>(_ => IsExcuteAddTourCommand(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteAddTourCommand(p);
                    });
                }
                return _AddTourCommand;
            }
        }

        private async void ExcuteAddTourCommand(ContentControl p)
        {
            await Task.Delay(5000);
            TourModel tour = InsertTourModel();
            if (TourHandleModel.InsertTour(tour, User_ID))
            {
                if (TourHandleModel.InsertImageTour(InsertImageModel(), Tour_Name))
                {
                    if (PlaceHandleModel.InsertPlaceDetail(PlaceSelectedList, Tour_Name, User_ID, true))
                    {
                        MessageWindow messageWindow = new MessageWindow("Add Tour Successfully!", MessageType.Success, MessageButtons.Ok);
                        messageWindow.ShowDialog();
                        //MessageBox.Show("Add Tour Successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        p.Content = new TourViewModel(User_ID, Visibility.Visible, Visibility.Visible);
                    }
                    else
                    {
                        MessageWindow messageWindow = new MessageWindow("Disconected to Server! Please try again!", MessageType.Error, MessageButtons.Ok);
                        messageWindow.ShowDialog();
                        //MessageBox.Show("Disconect to Server! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        ProgressBarVisbility = Visibility.Hidden;
                    }

                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Disconected to Server! Please try again!", MessageType.Error, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    //MessageBox.Show("Disconect to Server! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProgressBarVisbility = Visibility.Hidden;
                }

            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Disconected to Server! Please try again!", MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                //MessageBox.Show("Disconect to Server! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private bool IsExcuteAddTourCommand()
        {
            return !string.IsNullOrEmpty(Tour_Name) &&
                !string.IsNullOrEmpty(Tour_Type) &&
                PlaceSelectedList.Count > 0;
        }

        private TourModel InsertTourModel()
        {
            return new TourModel()
            {
                TOUR_NAME = Tour_Name,
                TOUR_TYPE = Tour_Type,
                TOUR_CHARACTERISTIS = string.IsNullOrEmpty(Tour_Description) ? "" : Tour_Description
            };
        }

        private bool IsImageNull(byte[] image_byte)
        {
            return image_byte != null;
        }

        private ObservableCollection<TourImageModel> InsertImageModel()
        {
            ObservableCollection<TourImageModel> images = new ObservableCollection<TourImageModel>();
            if (IsImageNull(Tour_Image_Byte_Source_1))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_1 });
            }
            if (IsImageNull(Tour_Image_Byte_Source_2))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_2 });
            }
            if (IsImageNull(Tour_Image_Byte_Source_3))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_3 });
            }
            if (IsImageNull(Tour_Image_Byte_Source_4))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_4 });
            }
            if (IsImageNull(Tour_Image_Byte_Source_5))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_5 });
            }
            if (IsImageNull(Tour_Image_Byte_Source_6))
            {
                images.Add(new TourImageModel() { TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_6 });
            }
            return images;
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TourViewModel(User_ID, Visibility.Visible, Visibility.Visible));
                }

                return _CancelCommand;
            }
        }

        private ICommand _AddImageCommand_1;
        public ICommand AddImageCommand_1
        {
            get
            {
                if (_AddImageCommand_1 == null)
                {
                    _AddImageCommand_1 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_1 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_1 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_1;
            }
        }

        private ICommand _AddImageCommand_2;
        public ICommand AddImageCommand_2
        {
            get
            {
                if (_AddImageCommand_2 == null)
                {
                    _AddImageCommand_2 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_2 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_2 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_2;
            }
        }

        private ICommand _AddImageCommand_3;
        public ICommand AddImageCommand_3
        {
            get
            {
                if (_AddImageCommand_3 == null)
                {
                    _AddImageCommand_3 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_3 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_3 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_3;
            }
        }

        private ICommand _AddImageCommand_4;
        public ICommand AddImageCommand_4
        {
            get
            {
                if (_AddImageCommand_4 == null)
                {
                    _AddImageCommand_4 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_4 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_4 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_4;
            }
        }

        private ICommand _AddImageCommand_5;
        public ICommand AddImageCommand_5
        {
            get
            {
                if (_AddImageCommand_5 == null)
                {
                    _AddImageCommand_5 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_5 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_5 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_5;
            }
        }

        private ICommand _AddImageCommand_6;
        public ICommand AddImageCommand_6
        {
            get
            {
                if (_AddImageCommand_6 == null)
                {
                    _AddImageCommand_6 = new RelayCommand<object>(p => true, p =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

                        if (ofd.ShowDialog() == true)
                        {
                            string string_File_Name = ofd.FileName;
                            BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                            Tour_Image_Byte_Source_6 = File.ReadAllBytes(string_File_Name);
                            Tour_Image_Source_6 = bitmap_Image;
                        }
                    });
                }

                return _AddImageCommand_6;
            }
        }
    }
}
