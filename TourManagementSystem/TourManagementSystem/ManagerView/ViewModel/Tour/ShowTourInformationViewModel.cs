using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowTourInformationViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private int _Tour_ID;
        public int Tour_ID { get => _Tour_ID; set { _Tour_ID = value; OnPropertyChanged(); } }

        private int _TourInformation_ID;
        public int TourInformation_ID { get => _TourInformation_ID; set { _TourInformation_ID = value; OnPropertyChanged(); } }

        private ObservableCollection<PlaceModel> _PlaceList;
        public ObservableCollection<PlaceModel> PlaceList { get => _PlaceList; set { _PlaceList = value; OnPropertyChanged(); } }

        private TourInformationModel _InformationSelected;
        public TourInformationModel InformationSelected { get => _InformationSelected; set { _InformationSelected = value; OnPropertyChanged(); } }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        public ShowTourInformationViewModel(int user_id, int tour_id, TourInformationModel tourInformation, ObservableCollection<PlaceModel> places, bool isenable)
        {
            //Setup when intitalize
            User_ID = user_id;
            Tour_ID = tour_id;
            PlaceList = places;
            IsEnable = isenable;
            Tour_Name = TourHandleModel.GetTourName(tour_id);
            InformationSelected = tourInformation;
            TourInformation_ID = tourInformation.INFORMATION_ID;

            //Setup Price && Time
            SelectPrice = tourInformation.INFORMATION_PRICE;
            SelectTime = tourInformation.INFORMATION_TIME;
            SetDataInView(SelectPrice, SelectTime);

            //Setup Schedule
            ScheduleList = new BindableCollection<TourScheduleModel>(tourInformation.INFORMATION_SCHEDULE_LIST);
            RefreshScheduleList = new BindableCollection<TourScheduleModel>(tourInformation.INFORMATION_SCHEDULE_LIST);
            ScheduleCount = ScheduleList.Count;
            ScheduleNotify = string.Format("Schedule hasn't updated yet!");

            //Setup Location
            LocationSelectedList = new BindableCollection<LocationModel>(tourInformation.INFORMATION_LOCATION_LIST);
            RefreshLocationSelectedList = new BindableCollection<LocationModel>(tourInformation.INFORMATION_LOCATION_LIST);
            LocationList = GetLocationList(PlaceList);
            RefreshLocationList = GetLocationList(PlaceList);

            //Setup Hotel
            HotelSelectedList = new BindableCollection<HotelModel>(tourInformation.INFORMATION_HOTEL_LIST);
            RefreshHotelSelectedList = new BindableCollection<HotelModel>(tourInformation.INFORMATION_HOTEL_LIST);
            HotelList = GetHotelList(PlaceList);
            RefreshHotelList = GetHotelList(PlaceList);
            HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");

            //Setup Transport
            TransportSelectedList = new BindableCollection<TransportModel>(tourInformation.INFORMATION_TRANSPORT_LIST);
            RefreshTransportSelectedList = new BindableCollection<TransportModel>(tourInformation.INFORMATION_TRANSPORT_LIST);
            TransportList = GetTransportList();
            RefreshTransportList = GetTransportList();
            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");

            //Setup Mission
            MissionList = new BindableCollection<MissionModel>(tourInformation.INFORMATION_MISSION_LIST);
            RefreshMissionList = new BindableCollection<MissionModel>(tourInformation.INFORMATION_MISSION_LIST);
            MissionCount = MissionList.Count;
            MissionBeforeSave = MissionList.Count;
            MissionPriceNotify = string.Format("Mission Price hasn't updated yet!");
        }

        private void SetDataInView(TourPriceModel price, TourTimeModel time)
        {
            Time_Day = time.TIME_DAY;
            Time_Night = time.TIME_NIGHT;
            Time_Department = time.TIME_DEPARTMENT_TIME;
            Time_End = time.TIME_END_TIME;
            TotalHotelPrice = price.PRICE_COST_HOTEL;
            TotalTransportPrice = price.PRICE_COST_TRANSPORT;
            TotalMissionPrice = price.PRICE_COST_SERVICE;
            TotalPrice = price.PRICE_COST_TOTAL;
        }

        #region Price
        private TourPriceModel _SelectPrice;
        public TourPriceModel SelectPrice { get => _SelectPrice; set { _SelectPrice = value; OnPropertyChanged(); } }

        private double _TotalPrice = 0;
        public double TotalPrice { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private double CalculatePrice(double hotelprice, double transportprice, double missionprice)
        {
            return hotelprice + transportprice + missionprice;
        }

        private TourPriceModel GetPriceModel()
        {
            return new TourPriceModel()
            {
                PRICE_ID = SelectPrice.PRICE_ID,
                PRICE_COST_HOTEL = TotalHotelPrice,
                PRICE_COST_SERVICE = TotalMissionPrice,
                PRICE_COST_TRANSPORT = TotalTransportPrice,
                PRICE_COST_TOTAL = TotalPrice,
                PRICE_NOTE = ""
            };
        }
        #endregion Price

        #region Time

        #region Parameter
        private TourTimeModel _SelectTime;
        public TourTimeModel SelectTime { get => _SelectTime; set { _SelectTime = value; OnPropertyChanged(); } }

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

        #region Command
        private ICommand _SaveTourTimeCommand;
        public ICommand SaveTourTimeCommand
        {
            get
            {
                if (_SaveTourTimeCommand == null)
                {
                    _SaveTourTimeCommand = new RelayCommand<object>(p => IsExcuteSaveTimeCommand(), p => ExcuteSaveTimeCommand());
                }
                return _SaveTourTimeCommand;
            }
        }

        private void ExcuteSaveTimeCommand()
        {
            TourTimeModel time = new TourTimeModel()
            {
                TIME_ID = SelectTime.TIME_ID,
                TIME_DAY = Time_Day,
                TIME_NIGHT = Time_Night,
                TIME_STRING = string.Format("{0} day(s) {1} night(s)", Time_Day, Time_Night),
                TIME_DEPARTMENT_TIME = Time_Department,
                TIME_DEPARTMENT_STRING = Time_Department.ToString("dd/MM/yyyy"),
                TIME_END_TIME = Time_End,
                TIME_END_STRING = Time_End.ToString("dd/MM/yyyy")
            };

            if (TourInformationHandleModel.UpdateTourTime(time, TourInformation_ID, User_ID))
            {
                MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectTime = time;
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveTimeCommand()
        {
            if (Time_Night == SelectTime.TIME_NIGHT && Time_Day == SelectTime.TIME_DAY &&
                Time_Department == SelectTime.TIME_DEPARTMENT_TIME && Time_End == SelectTime.TIME_END_TIME)
            {
                return false;
            }
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
            int total_date = Math.Max(Time_Day, Time_Night);
            int department_int = (Time_End - Time_Department).Days;
            return department_int >= total_date;
        }
        #endregion Command
        #endregion Time

        #region Location

        #region Parameter

        private BindableCollection<CheckBoxModel> _LocationList;
        public BindableCollection<CheckBoxModel> LocationList { get => _LocationList; set { _LocationList = value; OnPropertyChanged(); } }

        private BindableCollection<LocationModel> _LocationSelectedList;
        public BindableCollection<LocationModel> LocationSelectedList { get => _LocationSelectedList; set { _LocationSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<LocationModel> _RefreshLocationSelectedList;
        public BindableCollection<LocationModel> RefreshLocationSelectedList { get => _RefreshLocationSelectedList; set { _RefreshLocationSelectedList = value; OnPropertyChanged(); } }

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
                bool IsSelect = false;
                foreach (var itemselect in LocationSelectedList)
                {
                    if (itemselect.LOCATION_ID == item.LOCATION_ID)
                    {
                        IsSelect = true;
                        break;
                    }
                }
                locationList.Add(new CheckBoxModel(item.LOCATION_NAME, item.PLACE_NAME, item.LOCATION_ID, IsSelect));
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

        private ICommand _SaveLocationCommand;
        public ICommand SaveLocationCommand
        {
            get
            {
                if (_SaveLocationCommand == null)
                {
                    _SaveLocationCommand = new RelayCommand<object>(p => IsExcuteSaveLocationCommand(), p => ExcuteSaveLocationCommand());
                }
                return _SaveLocationCommand;
            }
        }

        private void ExcuteSaveLocationCommand()
        {
            if (PlaceHandleModel.DeleteLocationDetail(TourInformation_ID))
            {
                if (PlaceHandleModel.InsertLocationDetail(LocationSelectedList, TourInformation_ID, User_ID, false))
                {
                    MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshLocationSelectedList = PlaceHandleModel.GetLocationFromLocationDetail(TourInformation_ID, PlaceList);
                }
                else
                {
                    MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveLocationCommand()
        {
            if (RefreshLocationSelectedList.Count != LocationSelectedList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (LocationModel item in LocationSelectedList)
            {
                foreach (LocationModel item_before in RefreshLocationSelectedList)
                {
                    if (item.LOCATION_ID == item_before.LOCATION_ID)
                    {
                        count_equal++;
                        break;
                    }
                }
            }

            return count_equal != RefreshLocationSelectedList.Count;
        }
        #endregion Command

        #endregion Location

        #region Schedule

        #region Parameter
        private BindableCollection<TourScheduleModel> _ScheduleList;
        public BindableCollection<TourScheduleModel> ScheduleList { get => _ScheduleList; set { _ScheduleList = value; OnPropertyChanged(); } }

        private BindableCollection<TourScheduleModel> _RefreshScheduleList;
        public BindableCollection<TourScheduleModel> RefreshScheduleList { get => _RefreshScheduleList; set { _RefreshScheduleList = value; OnPropertyChanged(); } }

        private int _ScheduleCount;
        public int ScheduleCount { get => _ScheduleCount; set { _ScheduleCount = value; OnPropertyChanged(); } }

        private string _ScheduleNotify;
        public string ScheduleNotify { get => _ScheduleNotify; set { _ScheduleNotify = value; OnPropertyChanged(); } }
        #endregion Parameter

        #region Command
        private ICommand _AddScheduleCommand;
        public ICommand AddScheduleCommand
        {
            get
            {
                if (_AddScheduleCommand == null)
                {
                    _AddScheduleCommand = new RelayCommand<object>(p => IsEnable, p => AddTourSchedule());
                }
                return _AddScheduleCommand;
            }
        }
        private void AddTourSchedule()
        {
            ScheduleList.Add(new TourScheduleModel() { SCHEDULE_DAY = "", SCHEDULE_CONTENT = "" });
            ScheduleCount++;
            ScheduleNotify = string.Format("Schedule hasn't updated yet!");
        }

        private ICommand _RemoveScheduleCommand;
        public ICommand RemoveScheduleCommand
        {
            get
            {
                if (_RemoveScheduleCommand == null)
                {
                    _RemoveScheduleCommand = new RelayCommand<object>(p => ScheduleCount > 0 && IsEnable, p => RemoveTourSchedule());
                }
                return _RemoveScheduleCommand;
            }
        }

        private void RemoveTourSchedule()
        {
            ScheduleList.RemoveAt(ScheduleCount - 1);
            ScheduleCount--;
            ScheduleNotify = string.Format("Schedule hasn't updated yet!");
        }

        private ICommand _SaveScheduleListCommand;
        public ICommand SaveScheduleListCommand
        {
            get
            {
                if (_SaveScheduleListCommand == null)
                {
                    _SaveScheduleListCommand = new RelayCommand<object>(p => ScheduleCount > 0 && IsEnable, p =>
                    {
                        TotalMissionPrice = MissionHandleModel.CalculateTotalMissionPrice(MissionList);
                        ScheduleNotify = string.Format("Schedule has updated!");
                    });
                }
                return _SaveScheduleListCommand;
            }
        }

        private ICommand _SaveScheduleCommand;
        public ICommand SaveScheduleCommand
        {
            get
            {
                if (_SaveScheduleCommand == null)
                {
                    _SaveScheduleCommand = new RelayCommand<object>(p => IsExcuteSaveScheduleCommand(), p => ExcuteSaveScheduleCommand());
                }
                return _SaveScheduleCommand;
            }
        }

        private void ExcuteSaveScheduleCommand()
        {
            if (TourInformationHandleModel.DeleteScheduleDetail(TourInformation_ID))
            {
                if (TourInformationHandleModel.InsertTourScheduleList(ScheduleList, TourInformation_ID, User_ID, false))
                {
                    MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    ScheduleNotify = string.Format("Schedule hasn't updated yet!");
                    RefreshScheduleList = new BindableCollection<TourScheduleModel>(TourInformationHandleModel.GetTourScheduleList(TourInformation_ID));
                }
                else
                {
                    MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveScheduleCommand()
        {
            if (ScheduleNotify.Equals("Schedule has updated!"))
            {
                return true;
            }
            else
            {
                return false;
            }

            /*if (ScheduleList.Count > RefreshScheduleList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (TourScheduleModel item in ScheduleList)
            {
                foreach (TourScheduleModel item_before in RefreshScheduleList)
                {
                    if (item.SCHEDULE_DAY == item_before.SCHEDULE_DAY && item.SCHEDULE_CONTENT == item.SCHEDULE_CONTENT)
                    {
                        count_equal++;
                        break;
                    }
                }
            }
            Console.WriteLine("Count " + count_equal);
            return count_equal != RefreshScheduleList.Count;*/
        }
        #endregion Command

        #endregion Schedule

        #region Hotel

        #region Parameter
        private string _HotelPriceNotify;
        public string HotelPriceNotify { get => _HotelPriceNotify; set { _HotelPriceNotify = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxHotelModel> _HotelList;
        public BindableCollection<CheckBoxHotelModel> HotelList { get => _HotelList; set { _HotelList = value; OnPropertyChanged(); } }

        private BindableCollection<HotelModel> _HotelSelectedList;
        public BindableCollection<HotelModel> HotelSelectedList { get => _HotelSelectedList; set { _HotelSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<HotelModel> _RefreshHotelSelectedList;
        public BindableCollection<HotelModel> RefreshHotelSelectedList { get => _RefreshHotelSelectedList; set { _RefreshHotelSelectedList = value; OnPropertyChanged(); } }

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
                bool IsSelect = false;
                foreach (var itemselect in HotelSelectedList)
                {
                    if (itemselect.HOTEL_ID == item.HOTEL_ID)
                    {
                        IsSelect = true;
                        break;
                    }
                }
                hotelList.Add(new CheckBoxHotelModel(item.HOTEL_NAME, item.HOTEL_PRICE, item.HOTEL_ID, IsSelect, item));
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
                    _SaveHotelPriceCommand = new RelayCommand<object>(p => true, p =>
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
                        }
                        else
                        {
                            int index = HotelSelectedList.IndexOf(HotelSelectedList.Where(x => x.HOTEL_ID == checkBoxHotelModel.CB_ID).First());
                            HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");
                            HotelSelectedList.RemoveAt(index);
                        }

                        RefreshHotelList = HotelList;
                    });
                }
                return _HotelItemCheckCommand;
            }
        }

        private ICommand _SaveHotelCommand;
        public ICommand SaveHotelCommand
        {
            get
            {
                if (_SaveHotelCommand == null)
                {
                    _SaveHotelCommand = new RelayCommand<object>(p => IsExcuteSaveHotelCommand(), p => ExcuteSaveHotelCommand());
                }
                return _SaveHotelCommand;
            }
        }

        private void ExcuteSaveHotelCommand()
        {
            if (HotelHandleModel.DeleteHotelDetail(TourInformation_ID))
            {
                if (HotelHandleModel.InsertHotelDetail(HotelSelectedList, TourInformation_ID, User_ID, false))
                {
                    if (TourInformationHandleModel.UpdateTourPrice(GetPriceModel(), TourInformation_ID, User_ID))
                    {
                        MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshHotelSelectedList = HotelHandleModel.GetHotelFromTourInformation(TourInformation_ID);
                        HotelPriceNotify = string.Format("Hotel Price haven't updated yet!");
                    }
                    else
                    {
                        MessageBox.Show("Update Price failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Update Hotel Detail failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Delete failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveHotelCommand()
        {
            return !HotelPriceNotify.Equals("Hotel Price haven't updated yet!");
            /*if (RefreshHotelSelectedList.Count != HotelSelectedList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (HotelModel item in HotelSelectedList)
            {
                foreach (HotelModel item_before in RefreshHotelSelectedList)
                {
                    if (item.HOTEL_ID == item_before.HOTEL_ID)
                    {
                        count_equal++;
                        break;
                    }
                }
            }

            return count_equal != RefreshHotelSelectedList.Count;*/
        }
        #endregion Command

        #endregion Hotel

        #region Transport

        #region Parameter

        private string _TransportPriceNotify;
        public string TransportPriceNotify { get => _TransportPriceNotify; set { _TransportPriceNotify = value; OnPropertyChanged(); } }

        private BindableCollection<CheckBoxTransportModel> _TransportList;
        public BindableCollection<CheckBoxTransportModel> TransportList { get => _TransportList; set { _TransportList = value; OnPropertyChanged(); } }

        private BindableCollection<TransportModel> _TransportSelectedList;
        public BindableCollection<TransportModel> TransportSelectedList { get => _TransportSelectedList; set { _TransportSelectedList = value; OnPropertyChanged(); } }

        private BindableCollection<TransportModel> _RefreshTransportSelectedList;
        public BindableCollection<TransportModel> RefreshTransportSelectedList { get => _RefreshTransportSelectedList; set { _RefreshTransportSelectedList = value; OnPropertyChanged(); } }

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
                bool IsSelect = false;
                foreach (var itemselect in TransportSelectedList)
                {
                    if (itemselect.TRANSPORT_ID == item.TRANSPORT_ID)
                    {
                        IsSelect = true;
                        break;
                    }
                }
                transportList.Add(new CheckBoxTransportModel(item.TRANSPORT_NAME, item.TRANSPORT_PRICE, item.TRANSPORT_ID, IsSelect, item));
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
        private ICommand _SaveTransportPriceCommand;
        public ICommand SaveTransportPriceCommand
        {
            get
            {
                if (_SaveTransportPriceCommand == null)
                {
                    _SaveTransportPriceCommand = new RelayCommand<object>(p => true, p =>
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
                            TransportModel transport = checkBoxModel.TransportSelect;
                            transport.TRANSPORT_IS_AMOUNT = transport.TRANSPORT_AMOUNT_MAX == 0 ? false : true;
                            transport.CB_TransportAmount = LoadTransportComboBox(transport.TRANSPORT_AMOUNT_MAX);
                            TransportSelectedList.Add(transport);
                            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");
                        }
                        else
                        {
                            int index = TransportSelectedList.IndexOf(TransportSelectedList.Where(x => x.TRANSPORT_ID == checkBoxModel.CB_ID).First());
                            TransportSelectedList.RemoveAt(index);
                            TransportPriceNotify = string.Format("Transport Price haven't updated yet!");
                        }

                        RefreshTransportList = TransportList;
                    });
                }
                return _TransportItemCheckCommand;
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

        private ICommand _SaveTransportCommand;
        public ICommand SaveTransportCommand
        {
            get
            {
                if (_SaveTransportCommand == null)
                {
                    _SaveTransportCommand = new RelayCommand<object>(p => IsExcuteSaveTransportCommand(), p => ExcuteSaveTransportCommand());
                }
                return _SaveTransportCommand;
            }
        }

        private void ExcuteSaveTransportCommand()
        {
            if (TransportHandleModel.DeleteTransportDetail(TourInformation_ID))
            {
                if (TransportHandleModel.InsertTransportDetail(TransportSelectedList, TourInformation_ID, User_ID, false))
                {
                    if (TourInformationHandleModel.UpdateTourPrice(GetPriceModel(), TourInformation_ID, User_ID))
                    {
                        MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshTransportSelectedList = TransportHandleModel.GetTransportFromTourInformation(TourInformation_ID);
                        TransportPriceNotify = string.Format("Transport Price haven't updated yet!");
                    }
                    else
                    {
                        MessageBox.Show("Update Price failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Update Transport Detail failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Delete failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveTransportCommand()
        {
            return !TransportPriceNotify.Equals("Transport Price haven't updated yet!");
            /*if (RefreshTransportSelectedList.Count != TransportSelectedList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (TransportModel item in TransportSelectedList)
            {
                foreach (TransportModel item_before in RefreshTransportSelectedList)
                {
                    if (item.TRANSPORT_ID == item_before.TRANSPORT_ID)
                    {
                        count_equal++;
                        break;
                    }
                }
            }

            return count_equal != RefreshTransportSelectedList.Count;*/
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

        private BindableCollection<MissionModel> _RefreshMissionList;
        public BindableCollection<MissionModel> RefreshMissionList
        {
            get => _RefreshMissionList; set
            {
                _RefreshMissionList = value;
                OnPropertyChanged("RefreshMissionList");
            }
        }

        private int _MissionCount;
        public int MissionCount { get => _MissionCount; set { _MissionCount = value; OnPropertyChanged(); } }

        public int MissionBeforeSave { get; set; }

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
                    _AddMissionCommand = new RelayCommand<object>(p => IsEnable, p => AddMission());
                }
                return _AddMissionCommand;
            }
        }
        private void AddMission()
        {
            MissionCount++;
            MissionList.Add(new MissionModel()
            {
                Mission_ID = 0,
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
                    _SaveMissionPriceCommand = new RelayCommand<object>(p => MissionCount > 0 && IsEnable, p =>
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
                    _RemoveMissionCommand = new RelayCommand<object>(p => MissionCount > MissionBeforeSave && IsEnable, p => RemoveMission());
                }
                return _RemoveMissionCommand;
            }
        }

        private void RemoveMission()
        {
            MissionList.RemoveAt(MissionCount - 1);
            MissionCount--;
        }


        private ICommand _SaveMissionCommand;
        public ICommand SaveMissionCommand
        {
            get
            {
                if (_SaveMissionCommand == null)
                {
                    _SaveMissionCommand = new RelayCommand<object>(p => IsExcuteSaveMissionCommand(), p => ExcuteSaveMissionCommand());
                }

                return _SaveMissionCommand;
            }
        }

        private void ExcuteSaveMissionCommand()
        {
            if (MissionHandleModel.DeleteMissionDetail(TourInformation_ID))
            {
                if (MissionHandleModel.UpdateMissionList(MissionList, TourInformation_ID, User_ID))
                {
                    if (TourInformationHandleModel.UpdateTourPrice(GetPriceModel(), TourInformation_ID, User_ID))
                    {
                        MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        MissionPriceNotify = string.Format("Mission Price haven't updated yet!");
                        RefreshMissionList = MissionHandleModel.GetMissionFromTourInformation(TourInformation_ID);
                        MissionBeforeSave = MissionList.Count;
                    }
                    else
                    {
                        MessageBox.Show("Update Price failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Update Hotel Detail failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Delete failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveMissionCommand()
        {
            if (MissionPriceNotify.Equals("Mission Price haven't updated yet!"))
            {
                return false;
            }
            else
            {
                return true;
            }

            /*if (MissionList.Count > RefreshMissionList.Count)
            {
                return true;
            }

            int count_equal = 0;
            foreach (MissionModel item in MissionList)
            {
                foreach (MissionModel item_before in RefreshMissionList)
                {
                    if (item == item_before)
                    {
                        count_equal++;
                        break;
                    }
                }
            }

            return count_equal != RefreshMissionList.Count;*/
        }
        #endregion Command

        #endregion Mission

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
