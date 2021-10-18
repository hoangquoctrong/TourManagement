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
    public class TransportViewModel : BaseViewModel
    {
        #region Data
        #region Data Binding Of TransportUC

        /*
        * User is manager of system
        */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        /*
         * TransportList binding ItemSource on DataGridView of TransportUC
         */
        private ObservableCollection<TransportModel> _TransportList;
        public ObservableCollection<TransportModel> TransportList { get => _TransportList; set { _TransportList = value; OnPropertyChanged(); } }

        /*
         * Refresh_TransportList is refresh TransportList
         */
        private ObservableCollection<TransportModel> _Refresh_TransportList;
        public ObservableCollection<TransportModel> Refresh_TransportList { get => _Refresh_TransportList; set { _Refresh_TransportList = value; OnPropertyChanged(); } }

        /*
         * Transport_Selected binding SelectedItem on DataGridView of TransportUC
         */
        private TransportModel _Transport_Selected;
        public TransportModel Transport_Selected
        {
            get => _Transport_Selected;
            set
            {
                _Transport_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display TransportUC
                 */
                if (Transport_Selected != null)
                {
                    Transport_Name = Transport_Selected.TRANSPORT_NAME;
                    Transport_License_Plate = Transport_Selected.TRANSPORT_LICENSE_PLATE;
                    Transport_Company = Transport_Selected.TRANSPORT_COMPANY;
                    Transport_Type = Transport_Selected.TRANSPORT_TYPE;
                    Transport_Description = Transport_Selected.TRANSPORT_DESCRIPTION;
                    Transport_String_Date = Transport_Selected.TRANSPORT_STRING_DATE;
                    Transport_Date = Transport_Selected.TRANSPORT_DATE;
                    Transport_Image_Byte_Source = Transport_Selected.TRANSPORT_IMAGE_BYTE_SOURCE;
                    Transport_Image_Source = Transport_Image_Byte_Source == null
                        ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Transport.png", UriKind.Absolute))
                        : GlobalFunction.ToImage(Transport_Image_Byte_Source);
                }
            }
        }

        /*
        * Search_Text binding to Search button of TransportUC
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
                 * Step 1: Refresh TransportList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                TransportList = Refresh_TransportList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Text))
                {
                    //Step 3
                    if (CB_TransportSelected != null)
                    {
                        //Step 4
                        switch (CB_TransportSelected.CB_Name)
                        {
                            case "Name":
                                TransportList = new ObservableCollection<TransportModel>(TransportList.Where(x => x.TRANSPORT_NAME.Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_NAME.ToLower().Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_NAME.ToUpper().Contains(Search_Text)));
                                break;
                            case "License Plate":
                                TransportList = new ObservableCollection<TransportModel>(TransportList.Where(x => x.TRANSPORT_LICENSE_PLATE.Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_LICENSE_PLATE.ToLower().Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_LICENSE_PLATE.ToUpper().Contains(Search_Text)));
                                break;
                            case "Company":
                                TransportList = new ObservableCollection<TransportModel>(TransportList.Where(x => x.TRANSPORT_COMPANY.Contains(Search_Text) ||
                                                                                                       x.TRANSPORT_COMPANY.ToLower().Contains(Search_Text) ||
                                                                                                       x.TRANSPORT_COMPANY.ToUpper().Contains(Search_Text)));
                                break;
                            case "Status":
                                TransportList = new ObservableCollection<TransportModel>(TransportList.Where(x => x.TRANSPORT_TYPE.Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_TYPE.ToLower().Contains(Search_Text) ||
                                                                                                        x.TRANSPORT_TYPE.ToUpper().Contains(Search_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_TransportList binding ItemSource of ComboBox of TransportUC
         */
        private ObservableCollection<ComboBoxModel> _CB_TransportList;
        public ObservableCollection<ComboBoxModel> CB_TransportList { get => _CB_TransportList; set { _CB_TransportList = value; OnPropertyChanged(); } }

        /*
         * CB_TransportSelected binding SelectedValue of ComboBox of TransportUC
         */
        private ComboBoxModel _CB_TransportSelected;
        public ComboBoxModel CB_TransportSelected { get => _CB_TransportSelected; set { _CB_TransportSelected = value; OnPropertyChanged(); } }

        /*
         * Checkbox_DisplayAllTransport binding Checked of Checkbox of TransportUC
         */
        private bool _Checkbox_DisplayAllTransport;
        public bool Checkbox_DisplayAllTransport
        {
            get => _Checkbox_DisplayAllTransport;
            set
            {
                _Checkbox_DisplayAllTransport = value;
                OnPropertyChanged();

                if (Checkbox_DisplayAllTransport)
                {
                    LoadDataGridTransportUC();
                }
                else
                {
                    LoadDataGridExceptDeleteTransportUC();
                }
            }
        }

        #endregion Data Binding Of TransportUC

        #region Data Binding of AddTransportUC, DisplayTransportUC

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

        private string _Transport_Note_Remove;
        public string Transport_Note_Remove { get => _Transport_Note_Remove; set { _Transport_Note_Remove = value; OnPropertyChanged(); } }

        private string _Transport_String_Date;
        public string Transport_String_Date { get => _Transport_String_Date; set { _Transport_String_Date = value; OnPropertyChanged(); } }

        private DateTime _Transport_Date = DateTime.Now;
        public DateTime Transport_Date { get => _Transport_Date; set { _Transport_Date = value; OnPropertyChanged(); } }

        private byte[] _Transport_Image_Byte_Source;
        public byte[] Transport_Image_Byte_Source { get => _Transport_Image_Byte_Source; set { _Transport_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Transport_Image_Source;
        public BitmapImage Transport_Image_Source { get => _Transport_Image_Source; set { _Transport_Image_Source = value; OnPropertyChanged(); } }

        #endregion Data Binding of AddTransportUC, DisplayTransportUC
        #endregion Data

        #region Command

        #region Command of TransportUC
        /*
         * AddTransportCommand is a command which will change present content control to AddTransportUC
         */
        public ICommand AddTransportCommand { get; set; }

        /*
         * ShowTransportDetailCommand is a command which will show detail about the Transport is clicked in DataGrid in TransportUC
         */
        public ICommand ShowTransportDetailCommand { get; set; }

        #endregion Command of TransportUC

        #region Command of AddTransportUC

        /*
         * ConfirmCommand is a command which will save information above to database in AddTransportUC
         */
        public ICommand ConfirmCommand { get; set; }

        /*
         * CancelCommand is a command which will back to TransportUC
         */
        public ICommand CancelCommand { get; set; }

        /*
         * AddImageCommand is a command which will add image from computer to system
         */
        public ICommand AddImageCommand { get; set; }

        #endregion Command of AddTransportUC

        #region Command of DisplayTransportUC
        /*
         * ModifyImageCommand is a command which will update image from computer to system
         */
        public ICommand ModifyImageCommand { get; set; }

        /*
         * SaveChangeCommand is a command which will save all change in Tab Information to database in DisplayTransportUC
         */
        public ICommand SaveChangeCommand { get; set; }

        /*
         * RemoveCommand is a command which will remove account in Tab Delete in database in DisplayTransportUC
         */
        public ICommand RemoveCommand { get; set; }
        #endregion Command of DisplayTransportUC

        #endregion Command

        /*
         * Constructor for TransportUC, AddTransportUC
         */
        public TransportViewModel(int user_id)
        {
            User_ID = user_id;
            LoadTransportUC();

        }

        /*
         * Constructor for TransportDisplayUC
         */
        public TransportViewModel(TransportModel transportSelected, int user_id)
        {
            User_ID = user_id;
            Transport_Selected = transportSelected;
            LoadCommand();
        }

        #region All Load Function

        /*
        * Gather all Load into one function
        */
        private void LoadTransportUC()
        {
            LoadTransportComboBox();
            Checkbox_DisplayAllTransport = false;
            LoadCommand();
            Transport_Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Transport.png", UriKind.Absolute));
        }

        /* 
         * Load data from database to DataGridView
         * Reload data when back from detailed to global 
         */
        private void LoadDataGridTransportUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to TransportList
             * Step 3: Initialize TransportList, Refresh_TransportList
             * Step 4: Insert transportList to TransportList and Refresh_TransportList
             */

            //Step 1
            if (CB_TransportSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_TRANSPORT> transportList = from trans in db.TOUR_TRANSPORT
                                                       select trans;

            //Step 3
            TransportList = new ObservableCollection<TransportModel>();
            Refresh_TransportList = new ObservableCollection<TransportModel>();

            //Step 4
            foreach (TOUR_TRANSPORT item in transportList)
            {
                TransportModel transportModel = new TransportModel
                {
                    TRANSPORT_ID = item.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = item.TOUR_TRANSPORT_NAME,
                    TRANSPORT_LICENSE_PLATE = item.TOUR_TRANSPORT_LICENSE_PLATE,
                    TRANSPORT_COMPANY = item.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = item.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = item.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)item.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_IMAGE_BYTE_SOURCE = item.TOUR_TRANSPORT_IMAGE
                };
                transportModel.TRANSPORT_STRING_DATE = transportModel.TRANSPORT_DATE.ToString("dd/MM/yyyy");

                TransportList.Add(transportModel);
                Refresh_TransportList.Add(transportModel);
            }
        }

        private void LoadDataGridExceptDeleteTransportUC()
        {
            /*
             * Step 1: Check ComboBox is null or not
             * Step 2: Create db connect to Tour_Mangement_DatabaseEntities, make list to insert to TransportList, exclude all delete transport
             * Step 3: Initialize TransportList, Refresh_TransportList
             * Step 4: Insert transportList to TransportList and Refresh_TransportList
             */

            //Step 1
            if (CB_TransportSelected == null)
            {
                return;
            }

            //Step 2
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();
            IQueryable<TOUR_TRANSPORT> transportList = from trans in db.TOUR_TRANSPORT
                                                       join trans_delete in db.TOUR_TRANSPORT_DELETE on trans.TOUR_TRANSPORT_ID equals trans_delete.TOUR_TRANSPORT_ID
                                                       where trans_delete.TOUR_TRANSPORT_DELETE_ISDELETED == false
                                                       select trans;

            //Step 3
            TransportList = new ObservableCollection<TransportModel>();
            Refresh_TransportList = new ObservableCollection<TransportModel>();

            //Step 4
            foreach (TOUR_TRANSPORT item in transportList)
            {
                TransportModel transportModel = new TransportModel
                {
                    TRANSPORT_ID = item.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = item.TOUR_TRANSPORT_NAME,
                    TRANSPORT_LICENSE_PLATE = item.TOUR_TRANSPORT_LICENSE_PLATE,
                    TRANSPORT_COMPANY = item.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = item.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = item.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)item.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_IMAGE_BYTE_SOURCE = item.TOUR_TRANSPORT_IMAGE
                };
                transportModel.TRANSPORT_STRING_DATE = transportModel.TRANSPORT_DATE.ToString("dd/MM/yyyy");

                TransportList.Add(transportModel);
                Refresh_TransportList.Add(transportModel);
            }
        }

        /*
         * Load all command of View Model
         */
        private void LoadCommand()
        {
            #region Command of TransportUC

            AddTransportCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddTransportUC(User_ID));

            ShowTransportDetailCommand = new RelayCommand<ContentControl>(_ => Transport_Selected != null, p => p.Content = new DisplayTransportUC(Transport_Selected, User_ID));

            #endregion Command of TransportUC

            #region Command of AddTransportUC

            AddImageCommand = new RelayCommand<object>(_ => IsExcuteAddImageCommand(), _ => ExcuteAddImageCommand());

            ConfirmCommand = new RelayCommand<ContentControl>(_ => IsExcuteComfirmCommand(), p =>
            {
                ExcuteConfirmCommand();
                p.Content = new TransportUC(User_ID);
            });

            CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TransportUC(User_ID));

            #endregion Command of AddTransportUC

            #region Command of DisplayTransportUC

            ModifyImageCommand = new RelayCommand<object>(_ => true, _ => ExcuteAddImageCommand());

            SaveChangeCommand = new RelayCommand<ContentControl>(_ => IsExcuteSaveChangeCommand(), p =>
            {
                ExcuteSaveChangeCommand();
                p.Content = new TransportUC(User_ID);
            });

            RemoveCommand = new RelayCommand<ContentControl>(_ => IsExcuteRemoveCommand(), p =>
            {
                ExcuteRemoveCommand();
                p.Content = new TransportUC(User_ID);
            });

            #endregion Command of DisplayTransportUC
        }

        /*
         * Create list in ComboBox of TransportUC
         */
        private void LoadTransportComboBox()
        {
            CB_TransportList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("License Plate", false),
                new ComboBoxModel("Company", false),
                new ComboBoxModel("Status", false)
            };
            CB_TransportSelected = CB_TransportList.FirstOrDefault(x => x.IsSelected);
        }
        #endregion All Load Function

        #region AddTransportUC View Model

        /*
        * Function IsExcute and Excute of AddImageCommand
        * IsExcute = true when image in button is empty
        * Excute is save image to database and system
        */
        private bool IsExcuteAddImageCommand()
        {
            return Transport_Image_Byte_Source == null;
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

        /*
         * Function IsExcute and Excute of ConfirmCommand
         * IsExcute = true when any textbox in AddTransportUC except Descritption is filled
         * Excute is save all textbox to database
         */
        private bool IsExcuteComfirmCommand()
        {
            if (string.IsNullOrEmpty(Transport_Name) ||
                string.IsNullOrEmpty(Transport_Company) ||
                string.IsNullOrEmpty(Transport_License_Plate) ||
                string.IsNullOrEmpty(Transport_Type))
            {
                return false;
            }

            return true;
        }

        private void ExcuteConfirmCommand()
        {
            /*
             * Step 1: Connect to database
             * Step 2: Add new Transport to Tour_Transport in database
             * Step 3: Get ID new Transport and Add new Delete because when delete just enable the Transport
             * Step 4: Get ID new Transport and Add new Record
             * Step 5: Save Database
             * Step 6: Show Message Box
             */

            //Step 1
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            //Step 2
            TOUR_TRANSPORT tour_transport = new TOUR_TRANSPORT()
            {
                TOUR_TRANSPORT_NAME = Transport_Name,
                TOUR_TRANSPORT_COMPANY = Transport_Company,
                TOUR_TRANSPORT_LICENSE_PLATE = Transport_License_Plate,
                TOUR_TRANSPORT_TYPE = Transport_Type,
                TOUR_TRANSPORT_DESCRIPTION = string.IsNullOrEmpty(_Transport_Description) ? "" : Transport_Description,
                TOUR_TRANSPORT_START_DATE = DateTime.Now,
                TOUR_TRANSPORT_IMAGE = Transport_Image_Byte_Source
            };
            db.TOUR_TRANSPORT.Add(tour_transport);

            //Step 3
            TOUR_TRANSPORT_DELETE tour_transport_delete = new TOUR_TRANSPORT_DELETE()
            {
                TOUR_TRANSPORT_ID = tour_transport.TOUR_TRANSPORT_ID,
                TOUR_TRANSPORT_DELETE_ISDELETED = false
            };
            db.TOUR_TRANSPORT_DELETE.Add(tour_transport_delete);

            //Step 4
            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = "Add new transport with Name: " + Transport_Name
            };
            db.TOUR_RECORD.Add(tour_record);

            //Step 5
            db.SaveChanges();

            //Step 6
            MessageBox.Show("Added successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion AddTransportUC View Model

        #region DisplayTransportUC View Model

        /*
        * Function IsExcute and Excute of SaveChangeCommand
        * IsExcute = true when any textbox in DisplayTransportUC
        * Excute is save all textbox to database
        */
        private bool IsExcuteSaveChangeCommand()
        {
            Transport_String_Date = Transport_Date.ToString("dd/MM/yyyy");
            if (Transport_Name != Transport_Selected.TRANSPORT_NAME)
            {
                return true;
            }
            if (Transport_License_Plate != Transport_Selected.TRANSPORT_LICENSE_PLATE)
            {
                return true;
            }
            if (Transport_Company != Transport_Selected.TRANSPORT_COMPANY)
            {
                return true;
            }
            if (Transport_Type != Transport_Selected.TRANSPORT_TYPE)
            {
                return true;
            }
            if (Transport_String_Date != Transport_Selected.TRANSPORT_STRING_DATE)
            {
                return true;
            }
            if (Transport_Description != Transport_Selected.TRANSPORT_DESCRIPTION)
            {
                return true;
            }
            if (Transport_Image_Byte_Source != Transport_Selected.TRANSPORT_IMAGE_BYTE_SOURCE)
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

                TOUR_TRANSPORT tour_transport = db.TOUR_TRANSPORT.Where(x => x.TOUR_TRANSPORT_ID == Transport_Selected.TRANSPORT_ID).SingleOrDefault();
                tour_transport.TOUR_TRANSPORT_NAME = Transport_Name;
                tour_transport.TOUR_TRANSPORT_LICENSE_PLATE = Transport_License_Plate;
                tour_transport.TOUR_TRANSPORT_COMPANY = Transport_Company;
                tour_transport.TOUR_TRANSPORT_TYPE = Transport_Type;
                tour_transport.TOUR_TRANSPORT_DESCRIPTION = Transport_Description;
                tour_transport.TOUR_TRANSPORT_START_DATE = Transport_Date;
                tour_transport.TOUR_TRANSPORT_IMAGE = Transport_Image_Byte_Source;

                string changeToSave = "";
                int countChangeToSave = 0;
                Transport_String_Date = Transport_Date.ToString("dd/MM/yyyy");
                if (Transport_Name != Transport_Selected.TRANSPORT_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_NAME, Transport_Name);
                    countChangeToSave++;
                }
                if (Transport_License_Plate != Transport_Selected.TRANSPORT_LICENSE_PLATE)
                {
                    changeToSave += string.Format("License Plate Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_LICENSE_PLATE, Transport_License_Plate);
                    countChangeToSave++;
                }
                if (Transport_Company != Transport_Selected.TRANSPORT_COMPANY)
                {
                    changeToSave += string.Format("Company Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_COMPANY, Transport_Company);
                    countChangeToSave++;
                }
                if (Transport_Type != Transport_Selected.TRANSPORT_TYPE)
                {
                    changeToSave += string.Format("Type Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_TYPE, Transport_Type);
                    countChangeToSave++;
                }
                if (Transport_String_Date != Transport_Selected.TRANSPORT_STRING_DATE)
                {
                    changeToSave += string.Format("Start Date Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_STRING_DATE, Transport_String_Date);
                    countChangeToSave++;
                }
                if (Transport_Description != Transport_Selected.TRANSPORT_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", Transport_Selected.TRANSPORT_DESCRIPTION, Transport_Description);
                    countChangeToSave++;
                }
                if (Transport_Image_Byte_Source != Transport_Selected.TRANSPORT_IMAGE_BYTE_SOURCE)
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
         * Function IsExcute and Excute of RemoveCommand
         * IsExcute = true when remove note is filled
         * Excute is this staff will enable to database
         */
        private bool IsExcuteRemoveCommand()
        {
            return !string.IsNullOrEmpty(Transport_Note_Remove);
        }

        private void ExcuteRemoveCommand()
        {
            Tour_Mangement_DatabaseEntities db = new Tour_Mangement_DatabaseEntities();

            TOUR_TRANSPORT_DELETE tour_transport_delete = db.TOUR_TRANSPORT_DELETE.Where(x => x.TOUR_TRANSPORT_ID == Transport_Selected.TRANSPORT_ID).SingleOrDefault();
            tour_transport_delete.TOUR_TRANSPORT_DELETE_ISDELETED = true;
            tour_transport_delete.TOUR_TRANSPORT_DELETE_DATE = DateTime.Now;
            tour_transport_delete.TOUR_TRANSPORT_DELETE_CONTENT = Transport_Note_Remove;

            TOUR_RECORD tour_record = new TOUR_RECORD()
            {
                TOUR_STAFF_ID = User_ID,
                TOUR_RECORD_DATE = DateTime.Now,
                TOUR_RECORD_CONTENT = string.Format("Remove Transport {0} with {1} because {2}", Transport_Name, Transport_Selected.TRANSPORT_ID, Transport_Note_Remove)
            };
            db.TOUR_RECORD.Add(tour_record);
            db.SaveChanges();
            MessageBox.Show("Removed successfully");
        }
        #endregion DisplayTransportUC View Model
    }
}
