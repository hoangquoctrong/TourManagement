using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Managers.Model;
using TourManagementSystem.Managers.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        /**/

        #region Data

        #region Data Binding of StaffUC

        /*
         * User is manager of system
         */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }


        /*
         * StaffList binding ItemSource on DataGridView of StaffUC
         */
        private ObservableCollection<StaffModel> _StaffList;
        public ObservableCollection<StaffModel> StaffList { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }

        /*
         * Refresh_StaffList is refresh StaffList
         */
        private ObservableCollection<StaffModel> _Refresh_StaffList;
        public ObservableCollection<StaffModel> Refresh_StaffList { get => _Refresh_StaffList; set { _Refresh_StaffList = value; OnPropertyChanged(); } }

        /*
         * Staff_Selected binding SelectedItem on DataGridView of StaffUC
         */
        private StaffModel _Staff_Selected;
        public StaffModel Staff_Selected
        {
            get => _Staff_Selected;
            set
            {
                _Staff_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into displaystaffUC
                 */
                if (Staff_Selected != null)
                {
                    Staff_Name = Staff_Selected.STAFF_NAME;
                    Staff_Role = Staff_Selected.STAFF_ROLE;
                    Staff_ID_Card = Staff_Selected.STAFF_CITIZEN_CARD;
                    Staff_ID_Card_Date = Staff_Selected.STAFF_CITIZEN_CARD_DATE;
                    Staff_ID_Card_Place = Staff_Selected.STAFF_CITIZEN_CARD_PLACE;
                    Staff_Birthday = Staff_Selected.STAFF_BIRTH_DATE;
                    Staff_String_Birthday = Staff_Selected.STAFF_STRING_BIRTH_DATE;
                    Staff_Birth_Place = Staff_Selected.STAFF_BIRTH_PLACE;
                    Staff_Address = Staff_Selected.STAFF_ADDRESS;
                    Staff_Gender = Staff_Selected.STAFF_GENDER;
                    Staff_Academic_Level = Staff_Selected.STAFF_ACADEMIC_LEVEL;
                    Staff_Email = Staff_Selected.STAFF_EMAIL;
                    Staff_Start_Date = Staff_Selected.STAFF_START_DATE;
                    Staff_Note = Staff_Selected.STAFF_NOTE;
                    Staff_Phone_Number = Staff_Selected.STAFF_PHONE_NUMBER;
                    Staff_Image_Byte_Source = Staff_Selected.STAFF_IMAGE_BYTE_SOURCE;
                    Staff_Image_Source = Staff_Image_Byte_Source == null
                        ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/User.png", UriKind.Absolute))
                        : GlobalFunction.ToImage(Staff_Image_Byte_Source);

                    Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
                    TOUR_ACCOUNT tour_account = db.TOUR_ACCOUNT.FirstOrDefault(x => x.TOUR_STAFF_ID == Staff_Selected.STAFF_ID);
                    Staff_Username = tour_account.TOUR_ACCOUNT_NAME;
                }
            }
        }

        /*
         * Search_Text binding to Search button of StaffUC
         */
        private string _Search_Text;
        public string Search_Text
        {
            get => _Search_Text;
            set
            {
                _Search_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh StaffList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                StaffList = Refresh_StaffList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Text))
                {
                    //Step 3
                    if (CB_StaffSelected != null)
                    {
                        //Step 4
                        switch (CB_StaffSelected.CB_Name)
                        {
                            case "Name":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_NAME.Contains(Search_Text) ||
                                                                                                        x.STAFF_NAME.ToLower().Contains(Search_Text) ||
                                                                                                        x.STAFF_NAME.ToUpper().Contains(Search_Text)));
                                break;
                            case "Role":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_ROLE.Contains(Search_Text) ||
                                                                                                        x.STAFF_ROLE.ToLower().Contains(Search_Text) ||
                                                                                                        x.STAFF_ROLE.ToUpper().Contains(Search_Text)));
                                break;
                            case "Gender":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_GENDER.Contains(Search_Text) ||
                                                                                                       x.STAFF_GENDER.ToLower().Contains(Search_Text) ||
                                                                                                       x.STAFF_GENDER.ToUpper().Contains(Search_Text)));
                                break;
                            case "Citizen Identity":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_CITIZEN_CARD.Contains(Search_Text) ||
                                                                                                        x.STAFF_CITIZEN_CARD.ToLower().Contains(Search_Text) ||
                                                                                                        x.STAFF_CITIZEN_CARD.ToUpper().Contains(Search_Text)));
                                break;
                            case "Address":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_ADDRESS.Contains(Search_Text) ||
                                                                                                        x.STAFF_ADDRESS.ToLower().Contains(Search_Text) ||
                                                                                                        x.STAFF_ADDRESS.ToUpper().Contains(Search_Text)));
                                break;
                            case "Phone Number":
                                StaffList = new ObservableCollection<StaffModel>(StaffList.Where(x => x.STAFF_PHONE_NUMBER.Contains(Search_Text) ||
                                                                                                        x.STAFF_PHONE_NUMBER.ToLower().Contains(Search_Text) ||
                                                                                                        x.STAFF_PHONE_NUMBER.ToUpper().Contains(Search_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_StaffList binding ItemSource of ComboBox of StaffUC
         */
        private ObservableCollection<ComboBoxModel> _CB_StaffList;
        public ObservableCollection<ComboBoxModel> CB_StaffList { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged(); } }

        /*
         * CB_StaffSelected binding SelectedValue of ComboBox of StaffUC
         */
        private ComboBoxModel _CB_StaffSelected;
        public ComboBoxModel CB_StaffSelected { get => _CB_StaffSelected; set { _CB_StaffSelected = value; OnPropertyChanged(); } }

        /*
         * Checkbox_DisplayAllStaff binding Checked of Checkbox of StaffUC
         */
        private bool _Checkbox_DisplayAllStaff;
        public bool Checkbox_DisplayAllStaff
        {
            get => _Checkbox_DisplayAllStaff;
            set
            {
                _Checkbox_DisplayAllStaff = value;
                OnPropertyChanged();

                if (Checkbox_DisplayAllStaff)
                {
                    LoadDataGridStaffUC();
                }
                else
                {
                    LoadDataGridExceptDeleteStaffUC();
                }
            }
        }

        #endregion Data Binding of StaffUC

        #region Data Binding of AddStaffUC, DisplayStaffUC

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

        #endregion Data Binding of AddStaffUC, DisplayStaffUC

        #endregion Data

        #region Command

        #region Command of StaffUC
        /*
         * AddStaffCommand is a command which will change present content control to AddStaffUC
         */
        public ICommand AddStaffCommand { get; set; }

        /*
         * ShowStaffDetailCommand is a command which will show detail about the staff is clicked in DataGrid in StaffUC
         */
        public ICommand ShowStaffDetailCommand { get; set; }

        #endregion Command of StaffUC

        #region Command of AddStaffUC

        /*
         * ConfirmCommand is a command which will save information above to database in AddStaffUC
         */
        public ICommand ConfirmCommand { get; set; }

        /*
         * CancelCommand is a command which will back to StaffUC
         */
        public ICommand CancelCommand { get; set; }

        /*
         * AddImageCommand is a command which will add image from computer to system
         */
        public ICommand AddImageCommand { get; set; }

        #endregion Command of AddStaffUC

        #region Command of DisplayStaffUC
        /*
         * ModifyImageCommand is a command which will update image from computer to system
         */
        public ICommand ModifyImageCommand { get; set; }

        /*
         * SaveChangeCommand is a command which will save all change in Tab Information to database in DisplayStaffUC
         */
        public ICommand SaveChangeCommand { get; set; }

        /*
         * AddStaffCommand is a command which will change password in Tab Account to database in DisplayStaffUC
         */
        public ICommand ChangePasswordCommand { get; set; }

        /*
         * RemoveCommand is a command which will remove account in Tab Delete in database in DisplayStaffUC
         */
        public ICommand RemoveCommand { get; set; }
        #endregion Command of DisplayStaffUC

        #endregion Command

        /*
         * Constructor for StaffUC, AddStaffUC
         */
        public StaffViewModel(int user_id)
        {
            User_ID = user_id;
            LoadStaffUC();
        }

        /*
         * Constructor for StaffDisplayUC
         */
        public StaffViewModel(StaffModel staffSelected, int user_id)
        {
            User_ID = user_id;
            Staff_Selected = staffSelected;
            LoadCommand();
        }

        #region All Load Function
        /*
         * Gather all Load into one function
         */
        private void LoadStaffUC()
        {
            LoadStaffComboBox();
            Checkbox_DisplayAllStaff = false;
            LoadCommand();
            Staff_Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/User.png", UriKind.Absolute));
        }

        /* 
         * Load data from database to DataGridView
         * Reload data when back from detailed to global 
         */
        private void LoadDataGridStaffUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to StaffList
             * Step 3: Initialize StaffList, Refresh_StaffList
             * Step 4: Insert staffList to StaffList and Refresh_StaffList
             */

            //Step 1
            if (CB_StaffSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_STAFF> staffList = from staff in db.TOUR_STAFF
                                               select staff;

            //Step 3
            StaffList = new ObservableCollection<StaffModel>();
            Refresh_StaffList = new ObservableCollection<StaffModel>();

            //Step 4
            foreach (TOUR_STAFF item in staffList)
            {
                StaffModel staffModel = new StaffModel
                {
                    STAFF_ID = item.TOUR_STAFF_ID,
                    STAFF_NAME = item.TOUR_STAFF_NAME,
                    STAFF_ROLE = item.TOUR_STAFF_ROLE,
                    STAFF_BIRTH_DATE = (DateTime)item.TOUR_STAFF_BIRTH_DATE,
                    STAFF_BIRTH_PLACE = item.TOUR_STAFF_BIRTH_PLACE,
                    STAFF_GENDER = item.TOUR_STAFF_GENDER,
                    STAFF_CITIZEN_CARD = item.TOUR_STAFF_CITIZEN_IDENTITY,
                    STAFF_CITIZEN_CARD_DATE = (DateTime)item.TOUR_STAFF_CITIZEN_IDENTITY_DATE,
                    STAFF_CITIZEN_CARD_PLACE = item.TOUR_STAFF_CITIZEN_IDENTITY_PLACE,
                    STAFF_ADDRESS = item.TOUR_STAFF_ADDRESS,
                    STAFF_START_DATE = (DateTime)item.TOUR_STAFF_START_DATE,
                    STAFF_PHONE_NUMBER = item.TOUR_STAFF_PHONE_NUMBER,
                    STAFF_ACADEMIC_LEVEL = item.TOUR_STAFF_ACADEMIC_LEVEL,
                    STAFF_EMAIL = item.TOUR_STAFF_EMAIL,
                    STAFF_NOTE = item.TOUR_STAFF_NOTE,
                    STAFF_IMAGE_BYTE_SOURCE = item.TOUR_STAFF_IMAGE
                };
                staffModel.STAFF_STRING_BIRTH_DATE = staffModel.STAFF_BIRTH_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_START_DATE = staffModel.STAFF_START_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_CITIZEN_CARD_DATE = staffModel.STAFF_CITIZEN_CARD_DATE.ToString("dd/MM/yyyy");

                StaffList.Add(staffModel);
                Refresh_StaffList.Add(staffModel);
            }
        }

        private void LoadDataGridExceptDeleteStaffUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to StaffList, exclude all delete staff
             * Step 3: Initialize StaffList, Refresh_StaffList
             * Step 4: Insert staffList to StaffList and Refresh_StaffList
             */

            //Step 1
            if (CB_StaffSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_STAFF> staffList = from staff in db.TOUR_STAFF
                                               join staff_delete in db.TOUR_STAFF_DELETE on staff.TOUR_STAFF_ID equals staff_delete.TOUR_STAFF_ID
                                               where staff_delete.TOUR_STAFF_DELETE_ISDELETED == false
                                               select staff;

            //Step 3
            StaffList = new ObservableCollection<StaffModel>();
            Refresh_StaffList = new ObservableCollection<StaffModel>();

            //Step 4
            foreach (TOUR_STAFF item in staffList)
            {
                StaffModel staffModel = new StaffModel
                {
                    STAFF_ID = item.TOUR_STAFF_ID,
                    STAFF_NAME = item.TOUR_STAFF_NAME,
                    STAFF_ROLE = item.TOUR_STAFF_ROLE,
                    STAFF_BIRTH_DATE = (DateTime)item.TOUR_STAFF_BIRTH_DATE,
                    STAFF_BIRTH_PLACE = item.TOUR_STAFF_BIRTH_PLACE,
                    STAFF_GENDER = item.TOUR_STAFF_GENDER,
                    STAFF_CITIZEN_CARD = item.TOUR_STAFF_CITIZEN_IDENTITY,
                    STAFF_CITIZEN_CARD_DATE = (DateTime)item.TOUR_STAFF_CITIZEN_IDENTITY_DATE,
                    STAFF_CITIZEN_CARD_PLACE = item.TOUR_STAFF_CITIZEN_IDENTITY_PLACE,
                    STAFF_ADDRESS = item.TOUR_STAFF_ADDRESS,
                    STAFF_START_DATE = (DateTime)item.TOUR_STAFF_START_DATE,
                    STAFF_PHONE_NUMBER = item.TOUR_STAFF_PHONE_NUMBER,
                    STAFF_ACADEMIC_LEVEL = item.TOUR_STAFF_ACADEMIC_LEVEL,
                    STAFF_EMAIL = item.TOUR_STAFF_EMAIL,
                    STAFF_NOTE = item.TOUR_STAFF_NOTE,
                    STAFF_IMAGE_BYTE_SOURCE = item.TOUR_STAFF_IMAGE
                };
                staffModel.STAFF_STRING_BIRTH_DATE = staffModel.STAFF_BIRTH_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_START_DATE = staffModel.STAFF_START_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_CITIZEN_CARD_DATE = staffModel.STAFF_CITIZEN_CARD_DATE.ToString("dd/MM/yyyy");

                StaffList.Add(staffModel);
                Refresh_StaffList.Add(staffModel);
            }
        }

        /*
         * Load all command of View Model
         */
        private void LoadCommand()
        {
            #region Command of StaffUC

            AddStaffCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddStaffUC(User_ID));

            ShowStaffDetailCommand = new RelayCommand<ContentControl>(_ => Staff_Selected != null, p => p.Content = new DisplayStaffUC(Staff_Selected, User_ID));

            #endregion Command of StaffUC

            #region Command of AddStaffUC

            AddImageCommand = new RelayCommand<object>(_ => IsExcuteAddImageCommand(), _ => ExcuteAddImageCommand());

            ConfirmCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmCommand(), p =>
            {
                ExcuteConfirmCommand();
                p.Content = new StaffUC(User_ID);
            });

            CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new StaffUC(User_ID));

            #endregion Command of AddStaffUC

            #region Command of DisplayStaffUC

            ModifyImageCommand = new RelayCommand<object>(_ => true, _ => ExcuteAddImageCommand());

            SaveChangeCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangeCommand(), p =>
            {
                ExcuteSaveChangeCommand();
                p.Content = new StaffUC(User_ID);
            });

            ChangePasswordCommand = new RelayCommand<ContentControl>(_ => IsExcuteChangePasswordCommand(), p =>
            {
                ExcuteChangePasswordCommand();
                p.Content = new StaffUC(User_ID);
            });

            RemoveCommand = new RelayCommand<ContentControl>(_ => IsExcuteRemoveCommand(), p =>
            {
                ExcuteRemoveCommand();
                p.Content = new StaffUC(User_ID);
            });

            #endregion Command of DisplayStaffUC
        }

        /*
         * Create list in ComboBox of StaffUC
         */
        private void LoadStaffComboBox()
        {
            CB_StaffList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Role", false),
                new ComboBoxModel("Gender", false),
                new ComboBoxModel("Citizen Identity", false),
                new ComboBoxModel("Address", false),
                new ComboBoxModel("Phone Number", false)
            };
            CB_StaffSelected = CB_StaffList.FirstOrDefault(x => x.IsSelected);
        }

        #endregion All Load Function

        #region AddStaffUC View Model

        /*
         * Function IsExcute and Excute of AddImageCommand
         * IsExcute = true when image in button is empty
         * Excute is save image to database and system
         */
        private bool IsExcuteAddImageCommand()
        {
            return Staff_Image_Byte_Source == null;
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

        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddStaffUC except Note is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmCommand()
        {
            if (string.IsNullOrEmpty(Staff_Name) ||
                string.IsNullOrEmpty(Staff_Role) ||
                string.IsNullOrEmpty(Staff_ID_Card) ||
                string.IsNullOrEmpty(Staff_ID_Card_Place) ||
                string.IsNullOrEmpty(Staff_ID_Card) ||
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

            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            var user = db.TOUR_ACCOUNT.Where(x => x.TOUR_ACCOUNT_NAME == Staff_Username);
            return user?.Count() == 0;
        }

        private void ExcuteConfirmCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Staff to Tour_Staff in database
             * Step 3: Get ID new Staff and Add new Account from this ID
             * Step 4: Get ID new Staff and Add new Delete because when delete just enable the staff
             * Step 5: Get ID new Staff and Add new Record
             * Step 6: Save Database
             * Step 7: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            TOUR_STAFF tour_staff = new TOUR_STAFF()
            {
                TOUR_STAFF_NAME = Staff_Name,
                TOUR_STAFF_ROLE = Staff_Role,
                TOUR_STAFF_CITIZEN_IDENTITY = Staff_ID_Card,
                TOUR_STAFF_CITIZEN_IDENTITY_DATE = Staff_ID_Card_Date,
                TOUR_STAFF_CITIZEN_IDENTITY_PLACE = Staff_ID_Card_Place,
                TOUR_STAFF_BIRTH_DATE = Staff_Birthday,
                TOUR_STAFF_BIRTH_PLACE = Staff_Birth_Place,
                TOUR_STAFF_GENDER = Staff_Gender,
                TOUR_STAFF_ADDRESS = Staff_Address,
                TOUR_STAFF_ACADEMIC_LEVEL = Staff_Academic_Level,
                TOUR_STAFF_PHONE_NUMBER = Staff_Phone_Number,
                TOUR_STAFF_EMAIL = Staff_Email,
                TOUR_STAFF_NOTE = string.IsNullOrEmpty(Staff_Note) ? "" : Staff_Note,
                TOUR_STAFF_START_DATE = DateTime.Now,
                TOUR_STAFF_IMAGE = Staff_Image_Byte_Source
            };
            db.TOUR_STAFF.Add(tour_staff);

            //Step 3
            TOUR_ACCOUNT tour_account = new TOUR_ACCOUNT()
            {
                TOUR_STAFF_ID = tour_staff.TOUR_STAFF_ID,
                TOUR_ACCOUNT_NAME = Staff_Username,
                TOUR_ACCOUNT_PASSWORD = GlobalFunction.CreateMD5(GlobalFunction.Base64Encode(Staff_Password))
            };
            db.TOUR_ACCOUNT.Add(tour_account);

            //Step 4
            TOUR_STAFF_DELETE tour_staff_delete = new TOUR_STAFF_DELETE()
            {
                TOUR_STAFF_ID = tour_staff.TOUR_STAFF_ID,
                TOUR_STAFF_DELETE_ISDELETED = false
            };
            db.TOUR_STAFF_DELETE.Add(tour_staff_delete);

            //Step 5
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new staff with Name: " + Staff_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 6
            db.SaveChanges();

            //Step 7
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion AddStaffUC View Model

        #region DisplayStaffUC View Model

        /*
         * Function IsExcute and Excute of SaveChangeCommand
         * IsExcute = true when any textbox in DisplayStaffUC
         * Excute is save all textbox to database
         */
        private bool IsExcuteSaveChangeCommand()
        {
            Staff_String_Birthday = Staff_Birthday.ToString("dd/MM/yyyy");
            Staff_String_ID_Card_Date = Staff_ID_Card_Date.ToString("dd/MM/yyyy");
            if (Staff_Name != Staff_Selected.STAFF_NAME)
            {
                return true;
            }
            if (Staff_Role != Staff_Selected.STAFF_ROLE)
            {
                return true;
            }
            if (Staff_ID_Card != Staff_Selected.STAFF_CITIZEN_CARD)
            {
                return true;
            }
            if (Staff_String_ID_Card_Date != Staff_Selected.STAFF_STRING_CITIZEN_CARD_DATE)
            {
                return true;
            }
            if (Staff_ID_Card_Place != Staff_Selected.STAFF_CITIZEN_CARD_PLACE)
            {
                return true;
            }
            if (Staff_String_Birthday != Staff_Selected.STAFF_STRING_BIRTH_DATE)
            {
                return true;
            }
            if (Staff_Birth_Place != Staff_Selected.STAFF_BIRTH_PLACE)
            {
                return true;
            }
            if (Staff_Gender != Staff_Selected.STAFF_GENDER)
            {
                return true;
            }
            if (Staff_Address != Staff_Selected.STAFF_ADDRESS)
            {
                return true;
            }
            if (Staff_Academic_Level != Staff_Selected.STAFF_ACADEMIC_LEVEL)
            {
                return true;
            }
            if (Staff_Email != Staff_Selected.STAFF_EMAIL)
            {
                return true;
            }
            if (Staff_Phone_Number != Staff_Selected.STAFF_PHONE_NUMBER)
            {
                return true;
            }
            //Note: If Update Image will update note
            if (Staff_Note != Staff_Selected.STAFF_NOTE)
            {
                return true;
            }
            if (Staff_Image_Byte_Source != Staff_Selected.STAFF_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            return false;
        }

        private void ExcuteSaveChangeCommand()
        {
            try
            {
                Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

                TOUR_STAFF tour_staff = db.TOUR_STAFF.Where(x => x.TOUR_STAFF_ID == Staff_Selected.STAFF_ID).SingleOrDefault();
                tour_staff.TOUR_STAFF_NAME = Staff_Name;
                tour_staff.TOUR_STAFF_ROLE = Staff_Role;
                tour_staff.TOUR_STAFF_CITIZEN_IDENTITY = Staff_ID_Card;
                tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE = Staff_ID_Card_Date;
                tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE = Staff_ID_Card_Place;
                tour_staff.TOUR_STAFF_BIRTH_DATE = Staff_Birthday;
                tour_staff.TOUR_STAFF_BIRTH_PLACE = Staff_Birth_Place;
                tour_staff.TOUR_STAFF_ADDRESS = Staff_Address;
                tour_staff.TOUR_STAFF_GENDER = Staff_Gender;
                tour_staff.TOUR_STAFF_ACADEMIC_LEVEL = Staff_Academic_Level;
                tour_staff.TOUR_STAFF_EMAIL = Staff_Email;
                tour_staff.TOUR_STAFF_PHONE_NUMBER = Staff_Phone_Number;
                tour_staff.TOUR_STAFF_NOTE = Staff_Note;
                tour_staff.TOUR_STAFF_IMAGE = Staff_Image_Byte_Source;

                string changeToSave = "";
                int countChangeToSave = 0;
                Staff_String_Birthday = Staff_Birthday.ToString("dd/MM/yyyy");
                Staff_String_ID_Card_Date = Staff_ID_Card_Date.ToString("dd/MM/yyyy");
                if (Staff_Name != Staff_Selected.STAFF_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Staff_Selected.STAFF_NAME, Staff_Name);
                    countChangeToSave++;
                }
                if (Staff_Role != Staff_Selected.STAFF_ROLE)
                {
                    changeToSave += string.Format("Role Change ({0} -> {1})   ", Staff_Selected.STAFF_ROLE, Staff_Role);
                    countChangeToSave++;
                }
                if (Staff_ID_Card != Staff_Selected.STAFF_CITIZEN_CARD)
                {
                    changeToSave += string.Format("ID-Card Change ({0} -> {1})   ", Staff_Selected.STAFF_CITIZEN_CARD, Staff_ID_Card);
                    countChangeToSave++;
                }
                if (Staff_String_ID_Card_Date != Staff_Selected.STAFF_STRING_CITIZEN_CARD_DATE)
                {
                    changeToSave += string.Format("ID-Card Date Change ({0} -> {1})   ", Staff_Selected.STAFF_STRING_CITIZEN_CARD_DATE, Staff_String_ID_Card_Date);
                    countChangeToSave++;
                }
                if (Staff_ID_Card_Place != Staff_Selected.STAFF_CITIZEN_CARD_PLACE)
                {
                    changeToSave += string.Format("ID-Card Place Change ({0} -> {1})   ", Staff_Selected.STAFF_CITIZEN_CARD_PLACE, Staff_ID_Card_Place);
                    countChangeToSave++;
                }
                if (Staff_String_Birthday != Staff_Selected.STAFF_STRING_BIRTH_DATE)
                {
                    changeToSave += string.Format("Birthday Change ({0} -> {1})   ", Staff_Selected.STAFF_STRING_BIRTH_DATE, Staff_String_Birthday);
                    countChangeToSave++;
                }
                if (Staff_Birth_Place != Staff_Selected.STAFF_BIRTH_PLACE)
                {
                    changeToSave += string.Format("Birth Place Change ({0} -> {1})   ", Staff_Selected.STAFF_BIRTH_PLACE, Staff_Birth_Place);
                    countChangeToSave++;
                }
                if (Staff_Gender != Staff_Selected.STAFF_GENDER)
                {
                    changeToSave += string.Format("Gender Change ({0} -> {1})   ", Staff_Selected.STAFF_GENDER, Staff_Gender);
                    countChangeToSave++;
                }
                if (Staff_Address != Staff_Selected.STAFF_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", Staff_Selected.STAFF_ADDRESS, Staff_Address);
                    countChangeToSave++;
                }
                if (Staff_Academic_Level != Staff_Selected.STAFF_ACADEMIC_LEVEL)
                {
                    changeToSave += string.Format("Academic Level Change ({0} -> {1})   ", Staff_Selected.STAFF_ACADEMIC_LEVEL, Staff_Academic_Level);
                    countChangeToSave++;
                }
                if (Staff_Email != Staff_Selected.STAFF_EMAIL)
                {
                    changeToSave += string.Format("Email Change ({0} -> {1})   ", Staff_Selected.STAFF_EMAIL, Staff_Email);
                    countChangeToSave++;
                }
                if (Staff_Phone_Number != Staff_Selected.STAFF_PHONE_NUMBER)
                {
                    changeToSave += string.Format("Phone Number Change ({0} -> {1})   ", Staff_Selected.STAFF_PHONE_NUMBER, Staff_Phone_Number);
                    countChangeToSave++;
                }
                if (Staff_Note != Staff_Selected.STAFF_NOTE)
                {
                    changeToSave += string.Format("Note Change ({0} -> {1})   ", Staff_Selected.STAFF_NOTE, Staff_Note);
                    countChangeToSave++;
                }
                if (Staff_Image_Byte_Source != Staff_Selected.STAFF_IMAGE_BYTE_SOURCE)
                {
                    changeToSave += string.Format("Image Change");
                    countChangeToSave++;
                }

                if (countChangeToSave != 0)
                {
                    TOUR_RECORD tour_record = new TOUR_RECORD
                    {
                        TOUR_STAFF_ID = User_ID,
                        TOUR_RECORD_DATE = DateTime.Now,
                        TOUR_RECORD_CONTENT = changeToSave
                    };
                    db.TOUR_RECORD.Add(tour_record);
                    MessageBox.Show("Changed successfully");
                }
                else
                {
                    MessageBox.Show("There is nothing to change!");
                }

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        /*
         * Function IsExcute and Excute of ChangePasswordCommand
         * IsExcute = true when password textbox is fill
         * Excute is save new password to database
         */
        private bool IsExcuteChangePasswordCommand()
        {
            return Staff_Password != null;
        }

        private void ExcuteChangePasswordCommand()
        {
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            TOUR_ACCOUNT tour_account = db.TOUR_ACCOUNT.Where(x => x.TOUR_STAFF_ID == Staff_Selected.STAFF_ID).SingleOrDefault();
            tour_account.TOUR_ACCOUNT_PASSWORD = GlobalFunction.CreateMD5(GlobalFunction.Base64Encode(Staff_Password));
            db.SaveChanges();
        }

        /*
         * Function IsExcute and Excute of RemoveCommand
         * IsExcute = true when remove note is filled
         * Excute is this staff will enable to database
         */
        private bool IsExcuteRemoveCommand()
        {
            return !string.IsNullOrEmpty(Staff_Note_Remove);
        }

        private void ExcuteRemoveCommand()
        {
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            TOUR_STAFF_DELETE tour_staff_delete = db.TOUR_STAFF_DELETE.Where(x => x.TOUR_STAFF_ID == Staff_Selected.STAFF_ID).SingleOrDefault();
            tour_staff_delete.TOUR_STAFF_DELETE_ISDELETED = true;
            tour_staff_delete.TOUR_STAFF_DELETE_DATE = DateTime.Now;
            tour_staff_delete.TOUR_STAFF_DELETE_CONTENT = Staff_Note_Remove;

            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = string.Format("Remove Staff {0} with {1} because {2}", Staff_Name, Staff_Selected.STAFF_ID, Staff_Note_Remove)
            };
            db.TOUR_RECORD.Add(tour_record);
            db.SaveChanges();
            MessageBox.Show("Removed successfully");
        }
        #endregion DisplayStaffUC View Model
    }
}
