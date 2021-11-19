using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ShowStaffViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private StaffModel _StaffSelected;
        public StaffModel StaffSelected { get => _StaffSelected; set { _StaffSelected = value; OnPropertyChanged("StaffSelected"); } }


        #region Data Binding
        private int _Staff_ID;
        public int Staff_ID { get => _Staff_ID; set { _Staff_ID = value; OnPropertyChanged(); } }

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

        private string _Staff_Note_Remove;
        public string Staff_Note_Remove { get => _Staff_Note_Remove; set { _Staff_Note_Remove = value; OnPropertyChanged(); } }

        private DateTime _Staff_Start_Date = DateTime.Now;
        public DateTime Staff_Start_Date { get => _Staff_Start_Date; set { _Staff_Start_Date = value; OnPropertyChanged(); } }

        private byte[] _Staff_Image_Byte_Source;
        public byte[] Staff_Image_Byte_Source { get => _Staff_Image_Byte_Source; set { _Staff_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Staff_Image_Source;
        public BitmapImage Staff_Image_Source { get => _Staff_Image_Source; set { _Staff_Image_Source = value; OnPropertyChanged(); } }

        private bool _IsDelete;
        public bool IsDelete { get => _IsDelete; set { _IsDelete = value; OnPropertyChanged(); } }
        #endregion

        public ShowStaffViewModel(int user_id, StaffModel staff)
        {
            User_ID = user_id;
            StaffSelected = staff;
            SetStaffInView(staff);
            LoadTourMissionComboBox();
            ObservableCollection<TourMissionModel> tourmissionItems = TourDetailHandleModel.GetTourMissionList();
            TourMissionItemsCollection = new CollectionViewSource { Source = tourmissionItems };
            TourMissionItemsCollection.Filter += TourMissionItem_Filter;
        }

        private void SetStaffInView(StaffModel staff)
        {
            Staff_ID = staff.STAFF_ID;
            Staff_Name = staff.STAFF_NAME;
            Staff_Role = staff.STAFF_ROLE;
            Staff_ID_Card = staff.STAFF_CITIZEN_CARD;
            Staff_ID_Card_Date = staff.STAFF_CITIZEN_CARD_DATE;
            Staff_ID_Card_Place = staff.STAFF_CITIZEN_CARD_PLACE;
            Staff_Birthday = staff.STAFF_BIRTH_DATE;
            Staff_String_Birthday = staff.STAFF_STRING_BIRTH_DATE;
            Staff_Birth_Place = staff.STAFF_BIRTH_PLACE;
            Staff_Address = staff.STAFF_ADDRESS;
            Staff_Gender = staff.STAFF_GENDER;
            Staff_Academic_Level = staff.STAFF_ACADEMIC_LEVEL;
            Staff_Email = staff.STAFF_EMAIL;
            Staff_Start_Date = staff.STAFF_START_DATE;
            Staff_Note = staff.STAFF_NOTE;
            Staff_Phone_Number = staff.STAFF_PHONE_NUMBER;
            Staff_Username = staff.STAFF_USERNAME;
            Staff_Image_Byte_Source = staff.STAFF_IMAGE_BYTE_SOURCE;
            Staff_Image_Source = Staff_Image_Byte_Source == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/User.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Staff_Image_Byte_Source);

            IsDelete = !staff.STAFF_IS_DELETE;

            if (!IsDelete)
            {
                Staff_Note_Remove = string.Format("Vao ngay {0} da nghi lam do {1}", staff.STAFF_DELETE_STRING_DATE, staff.STAFF_DELETE_NOTE);
            }
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

        private ICommand _ModifyImageCommand;
        public ICommand ModifyImageCommand
        {
            get
            {
                if (_ModifyImageCommand == null)
                {
                    _ModifyImageCommand = new RelayCommand<object>(p => IsDelete, p => ExcuteAddImageCommand());
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
                Staff_Image_Byte_Source = File.ReadAllBytes(string_File_Name);
                Staff_Image_Source = bitmap_Image;
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

        private bool IsExcuteSaveChangeCommand()
        {
            if (!IsDelete)
            {
                return false;
            }

            Staff_String_Birthday = Staff_Birthday.ToString("dd/MM/yyyy");
            Staff_String_ID_Card_Date = Staff_ID_Card_Date.ToString("dd/MM/yyyy");
            if (Staff_Name != StaffSelected.STAFF_NAME)
            {
                return true;
            }
            if (Staff_Role != StaffSelected.STAFF_ROLE)
            {
                return true;
            }
            if (Staff_ID_Card != StaffSelected.STAFF_CITIZEN_CARD)
            {
                return true;
            }
            if (Staff_String_ID_Card_Date != StaffSelected.STAFF_STRING_CITIZEN_CARD_DATE)
            {
                return true;
            }
            if (Staff_ID_Card_Place != StaffSelected.STAFF_CITIZEN_CARD_PLACE)
            {
                return true;
            }
            if (Staff_String_Birthday != StaffSelected.STAFF_STRING_BIRTH_DATE)
            {
                return true;
            }
            if (Staff_Birth_Place != StaffSelected.STAFF_BIRTH_PLACE)
            {
                return true;
            }
            if (Staff_Gender != StaffSelected.STAFF_GENDER)
            {
                return true;
            }
            if (Staff_Address != StaffSelected.STAFF_ADDRESS)
            {
                return true;
            }
            if (Staff_Academic_Level != StaffSelected.STAFF_ACADEMIC_LEVEL)
            {
                return true;
            }
            if (Staff_Email != StaffSelected.STAFF_EMAIL)
            {
                return true;
            }
            if (Staff_Phone_Number != StaffSelected.STAFF_PHONE_NUMBER)
            {
                return true;
            }
            //Note: If Update Image will update note
            if (Staff_Note != StaffSelected.STAFF_NOTE)
            {
                return true;
            }
            if (Staff_Image_Byte_Source != StaffSelected.STAFF_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            return false;
        }

        private void ExcuteSaveChangeCommand()
        {
            StaffSelected = InsertDataToStaffSeleted();
            if (StaffHandleModel.UpdateStaff(StaffSelected, User_ID))
            {
                MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private StaffModel InsertDataToStaffSeleted()
        {
            return new StaffModel
            {
                STAFF_NAME = Staff_Name,
                STAFF_ROLE = Staff_Role,
                STAFF_CITIZEN_CARD = Staff_ID_Card,
                STAFF_CITIZEN_CARD_DATE = Staff_ID_Card_Date,
                STAFF_CITIZEN_CARD_PLACE = Staff_ID_Card_Place,
                STAFF_BIRTH_DATE = Staff_Birthday,
                STAFF_BIRTH_PLACE = Staff_Birth_Place,
                STAFF_ADDRESS = Staff_Address,
                STAFF_GENDER = Staff_Gender,
                STAFF_ACADEMIC_LEVEL = Staff_Academic_Level,
                STAFF_EMAIL = Staff_Email,
                STAFF_PHONE_NUMBER = Staff_Phone_Number,
                STAFF_START_DATE = Staff_Start_Date,
                STAFF_NOTE = Staff_Note,
                STAFF_IMAGE_BYTE_SOURCE = Staff_Image_Byte_Source,
                STAFF_STRING_BIRTH_DATE = Staff_Birthday.ToString("dd/MM/yyyy"),
                STAFF_STRING_CITIZEN_CARD_DATE = Staff_ID_Card_Date.ToString("dd/MM/yyyy"),
                STAFF_STRING_START_DATE = Staff_Start_Date.ToString("dd/MM/yyyy"),
                STAFF_USERNAME = Staff_Username,
                STAFF_PASSWORD = Staff_Password,
                STAFF_ID = Staff_ID
            };
        }

        private ICommand _ChangePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (_ChangePasswordCommand == null)
                {
                    _ChangePasswordCommand = new RelayCommand<object>(p => !string.IsNullOrEmpty(Staff_Password) && IsDelete, p =>
                    {
                        if (StaffHandleModel.ChangePassword(Staff_ID, Staff_Password))
                        {
                            MessageBox.Show("Change Password successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                            Staff_Password = "";
                        }
                        else
                        {
                            MessageBox.Show("Change Password failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                return _ChangePasswordCommand;
            }
        }

        private ICommand _RemoveStaffCommand;
        public ICommand RemoveStaffCommand
        {
            get
            {
                if (_RemoveStaffCommand == null)
                {
                    _RemoveStaffCommand = new RelayCommand<ContentControl>(p => !string.IsNullOrEmpty(Staff_Note_Remove) && IsDelete, p =>
                    {
                        if (StaffHandleModel.DeleteStaff(Staff_ID, Staff_Name, Staff_Note_Remove, User_ID))
                        {
                            MessageBox.Show("Delete staff successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                            p.Content = new StaffViewModel(User_ID);
                        }
                        else
                        {
                            MessageBox.Show("Delete staff failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                return _RemoveStaffCommand;
            }
        }

        private CollectionViewSource TourMissionItemsCollection;

        public ICollectionView TourMissionCollection => TourMissionItemsCollection.View;


        private ObservableCollection<ComboBoxModel> _CB_TourMissionList;
        public ObservableCollection<ComboBoxModel> CB_TourMissionList { get => _CB_TourMissionList; set { _CB_TourMissionList = value; OnPropertyChanged("CB_HistoryList"); } }

        private ComboBoxModel _CB_TourMissionSelected;
        public ComboBoxModel CB_TourMissionSelected { get => _CB_TourMissionSelected; set { _CB_TourMissionSelected = value; OnPropertyChanged("CB_HistorySelected"); } }

        private void LoadTourMissionComboBox()
        {
            CB_TourMissionList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Responsibility", true),
                new ComboBoxModel("Tour Name", false),
                new ComboBoxModel("Travel Group Name", false),
                new ComboBoxModel("Start Date", false),
                new ComboBoxModel("End Date", false)
            };
            CB_TourMissionSelected = CB_TourMissionList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TourMissionFilterText;
        public string TourMissionFilterText
        {
            get => _TourMissionFilterText;
            set
            {
                _TourMissionFilterText = value;
                TourMissionItemsCollection.View.Refresh();
                OnPropertyChanged("TourMissionFilterText");
            }
        }

        private void TourMissionItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(TourMissionFilterText))
            {
                e.Accepted = true;
                return;
            }

            TourMissionModel _items = e.Item as TourMissionModel;
            switch (CB_TourMissionSelected.CB_Name)
            {
                case "Responsibility":
                    if (_items.TOUR_MISSION_RESPONSIBILITY.IndexOf(TourMissionFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Tour Name":
                    if (_items.TOUR_MISSION_TOURNAME.IndexOf(TourMissionFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Travel Group Name":
                    if (_items.TOUR_MISSION_TRAVELGROUPNAME.IndexOf(TourMissionFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "Start Date":
                    if (_items.TOUR_MISSION_STRING_STARTDATE.IndexOf(TourMissionFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
                case "End Date":
                    if (_items.TOUR_MISSION_STRING_ENDDATE.IndexOf(TourMissionFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
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
