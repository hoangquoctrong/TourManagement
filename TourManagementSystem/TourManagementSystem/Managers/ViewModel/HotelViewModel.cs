using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Managers.Model;
using TourManagementSystem.Managers.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.ViewModel
{
    public class HotelViewModel : BaseViewModel
    {
        #region Data
        #region Data Binding Of HotelUC

        /*
        * User is manager of system
        */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        /*
         * HotelList binding ItemSource on DataGridView of HotelUC
         */
        private ObservableCollection<HotelModel> _HotelList;
        public ObservableCollection<HotelModel> HotelList { get => _HotelList; set { _HotelList = value; OnPropertyChanged(); } }

        /*
         * Refresh_HotelList is refresh HotelList
         */
        private ObservableCollection<HotelModel> _Refresh_HotelList;
        public ObservableCollection<HotelModel> Refresh_HotelList { get => _Refresh_HotelList; set { _Refresh_HotelList = value; OnPropertyChanged(); } }

        /*
         * Hotel_Selected binding SelectedItem on DataGridView of HotelUC
         */
        private HotelModel _Hotel_Selected;
        public HotelModel Hotel_Selected
        {
            get => _Hotel_Selected;
            set
            {
                _Hotel_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display HotelUC
                 */
                if (Hotel_Selected != null)
                {
                    Hotel_Name = Hotel_Selected.HOTEL_NAME;
                    Hotel_Address = Hotel_Selected.HOTEL_ADDRESS;
                    Hotel_Phone_Number = Hotel_Selected.HOTEL_PHONE_NUMBER;
                    Hotel_Type = Hotel_Selected.HOTEL_TYPE;
                    Hotel_Description = Hotel_Selected.HOTEL_DESCRIPTION;
                    Hotel_Email = Hotel_Selected.HOTEL_EMAIL;
                    Hotel_Price = Hotel_Selected.HOTEL_PRICE;
                    Hotel_Is_Restaurant = Hotel_Selected.HOTEL_IS_RESTAURANT;
                    Hotel_Image_Byte_Source = Hotel_Selected.HOTEL_IMAGE_BYTE_SOURCE;
                    Hotel_Image_Source = Hotel_Image_Byte_Source == null
                        ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Hotel.png", UriKind.Absolute))
                        : GlobalFunction.ToImage(Hotel_Image_Byte_Source);
                }
            }
        }

        /*
        * Search_Text binding to Search button of HotelUC
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
                 * Step 1: Refresh HotelList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                HotelList = Refresh_HotelList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Text))
                {
                    //Step 3
                    if (CB_HotelSelected != null)
                    {
                        //Step 4
                        switch (CB_HotelSelected.CB_Name)
                        {
                            case "Name":
                                HotelList = new ObservableCollection<HotelModel>(HotelList.Where(x => x.HOTEL_NAME.Contains(Search_Text) ||
                                                                                                        x.HOTEL_NAME.ToLower().Contains(Search_Text) ||
                                                                                                        x.HOTEL_NAME.ToUpper().Contains(Search_Text)));
                                break;
                            case "Phone_Number":
                                HotelList = new ObservableCollection<HotelModel>(HotelList.Where(x => x.HOTEL_PHONE_NUMBER.Contains(Search_Text) ||
                                                                                                       x.HOTEL_PHONE_NUMBER.ToLower().Contains(Search_Text) ||
                                                                                                       x.HOTEL_PHONE_NUMBER.ToUpper().Contains(Search_Text)));
                                break;
                            case "Email":
                                HotelList = new ObservableCollection<HotelModel>(HotelList.Where(x => x.HOTEL_EMAIL.Contains(Search_Text) ||
                                                                                                        x.HOTEL_EMAIL.ToLower().Contains(Search_Text) ||
                                                                                                        x.HOTEL_EMAIL.ToUpper().Contains(Search_Text)));
                                break;
                            case "Status":
                                HotelList = new ObservableCollection<HotelModel>(HotelList.Where(x => x.HOTEL_TYPE.Contains(Search_Text) ||
                                                                                                        x.HOTEL_TYPE.ToLower().Contains(Search_Text) ||
                                                                                                        x.HOTEL_TYPE.ToUpper().Contains(Search_Text)));
                                break;
                            case "Address":
                                HotelList = new ObservableCollection<HotelModel>(HotelList.Where(x => x.HOTEL_ADDRESS.Contains(Search_Text) ||
                                                                                                        x.HOTEL_ADDRESS.ToLower().Contains(Search_Text) ||
                                                                                                        x.HOTEL_ADDRESS.ToUpper().Contains(Search_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_HotelList binding ItemSource of ComboBox of HotelUC
         */
        private ObservableCollection<ComboBoxModel> _CB_HotelList;
        public ObservableCollection<ComboBoxModel> CB_HotelList { get => _CB_HotelList; set { _CB_HotelList = value; OnPropertyChanged(); } }

        /*
         * CB_HotelSelected binding SelectedValue of ComboBox of HotelUC
         */
        private ComboBoxModel _CB_HotelSelected;
        public ComboBoxModel CB_HotelSelected { get => _CB_HotelSelected; set { _CB_HotelSelected = value; OnPropertyChanged(); } }

        /*
         * Checkbox_DisplayAllHotel binding Checked of Checkbox of HotelUC
         */
        private bool _Checkbox_DisplayAllHotel;
        public bool Checkbox_DisplayAllHotel
        {
            get => _Checkbox_DisplayAllHotel;
            set
            {
                _Checkbox_DisplayAllHotel = value;
                OnPropertyChanged();

                if (Checkbox_DisplayAllHotel)
                {
                    LoadDataGridHotelUC();
                }
                else
                {
                    LoadDataGridExceptDeleteHotelUC();
                }
            }
        }

        #endregion Data Binding Of HotelUC

        #region Data Binding of AddStaffUC, DisplayStaffUC

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

        #endregion Data Binding of AddHotelUC, DisplayHotelUC
        #endregion Data

        #region Command

        #region Command of HotelUC
        /*
         * AddHotelCommand is a command which will change present content control to AddHotelUC
         */
        public ICommand AddHotelCommand { get; set; }

        /*
         * ShowHotelDetailCommand is a command which will show detail about the Hotel is clicked in DataGrid in HotelUC
         */
        public ICommand ShowHotelDetailCommand { get; set; }

        #endregion Command of HotelUC

        #region Command of AddHotelUC

        /*
         * ConfirmCommand is a command which will save information above to database in AddHotelUC
         */
        public ICommand ConfirmCommand { get; set; }

        /*
         * CancelCommand is a command which will back to HotelUC
         */
        public ICommand CancelCommand { get; set; }

        /*
         * AddImageCommand is a command which will add image from computer to system
         */
        public ICommand AddImageCommand { get; set; }

        #endregion Command of AddHotelUC

        #region Command of DisplayHotelUC
        /*
         * ModifyImageCommand is a command which will update image from computer to system
         */
        public ICommand ModifyImageCommand { get; set; }

        /*
         * SaveChangeCommand is a command which will save all change in Tab Information to database in DisplayHotelUC
         */
        public ICommand SaveChangeCommand { get; set; }

        /*
         * RemoveCommand is a command which will remove account in Tab Delete in database in DisplayHotelUC
         */
        public ICommand RemoveCommand { get; set; }
        #endregion Command of DisplayHotelUC

        #endregion Command

        /*
         * Constructor for HotelUC, AddHotelUC
         */
        public HotelViewModel(int user_id)
        {
            User_ID = user_id;
            LoadHotelUC();

        }

        /*
         * Constructor for HotelDisplayUC
         */
        public HotelViewModel(HotelModel hotelSelected, int user_id)
        {
            User_ID = user_id;
            Hotel_Selected = hotelSelected;
            LoadCommand();
            LoadPlaceCombobox();
        }

        #region All Load Function

        /*
        * Gather all Load into one function
        */
        private void LoadHotelUC()
        {
            LoadHotelComboBox();
            LoadPlaceCombobox();
            Checkbox_DisplayAllHotel = false;
            LoadCommand();
            Hotel_Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Hotel.png", UriKind.Absolute));
        }

        /* 
         * Load data from database to DataGridView
         * Reload data when back from detailed to global 
         */
        private void LoadDataGridHotelUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to HotelList
             * Step 3: Initialize HotelList, Refresh_HotelList
             * Step 4: Insert hotelList to HotelList and Refresh_HotelList
             */

            //Step 1
            if (CB_HotelSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_HOTEL> hotelList = from hotel in db.TOUR_HOTEL
                                               select hotel;

            //Step 3
            HotelList = new ObservableCollection<HotelModel>();
            Refresh_HotelList = new ObservableCollection<HotelModel>();

            //Step 4
            foreach (TOUR_HOTEL item in hotelList)
            {
                HotelModel hotelModel = new HotelModel
                {
                    HOTEL_ID = item.TOUR_HOTEL_ID,
                    HOTEL_NAME = item.TOUR_HOTEL_NAME,
                    HOTEL_ADDRESS = item.TOUR_HOTEL_ADDRESS,
                    HOTEL_PHONE_NUMBER = item.TOUR_HOTEL_PHONE_NUMBER,
                    HOTEL_DESCRIPTION = item.TOUR_HOTEL_DESCRIPTION,
                    HOTEL_TYPE = item.TOUR_HOTEL_TYPE,
                    HOTEL_IS_RESTAURANT = item.TOUR_HOTEL_IS_RESTAURANT,
                    HOTEL_PRICE = (float)item.TOUR_HOTEL_PRICE,
                    HOTEL_EMAIL = item.TOUR_HOTEL_EMAIL,
                    HOTEL_IMAGE_BYTE_SOURCE = item.TOUR_HOTEL_IMAGE,
                    PLACE_ID = item.PLACE_ID
                };

                var place_name = from hotel in db.TOUR_HOTEL
                                 join place in db.PLACE on hotel.PLACE_ID equals place.PLACE_ID
                                 select place.PLACE_NAME;
                hotelModel.PLACE_NAME = place_name.ToString();

                HotelList.Add(hotelModel);
                Refresh_HotelList.Add(hotelModel);
            }
        }

        private void LoadDataGridExceptDeleteHotelUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to HotelList, exclude all delete hotel
             * Step 3: Initialize HotelList, Refresh_HotelList
             * Step 4: Insert hotelList to HotelList and Refresh_HotelList
             */

            //Step 1
            if (CB_HotelSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_HOTEL> hotelList = from hotel in db.TOUR_HOTEL
                                               join hotel_delete in db.TOUR_HOTEL_DELETE on hotel.TOUR_HOTEL_ID equals hotel_delete.TOUR_HOTEL_ID
                                               where hotel_delete.TOUR_HOTEL_DELETE_ISDELETED == false
                                               select hotel;

            //Step 3
            HotelList = new ObservableCollection<HotelModel>();
            Refresh_HotelList = new ObservableCollection<HotelModel>();

            //Step 4
            foreach (TOUR_HOTEL item in hotelList)
            {
                HotelModel hotelModel = new HotelModel
                {
                    HOTEL_ID = item.TOUR_HOTEL_ID,
                    HOTEL_NAME = item.TOUR_HOTEL_NAME,
                    HOTEL_ADDRESS = item.TOUR_HOTEL_ADDRESS,
                    HOTEL_PHONE_NUMBER = item.TOUR_HOTEL_PHONE_NUMBER,
                    HOTEL_DESCRIPTION = item.TOUR_HOTEL_DESCRIPTION,
                    HOTEL_IS_RESTAURANT = item.TOUR_HOTEL_IS_RESTAURANT,
                    HOTEL_PRICE = (float)item.TOUR_HOTEL_PRICE,
                    HOTEL_EMAIL = item.TOUR_HOTEL_EMAIL,
                    HOTEL_TYPE = item.TOUR_HOTEL_TYPE,
                    HOTEL_IMAGE_BYTE_SOURCE = item.TOUR_HOTEL_IMAGE,
                    PLACE_ID = item.PLACE_ID
                };

                var place_name = from hotel in db.TOUR_HOTEL
                                 join place in db.PLACE on hotel.PLACE_ID equals place.PLACE_ID
                                 select place.PLACE_NAME;
                hotelModel.PLACE_NAME = place_name.ToString();

                HotelList.Add(hotelModel);
                Refresh_HotelList.Add(hotelModel);
            }
        }

        /*
         * Load all command of View Model
         */
        private void LoadCommand()
        {
            #region Command of HotelUC

            AddHotelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddHotelUC(User_ID));

            ShowHotelDetailCommand = new RelayCommand<ContentControl>(_ => Hotel_Selected != null, p => p.Content = new DisplayHotelUC(Hotel_Selected, User_ID));

            #endregion Command of HotelUC

            #region Command of AddHotelUC

            AddImageCommand = new RelayCommand<object>(_ => IsExcuteAddImageCommand(), _ => ExcuteAddImageCommand());

            ConfirmCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmCommand(), p =>
            {
                ExcuteConfirmCommand();
                p.Content = new HotelUC(User_ID);
            });

            CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new HotelUC(User_ID));

            #endregion Command of AddHotelUC

            #region Command of DisplayHotelUC

            ModifyImageCommand = new RelayCommand<object>(_ => true, _ => ExcuteAddImageCommand());

            SaveChangeCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangeCommand(), p =>
            {
                ExcuteSaveChangeCommand();
                p.Content = new HotelUC(User_ID);
            });

            RemoveCommand = new RelayCommand<ContentControl>(_ => IsExcuteRemoveCommand(), p =>
            {
                ExcuteRemoveCommand();
                p.Content = new HotelUC(User_ID);
            });

            #endregion Command of DisplayHotelUC
        }

        /*
         * Create list in ComboBox of HotelUC
         */
        private void LoadHotelComboBox()
        {
            CB_HotelList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("License Plate", false),
                new ComboBoxModel("Phone_Number", false),
                new ComboBoxModel("Status", false)
            };
            CB_HotelSelected = CB_HotelList.FirstOrDefault(x => x.IsSelected);
        }
        #endregion All Load Function

        #region AddHotelUC View Model

        private void LoadPlaceCombobox()
        {
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            var list = from place in db.PLACE select place;

            CB_PlaceList = new ObservableCollection<ComboBoxModel>();

            foreach (var item in list)
            {
                ComboBoxModel cbm = new ComboBoxModel(item.PLACE_NAME, item.PLACE_ID, false);

                CB_PlaceList.Add(cbm);
            }

            CB_PlaceSelected = CB_PlaceList.FirstOrDefault(x =>
            {
                if (Hotel_Selected == null)
                    return x.IsSelected;
                else
                    return x.CB_ID == Hotel_Selected.PLACE_ID;
            });
        }

        /*
        * Function IsExcute and Excute of AddImageCommand
        * IsExcute = true when image in button is empty
        * Excute is save image to database and system
        */
        private bool IsExcuteAddImageCommand()
        {
            return Hotel_Image_Byte_Source == null;
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

        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddHotelUC except Descritption is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmCommand()
        {
            if (string.IsNullOrEmpty(Hotel_Name) ||
                string.IsNullOrEmpty(Hotel_Phone_Number) ||
                string.IsNullOrEmpty(Hotel_Address) ||
                string.IsNullOrEmpty(Hotel_Type) ||
                string.IsNullOrEmpty(Hotel_Email) ||
                CB_PlaceSelected == null)
            {
                return false;
            }

            return true;
        }

        private void ExcuteConfirmCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Hotel to Tour_Hotel in database
             * Step 3: Get ID new Hotel and Add new Delete because when delete just enable the Hotel
             * Step 4: Get ID new Hotel and Add new Record
             * Step 5: Save Database
             * Step 6: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            TOUR_HOTEL tour_hotel = new TOUR_HOTEL()
            {
                TOUR_HOTEL_NAME = Hotel_Name,
                TOUR_HOTEL_PHONE_NUMBER = Hotel_Phone_Number,
                TOUR_HOTEL_ADDRESS = Hotel_Address,
                TOUR_HOTEL_TYPE = Hotel_Type,
                TOUR_HOTEL_EMAIL = Hotel_Email,
                TOUR_HOTEL_IS_RESTAURANT = Hotel_Is_Restaurant,
                TOUR_HOTEL_PRICE = Hotel_Price,
                TOUR_HOTEL_DESCRIPTION = string.IsNullOrEmpty(_Hotel_Description) ? "" : Hotel_Description,
                TOUR_HOTEL_IMAGE = Hotel_Image_Byte_Source,
                PLACE_ID = CB_PlaceSelected.CB_ID
            };
            db.TOUR_HOTEL.Add(tour_hotel);

            //Step 3
            TOUR_HOTEL_DELETE tour_hotel_delete = new TOUR_HOTEL_DELETE()
            {
                TOUR_HOTEL_ID = tour_hotel.TOUR_HOTEL_ID,
                TOUR_HOTEL_DELETE_ISDELETED = false
            };
            db.TOUR_HOTEL_DELETE.Add(tour_hotel_delete);

            //Step 4
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new hotel with Name: " + Hotel_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 5
            db.SaveChanges();

            //Step 6
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion AddHotelUC View Model

        #region DisplayHotelUC View Model

        /*
        * Function IsExcute and Excute of SaveChangeCommand
        * IsExcute = true when any textbox in DisplayHotelUC
        * Excute is save all textbox to database
        */
        private bool IsExcuteSaveChangeCommand()
        {
            if (Hotel_Name != Hotel_Selected.HOTEL_NAME)
            {
                return true;
            }
            if (Hotel_Address != Hotel_Selected.HOTEL_ADDRESS)
            {
                return true;
            }
            if (Hotel_Phone_Number != Hotel_Selected.HOTEL_PHONE_NUMBER)
            {
                return true;
            }
            if (Hotel_Type != Hotel_Selected.HOTEL_TYPE)
            {
                return true;
            }
            if (Hotel_Email != Hotel_Selected.HOTEL_EMAIL)
            {
                return true;
            }
            if (Hotel_Description != Hotel_Selected.HOTEL_DESCRIPTION)
            {
                return true;
            }
            if (Hotel_Is_Restaurant != Hotel_Selected.HOTEL_IS_RESTAURANT)
            {
                return true;
            }
            if (Hotel_Price != Hotel_Selected.HOTEL_PRICE)
            {
                return true;
            }
            if (Hotel_Image_Byte_Source != Hotel_Selected.HOTEL_IMAGE_BYTE_SOURCE)
            {
                return true;
            }
            if (CB_PlaceSelected.CB_ID != Hotel_Selected.PLACE_ID)
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

                TOUR_HOTEL tour_hotel = db.TOUR_HOTEL.Where(x => x.TOUR_HOTEL_ID == Hotel_Selected.HOTEL_ID).SingleOrDefault();
                tour_hotel.TOUR_HOTEL_NAME = Hotel_Name;
                tour_hotel.TOUR_HOTEL_ADDRESS = Hotel_Address;
                tour_hotel.TOUR_HOTEL_PHONE_NUMBER = Hotel_Phone_Number;
                tour_hotel.TOUR_HOTEL_TYPE = Hotel_Type;
                tour_hotel.TOUR_HOTEL_EMAIL = Hotel_Email;
                tour_hotel.TOUR_HOTEL_IS_RESTAURANT = Hotel_Is_Restaurant;
                tour_hotel.TOUR_HOTEL_PRICE = Hotel_Price;
                tour_hotel.TOUR_HOTEL_DESCRIPTION = Hotel_Description;
                tour_hotel.TOUR_HOTEL_IMAGE = Hotel_Image_Byte_Source;
                tour_hotel.PLACE_ID = CB_PlaceSelected.CB_ID;

                string changeToSave = "";
                int countChangeToSave = 0;
                if (Hotel_Name != Hotel_Selected.HOTEL_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Hotel_Selected.HOTEL_NAME, Hotel_Name);
                    countChangeToSave++;
                }
                if (Hotel_Address != Hotel_Selected.HOTEL_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", Hotel_Selected.HOTEL_ADDRESS, Hotel_Address);
                    countChangeToSave++;
                }
                if (Hotel_Phone_Number != Hotel_Selected.HOTEL_PHONE_NUMBER)
                {
                    changeToSave += string.Format("Phone Number Change ({0} -> {1})   ", Hotel_Selected.HOTEL_PHONE_NUMBER, Hotel_Phone_Number);
                    countChangeToSave++;
                }
                if (Hotel_Type != Hotel_Selected.HOTEL_TYPE)
                {
                    changeToSave += string.Format("Type Change ({0} -> {1})   ", Hotel_Selected.HOTEL_TYPE, Hotel_Type);
                    countChangeToSave++;
                }
                if (Hotel_Email != Hotel_Selected.HOTEL_EMAIL)
                {
                    changeToSave += string.Format("Email Change ({0} -> {1})   ", Hotel_Selected.HOTEL_EMAIL, Hotel_Email);
                    countChangeToSave++;
                }
                if (Hotel_Description != Hotel_Selected.HOTEL_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", Hotel_Selected.HOTEL_DESCRIPTION, Hotel_Description);
                    countChangeToSave++;
                }
                if (Hotel_Is_Restaurant != Hotel_Selected.HOTEL_IS_RESTAURANT)
                {
                    changeToSave += string.Format("Hotel has restaurant Change ({0} -> {1})   ", Hotel_Selected.HOTEL_IS_RESTAURANT, Hotel_Is_Restaurant);
                    countChangeToSave++;
                }
                if (Hotel_Price != Hotel_Selected.HOTEL_PRICE)
                {
                    changeToSave += string.Format("Price Change ({0} -> {1})   ", Hotel_Selected.HOTEL_PRICE, Hotel_Price);
                    countChangeToSave++;
                }
                if (Hotel_Image_Byte_Source != Hotel_Selected.HOTEL_IMAGE_BYTE_SOURCE)
                {
                    changeToSave += string.Format("Image Change");
                    countChangeToSave++;
                }
                if (CB_HotelSelected.CB_ID != Hotel_Selected.PLACE_ID)
                {
                    changeToSave += string.Format("Place Change ({0} -> {1})    ", Hotel_Selected.PLACE_NAME, CB_HotelSelected.CB_Name);
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
         * Function IsExcute and Excute of RemoveCommand
         * IsExcute = true when remove note is filled
         * Excute is this staff will enable to database
         */
        private bool IsExcuteRemoveCommand()
        {
            return !string.IsNullOrEmpty(Hotel_Note_Remove);
        }

        private void ExcuteRemoveCommand()
        {
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            TOUR_HOTEL_DELETE tour_hotel_delete = db.TOUR_HOTEL_DELETE.Where(x => x.TOUR_HOTEL_ID == Hotel_Selected.HOTEL_ID).SingleOrDefault();
            tour_hotel_delete.TOUR_HOTEL_DELETE_ISDELETED = true;
            tour_hotel_delete.TOUR_HOTEL_DELETE_DATE = DateTime.Now;
            tour_hotel_delete.TOUR_HOTEL_DELETE_CONTENT = Hotel_Note_Remove;

            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = string.Format("Remove Hotel {0} with {1} because {2}", Hotel_Name, Hotel_Selected.HOTEL_ID, Hotel_Note_Remove)
            };
            db.TOUR_RECORD.Add(tour_record);
            db.SaveChanges();
            MessageBox.Show("Removed successfully");
        }
        #endregion DisplayHotelUC View Model
    }
}
