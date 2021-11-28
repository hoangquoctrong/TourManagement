using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTourInformationViewModel : BaseViewModel
    {

        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;
        public int Tour_ID { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private ObservableCollection<PlaceModel> _PlaceList;
        public ObservableCollection<PlaceModel> PlaceList { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        public AddTourInformationViewModel(int user_id, int tour_id, ObservableCollection<PlaceModel> places)
        {
            //Setup when intitalize
            User_ID = user_id;
            Tour_ID = tour_id;
            PlaceList = places;
            Tour_Name = TourHandleModel.GetTourName(tour_id);

            //Setup Schedule
            ScheduleList = new BindableCollection<TourScheduleModel>();
            ScheduleCount = 0;

            //Setup Location
            LocationList = GetLocationList(PlaceList);
            RefreshLocationList = GetLocationList(PlaceList);
            LocationSelectedList = new BindableCollection<LocationModel>();

            //Setup Hotel
            HotelList = GetHotelList(PlaceList);
            RefreshHotelList = GetHotelList(PlaceList);
            HotelSelectedList = new BindableCollection<HotelModel>();

            //Setup Transport
            TransportList = GetTransportList();
            RefreshTransportList = GetTransportList();
            TransportSelectedList = new BindableCollection<TransportModel>();

            //Setup Mission
            MissionList = new BindableCollection<MissionModel>();
            MissionCount = 0;
            MissionPriceNotify = string.Format("Mission Price haven't updated yet!");
        }

        #region Price
        private double _TotalPrice = 0;
        public double TotalPrice { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private double CalculatePrice(double hotelprice, double transportprice, double missionprice)
        {
            return hotelprice + transportprice + missionprice;
        }
        #endregion Price

        #region Time

        #region Parameter
        private string _Tour_Name;
        public string Tour_Name { get => _Tour_Name; set { _Tour_Name = value; OnPropertyChanged(); } }

        private int _Time_Day;
        public int Time_Day { get => _Time_Day; set { _Time_Day = value; OnPropertyChanged(); } }

        private int _Time_Night;
        public int Time_Night { get => _Time_Night; set { _Time_Night = value; OnPropertyChanged(); } }

        private DateTime _Time_Department = DateTime.Now;
        public DateTime Time_Department { get => _Time_Department; set { _Time_Department = value; OnPropertyChanged(); } }

        private DateTime _Time_End = DateTime.Now;
        public DateTime Time_End { get => _Time_End; set { _Time_End = value; OnPropertyChanged(); } }
        #endregion Parameter

        #endregion Time

        #region Location

        #region Parameter

        private BindableCollection<CheckBoxModel> _LocationList;
        public BindableCollection<CheckBoxModel> LocationList { get => _LocationList; set { _LocationList = value; OnPropertyChanged(); } }

        public BindableCollection<LocationModel> LocationSelectedList { get; set; }

        private BindableCollection<CheckBoxModel> _RefreshLocationList;
        public BindableCollection<CheckBoxModel> RefreshLocationList { get => _RefreshLocationList; set { _RefreshLocationList = value; OnPropertyChanged(); } }

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
        private BindableCollection<TourScheduleModel> _ScheduleList;
        public BindableCollection<TourScheduleModel> ScheduleList { get => _ScheduleList; set { _ScheduleList = value; OnPropertyChanged(); } }

        private int _ScheduleCount;
        public int ScheduleCount { get => _ScheduleCount; set { _ScheduleCount = value; OnPropertyChanged(); } }
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

        private BindableCollection<CheckBoxHotelModel> _HotelList;
        public BindableCollection<CheckBoxHotelModel> HotelList { get => _HotelList; set { _HotelList = value; OnPropertyChanged(); } }

        private BindableCollection<HotelModel> _HotelSelectedList;
        public BindableCollection<HotelModel> HotelSelectedList { get => _HotelSelectedList; set { _HotelSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxHotelModel> _RefreshHotelList;
        public BindableCollection<CheckBoxHotelModel> RefreshHotelList { get => _RefreshHotelList; set { _RefreshHotelList = value; OnPropertyChanged(); } }

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
                            TotalHotelPrice = HotelHandleModel.CalculateTotalHotelPrice(HotelSelectedList);
                        }
                        else
                        {
                            int index = HotelSelectedList.IndexOf(checkBoxHotelModel.HotelSelect);
                            HotelSelectedList.RemoveAt(index);
                            TotalHotelPrice = HotelHandleModel.CalculateTotalHotelPrice(HotelSelectedList);
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

        private BindableCollection<CheckBoxTransportModel> _TransportList;
        public BindableCollection<CheckBoxTransportModel> TransportList { get => _TransportList; set { _TransportList = value; OnPropertyChanged(); } }

        private BindableCollection<TransportModel> _TransportSelectedList;
        public BindableCollection<TransportModel> TransportSelectedList { get => _TransportSelectedList; set { _TransportSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxTransportModel> _RefreshTransportList;
        public BindableCollection<CheckBoxTransportModel> RefreshTransportList { get => _RefreshTransportList; set { _RefreshTransportList = value; OnPropertyChanged(); } }

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
        #endregion Function

        #region Command
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
                            TransportSelectedList.Add(checkBoxModel.TransportSelect);
                            TotalTransportPrice = TransportHandleModel.CalculateTotalTransportPrice(TransportSelectedList);
                        }
                        else
                        {
                            int index = TransportSelectedList.IndexOf(checkBoxModel.TransportSelect);
                            TransportSelectedList.RemoveAt(index);
                            TotalTransportPrice = TransportHandleModel.CalculateTotalTransportPrice(TransportSelectedList);
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
        private BindableCollection<MissionModel> _MissionList;
        public BindableCollection<MissionModel> MissionList
        {
            get => _MissionList; set
            {
                _MissionList = value;
                OnPropertyChanged("MissionList");
            }
        }

        private int _MissionCount;
        public int MissionCount { get => _MissionCount; set { _MissionCount = value; OnPropertyChanged(); } }

        private string _MissionPriceNotify;
        public string MissionPriceNotify { get => _MissionPriceNotify; set { _MissionPriceNotify = value; OnPropertyChanged(); } }

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
                    _AddTourInformationCommand = new RelayCommand<ContentControl>(p => IsExcuteAddInformationCommand(), p => ExcuteAddInformationCommand(p));
                }
                return _AddTourInformationCommand;
            }
        }

        private void ExcuteAddInformationCommand(ContentControl p)
        {
            throw new NotImplementedException();
        }

        private bool IsExcuteAddInformationCommand()
        {
            return false;
        }

        private bool IsExcuteTime()
        {
            return Time_Day > 0 && Time_Night > 0 && Time_Department != null && Time_End != null;
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new ShowTourViewModel(User_ID, Tour_ID));
                }
                return _CancelCommand;
            }
        }
    }
}
