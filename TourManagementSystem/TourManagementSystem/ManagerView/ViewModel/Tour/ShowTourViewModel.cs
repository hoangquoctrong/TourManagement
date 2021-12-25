using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
    public class ShowTourViewModel : BaseViewModel
    {
        private int _User_ID;

        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _IsVisibility;

        public Visibility IsVisibility
        { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private Visibility _IsDirectorVisibility;

        public Visibility IsDirectorVisibility
        { get => _IsDirectorVisibility; set { _IsDirectorVisibility = value; OnPropertyChanged("IsDirectorVisibility"); } }

        private Visibility _ProgressBarVisbility;

        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        private TourModel _TourSelected;

        public TourModel TourSelected
        { get => _TourSelected; set { _TourSelected = value; OnPropertyChanged("TourSelected"); } }

        #region Data Binding

        private int _Tour_ID;

        public int Tour_ID
        { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private string _Tour_Name;

        public string Tour_Name
        { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private string _Tour_Type;

        public string Tour_Type
        { get => _Tour_Type; set { _Tour_Type = value; OnPropertyChanged(); } }

        private string _Tour_Description;

        public string Tour_Description
        { get => _Tour_Description; set { _Tour_Description = value; OnPropertyChanged(); } }

        private bool _Is_Exist;

        public bool Is_Exist
        { get => _Is_Exist; set { _Is_Exist = value; OnPropertyChanged(); } }

        private double _Tour_Star;

        public double Tour_Star
        { get => _Tour_Star; set { _Tour_Star = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxModel> _PlaceList;

        public BindableCollection<CheckBoxModel> PlaceList
        { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        private BindableCollection<PlaceModel> _PlaceSelectedList;

        public BindableCollection<PlaceModel> PlaceSelectedList
        { get => _PlaceSelectedList; set { _PlaceSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<PlaceModel> _RefreshPlaceSelectedList;

        public BindableCollection<PlaceModel> RefreshPlaceSelectedList
        { get => _RefreshPlaceSelectedList; set { _RefreshPlaceSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxModel> _RefreshPlaceList;

        public BindableCollection<CheckBoxModel> RefreshPlaceList
        { get => _RefreshPlaceList; set { _RefreshPlaceList = value; OnPropertyChanged(); } }

        #endregion Data Binding

        public ShowTourViewModel(int user_id, int tour_id, Visibility visibility, Visibility directorVisibility)
        {
            User_ID = user_id;
            IsVisibility = visibility;
            IsDirectorVisibility = directorVisibility;
            TourSelected = TourHandleModel.GetTour(tour_id);

            PlaceList = new BindableCollection<CheckBoxModel>();
            PlaceSelectedList = new BindableCollection<PlaceModel>();
            RefreshPlaceSelectedList = new BindableCollection<PlaceModel>();
            TourInformationItems = new ObservableCollection<TourInformationModel>();

            ProgressBarVisbility = Visibility.Visible;
            SetDataToUC(tour_id);
        }

        private async void SetDataToUC(int tour_id)
        {
            await Task.Delay(5000);
            SetTourInView(TourSelected);
            SetTourImageInView(Tour_ID);
            LoadTourInformationComboBox();
            PlaceSelectedList = PlaceHandleModel.GetPlaceFromPlaceDetail(tour_id);
            RefreshPlaceSelectedList = PlaceHandleModel.GetPlaceFromPlaceDetail(tour_id);
            PlaceList = GetPlaceList();
            RefreshPlaceList = GetPlaceList();

            TourInformationItems = GetTourInformationList(tour_id);
            Refresh_TourInformationItems = GetTourInformationList(tour_id);
            ProgressBarVisbility = Visibility.Hidden;
        }

        private void SetTourInView(TourModel tour)
        {
            Tour_ID = tour.TOUR_ID;
            Tour_Name = tour.TOUR_NAME;
            Tour_Type = tour.TOUR_TYPE;
            Tour_Description = tour.TOUR_CHARACTERISTIS;
            Tour_Star = tour.TOUR_STAR;
            Is_Exist = tour.TOUR_IS_EXIST.Equals("No") && IsVisibility == Visibility.Visible ? true : false;
        }

        private void SetTourImageInView(int tour_id)
        {
            ImageByteSource = TourHandleModel.GetTourImage(tour_id);

            if (ImageByteSource.Count > 0)
            {
                Tour_Image_Byte_Source_1 = ImageByteSource.ElementAt(0).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_1 = Tour_Image_Byte_Source_1 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_1);

                Tour_Image_Byte_Source_2 = ImageByteSource.ElementAt(1).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_2 = Tour_Image_Byte_Source_2 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_2);

                Tour_Image_Byte_Source_3 = ImageByteSource.ElementAt(2).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_3 = Tour_Image_Byte_Source_3 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_3);

                Tour_Image_Byte_Source_4 = ImageByteSource.ElementAt(3).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_4 = Tour_Image_Byte_Source_4 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_4);

                Tour_Image_Byte_Source_5 = ImageByteSource.ElementAt(4).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_5 = Tour_Image_Byte_Source_5 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_5);

                Tour_Image_Byte_Source_6 = ImageByteSource.ElementAt(5).TOUR_IMAGE_BYTE_SOURCE;
                Tour_Image_Source_6 = Tour_Image_Byte_Source_6 == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Tour_Image_Byte_Source_6);
            }
            else
            {
                Tour_Image_Source_1 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                Tour_Image_Source_2 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                Tour_Image_Source_3 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                Tour_Image_Source_4 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                Tour_Image_Source_5 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                Tour_Image_Source_6 = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Add.png", UriKind.Absolute));

                for (int i = 0; i < 6; i++)
                {
                    ImageByteSource.Add(new TourImageModel()
                    {
                        TOUR_ID = tour_id,
                        IMAGE_ID = 0,
                        TOUR_IMAGE_BYTE_SOURCE = null
                    });
                }
            }
        }

        private BindableCollection<CheckBoxModel> GetPlaceList()
        {
            ObservableCollection<PlaceModel> PlaceItems = PlaceHandleModel.GetPlaceList();
            BindableCollection<CheckBoxModel> placeList = new BindableCollection<CheckBoxModel>();
            foreach (PlaceModel item in PlaceItems)
            {
                bool IsSelect = false;
                foreach (var itemselect in PlaceSelectedList)
                {
                    if (itemselect.PLACE_ID == item.PLACE_ID)
                    {
                        IsSelect = true;
                        break;
                    }
                }
                placeList.Add(new CheckBoxModel(item.PLACE_NAME, item.PLACE_NATION, item.PLACE_ID, IsSelect));
            }

            return placeList;
        }

        private ObservableCollection<TourInformationModel> GetTourInformationList(int tour_id)
        {
            ObservableCollection<TourInformationModel> InformationList = TourInformationHandleModel.GetTourInformationList(tour_id);

            return InformationList;
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

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TourViewModel(User_ID, IsVisibility, IsDirectorVisibility));
                }

                return _CancelCommand;
            }
        }

        #region Image Binding

        private ObservableCollection<TourImageModel> _ImageByteSources;

        public ObservableCollection<TourImageModel> ImageByteSource
        { get => _ImageByteSources; set { _ImageByteSources = value; OnPropertyChanged(); } }

        #region Image Binding

        private byte[] _Tour_Image_Byte_Source_1;

        public byte[] Tour_Image_Byte_Source_1
        { get => _Tour_Image_Byte_Source_1; set { _Tour_Image_Byte_Source_1 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_1;

        public BitmapImage Tour_Image_Source_1
        { get => _Tour_Image_Source_1; set { _Tour_Image_Source_1 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_2;

        public byte[] Tour_Image_Byte_Source_2
        { get => _Tour_Image_Byte_Source_2; set { _Tour_Image_Byte_Source_2 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_2;

        public BitmapImage Tour_Image_Source_2
        { get => _Tour_Image_Source_2; set { _Tour_Image_Source_2 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_3;

        public byte[] Tour_Image_Byte_Source_3
        { get => _Tour_Image_Byte_Source_3; set { _Tour_Image_Byte_Source_3 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_3;

        public BitmapImage Tour_Image_Source_3
        { get => _Tour_Image_Source_3; set { _Tour_Image_Source_3 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_4;

        public byte[] Tour_Image_Byte_Source_4
        { get => _Tour_Image_Byte_Source_4; set { _Tour_Image_Byte_Source_4 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_4;

        public BitmapImage Tour_Image_Source_4
        { get => _Tour_Image_Source_4; set { _Tour_Image_Source_4 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_5;

        public byte[] Tour_Image_Byte_Source_5
        { get => _Tour_Image_Byte_Source_5; set { _Tour_Image_Byte_Source_5 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_5;

        public BitmapImage Tour_Image_Source_5
        { get => _Tour_Image_Source_5; set { _Tour_Image_Source_5 = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source_6;

        public byte[] Tour_Image_Byte_Source_6
        { get => _Tour_Image_Byte_Source_6; set { _Tour_Image_Byte_Source_6 = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source_6;

        public BitmapImage Tour_Image_Source_6
        { get => _Tour_Image_Source_6; set { _Tour_Image_Source_6 = value; OnPropertyChanged(); } }

        #endregion Image Binding

        #region Image Command

        private ICommand _ModifyImageCommand_1;

        public ICommand ModifyImageCommand_1
        {
            get
            {
                if (_ModifyImageCommand_1 == null)
                {
                    _ModifyImageCommand_1 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_1;
            }
        }

        private ICommand _ModifyImageCommand_2;

        public ICommand ModifyImageCommand_2
        {
            get
            {
                if (_ModifyImageCommand_2 == null)
                {
                    _ModifyImageCommand_2 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_2;
            }
        }

        private ICommand _ModifyImageCommand_3;

        public ICommand ModifyImageCommand_3
        {
            get
            {
                if (_ModifyImageCommand_3 == null)
                {
                    _ModifyImageCommand_3 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_3;
            }
        }

        private ICommand _ModifyImageCommand_4;

        public ICommand ModifyImageCommand_4
        {
            get
            {
                if (_ModifyImageCommand_4 == null)
                {
                    _ModifyImageCommand_4 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_4;
            }
        }

        private ICommand _ModifyImageCommand_5;

        public ICommand ModifyImageCommand_5
        {
            get
            {
                if (_ModifyImageCommand_5 == null)
                {
                    _ModifyImageCommand_5 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_5;
            }
        }

        private ICommand _ModifyImageCommand_6;

        public ICommand ModifyImageCommand_6
        {
            get
            {
                if (_ModifyImageCommand_6 == null)
                {
                    _ModifyImageCommand_6 = new RelayCommand<object>(p => Is_Exist, p =>
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

                return _ModifyImageCommand_6;
            }
        }

        #endregion Image Command

        #endregion Image Binding

        private ICommand _SaveTourCommand;

        public ICommand SaveTourCommand
        {
            get
            {
                if (_SaveTourCommand == null)
                {
                    _SaveTourCommand = new RelayCommand<object>(p => IsExcuteSaveTourCommand(), p => ExcuteSaveTourCommand());
                }
                return _SaveTourCommand;
            }
        }

        private void ExcuteSaveTourCommand()
        {
            TourSelected = InsertDataToTourSelect();
            if (TourHandleModel.UpdateTour(TourSelected, User_ID))
            {
                string messageDisplay = string.Format("Update Tour Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
            else
            {
                string messageDisplay = string.Format("Update Tour Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
        }

        private bool IsExcuteSaveTourCommand()
        {
            if (Tour_Name != TourSelected.TOUR_NAME)
            {
                return true;
            }

            if (Tour_Type != TourSelected.TOUR_TYPE)
            {
                return true;
            }

            if (Tour_Description != TourSelected.TOUR_CHARACTERISTIS)
            {
                return true;
            }

            return false;
        }

        private TourModel InsertDataToTourSelect()
        {
            return new TourModel()
            {
                TOUR_ID = Tour_ID,
                TOUR_NAME = Tour_Name,
                TOUR_TYPE = Tour_Type,
                TOUR_CHARACTERISTIS = Tour_Description
            };
        }

        private ICommand _SaveImageTourCommand;

        public ICommand SaveImageTourCommand
        {
            get
            {
                if (_SaveImageTourCommand == null)
                {
                    _SaveImageTourCommand = new RelayCommand<object>(p => IsExcuteSaveImageCommand(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteSaveImageCommand();
                    });
                }
                return _SaveImageTourCommand;
            }
        }

        private async void ExcuteSaveImageCommand()
        {
            await Task.Delay(4000);
            InsertDataToTourImageSelect();
            if (TourHandleModel.UpdateTourImage(ImageByteSource, Tour_ID, User_ID))
            {
                string messageDisplay = string.Format("Update Tour Image Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                string messageDisplay = string.Format("Update Tour Image Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private bool IsExcuteSaveImageCommand()
        {
            if (Tour_Image_Byte_Source_1 != ImageByteSource.ElementAt(0).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            if (Tour_Image_Byte_Source_2 != ImageByteSource.ElementAt(1).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            if (Tour_Image_Byte_Source_3 != ImageByteSource.ElementAt(2).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            if (Tour_Image_Byte_Source_4 != ImageByteSource.ElementAt(3).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            if (Tour_Image_Byte_Source_5 != ImageByteSource.ElementAt(4).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            if (Tour_Image_Byte_Source_6 != ImageByteSource.ElementAt(5).TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            return false;
        }

        private void InsertDataToTourImageSelect()
        {
            ImageByteSource.ElementAt(0).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_1;
            ImageByteSource.ElementAt(1).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_2;
            ImageByteSource.ElementAt(2).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_3;
            ImageByteSource.ElementAt(3).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_4;
            ImageByteSource.ElementAt(4).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_5;
            ImageByteSource.ElementAt(5).TOUR_IMAGE_BYTE_SOURCE = Tour_Image_Byte_Source_6;
        }

        private ICommand _DeleteTourCommand;

        public ICommand DeleteTourCommand
        {
            get
            {
                if (_DeleteTourCommand == null)
                {
                    _DeleteTourCommand = new RelayCommand<ContentControl>(p => Is_Exist, p => ExcuteDeleteCommand(p));
                }
                return _DeleteTourCommand;
            }
        }

        private async void ExcuteDeleteCommand(ContentControl p)
        {
            bool? Result = new MessageWindow("Do you want to delete this tour?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result == true)
            {
                ProgressBarVisbility = Visibility.Visible;
                await Task.Delay(2000);
                if (TourHandleModel.DeleteTour(Tour_ID, User_ID))
                {
                    string messageDisplay = string.Format("Delete Tour Successfully!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    p.Content = new TourViewModel(User_ID, IsVisibility, IsDirectorVisibility);
                }
                else
                {
                    string messageDisplay = string.Format("Delete Tour Failed! Please try again!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    ProgressBarVisbility = Visibility.Hidden;
                }
            }
        }

        private ICommand _SavePlaceDetailCommand;

        public ICommand SavePlaceDetailCommand
        {
            get
            {
                if (_SavePlaceDetailCommand == null)
                {
                    _SavePlaceDetailCommand = new RelayCommand<object>(p => IsExcuteSavePlaceDetailCommand(), p => ExcuteSavePlaceDetailCommand());
                }
                return _SavePlaceDetailCommand;
            }
        }

        private void ExcuteSavePlaceDetailCommand()
        {
            if (PlaceHandleModel.DeletePlaceDetail(Tour_ID))
            {
                if (PlaceHandleModel.InsertPlaceDetail(PlaceSelectedList, Tour_Name, User_ID, false))
                {
                    string messageDisplay = string.Format("Update Places For Tour Successfully!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    RefreshPlaceSelectedList = PlaceHandleModel.GetPlaceFromPlaceDetail(Tour_ID);
                }
                else
                {
                    string messageDisplay = string.Format("Update Places For Tour failed! Please try again!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                }
            }
            else
            {
                string messageDisplay = string.Format("Update Places For Tour failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
        }

        private bool IsExcuteSavePlaceDetailCommand()
        {
            if (RefreshPlaceSelectedList.Count != PlaceSelectedList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (PlaceModel item in PlaceSelectedList)
            {
                foreach (PlaceModel item_before in RefreshPlaceSelectedList)
                {
                    if (item.PLACE_ID == item_before.PLACE_ID)
                    {
                        count_equal++;
                        break;
                    }
                }
            }

            return count_equal != RefreshPlaceSelectedList.Count;
        }

        private ObservableCollection<TourInformationModel> _TourInformationItems;

        public ObservableCollection<TourInformationModel> TourInformationItems
        { get => _TourInformationItems; set { _TourInformationItems = value; OnPropertyChanged("TourInformationItems"); } }

        private ObservableCollection<TourInformationModel> _Refresh_TourInformationItems;

        public ObservableCollection<TourInformationModel> Refresh_TourInformationItems
        { get => _Refresh_TourInformationItems; set { _Refresh_TourInformationItems = value; OnPropertyChanged("Refresh_TourInformationItems"); } }

        private TourInformationModel _TourInformationSelected;

        public TourInformationModel TourInformationSelected
        { get => _TourInformationSelected; set { _TourInformationSelected = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxModel> _CB_TourInformationList;

        public ObservableCollection<ComboBoxModel> CB_TourInformationList
        { get => _CB_TourInformationList; set { _CB_TourInformationList = value; OnPropertyChanged("CB_TourInformationList"); } }

        private ComboBoxModel _CB_TourInformationSelected;

        public ComboBoxModel CB_TourInformationSelected
        { get => _CB_TourInformationSelected; set { _CB_TourInformationSelected = value; OnPropertyChanged("CB_TourInformationSelected"); } }

        private void LoadTourInformationComboBox()
        {
            CB_TourInformationList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Schedule", true),
                new ComboBoxModel("Department Date", false),
                new ComboBoxModel("End Date", false),
                new ComboBoxModel("Total Cost", false)
            };
            CB_TourInformationSelected = CB_TourInformationList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TourInformationFilterText;

        public string TourInformationFilterText
        {
            get => _TourInformationFilterText;
            set
            {
                _TourInformationFilterText = value;
                OnPropertyChanged("TourInformationFilterText");
                TourInformationItem_Filter();
            }
        }

        private void TourInformationItem_Filter()
        {
            TourInformationItems = Refresh_TourInformationItems;
            if (!string.IsNullOrEmpty(TourInformationFilterText))
            {
                switch (CB_TourInformationSelected.CB_Name)
                {
                    case "Schedule":
                        TourInformationItems = new ObservableCollection<TourInformationModel>(TourInformationItems.Where(x => x.INFORMATION_TIME.TIME_STRING.Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_STRING.ToLower().Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_STRING.ToUpper().Contains(TourInformationFilterText)));
                        break;

                    case "Department Date":
                        TourInformationItems = new ObservableCollection<TourInformationModel>(TourInformationItems.Where(x => x.INFORMATION_TIME.TIME_DEPARTMENT_STRING.Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_DEPARTMENT_STRING.ToLower().Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_DEPARTMENT_STRING.ToUpper().Contains(TourInformationFilterText)));
                        break;

                    case "End Date":
                        TourInformationItems = new ObservableCollection<TourInformationModel>(TourInformationItems.Where(x => x.INFORMATION_TIME.TIME_END_STRING.Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_END_STRING.ToLower().Contains(TourInformationFilterText) ||
                                                                                                            x.INFORMATION_TIME.TIME_END_STRING.ToUpper().Contains(TourInformationFilterText)));
                        break;

                    case "Total Cost":
                        TourInformationItems = new ObservableCollection<TourInformationModel>(TourInformationItems.Where(x => x.INFORMATION_PRICE.PRICE_COST_TOTAL.ToString().Contains(TourInformationFilterText)));
                        break;
                }
            }
        }

        private ICommand _AddTourInformationCommand;

        public ICommand AddTourInformationCommand
        {
            get
            {
                if (_AddTourInformationCommand == null)
                {
                    _AddTourInformationCommand = new RelayCommand<ContentControl>(p => Is_Exist, p => p.Content = new AddTourInformationViewModel(User_ID, Tour_ID, PlaceSelectedList));
                }
                return _AddTourInformationCommand;
            }
        }

        private ICommand _ShowDetailTourInformationCommand;

        public ICommand ShowDetailTourInformationCommand
        {
            get
            {
                if (_ShowDetailTourInformationCommand == null)
                {
                    _ShowDetailTourInformationCommand = new RelayCommand<ContentControl>(p => TourInformationSelected != null, p =>
                    {
                        bool IsEnable = Is_Exist && TourInformationSelected.INFORMATION_ENABLE;
                        p.Content = new ShowTourInformationViewModel(User_ID, Tour_ID, TourInformationSelected, PlaceSelectedList, IsEnable, IsVisibility, IsDirectorVisibility);
                    });
                }
                return _ShowDetailTourInformationCommand;
            }
        }
    }
}