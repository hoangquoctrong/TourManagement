using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTourInformationViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;
        public int Tour_ID
        { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private ObservableCollection<PlaceModel> _PlaceList;
        public ObservableCollection<PlaceModel> PlaceList
        { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        public AddTourInformationViewModel(int user_id, int tour_id, ObservableCollection<PlaceModel> places)
        {
            //Setup when intitalize
            User_ID = user_id;
            Tour_ID = tour_id;
            PlaceList = places;

            //Setup Schedule
            ScheduleList = new BindableCollection<TourScheduleModel>();
            ScheduleCount = 0;

            //Setup Location
            LocationSelectedList = new BindableCollection<LocationModel>();

            //Setup Hotel
            HotelSelectedList = new BindableCollection<HotelModel>();
            HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");

            //Setup Transport
            TransportSelectedList = new BindableCollection<TransportModel>();
            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");

            //Setup Mission
            MissionList = new BindableCollection<MissionModel>();
            MissionCount = 0;
            MissionPriceNotify = string.Format("Mission Price haven't updated yet!");

            ProgressBarVisbility = Visibility.Visible;
            LoadDataInUC(tour_id);
        }

        private async void LoadDataInUC(int tour_id)
        {
            await Task.Delay(3000);
            Tour_Name = TourHandleModel.GetTourName(tour_id);

            //Setup Location
            LocationList = GetLocationList(PlaceList);
            RefreshLocationList = GetLocationList(PlaceList);

            //Setup Hotel
            HotelList = GetHotelList(PlaceList);
            RefreshHotelList = GetHotelList(PlaceList);

            //Setup Transport
            TransportList = GetTransportList();
            RefreshTransportList = GetTransportList();

            ProgressBarVisbility = Visibility.Hidden;
        }

        #region Price

        private double _TotalPrice = 0;
        public double TotalPrice
        { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private double CalculatePrice(double hotelprice, double transportprice, double missionprice)
        {
            return hotelprice + transportprice + missionprice;
        }

        #endregion Price

        #region Time

        #region Parameter

        private string _Tour_Name;
        public string Tour_Name
        { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private int _Time_Day;
        public int Time_Day
        { get => _Time_Day; set { _Time_Day = value; OnPropertyChanged(); } }

        private int _Time_Night;
        public int Time_Night
        { get => _Time_Night; set { _Time_Night = value; OnPropertyChanged(); } }

        private DateTime _Time_Department = DateTime.Now;
        public DateTime Time_Department
        { get => _Time_Department; set { _Time_Department = value; OnPropertyChanged(); Time_End = Time_Department; } }

        private DateTime _Time_End = DateTime.Now;
        public DateTime Time_End
        { get => _Time_End; set { _Time_End = value; OnPropertyChanged(); } }

        #endregion Parameter

        #endregion Time

        #region Location

        #region Parameter

        private BindableCollection<CheckBoxModel> _LocationList = new BindableCollection<CheckBoxModel>();
        public BindableCollection<CheckBoxModel> LocationList
        { get => _LocationList; set { _LocationList = value; OnPropertyChanged(); } }

        public BindableCollection<LocationModel> LocationSelectedList { get; set; }

        private BindableCollection<CheckBoxModel> _RefreshLocationList = new BindableCollection<CheckBoxModel>();
        public BindableCollection<CheckBoxModel> RefreshLocationList
        { get => _RefreshLocationList; set { _RefreshLocationList = value; OnPropertyChanged(); } }

        private string _FilterLocationText;

        public string FilterLocationText
        {
            get => _FilterLocationText;
            set
            {
                _FilterLocationText = value;
                OnPropertyChanged("FilterLocationText");
                LocationItems_Filter();
            }
        }

        #endregion Parameter

        #region Function

        private BindableCollection<CheckBoxModel> GetLocationList(ObservableCollection<PlaceModel> places)
        {
            BindableCollection<LocationModel> LocationItems = PlaceHandleModel.GetLocationFromPlaceList(places);
            BindableCollection<CheckBoxModel> locationList = new BindableCollection<CheckBoxModel>();
            foreach (LocationModel item in LocationItems)
            {
                locationList.Add(new CheckBoxModel(item.LOCATION_NAME, item.PLACE_NAME, item.LOCATION_ID, false));
            }

            return locationList;
        }

        private void LocationItems_Filter()
        {
            LocationList = RefreshLocationList;
            if (!string.IsNullOrEmpty(FilterLocationText))
            {
                LocationList = new BindableCollection<CheckBoxModel>(LocationList.Where(x => x.CB_Name.Contains(FilterLocationText) ||
                                                            x.CB_Name.ToLower().Contains(FilterLocationText) ||
                                                            x.CB_Name.ToUpper().Contains(FilterLocationText)));
            }
        }

        #endregion Function

        #region Command

        private ICommand _LocationItemCheckCommand;

        public ICommand LocationItemCheckCommand
        {
            get
            {
                if (_LocationItemCheckCommand == null)
                {
                    _LocationItemCheckCommand = new RelayCommand<int>(null, p =>
                    {
                        CheckBoxModel checkBoxModel = LocationList.Where(x => x.CB_ID == p).FirstOrDefault();
                        if (checkBoxModel.IsSelected)
                        {
                            LocationSelectedList.Add(new LocationModel()
                            {
                                LOCATION_ID = checkBoxModel.CB_ID,
                                LOCATION_NAME = checkBoxModel.CB_Name,
                                PLACE_NAME = checkBoxModel.CB_Sub_Name
                            });
                        }
                        else
                        {
                            LocationModel locationModel = LocationSelectedList.Where(x => x.LOCATION_ID == checkBoxModel.CB_ID).FirstOrDefault();
                            int index = LocationSelectedList.IndexOf(locationModel);
                            LocationSelectedList.RemoveAt(index);
                        }

                        RefreshLocationList = LocationList;
                    });
                }
                return _LocationItemCheckCommand;
            }
        }

        #endregion Command

        #endregion Location

        #region Schedule

        #region Parameter

        private BindableCollection<TourScheduleModel> _ScheduleList = new BindableCollection<TourScheduleModel>();
        public BindableCollection<TourScheduleModel> ScheduleList
        { get => _ScheduleList; set { _ScheduleList = value; OnPropertyChanged(); } }

        private int _ScheduleCount;
        public int ScheduleCount
        { get => _ScheduleCount; set { _ScheduleCount = value; OnPropertyChanged(); } }

        #endregion Parameter

        #region Command

        private ICommand _AddScheduleCommand;

        public ICommand AddScheduleCommand
        {
            get
            {
                if (_AddScheduleCommand == null)
                {
                    _AddScheduleCommand = new RelayCommand<object>(null, p => AddTourSchedule());
                }
                return _AddScheduleCommand;
            }
        }

        private void AddTourSchedule()
        {
            ScheduleList.Add(new TourScheduleModel() { SCHEDULE_DAY = "", SCHEDULE_CONTENT = "" });
            ScheduleCount++;
        }

        private ICommand _RemoveScheduleCommand;

        public ICommand RemoveScheduleCommand
        {
            get
            {
                if (_RemoveScheduleCommand == null)
                {
                    _RemoveScheduleCommand = new RelayCommand<object>(p => ScheduleCount > 0, p => RemoveTourSchedule());
                }
                return _RemoveScheduleCommand;
            }
        }

        private void RemoveTourSchedule()
        {
            ScheduleList.RemoveAt(ScheduleCount - 1);
            ScheduleCount--;
        }

        #endregion Command

        #endregion Schedule

        #region Hotel

        #region Parameter

        private string _HotelPriceNotify = "";
        public string HotelPriceNotify
        { get => _HotelPriceNotify; set { _HotelPriceNotify = value; OnPropertyChanged(); } }

        private bool _ChangeHotel;
        public bool ChangeHotel
        { get => _ChangeHotel; set { _ChangeHotel = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxHotelModel> _HotelList = new BindableCollection<CheckBoxHotelModel>();
        public BindableCollection<CheckBoxHotelModel> HotelList
        { get => _HotelList; set { _HotelList = value; OnPropertyChanged(); } }

        private BindableCollection<HotelModel> _HotelSelectedList = new BindableCollection<HotelModel>();
        public BindableCollection<HotelModel> HotelSelectedList
        { get => _HotelSelectedList; set { _HotelSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxHotelModel> _RefreshHotelList = new BindableCollection<CheckBoxHotelModel>();
        public BindableCollection<CheckBoxHotelModel> RefreshHotelList
        { get => _RefreshHotelList; set { _RefreshHotelList = value; OnPropertyChanged(); } }

        private string _FilterHotelText;

        public string FilterHotelText
        {
            get => _FilterHotelText;
            set
            {
                _FilterHotelText = value;
                OnPropertyChanged("FilterHotelText");
                HotelItems_Filter();
            }
        }

        private double _TotalHotelPrice = 0;

        public double TotalHotelPrice
        {
            get => _TotalHotelPrice;
            set
            {
                _TotalHotelPrice = value;
                OnPropertyChanged();
                TotalPrice = CalculatePrice(TotalHotelPrice, TotalTransportPrice, TotalMissionPrice);
            }
        }

        #endregion Parameter

        #region Function

        private BindableCollection<CheckBoxHotelModel> GetHotelList(ObservableCollection<PlaceModel> places)
        {
            BindableCollection<HotelModel> HotelItems = HotelHandleModel.GetHotelFromPlaceList(places);
            BindableCollection<CheckBoxHotelModel> hotelList = new BindableCollection<CheckBoxHotelModel>();
            foreach (HotelModel item in HotelItems)
            {
                hotelList.Add(new CheckBoxHotelModel(item.HOTEL_NAME, item.HOTEL_PRICE, item.HOTEL_ID, false, item));
            }

            return hotelList;
        }

        private void HotelItems_Filter()
        {
            HotelList = RefreshHotelList;
            if (!string.IsNullOrEmpty(FilterHotelText))
            {
                HotelList = new BindableCollection<CheckBoxHotelModel>(HotelList.Where(x => x.CB_Name.Contains(FilterHotelText) ||
                                                            x.CB_Name.ToLower().Contains(FilterHotelText) ||
                                                            x.CB_Name.ToUpper().Contains(FilterHotelText)));
            }
        }

        #endregion Function

        #region Command

        private ICommand _SaveHotelPriceCommand;

        public ICommand SaveHotelPriceCommand
        {
            get
            {
                if (_SaveHotelPriceCommand == null)
                {
                    _SaveHotelPriceCommand = new RelayCommand<object>(p => ChangeHotel, p =>
                    {
                        TotalHotelPrice = HotelHandleModel.CalculateTotalHotelPrice(HotelSelectedList);
                        HotelPriceNotify = string.Format("Hotel Price have updated!");
                    });
                }
                return _SaveHotelPriceCommand;
            }
        }

        private ICommand _HotelItemCheckCommand;

        public ICommand HotelItemCheckCommand
        {
            get
            {
                if (_HotelItemCheckCommand == null)
                {
                    _HotelItemCheckCommand = new RelayCommand<int>(null, p =>
                    {
                        CheckBoxHotelModel checkBoxHotelModel = HotelList.Where(x => x.CB_ID == p).FirstOrDefault();
                        if (checkBoxHotelModel.IsSelected)
                        {
                            HotelSelectedList.Add(checkBoxHotelModel.HotelSelect);
                            HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");
                            ChangeHotel = true;
                        }
                        else
                        {
                            int index = HotelSelectedList.IndexOf(checkBoxHotelModel.HotelSelect);
                            HotelSelectedList.RemoveAt(index);
                            HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");
                            ChangeHotel = true;
                        }

                        RefreshHotelList = HotelList;
                    });
                }
                return _HotelItemCheckCommand;
            }
        }

        #endregion Command

        #endregion Hotel

        #region Transport

        #region Parameter

        private string _TransportPriceNotify = "";
        public string TransportPriceNotify
        { get => _TransportPriceNotify; set { _TransportPriceNotify = value; OnPropertyChanged(); } }

        private bool _ChangeTransport;
        public bool ChangeTransport
        { get => _ChangeTransport; set { _ChangeTransport = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxTransportModel> _TransportList = new BindableCollection<CheckBoxTransportModel>();
        public BindableCollection<CheckBoxTransportModel> TransportList
        { get => _TransportList; set { _TransportList = value; OnPropertyChanged(); } }

        private BindableCollection<TransportModel> _TransportSelectedList = new BindableCollection<TransportModel>();
        public BindableCollection<TransportModel> TransportSelectedList
        { get => _TransportSelectedList; set { _TransportSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxTransportModel> _RefreshTransportList = new BindableCollection<CheckBoxTransportModel>();
        public BindableCollection<CheckBoxTransportModel> RefreshTransportList
        { get => _RefreshTransportList; set { _RefreshTransportList = value; OnPropertyChanged(); } }

        private string _FilterTransportText;

        public string FilterTransportText
        {
            get => _FilterTransportText;
            set
            {
                _FilterTransportText = value;
                OnPropertyChanged("FilterTransportText");
                TransportItems_Filter();
            }
        }

        private double _TotalTransportPrice = 0;

        public double TotalTransportPrice
        {
            get => _TotalTransportPrice;
            set
            {
                _TotalTransportPrice = value;
                OnPropertyChanged();
                TotalPrice = CalculatePrice(TotalHotelPrice, TotalTransportPrice, TotalMissionPrice);
            }
        }

        #endregion Parameter

        #region Function

        private BindableCollection<CheckBoxTransportModel> GetTransportList()
        {
            ObservableCollection<TransportModel> TransportItems = TransportHandleModel.GetTransportListWithoutDelete();
            BindableCollection<CheckBoxTransportModel> transportList = new BindableCollection<CheckBoxTransportModel>();
            foreach (TransportModel item in TransportItems)
            {
                transportList.Add(new CheckBoxTransportModel(item.TRANSPORT_NAME, item.TRANSPORT_PRICE, item.TRANSPORT_ID, false, item));
            }

            return transportList;
        }

        private void TransportItems_Filter()
        {
            TransportList = RefreshTransportList;
            if (!string.IsNullOrEmpty(FilterTransportText))
            {
                TransportList = new BindableCollection<CheckBoxTransportModel>(TransportList.Where(x => x.CB_Name.Contains(FilterTransportText) ||
                                                            x.CB_Name.ToLower().Contains(FilterTransportText) ||
                                                            x.CB_Name.ToUpper().Contains(FilterTransportText)));
            }
        }

        private ObservableCollection<ComboBoxModel> LoadTransportComboBox(int amount_max)
        {
            ObservableCollection<ComboBoxModel> CB_TransportAmount = new ObservableCollection<ComboBoxModel>();
            if (amount_max == 0)
            {
                return CB_TransportAmount;
            }

            for (int i = 1; i <= amount_max; i++)
            {
                ComboBoxModel comboBox = new ComboBoxModel(i.ToString(), i, false);
                CB_TransportAmount.Add(comboBox);
            }

            return CB_TransportAmount;
        }

        #endregion Function

        #region Command

        private ICommand _SaveTransportPriceCommand;

        public ICommand SaveTransportPriceCommand
        {
            get
            {
                if (_SaveTransportPriceCommand == null)
                {
                    _SaveTransportPriceCommand = new RelayCommand<object>(p => ChangeTransport, p =>
                    {
                        TotalTransportPrice = TransportHandleModel.CalculateTotalTransportPrice(TransportSelectedList);
                        TransportPriceNotify = string.Format("Transport Price have updated!");
                    });
                }
                return _SaveTransportPriceCommand;
            }
        }

        private ICommand _TransportItemCheckCommand;

        public ICommand TransportItemCheckCommand
        {
            get
            {
                if (_TransportItemCheckCommand == null)
                {
                    _TransportItemCheckCommand = new RelayCommand<int>(null, p =>
                    {
                        CheckBoxTransportModel checkBoxModel = TransportList.Where(x => x.CB_ID == p).FirstOrDefault();
                        if (checkBoxModel.IsSelected)
                        {
                            ChangeTransport = true;
                            TransportModel transport = checkBoxModel.TransportSelect;
                            transport.TRANSPORT_IS_AMOUNT = transport.TRANSPORT_AMOUNT_MAX == 0 ? false : true;
                            transport.CB_TransportAmount = LoadTransportComboBox(transport.TRANSPORT_AMOUNT_MAX);
                            TransportSelectedList.Add(transport);
                            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");
                        }
                        else
                        {
                            ChangeTransport = true;
                            int index = TransportSelectedList.IndexOf(checkBoxModel.TransportSelect);
                            TransportSelectedList.RemoveAt(index);
                            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");
                        }

                        RefreshTransportList = TransportList;
                    });
                }
                return _TransportItemCheckCommand;
            }
        }

        #endregion Command

        #endregion Transport

        #region Mission

        #region Parameter

        private BindableCollection<MissionModel> _MissionList = new BindableCollection<MissionModel>();

        public BindableCollection<MissionModel> MissionList
        {
            get => _MissionList; set
            {
                _MissionList = value;
                OnPropertyChanged("MissionList");
            }
        }

        private int _MissionCount;
        public int MissionCount
        { get => _MissionCount; set { _MissionCount = value; OnPropertyChanged(); } }

        private string _MissionPriceNotify = "";
        public string MissionPriceNotify
        { get => _MissionPriceNotify; set { _MissionPriceNotify = value; OnPropertyChanged(); } }

        private double _TotalMissionPrice = 0;

        public double TotalMissionPrice
        {
            get => _TotalMissionPrice;
            set
            {
                _TotalMissionPrice = value;
                OnPropertyChanged();
                TotalPrice = CalculatePrice(TotalHotelPrice, TotalTransportPrice, TotalMissionPrice);
            }
        }

        #endregion Parameter

        #region Command

        private ICommand _AddMissionCommand;

        public ICommand AddMissionCommand
        {
            get
            {
                if (_AddMissionCommand == null)
                {
                    _AddMissionCommand = new RelayCommand<object>(null, p => AddMission());
                }
                return _AddMissionCommand;
            }
        }

        private void AddMission()
        {
            MissionCount++;
            MissionList.Add(new MissionModel()
            {
                Mission_ID = MissionCount,
                Mission_Responsibility = "",
                Mission_Description = "",
                Mission_Count = 0,
                Mission_Price = 0
            });
            TotalMissionPrice = MissionHandleModel.CalculateTotalMissionPrice(MissionList);
            MissionPriceNotify = string.Format("Mission Price haven't updated yet!");
        }

        private ICommand _SaveMissionPriceCommand;

        public ICommand SaveMissionPriceCommand
        {
            get
            {
                if (_SaveMissionPriceCommand == null)
                {
                    _SaveMissionPriceCommand = new RelayCommand<object>(p => MissionCount > 0, p =>
                    {
                        TotalMissionPrice = MissionHandleModel.CalculateTotalMissionPrice(MissionList);
                        MissionPriceNotify = string.Format("Mission Price have updated!");
                    });
                }
                return _SaveMissionPriceCommand;
            }
        }

        private ICommand _RemoveMissionCommand;

        public ICommand RemoveMissionCommand
        {
            get
            {
                if (_RemoveMissionCommand == null)
                {
                    _RemoveMissionCommand = new RelayCommand<object>(p => MissionCount > 0, p => RemoveMission());
                }
                return _RemoveMissionCommand;
            }
        }

        private void RemoveMission()
        {
            MissionList.RemoveAt(ScheduleCount - 1);
            MissionCount--;
        }

        #endregion Command

        #endregion Mission

        private ICommand _AddTourInformationCommand;

        public ICommand AddTourInformationCommand
        {
            get
            {
                if (_AddTourInformationCommand == null)
                {
                    _AddTourInformationCommand = new RelayCommand<ContentControl>(p => IsExcuteAddInformationCommand(), p =>
                    {
                        if (IsExcuteAddInformationCommand())
                        {
                            ProgressBarVisbility = Visibility.Visible;
                            ExcuteAddInformationCommand(p);
                        }
                    });
                }
                return _AddTourInformationCommand;
            }
        }

        #region ExcuteAddInformationCommand

        private async void ExcuteAddInformationCommand(ContentControl p)
        {
            await Task.Delay(6000);
            int countSuccess = 0;
            int tourinformation_id = TourInformationHandleModel.InsertTourInformation(new TourInformationModel() { TOUR_ID = Tour_ID }, User_ID);

            if (tourinformation_id <= 0)
            {
                string messageDisplay = string.Format("Add Tour Information Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }
            //Time
            if (TourInformationHandleModel.InsertTourTime(InsertTourTime(), tourinformation_id, User_ID))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Time Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Price
            if (TourInformationHandleModel.InsertTourPrice(InsertTourPrice(), tourinformation_id, User_ID))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Price Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Location
            if (PlaceHandleModel.InsertLocationDetail(LocationSelectedList, tourinformation_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Locations Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Hotel
            if (HotelHandleModel.InsertHotelDetail(HotelSelectedList, tourinformation_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Hotel Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Transport
            if (TransportHandleModel.InsertTransportDetail(TransportSelectedList, tourinformation_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Transport Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Mission
            if (MissionHandleModel.InsertMissionList(MissionList, tourinformation_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Mission Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Schedule
            if (TourInformationHandleModel.InsertTourScheduleList(ScheduleList, tourinformation_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Schedule Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            if (countSuccess == 7)
            {
                string messageDisplay = string.Format("Add Tour Information Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                p.Content = new ShowTourViewModel(User_ID, Tour_ID, Visibility.Visible, Visibility.Visible);
            }
            else
            {
                string messageDisplay = string.Format("Add Tour Information failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private TourTimeModel InsertTourTime()
        {
            return new TourTimeModel()
            {
                TIME_DAY = Time_Day,
                TIME_NIGHT = Time_Night,
                TIME_DEPARTMENT_TIME = Time_Department,
                TIME_END_TIME = Time_End,
                TIME_DEPARTMENT_STRING = Time_Department.ToString("dd/MM/yyyy"),
                TIME_END_STRING = Time_Department.ToString("dd/MM/yyyy"),
                TIME_STRING = string.Format("{0} day(s) {1} night(s)", Time_Day, Time_End)
            };
        }

        private TourPriceModel InsertTourPrice()
        {
            return new TourPriceModel()
            {
                PRICE_COST_HOTEL = TotalHotelPrice,
                PRICE_COST_TRANSPORT = TotalTransportPrice,
                PRICE_COST_SERVICE = TotalMissionPrice,
                PRICE_COST_TOTAL = TotalPrice,
                PRICE_NOTE = ""
            };
        }

        #endregion ExcuteAddInformationCommand

        #region IsExcuteAddInformationCommand

        private bool IsExcuteAddInformationCommand()
        {
            Console.WriteLine("Time " + IsExcuteTime());
            Console.WriteLine("Hotel " + IsExcuteHotel());
            Console.WriteLine("Transport " + IsExcuteTransport());
            Console.WriteLine("Mission " + IsExcuteMission());
            Console.WriteLine("Price " + IsExcutePrice());
            Console.WriteLine("Schedule " + IsExcuteSchedule());
            string messageError = "";
            int countValid = 0;
            if (!IsExcuteTime())
            {
                messageError += string.Format("The Time is unavailable.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcuteLocation())
            {
                messageError += string.Format("Please choose the location.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcuteHotel())
            {
                messageError += string.Format("Please choose hotel and choose the Day of each Hotel.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcuteTransport())
            {
                messageError += string.Format("Please choose transport and choose the Amount of each Transport.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcuteMission())
            {
                messageError += string.Format("Please fill Mission for tour.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcutePrice())
            {
                messageError += string.Format("Please check the Price of Hotel,Transport, Mission.\n");
            }
            else
            {
                countValid++;
            }

            if (!IsExcuteSchedule())
            {
                messageError += string.Format("Please fill Schedule for tour. ");
            }
            else
            {
                countValid++;
            }

            if (countValid == 7)
            {
                return true;
            }
            else
            {
                //MessageWindow messageWindow = new MessageWindow(messageError, MessageType.Error, MessageButtons.Ok);
                //messageWindow.ShowDialog();
                return false;
            }
        }

        private bool IsExcuteTime()
        {
            if (Time_Night <= 0)
            {
                return false;
            }
            if (Time_Day <= 0)
            {
                return false;
            }
            if (Math.Abs(Time_Day - Time_Night) > 1)
            {
                return false;
            }
            int total_date = Math.Min(Time_Day, Time_Night);
            int department_int = (Time_End - Time_Department).Days;
            return department_int == total_date;
        }

        private bool IsExcuteLocation()
        {
            return LocationSelectedList.Count() > 0;
        }

        private bool IsExcuteSchedule()
        {
            if (ScheduleCount == 0)
            {
                return false;
            }

            foreach (var item in ScheduleList)
            {
                if (string.IsNullOrEmpty(item.SCHEDULE_DAY) || string.IsNullOrEmpty(item.SCHEDULE_CONTENT))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsExcuteHotel()
        {
            if (HotelSelectedList.Count() == 0)
            {
                return false;
            }

            int countValid = 0;
            for (int i = 0; i < HotelSelectedList.Count(); i++)
            {
                if (HotelSelectedList[i].HOTEL_DAY > 0)
                {
                    countValid++;
                }
            }

            if (countValid == HotelSelectedList.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExcuteTransport()
        {
            if (TransportSelectedList.Count() == 0)
            {
                return false;
            }

            int countValid = 0;
            for (int i = 0; i < TransportSelectedList.Count(); i++)
            {
                if (TransportSelectedList[i].TRANSPORT_AMOUNT > 0)
                {
                    countValid++;
                }
            }

            if (countValid == TransportSelectedList.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExcuteMission()
        {
            if (MissionCount == 0)
            {
                return false;
            }

            foreach (var item in MissionList)
            {
                if (string.IsNullOrEmpty(item.Mission_Responsibility) || item.Mission_Price == 0 || item.Mission_Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsExcutePrice()
        {
            return TotalPrice > 0 && !MissionPriceNotify.Equals("Mission Price haven't updated yet!") &&
                                    !HotelPriceNotify.Equals("Hotel Price haven't updated yet!") &&
                                    !TransportPriceNotify.Equals("Transport Price haven't updated yet!");
        }

        #endregion IsExcuteAddInformationCommand

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new ShowTourViewModel(User_ID, Tour_ID, Visibility.Visible, Visibility.Visible));
                }
                return _CancelCommand;
            }
        }
    }
}