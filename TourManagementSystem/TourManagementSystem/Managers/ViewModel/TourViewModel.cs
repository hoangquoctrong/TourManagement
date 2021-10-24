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
using TourManagementSystem.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.ViewModel
{
    public class TourViewModel : BaseViewModel
    {

        /*
         * Constructor for TourUC, AddTourUC
         */
        public TourViewModel(int user_id)
        {
            User_ID = user_id;
            LoadTourUC();
        }

        /*
         * Constructor for TourDisplayUC
         */
        public TourViewModel(TourModel tourSelected, int user_id)
        {
            User_ID = user_id;
            Tour_Selected = tourSelected;
            LoadCommand();
        }

        #region Tour
        #region Data
        #region Data Binding for TourUC
        /*
        * User is manager of system
        */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        /*
         * TourList binding ItemSource on DataGridView of TourUC
         */
        private ObservableCollection<TourModel> _TourList;
        public ObservableCollection<TourModel> TourList { get => _TourList; set { _TourList = value; OnPropertyChanged(); } }

        /*
         * Refresh_TourList is refresh TourList
         */
        private ObservableCollection<TourModel> _Refresh_TourList;
        public ObservableCollection<TourModel> Refresh_TourList { get => _Refresh_TourList; set { _Refresh_TourList = value; OnPropertyChanged(); } }


        /*
         * Tour_Selected binding SelectedItem on DataGridView of TourUC
         */
        private TourModel _Tour_Selected;
        public TourModel Tour_Selected
        {
            get => _Tour_Selected;
            set
            {
                _Tour_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display TourUC
                 */
                if (Tour_Selected != null)
                {
                    Tour_Name = Tour_Selected.TOUR_NAME;
                    Tour_Type = Tour_Selected.TOUR_TYPE;
                    Tour_Is_Exist = Tour_Selected.TOUR_IS_EXIST;
                    Tour_Characteristic = Tour_Selected.TOUR_CHARACTERISTIS;
                    Tour_Image_Byte_Source = Tour_Selected.TOUR_IMAGE_BYTE_SOURCE;
                    Tour_Image_Source = Tour_Image_Byte_Source == null
                        ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Tour.png", UriKind.Absolute))
                        : GlobalFunction.ToImage(Tour_Image_Byte_Source);
                }
            }
        }


        /*
        * Search_Tour_Text binding to Search button of TourUC
        */
        private string _Search_Tour_Text;
        public string Search_Tour_Text
        {
            get => _Search_Tour_Text;
            set
            {
                _Search_Tour_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh TourList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                TourList = Refresh_TourList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Tour_Text))
                {
                    //Step 3
                    if (CB_TourSelected != null)
                    {
                        //Step 4
                        switch (CB_TourSelected.CB_Name)
                        {
                            case "Name":
                                TourList = new ObservableCollection<TourModel>(TourList.Where(x => x.TOUR_NAME.Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_NAME.ToLower().Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_NAME.ToUpper().Contains(Search_Tour_Text)));
                                break;
                            case "Type":
                                TourList = new ObservableCollection<TourModel>(TourList.Where(x => x.TOUR_TYPE.Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_TYPE.ToLower().Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_TYPE.ToUpper().Contains(Search_Tour_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_TourList binding ItemSource of ComboBox of TourUC
         */
        private ObservableCollection<ComboBoxModel> _CB_TourList;
        public ObservableCollection<ComboBoxModel> CB_TourList { get => _CB_TourList; set { _CB_TourList = value; OnPropertyChanged(); } }

        /*
         * CB_TourSelected binding SelectedValue of ComboBox of TourUC
         */
        private ComboBoxModel _CB_TourSelected;
        public ComboBoxModel CB_TourSelected { get => _CB_TourSelected; set { _CB_TourSelected = value; OnPropertyChanged(); } }

        /*
         * Checkbox_DisplayAllTour binding Checked of Checkbox of TourUC
         */
        private bool _Checkbox_DisplayAllTour;
        public bool Checkbox_DisplayAllTour
        {
            get => _Checkbox_DisplayAllTour;
            set
            {
                _Checkbox_DisplayAllTour = value;
                OnPropertyChanged();

                if (Checkbox_DisplayAllTour)
                {
                    LoadDataGridTourUC();
                }
                else
                {
                    LoadDataGridExceptDeleteTourUC();
                }
            }
        }

        #endregion Data Binding for TourUC

        #region Data Binding of AddTourUC, DisplayTourUC
        private string _Tour_Name;
        public string Tour_Name { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private string _Tour_Type;
        public string Tour_Type { get => _Tour_Type; set { _Tour_Type = value; OnPropertyChanged(); } }

        private string _Tour_Is_Exist;
        public string Tour_Is_Exist { get => _Tour_Is_Exist; set { _Tour_Is_Exist = value; OnPropertyChanged(); } }

        private string _Tour_Characteristic;
        public string Tour_Characteristic { get => _Tour_Characteristic; set { _Tour_Characteristic = value; OnPropertyChanged(); } }

        private byte[] _Tour_Image_Byte_Source;
        public byte[] Tour_Image_Byte_Source { get => _Tour_Image_Byte_Source; set { _Tour_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Tour_Image_Source;
        public BitmapImage Tour_Image_Source { get => _Tour_Image_Source; set { _Tour_Image_Source = value; OnPropertyChanged(); } }
        #endregion Data Binding of AddTourUC, DisplayTourUC

        #endregion Data

        #region Command

        #region Command of TourUC
        /*
         * AddTourCommand is a command which will change present content control to AddTourUC
         */
        public ICommand AddTourCommand { get; set; }

        /*
         * ShowTourDetailCommand is a command which will show detail about the Tour is clicked in DataGrid in TourUC
         */
        public ICommand ShowTourDetailCommand { get; set; }

        /*
         * StatisticTourCommand is a command which will change present content control to StatisticTourUC
         */
        public ICommand StatisticTourCommand { get; set; }

        #endregion Command of TourUC

        #region Command of AddTourUC

        /*
         * ConfirmCommand is a command which will save information above to database in AddTourUC
         */
        public ICommand ConfirmTourCommand { get; set; }

        /*
         * CancelCommand is a command which will back to TourUC
         */
        public ICommand CancelTourCommand { get; set; }

        /*
         * AddImageCommand is a command which will add image from computer to system
         */
        public ICommand AddImageCommand { get; set; }

        #endregion Command of AddTourUC

        #region Command of DisplayTourUC
        /*
         * ModifyImageCommand is a command which will update image from computer to system
         */
        public ICommand ModifyImageCommand { get; set; }

        /*
         * SaveChangeCommand is a command which will save all change in Tab Information to database in DisplayTourUC
         */
        public ICommand SaveChangeTourCommand { get; set; }
        #endregion Command of DisplayTourUC

        #endregion Command

        #region All Load Function

        /*
        * Gather all Load into one function
        */
        private void LoadTourUC()
        {
            LoadTourComboBox();
            Checkbox_DisplayAllTour = false;
            LoadCommand();
            Tour_Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Tour.png", UriKind.Absolute));
        }

        /* 
         * Load data from database to DataGridView
         * Reload data when back from detailed to global 
         */
        private void LoadDataGridTourUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to TourList
             * Step 3: Initialize TourList, Refresh_TourList
             * Step 4: Insert hotelList to TourList and Refresh_TourList
             */

            //Step 1
            if (CB_TourSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR> tourList = from tour in db.TOUR select tour;

            //Step 3
            TourList = new ObservableCollection<TourModel>();
            Refresh_TourList = new ObservableCollection<TourModel>();

            //Step 4
            foreach (TOUR item in tourList)
            {
                TourModel tourModel = new TourModel
                {
                    TOUR_ID = item.TOUR_ID,
                    TOUR_NAME = item.TOUR_NAME,
                    TOUR_TYPE = item.TOUR_TYPE,
                    TOUR_CHARACTERISTIS = item.TOUR_CHARACTERISTIS,
                    TOUR_IS_EXIST = item.TOUR_IS_EXIST,
                    TOUR_IMAGE_BYTE_SOURCE = item.TOUR_IMAGE
                };

                TourList.Add(tourModel);
                Refresh_TourList.Add(tourModel);
            }
        }

        private void LoadDataGridExceptDeleteTourUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to TourList, exclude all delete hotel
             * Step 3: Initialize TourList, Refresh_TourList
             * Step 4: Insert hotelList to TourList and Refresh_TourList
             */

            //Step 1
            if (CB_TourSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR> tourList = from tour in db.TOUR
                                        where tour.TOUR_IS_EXIST == "No"
                                        select tour;

            //Step 3
            TourList = new ObservableCollection<TourModel>();
            Refresh_TourList = new ObservableCollection<TourModel>();

            //Step 4
            foreach (TOUR item in tourList)
            {
                TourModel tourModel = new TourModel
                {
                    TOUR_ID = item.TOUR_ID,
                    TOUR_NAME = item.TOUR_NAME,
                    TOUR_TYPE = item.TOUR_TYPE,
                    TOUR_CHARACTERISTIS = item.TOUR_CHARACTERISTIS,
                    TOUR_IS_EXIST = item.TOUR_IS_EXIST,
                    TOUR_IMAGE_BYTE_SOURCE = item.TOUR_IMAGE
                };

                TourList.Add(tourModel);
                Refresh_TourList.Add(tourModel);
            }
        }

        /*
         * Load all command of View Model
         */
        private void LoadCommand()
        {
            #region Command of TourUC

            AddTourCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddTourUC(User_ID));

            ShowTourDetailCommand = new RelayCommand<ContentControl>(_ => Tour_Selected != null, p => p.Content = new DisplayTourUC(Tour_Selected, User_ID));

            #endregion Command of TourUC

            #region Command of AddTourUC

            AddImageCommand = new RelayCommand<object>(_ => IsExcuteAddImageCommand(), _ => ExcuteAddImageCommand());

            ConfirmTourCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmTourCommand(), p =>
            {
                ExcuteConfirmTourCommand();
                p.Content = new TourUC(User_ID);
            });

            CancelTourCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TourUC(User_ID));

            #endregion Command of AddTourUC

            #region Command of DisplayTourUC

            ModifyImageCommand = new RelayCommand<object>(_ => true, _ => ExcuteAddImageCommand());

            SaveChangeTourCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangeTourCommand(), p =>
            {
                ExcuteSaveChangeTourCommand();
                p.Content = new TourUC(User_ID);
            });

            #endregion Command of DisplayTourUC
        }

        /*
         * Create list in ComboBox of TourUC
         */
        private void LoadTourComboBox()
        {
            CB_TourList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Type", false)
            };
            CB_TourSelected = CB_TourList.FirstOrDefault(x => x.IsSelected);
        }
        #endregion All Load Function

        #region AddTourUC View Model

        /*
        * Function IsExcute and Excute of AddImageCommand
        * IsExcute = true when image in button is empty
        * Excute is save image to database and system
        */
        private bool IsExcuteAddImageCommand()
        {
            return Tour_Image_Byte_Source == null;
        }

        private void ExcuteAddImageCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false };

            if (ofd.ShowDialog() == true)
            {
                string string_File_Name = ofd.FileName;
                BitmapImage bitmap_Image = new BitmapImage(new Uri(string_File_Name));
                Tour_Image_Byte_Source = File.ReadAllBytes(string_File_Name);
                Tour_Image_Source = bitmap_Image;
            }
        }

        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddTourUC except Descritption is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmTourCommand()
        {
            if (string.IsNullOrEmpty(Tour_Name) ||
                string.IsNullOrEmpty(Tour_Type))
            {
                return false;
            }

            return true;
        }

        private void ExcuteConfirmTourCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Tour to Tour_Tour in database
             * Step 3: Get ID new Tour 
             * Step 4: Get ID new Tour and Add new Record
             * Step 5: Save Database
             * Step 6: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            TOUR tour = new TOUR()
            {
                TOUR_NAME = Tour_Name,
                TOUR_TYPE = Tour_Type,
                TOUR_CHARACTERISTIS = string.IsNullOrEmpty(Tour_Characteristic) ? "" : Tour_Characteristic,
                TOUR_IS_EXIST = "No",
                TOUR_IMAGE = Tour_Image_Byte_Source
            };
            db.TOUR.Add(tour);

            //Step 4
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new tour with Name: " + Tour_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 5
            db.SaveChanges();

            //Step 6
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion AddTourUC View Model

        #region DisplayTourUC View Model

        /*
        * Function IsExcute and Excute of SaveChangeCommand
        * IsExcute = true when any textbox in DisplayTourUC
        * Excute is save all textbox to database
        */
        private bool IsExcuteSaveChangeTourCommand()
        {
            if (Tour_Name != Tour_Selected.TOUR_NAME)
            {
                return true;
            }
            if (Tour_Type != Tour_Selected.TOUR_TYPE)
            {
                return true;
            }
            if (Tour_Is_Exist != Tour_Selected.TOUR_IS_EXIST)
            {
                return true;
            }
            if (Tour_Characteristic != Tour_Selected.TOUR_CHARACTERISTIS)
            {
                return true;
            }
            if (Tour_Image_Byte_Source != Tour_Selected.TOUR_IMAGE_BYTE_SOURCE)
            {
                return true;
            }

            return false;
        }

        private void ExcuteSaveChangeTourCommand()
        {
            try
            {
                Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

                TOUR tour = db.TOUR.Where(x => x.TOUR_ID == Tour_Selected.TOUR_ID).SingleOrDefault();
                tour.TOUR_NAME = Tour_Name;
                tour.TOUR_TYPE = Tour_Type;
                tour.TOUR_IS_EXIST = Tour_Is_Exist;
                tour.TOUR_CHARACTERISTIS = Tour_Characteristic;
                tour.TOUR_IMAGE = Tour_Image_Byte_Source;

                string changeToSave = "";
                int countChangeToSave = 0;
                if (Tour_Name != Tour_Selected.TOUR_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Tour_Selected.TOUR_NAME, Tour_Name);
                    countChangeToSave++;
                }
                if (Tour_Type != Tour_Selected.TOUR_TYPE)
                {
                    changeToSave += string.Format("Type Change ({0} -> {1})   ", Tour_Selected.TOUR_TYPE, Tour_Type);
                    countChangeToSave++;
                }
                if (Tour_Is_Exist != Tour_Selected.TOUR_IS_EXIST)
                {
                    changeToSave += string.Format("Tour Exist Change ({0} -> {1})   ", Tour_Selected.TOUR_IS_EXIST, Tour_Is_Exist);
                    countChangeToSave++;
                }
                if (Tour_Characteristic != Tour_Selected.TOUR_CHARACTERISTIS)
                {
                    changeToSave += string.Format("Tour Characteristic Change ({0} -> {1})   ", Tour_Selected.TOUR_CHARACTERISTIS, Tour_Characteristic);
                    countChangeToSave++;
                }
                if (Tour_Image_Byte_Source != Tour_Selected.TOUR_IMAGE_BYTE_SOURCE)
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

        #endregion DisplayTourUC View Model
        #endregion Tour

        #region Tour Information
        #region Data Binding for TourInformationUC
        /*
         * TourInformationList binding ItemSource on DataGridView of TourInformationUC
         */
        private ObservableCollection<TourInformationModel> _TourInformationList;
        public ObservableCollection<TourInformationModel> TourInformationList { get => _TourInformationList; set { _TourInformationList = value; OnPropertyChanged(); } }

        /*
         * Refresh_TourInformationList is refresh TourInformationList
         */
        private ObservableCollection<TourInformationModel> _Refresh_TourInformationList;
        public ObservableCollection<TourInformationModel> Refresh_TourInformationList { get => _Refresh_TourInformationList; set { _Refresh_TourInformationList = value; OnPropertyChanged(); } }


        /*
         * TourInformation_Selected binding SelectedItem on DataGridView of TourInformationUC
         */
        private TourInformationModel _TourInformation_Selected;
        public TourInformationModel TourInformation_Selected
        {
            get => _TourInformation_Selected;
            set
            {
                _TourInformation_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display TourInformationUC
                 */
                if (TourInformation_Selected != null)
                {

                }
            }
        }


        /*
        * Search_TourInformation_Text binding to Search button of TourInformationUC
        */
        private string _Search_TourInformation_Text;
        public string Search_TourInformation_Text
        {
            get => _Search_TourInformation_Text;
            set
            {
                _Search_TourInformation_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh TourInformationList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                TourInformationList = Refresh_TourInformationList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_TourInformation_Text))
                {
                    //Step 3
                    if (CB_TourInformationSelected != null)
                    {
                        //Step 4
                    }
                }
            }
        }

        /*
         * CB_TourInformationList binding ItemSource of ComboBox of TourInformationUC
         */
        private ObservableCollection<ComboBoxModel> _CB_TourInformationList;
        public ObservableCollection<ComboBoxModel> CB_TourInformationList { get => _CB_TourInformationList; set { _CB_TourInformationList = value; OnPropertyChanged(); } }

        /*
         * CB_TourInformationSelected binding SelectedValue of ComboBox of TourInformationUC
         */
        private ComboBoxModel _CB_TourInformationSelected;
        public ComboBoxModel CB_TourInformationSelected { get => _CB_TourInformationSelected; set { _CB_TourInformationSelected = value; OnPropertyChanged(); } }

        #endregion Data Binding for TourInformationUC

        #region Data Binding for AddTourInformationUC
        #region Data Binding for TourTime
        private int _Time_Day;
        public int Time_Day { get => _Time_Day; set { _Time_Day = value; OnPropertyChanged(); } }

        private int _Time_Night;
        public int Time_Night { get => _Time_Night; set { _Time_Night = value; OnPropertyChanged(); } }

        private DateTime _Time_Department = DateTime.Now;
        public DateTime Time_Department { get => _Time_Department; set { _Time_Department = value; OnPropertyChanged(); } }

        private string _Time_Department_String;
        public string Time_Department_String { get => _Time_Department_String; set { _Time_Department_String = value; OnPropertyChanged(); } }

        private DateTime _Time_End = DateTime.Now;
        public DateTime Time_End { get => _Time_End; set { _Time_End = value; OnPropertyChanged(); } }

        private string _Time_End_String;
        public string Time_End_String { get => _Time_End_String; set { _Time_End_String = value; OnPropertyChanged(); } }
        #endregion Data Binding for TourTime
        #region Data Binding for TourPlace
        private ObservableCollection<CheckBoxModel> _PlaceList;
        public ObservableCollection<CheckBoxModel> PlaceList { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }
        #endregion Data Binding for TourPlace
        #endregion Data Binding for AddTourInformationUC
        #endregion TourInformation Information
    }
}
