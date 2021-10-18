using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Managers.Model;
using TourManagementSystem.Managers.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.ViewModel
{
    public class PlaceViewModel : BaseViewModel
    {
        #region Data
        #region Data Binding Of PlaceUC

        /*
        * User is manager of system
        */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        /*
         * PlaceList binding ItemSource on DataGridView of PlaceUC
         */
        private ObservableCollection<PlaceModel> _PlaceList;
        public ObservableCollection<PlaceModel> PlaceList { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        /*
         * Refresh_PlaceList is refresh PlaceList
         */
        private ObservableCollection<PlaceModel> _Refresh_PlaceList;
        public ObservableCollection<PlaceModel> Refresh_PlaceList { get => _Refresh_PlaceList; set { _Refresh_PlaceList = value; OnPropertyChanged(); } }

        /*
         * Place_Selected binding SelectedItem on DataGridView of PlaceUC
         */
        private PlaceModel _Place_Selected;
        public PlaceModel Place_Selected
        {
            get => _Place_Selected;
            set
            {
                _Place_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display PlaceUC
                 */
                if (Place_Selected != null)
                {
                    Place_Name = Place_Selected.PLACE_NAME;
                    Place_Nation = Place_Selected.PLACE_NATION;
                    Place_Location = Place_Selected.PLACE_LOCATION;
                }
            }
        }

        /*
        * Search_Place_Text binding to Search button of PlaceUC
        */
        private string _Search_Place_Text;
        public string Search_Place_Text
        {
            get => _Search_Place_Text;
            set
            {
                _Search_Place_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh PlaceList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                PlaceList = Refresh_PlaceList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Place_Text))
                {
                    //Step 3
                    if (CB_PlaceSelected != null)
                    {
                        //Step 4
                        switch (CB_PlaceSelected.CB_Name)
                        {
                            case "Name":
                                PlaceList = new ObservableCollection<PlaceModel>(PlaceList.Where(x => x.PLACE_NAME.Contains(Search_Place_Text) ||
                                                                                                        x.PLACE_NAME.ToLower().Contains(Search_Place_Text) ||
                                                                                                        x.PLACE_NAME.ToUpper().Contains(Search_Place_Text)));
                                break;
                            case "Nation":
                                PlaceList = new ObservableCollection<PlaceModel>(PlaceList.Where(x => x.PLACE_NATION.Contains(Search_Place_Text) ||
                                                                                                        x.PLACE_NATION.ToLower().Contains(Search_Place_Text) ||
                                                                                                        x.PLACE_NATION.ToUpper().Contains(Search_Place_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_PlaceList binding ItemSource of ComboBox of PlaceUC
         */
        private ObservableCollection<ComboBoxModel> _CB_PlaceList;
        public ObservableCollection<ComboBoxModel> CB_PlaceList { get => _CB_PlaceList; set { _CB_PlaceList = value; OnPropertyChanged(); } }

        /*
         * CB_PlaceSelected binding SelectedValue of ComboBox of PlaceUC
         */
        private ComboBoxModel _CB_PlaceSelected;
        public ComboBoxModel CB_PlaceSelected { get => _CB_PlaceSelected; set { _CB_PlaceSelected = value; OnPropertyChanged(); } }

        #endregion Data Binding Of PlaceUC

        #region Data Binding of AddPlaceUC, DisplayPlaceUC

        private string _Place_Name;
        public string Place_Name { get => _Place_Name; set { _Place_Name = value; OnPropertyChanged(); } }

        private string _Place_Nation;
        public string Place_Nation { get => _Place_Nation; set { _Place_Nation = value; OnPropertyChanged(); } }

        private int _Place_Location;
        public int Place_Location { get => _Place_Location; set { _Place_Location = value; OnPropertyChanged(); } }

        #endregion Data Binding of AddPlaceUC, DisplayPlaceUC

        #region Data Binding Location in DisplayPlaceUC
        /*
        * LocationList binding ItemSource on DataGridView of DisplayPlaceUC
        */
        private ObservableCollection<LocationModel> _LocationList;
        public ObservableCollection<LocationModel> LocationList { get => _LocationList; set { _LocationList = value; OnPropertyChanged(); } }

        /*
         * Refresh_LocationList is refresh LocationList
         */
        private ObservableCollection<LocationModel> _Refresh_LocationList;
        public ObservableCollection<LocationModel> Refresh_LocationList { get => _Refresh_LocationList; set { _Refresh_LocationList = value; OnPropertyChanged(); } }

        /*
         * Location_Selected binding SelectedItem on DataGridView of DisplayPlaceUC
         */
        private LocationModel _Location_Selected;
        public LocationModel Location_Selected
        {
            get => _Location_Selected;
            set
            {
                _Location_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display DisplayPlaceUC
                 */
                if (Location_Selected != null)
                {
                    Location_Name = Location_Selected.LOCATION_NAME;
                    Location_Address = Location_Selected.LOCATION_ADDRESS;
                    Location_Description = Location_Selected.LOCATION_DESCRIPTION;
                }
            }
        }

        /*
        * Search_Location_Text binding to Search button of DisplayPlaceUC
        */
        private string _Search_Location_Text;
        public string Search_Location_Text
        {
            get => _Search_Location_Text;
            set
            {
                _Search_Location_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh LocationList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                LocationList = Refresh_LocationList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Location_Text))
                {
                    //Step 3
                    if (CB_LocationSelected != null)
                    {
                        //Step 4
                        switch (CB_LocationSelected.CB_Name)
                        {
                            case "Name":
                                LocationList = new ObservableCollection<LocationModel>(LocationList.Where(x => x.LOCATION_NAME.Contains(Search_Location_Text) ||
                                                                                                        x.LOCATION_NAME.ToLower().Contains(Search_Location_Text) ||
                                                                                                        x.LOCATION_NAME.ToUpper().Contains(Search_Location_Text)));
                                break;
                            case "Address":
                                LocationList = new ObservableCollection<LocationModel>(LocationList.Where(x => x.LOCATION_ADDRESS.Contains(Search_Location_Text) ||
                                                                                                        x.LOCATION_ADDRESS.ToLower().Contains(Search_Location_Text) ||
                                                                                                        x.LOCATION_ADDRESS.ToUpper().Contains(Search_Location_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_LocationList binding ItemSource of ComboBox of DisplayPlaceUC
         */
        private ObservableCollection<ComboBoxModel> _CB_LocationList;
        public ObservableCollection<ComboBoxModel> CB_LocationList { get => _CB_LocationList; set { _CB_LocationList = value; OnPropertyChanged(); } }

        /*
         * CB_LocationSelected binding SelectedValue of ComboBox of DisplayPlaceUC
         */
        private ComboBoxModel _CB_LocationSelected;
        public ComboBoxModel CB_LocationSelected { get => _CB_LocationSelected; set { _CB_LocationSelected = value; OnPropertyChanged(); } }

        #region Data Binding Datagrid Location in DisplayPlaceUC

        private string _Location_Name;
        public string Location_Name { get => _Location_Name; set { _Location_Name = value; OnPropertyChanged(); } }

        private string _Location_Address;
        public string Location_Address { get => _Location_Address; set { _Location_Address = value; OnPropertyChanged(); } }

        private string _Location_Description;
        public string Location_Description { get => _Location_Description; set { _Location_Description = value; OnPropertyChanged(); } }

        #endregion Data Binding Datagrid Location in DisplayPlaceUC

        #endregion Data Binding Location in DisplayPlaceUC

        #endregion Data

        #region Command

        #region Command of PlaceUC
        /*
         * AddPlaceCommand is a command which will change present content control to AddPlaceUC
         */
        public ICommand AddPlaceCommand { get; set; }

        /*
         * ShowPlaceDetailCommand is a command which will show detail about the Place is clicked in DataGrid in PlaceUC
         */
        public ICommand ShowPlaceDetailCommand { get; set; }

        #endregion Command of PlaceUC

        #region Command of AddPlaceUC

        /*
         * ConfirmPlaceCommand is a command which will save information above to database in AddPlaceUC
         */
        public ICommand ConfirmPlaceCommand { get; set; }

        /*
         * CancelPlaceCommand is a command which will back to PlaceUC
         */
        public ICommand CancelPlaceCommand { get; set; }

        #endregion Command of AddPlaceUC

        #region Command of DisplayPlaceUC

        /*
         * SaveChangePlaceCommand is a command which will save all change in Tab Information to database in DisplayPlaceUC
         */
        public ICommand SaveChangePlaceCommand { get; set; }

        /*
         * AddLocationCommand is a command which will change present content control to AddLocationUC
         */
        public ICommand AddLocationCommand { get; set; }

        /*
         * ShowLocationDetailCommand is a command which will show detail about the Place is clicked in DataGrid in DisplayPlaceUC
         */
        public ICommand ShowLocationDetailCommand { get; set; }

        #endregion Command of DisplayPlaceUC

        #region Command of AddLocationUC

        /*
         * ConfirmLocationCommand is a command which will save information above to database in AddLocationUC
         */
        public ICommand ConfirmLocationCommand { get; set; }

        /*
         * CancelLocationCommand is a command which will back to DisplayPlaceUC
         */
        public ICommand CancelLocationCommand { get; set; }

        #endregion Command of AddLocationUC

        #region Command of DisplayLocationUC
        /*
         * SaveChangeLocationCommand is a command which will save all change in Tab Information to database in DisplayLocationUC
         */
        public ICommand SaveChangeLocationCommand { get; set; }
        #endregion Command of DisplayLocationUC

        #endregion Command

        /*
         * Constructor for PlaceUC, AddPlaceUC
         */
        public PlaceViewModel(int user_id)
        {
            User_ID = user_id;
            LoadUC();

        }

        /*
         * Constructor for DisplayPlaceUC
         */
        public PlaceViewModel(PlaceModel placeSelected, int user_id)
        {
            User_ID = user_id;
            Place_Selected = placeSelected;
            LoadDisplayUC();
        }

        /*
         * Constructor for DisplayLocationUC
         */
        public PlaceViewModel(PlaceModel placeSelected, LocationModel locationSelected, int user_id)
        {
            User_ID = user_id;
            Place_Selected = placeSelected;
            Location_Selected = locationSelected;
            LoadCommand();
        }

        #region All Load Function

        /*
        * Gather all Load into one function
        */
        private void LoadUC()
        {
            LoadPlaceComboBox();
            LoadDataGridPlaceUC();
            LoadCommand();
        }

        private void LoadDisplayUC()
        {
            LoadLocationComboBox();
            LoadDataGridLocationUC();
            LoadCommand();
        }

        /* 
         * Load data from database to DataGridView
         * Reload data when back from detailed to global 
         */
        private void LoadDataGridPlaceUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to PlaceList
             * Step 3: Initialize PlaceList, Refresh_PlaceList
             * Step 4: Insert placeportList to PlaceList and Refresh_PlaceList
             */

            //Step 1
            if (CB_PlaceSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            if (db.TOUR_LOCATION.Count() > 0)
            {
                var NumberOfLocation = db.TOUR_LOCATION.GroupBy(x => x.PLACE_ID).Select(x => new { x.Key, count = x.Count() });

                var placeList = from place in db.PLACE
                                join numlocation in NumberOfLocation on place.PLACE_ID equals numlocation.Key into temp
                                from numlocation in temp.DefaultIfEmpty()
                                select new
                                {
                                    PLACE_ID = place.PLACE_ID,
                                    PLACE_NAME = place.PLACE_NAME,
                                    PLACE_NATION = place.PLACE_NATION,
                                    PLACE_LOCATION = numlocation == null ? 0 : numlocation.count
                                };

                //Step 3
                PlaceList = new ObservableCollection<PlaceModel>();
                Refresh_PlaceList = new ObservableCollection<PlaceModel>();

                //Step 4
                foreach (var item in placeList)
                {
                    PlaceModel placeModel = new PlaceModel
                    {
                        PLACE_ID = item.PLACE_ID,
                        PLACE_NAME = item.PLACE_NAME,
                        PLACE_NATION = item.PLACE_NATION,
                        PLACE_LOCATION = item.PLACE_LOCATION
                    };

                    PlaceList.Add(placeModel);
                    Refresh_PlaceList.Add(placeModel);
                }
            }
            else
            {
                var placeList = from place in db.PLACE
                                select new
                                {
                                    PLACE_ID = place.PLACE_ID,
                                    PLACE_NAME = place.PLACE_NAME,
                                    PLACE_NATION = place.PLACE_NATION,
                                    PLACE_LOCATION = 0
                                };

                //Step 3
                PlaceList = new ObservableCollection<PlaceModel>();
                Refresh_PlaceList = new ObservableCollection<PlaceModel>();

                //Step 4
                foreach (var item in placeList)
                {
                    PlaceModel placeModel = new PlaceModel
                    {
                        PLACE_ID = item.PLACE_ID,
                        PLACE_NAME = item.PLACE_NAME,
                        PLACE_NATION = item.PLACE_NATION,
                        PLACE_LOCATION = item.PLACE_LOCATION
                    };

                    PlaceList.Add(placeModel);
                    Refresh_PlaceList.Add(placeModel);
                }
            }
        }

        /*
         * Load all command of View Model
         */
        private void LoadCommand()
        {
            #region Command of PlaceUC

            AddPlaceCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddPlaceUC(User_ID));

            ShowPlaceDetailCommand = new RelayCommand<ContentControl>(_ => Place_Selected != null, p => p.Content = new DisplayPlaceUC(Place_Selected, User_ID));

            #endregion Command of PlaceUC

            #region Command of AddPlaceUC

            ConfirmPlaceCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmPlaceCommand(), p =>
            {
                ExcuteConfirmPlaceCommand();
                p.Content = new PlaceUC(User_ID);
            });

            CancelPlaceCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new PlaceUC(User_ID));

            #endregion Command of AddPlaceUC

            #region Command of DisplayPlaceUC

            SaveChangePlaceCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangePlaceCommand(), p =>
            {
                ExcuteSaveChangePlaceCommand();
                p.Content = new PlaceUC(User_ID);
            });

            AddLocationCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddLocationUC(Place_Selected, User_ID));

            ShowLocationDetailCommand = new RelayCommand<ContentControl>(_ => Location_Selected != null, p => p.Content = new DisplayLocationUC(Place_Selected, Location_Selected, User_ID));

            #endregion Command of DisplayPlaceUC

            #region Command of AddLocationUC
            ConfirmLocationCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmLocationCommand(), p =>
            {
                ExcuteConfirmLocationCommand();
                p.Content = new DisplayPlaceUC(Place_Selected, User_ID);
            });

            CancelLocationCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new DisplayPlaceUC(Place_Selected, User_ID));
            #endregion Command of AddLocationUC

            #region Command of DisplayLocationUC
            SaveChangeLocationCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangeLocationCommand(), p =>
            {
                ExcuteSaveChangeLocationCommand();
                p.Content = new DisplayPlaceUC(Place_Selected, User_ID);
            });
            #endregion Command of DisplayLocationUC
        }

        /*
         * Create list in ComboBox of PlaceUC
         */
        private void LoadPlaceComboBox()
        {
            CB_PlaceList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Nation", false)
            };
            CB_PlaceSelected = CB_PlaceList.FirstOrDefault(x => x.IsSelected);
        }

        /*
         * Create list in Location ComboBox of DisplayPlaceUC
         */
        private void LoadLocationComboBox()
        {
            CB_LocationList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Address", false)
            };
            CB_LocationSelected = CB_LocationList.FirstOrDefault(x => x.IsSelected);
        }
        #endregion All Load Function

        #region AddPlaceUC View Model
        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddPlaceUC except Descritption is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmPlaceCommand()
        {
            if (string.IsNullOrEmpty(Place_Name) ||
                string.IsNullOrEmpty(Place_Nation))
            {
                return false;
            }

            return true;
        }

        private void ExcuteConfirmPlaceCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Place to Tour_Place in database
             * Step 3: Get ID new Place and Add new Record
             * Step 4: Save Database
             * Step 5: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            PLACE tour_place = new PLACE()
            {
                PLACE_NAME = Place_Name,
                PLACE_NATION = Place_Nation
            };
            db.PLACE.Add(tour_place);

            //Step 4
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new place with Name: " + Place_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 5
            db.SaveChanges();

            //Step 6
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion AddPlaceUC View Model

        #region DisplayPlaceUC View Model
        /* 
        * Load data from database to DataGridView
        * Reload data when back from detailed to global 
        */
        private void LoadDataGridLocationUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to LocationList
             * Step 3: Initialize LocationList, Refresh_LocationList
             * Step 4: Insert placeportList to LocationList and Refresh_LocationList
             */

            //Step 1
            if (CB_LocationSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            var locationlist = from location in db.TOUR_LOCATION
                               where location.PLACE_ID == Place_Selected.PLACE_ID
                               select location;

            //Step 3
            LocationList = new ObservableCollection<LocationModel>();
            Refresh_LocationList = new ObservableCollection<LocationModel>();

            //Step 4
            foreach (var item in locationlist)
            {
                LocationModel locationModel = new LocationModel
                {
                    LOCATION_ID = item.TOUR_LOCATION_ID,
                    LOCATION_NAME = item.TOUR_LOCATION_NAME,
                    LOCATION_ADDRESS = item.TOUR_LOCATION_ADDRESS,
                    LOCATION_DESCRIPTION = item.TOUR_LOCATION_DESCRIPTION,
                    PLACE_ID = item.PLACE_ID
                };

                LocationList.Add(locationModel);
                Refresh_LocationList.Add(locationModel);
            }
        }


        /*
        * Function IsExcute and Excute of SaveChangeCommand
        * IsExcute = true when any textbox in DisplayPlaceUC
        * Excute is save all textbox to database
        */
        private bool IsExcuteSaveChangePlaceCommand()
        {
            if (Place_Name != Place_Selected.PLACE_NAME)
            {
                return true;
            }
            if (Place_Nation != Place_Selected.PLACE_NATION)
            {
                return true;
            }

            return false;
        }

        private void ExcuteSaveChangePlaceCommand()
        {
            try
            {
                Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

                PLACE tour_place = db.PLACE.Where(x => x.PLACE_ID == Place_Selected.PLACE_ID).SingleOrDefault();
                tour_place.PLACE_NAME = Place_Name;
                tour_place.PLACE_NATION = Place_Nation;

                string changeToSave = "";
                int countChangeToSave = 0;
                if (Place_Name != Place_Selected.PLACE_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Place_Selected.PLACE_NAME, Place_Name);
                    countChangeToSave++;
                }
                if (Place_Nation != Place_Selected.PLACE_NATION)
                {
                    changeToSave += string.Format("Nation Change ({0} -> {1})   ", Place_Selected.PLACE_NATION, Place_Nation);
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
        #endregion DisplayPlaceUC View Model

        #region AddLocationUC View Model
        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddLocationUC except Descritption is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmLocationCommand()
        {
            if (string.IsNullOrEmpty(Location_Name) ||
                string.IsNullOrEmpty(Location_Address))
            {
                return false;
            }

            return true;
        }

        private void ExcuteConfirmLocationCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Location to Tour_Location in database
             * Step 3: Get ID new Location and Add new Record
             * Step 4: Save Database
             * Step 5: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            TOUR_LOCATION tour_location = new TOUR_LOCATION()
            {
                TOUR_LOCATION_NAME = Location_Name,
                TOUR_LOCATION_ADDRESS = Location_Address,
                TOUR_LOCATION_DESCRIPTION = Location_Description,
                PLACE_ID = Place_Selected.PLACE_ID
            };
            db.TOUR_LOCATION.Add(tour_location);

            //Step 4
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new location with Name: " + Location_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 5
            db.SaveChanges();

            //Step 6
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion AddLocationUC View Model

        #region DisplayLocationUC View Model
        /*
        * Function IsExcute and Excute of SaveChangeCommand
        * IsExcute = true when any textbox in DisplayLocationUC
        * Excute is save all textbox to database
        */
        private bool IsExcuteSaveChangeLocationCommand()
        {
            if (Location_Name != Location_Selected.LOCATION_NAME)
            {
                return true;
            }
            if (Location_Address != Location_Selected.LOCATION_ADDRESS)
            {
                return true;
            }
            if (Location_Description != Location_Selected.LOCATION_DESCRIPTION)
            {
                return true;
            }

            return false;
        }

        private void ExcuteSaveChangeLocationCommand()
        {
            try
            {
                Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

                TOUR_LOCATION tour_location = db.TOUR_LOCATION.Where(x => x.TOUR_LOCATION_ID == Location_Selected.LOCATION_ID).SingleOrDefault();
                tour_location.TOUR_LOCATION_NAME = Location_Name;
                tour_location.TOUR_LOCATION_ADDRESS = Location_Address;
                tour_location.TOUR_LOCATION_DESCRIPTION = Location_Description;

                string changeToSave = "";
                int countChangeToSave = 0;
                if (Location_Name != Location_Selected.LOCATION_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Location_Selected.LOCATION_NAME, Location_Name);
                    countChangeToSave++;
                }
                if (Location_Address != Location_Selected.LOCATION_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", Location_Selected.LOCATION_ADDRESS, Location_Address);
                    countChangeToSave++;
                }
                if (Location_Description != Location_Selected.LOCATION_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", Location_Selected.LOCATION_DESCRIPTION, Location_Description);
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
        #endregion DisplayLocationUC View Model
    }
}
