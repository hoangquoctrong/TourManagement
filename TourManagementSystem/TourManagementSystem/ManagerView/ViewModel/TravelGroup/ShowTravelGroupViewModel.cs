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
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowTravelGroupViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        private TravelGroupModel _TravelGroupSelect;
        public TravelGroupModel TravelGroupSelect { get => _TravelGroupSelect; set { _TravelGroupSelect = value; OnPropertyChanged(); } }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }
        public ShowTravelGroupViewModel(int user_id, TravelGroupModel travelgroup)
        {
            User_ID = user_id;
            TravelGroupSelect = travelgroup;
            ProgressBarVisbility = Visibility.Visible;
            SetTravelGroupInformation(travelgroup);
            GetTravellerList();
            TravellerList_Notify = "";
        }

        #region Data Binding

        #region Information

        private int _TravelGroup_ID;
        public int TravelGroup_ID { get => _TravelGroup_ID; set { _TravelGroup_ID = value; OnPropertyChanged(); } }

        private bool _TravelGroup_IsDelete;
        public bool TravelGroup_IsDelete { get => _TravelGroup_IsDelete; set { _TravelGroup_IsDelete = value; OnPropertyChanged(); } }

        private string _TravelGroup_Name;
        public string TravelGroup_Name { get => _TravelGroup_Name; set { _TravelGroup_Name = value; OnPropertyChanged(); } }

        private string _TravelGroup_Type;
        public string TravelGroup_Type { get => _TravelGroup_Type; set { _TravelGroup_Type = value; OnPropertyChanged(); } }

        private DateTime _Tour_Department;
        public DateTime Tour_Department { get => _Tour_Department; set { _Tour_Department = value; OnPropertyChanged(); } }

        private DateTime _Tour_End;
        public DateTime Tour_End { get => _Tour_End; set { _Tour_End = value; OnPropertyChanged(); } }

        private TourModel _TournData;
        public TourModel TourData { get => _TournData; set { _TournData = value; OnPropertyChanged(); } }

        private TourInformationModel _TourInformationData;
        public TourInformationModel TourInformationData
        {
            get => _TourInformationData; set
            {
                _TourInformationData = value;
                OnPropertyChanged();
            }
        }
        #endregion Travel Information

        #region Traveller
        private int _TravellerCount;
        public int TravellerCount { get => _TravellerCount; set { _TravellerCount = value; OnPropertyChanged(); } }

        private string _TravellerList_Notify;
        public string TravellerList_Notify { get => _TravellerList_Notify; set { _TravellerList_Notify = value; OnPropertyChanged(); } }

        private BindableCollection<TravellerModel> _TravellerList;
        public BindableCollection<TravellerModel> TravellerList { get => _TravellerList; set { _TravellerList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxTravellerModel> _CB_TravellerList;
        public BindableCollection<ComboBoxTravellerModel> CB_TravellerList { get => _CB_TravellerList; set { _CB_TravellerList = value; OnPropertyChanged(); } }
        #endregion Traveller

        #region Staff
        private int _StaffCount;
        public int StaffCount { get => _StaffCount; set { _StaffCount = value; OnPropertyChanged(); } }

        private BindableCollection<StaffDetailModel> _StaffList;
        public BindableCollection<StaffDetailModel> StaffList { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxStaffModel> _CB_StaffList;
        public BindableCollection<ComboBoxStaffModel> CB_StaffList { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged(); } }
        #endregion Staff

        #region Price
        private double _TotalHotelPrice;
        public double TotalHotelPrice { get => _TotalHotelPrice; set { _TotalHotelPrice = value; OnPropertyChanged(); } }

        private double _TotalTransportPrice;
        public double TotalTransportPrice { get => _TotalTransportPrice; set { _TotalTransportPrice = value; OnPropertyChanged(); } }

        private double _TotalServicePrice;
        public double TotalServicePrice { get => _TotalServicePrice; set { _TotalServicePrice = value; OnPropertyChanged(); } }

        private double _TotalAnotherPrice;
        public double TotalAnotherPrice { get => _TotalAnotherPrice; set { _TotalAnotherPrice = value; OnPropertyChanged(); SetPriceBaseOnCustomer(TravellerCount); } }

        private double _TotalPrice;
        public double TotalPrice { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private string _TravelCost_Description;
        public string TravelCost_Description { get => _TravelCost_Description; set { _TravelCost_Description = value; OnPropertyChanged(); } }
        #endregion Price

        #endregion Data Binding

        #region Information
        private bool IsEnableFunction(DateTime date, bool isDelete)
        {
            if (DateTime.Now < date && !isDelete)
            {
                return true;
            }
            return false;
        }
        private async void SetTravelGroupInformation(TravelGroupModel travelGroup)
        {
            await Task.Delay(5000);
            TravelGroup_ID = travelGroup.TravelGroup_ID;
            TravelGroup_Name = travelGroup.TravelGroup_Name;
            TravelGroup_Type = travelGroup.TravelGroup_Type;
            TravelGroup_IsDelete = travelGroup.IsDelete;
            TourData = TourInformationHandleModel.GetTourTravelGroup(travelGroup.TourInformation_ID);
            TourInformationData = TourInformationHandleModel.GetTourInformationTravelGroup(travelGroup.TourInformation_ID);
            Tour_Department = TourInformationData.INFORMATION_TIME.TIME_DEPARTMENT_TIME;
            Tour_End = TourInformationData.INFORMATION_TIME.TIME_END_TIME;


            IsEnable = IsEnableFunction(Tour_Department, TravelGroup_IsDelete);
            IsEnablePrice = IsEnablePriceFunction(Tour_End);
            SetComboBoxStaffList();
            SetStaffList(travelGroup.TravelGroup_ID);
            SetTravellerList(travelGroup.TravelGroup_ID);
            SetPrice(travelGroup.TravelGroup_ID);
            SetPriceEnable();

            ProgressBarVisbility = Visibility.Hidden;
        }

        private ICommand _SaveTravelGroupCommand;
        public ICommand SaveTravelGroupCommand
        {
            get
            {
                if (_SaveTravelGroupCommand == null)
                {
                    _SaveTravelGroupCommand = new RelayCommand<object>(p => IsExcuteSaveTravelGroup(), p => ExcuteSaveTravelGroup());
                }
                return _SaveTravelGroupCommand;
            }
        }

        private void ExcuteSaveTravelGroup()
        {
            if (TravelGroupHandleModel.UpdateTravelGroup(UpdateTravelGroup(), User_ID))
            {
                MessageBox.Show("Update successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                TravelGroupSelect.TravelGroup_Name = TravelGroup_Name;
                TravelGroupSelect.TravelGroup_Type = TravelGroup_Type;
            }
            else
            {
                MessageBox.Show("Update failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsExcuteSaveTravelGroup()
        {
            if (TravelGroup_Name != TravelGroupSelect.TravelGroup_Name)
            {
                return true;
            }

            if (TravelGroup_Type != TravelGroupSelect.TravelGroup_Type)
            {
                return true;
            }

            return false;
        }

        private TravelGroupModel UpdateTravelGroup()
        {
            return new TravelGroupModel()
            {
                TravelGroup_ID = TravelGroup_ID,
                TravelGroup_Name = TravelGroup_Name,
                TravelGroup_Type = TravelGroup_Type
            };
        }

        private ICommand _DeleteTravelGroupCommand;
        public ICommand DeleteTravelGroupCommand
        {
            get
            {
                if (_DeleteTravelGroupCommand == null)
                {
                    _DeleteTravelGroupCommand = new RelayCommand<ContentControl>(p => IsEcuteDelete(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteDelete(p);
                    });
                }
                return _DeleteTravelGroupCommand;
            }
        }

        private async void ExcuteDelete(ContentControl p)
        {
            if (MessageBox.Show("Do you want to delete this travel group?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProgressBarVisbility = Visibility.Visible;
                await Task.Delay(2000);
                if (TravelGroupHandleModel.DeleteTravelGroup(TravelGroup_ID, User_ID))
                {
                    MessageBox.Show("Delete Travel Group successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Content = new TravelGroupViewModel(User_ID);
                }
                else
                {
                    MessageBox.Show("Delete Travel Group failed! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProgressBarVisbility = Visibility.Hidden;
                }
            }
        }

        private bool IsEcuteDelete()
        {
            if(TourInformationData == null)
            {
                return false;
            }
            DateTime TravelDate = TourInformationData.INFORMATION_TIME.TIME_DEPARTMENT_TIME;
            if (DateTime.Now < TravelDate && !TravelGroup_IsDelete)
            {
                return true;
            }

            return false;
        }
        #endregion Information

        #region Staff
        public int MissionCount { get; set; }
        public bool CountChange { get; set; }

        private void SetStaffList(int travelgroup_id)
        {
            BindableCollection<MissionModel> MissionList = MissionHandleModel.GetMissionFromTourInformation(TourInformationData.INFORMATION_ID);
            MissionCount = MissionList.Count;

            StaffList = new BindableCollection<StaffDetailModel>();
            BindableCollection<StaffDetailModel> staffList = MissionHandleModel.GetStaffDetailList(travelgroup_id);
            StaffCount = staffList.Count;

            foreach (var item in staffList)
            {
                StaffDetailModel staffDetail = item;
                staffDetail.CB_StaffList = CB_StaffList;
                staffDetail.Staff_Notify = "Staff Valid";
                staffDetail.Staff_Check = false;
                StaffList.Add(staffDetail);
            }
        }

        private void SetComboBoxStaffList()
        {
            ObservableCollection<StaffModel> StaffList = StaffHandleModel.GetStaffListForTraveller();

            CB_StaffList = new BindableCollection<ComboBoxStaffModel>();
            foreach (var item in StaffList)
            {
                ComboBoxStaffModel cbStaff = new ComboBoxStaffModel(item.STAFF_NAME, item.STAFF_ID, false, item);
                CB_StaffList.Add(cbStaff);
            }
        }

        private void SearchStaffItem(int staffdetailid)
        {
            StaffDetailModel staffdetail = StaffList.Where(x => x.StaffDetailID == staffdetailid).FirstOrDefault();
            staffdetail.Staff_Check = false;
            if (string.IsNullOrEmpty(staffdetail.StaffName))
            {
                staffdetail.Staff_Notify = string.Format("Please choose Staff");
                return;
            }
            foreach (var item in StaffList)
            {
                if (staffdetail.StaffName.Equals(item.StaffName) && item.StaffDetailID != staffdetailid)
                {
                    staffdetail.Staff_Notify = string.Format("Can't have the same Staff in {0} mission", MissionCount);
                    return;
                }
            }
            staffdetail.Staff_Notify = "Staff Valid";

        }

        private ICommand _SearchStaffCommand;
        public ICommand SearchStaffCommand
        {
            get
            {
                if (_SearchStaffCommand == null)
                {
                    _SearchStaffCommand = new RelayCommand<int>(p =>
                    {
                        StaffDetailModel staffdetail = StaffList.Where(x => x.StaffDetailID == p).FirstOrDefault();
                        return staffdetail.Staff_Check;
                    }, p =>
                    {
                        SearchStaffItem(p);
                        CountChange = true;
                    });
                }
                return _SearchStaffCommand;
            }
        }

        private ICommand _SaveStaffCommand;
        public ICommand SaveStaffCommand
        {
            get
            {
                if (_SaveStaffCommand == null)
                {
                    _SaveStaffCommand = new RelayCommand<object>(p => IsExcuteSaveStaffList(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteSaveStaffList();
                    });
                }

                return _SaveStaffCommand;
            }
        }

        private async void ExcuteSaveStaffList()
        {
            await Task.Delay(5000);
            if (MissionHandleModel.DeleteMissionDetail(TravelGroup_ID))
            {
                if (MissionHandleModel.InsertStaffDetailList(StaffList, TravelGroup_ID, User_ID, false))
                {
                    MessageBox.Show("Update Staff Mission List successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    CountChange = false;
                    ProgressBarVisbility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Update Staff Mission List failed! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProgressBarVisbility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("There is something wrong when update Staff Mission List. Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private bool IsExcuteSaveStaffList()
        {
            if(StaffList == null)
            {
                return false;
            }
            int count = 0;
            foreach (var item in StaffList)
            {
                if (item.Staff_Check)
                {
                    count++;
                }
            }
            if (CountChange && count == StaffCount)
            {
                return true;
            }
            return false;

        }
        #endregion Staff

        #region Price
        public double HotelPrice { get; set; }
        public double TransportPrice { get; set; }
        public double ServicePrice { get; set; }

        private TravelCostModel _PriceItem;
        public TravelCostModel PriceItem { get => _PriceItem; set { _PriceItem = value; OnPropertyChanged(); } }

        private bool _IsEnablePrice;
        public bool IsEnablePrice { get => _IsEnablePrice; set { _IsEnablePrice = value; OnPropertyChanged(); } }

        private void SetPrice(int travelgroup_id)
        {
            PriceItem = TravelGroupHandleModel.GetTravelGroupCost(travelgroup_id);
            TotalHotelPrice = PriceItem.HotelPrice;
            TotalTransportPrice = PriceItem.TransportPrice;
            TotalServicePrice = PriceItem.ServicePrice;
            TotalAnotherPrice = PriceItem.AnotherPrice;
            TotalPrice = PriceItem.TotalPrice;
            TravelCost_Description = PriceItem.TravelCostDescription;
        }

        private bool IsEnablePriceFunction(DateTime dateend)
        {
            TimeSpan span = DateTime.Now - dateend;
            if (span.Days < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetPriceEnable()
        {
            HotelPrice = TourInformationData.INFORMATION_PRICE.PRICE_COST_HOTEL;
            TransportPrice = TourInformationData.INFORMATION_PRICE.PRICE_COST_TRANSPORT;
            ServicePrice = TourInformationData.INFORMATION_PRICE.PRICE_COST_SERVICE;
        }

        private void SetPriceBaseOnCustomer(int count)
        {
            TotalHotelPrice = HotelPrice * count;
            TotalTransportPrice = TransportPrice * count;
            TotalServicePrice = ServicePrice * count;
            TotalPrice = TotalHotelPrice + TotalTransportPrice + TotalServicePrice + TotalAnotherPrice;
        }

        private ICommand _SavePriceCommand;
        public ICommand SavePriceCommand
        {
            get
            {
                if (_SavePriceCommand == null)
                {
                    _SavePriceCommand = new RelayCommand<object>(p => IsExcuteSavePrice(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteSavePrice();
                    });
                }
                return _SavePriceCommand;
            }

        }

        private async void ExcuteSavePrice()
        {
            await Task.Delay(3000);
            if (TravelGroupHandleModel.UpdateTravelGroupCost(UpdateTraveGroupCost(), User_ID))
            {
                MessageBox.Show("Update Travel Group Price successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                PriceItem = UpdateTraveGroupCost();
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Update Travel Group Price failed! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private TravelCostModel UpdateTraveGroupCost()
        {
            return new TravelCostModel()
            {
                TravelCost_ID = PriceItem.TravelCost_ID,
                TravelGroup_ID = PriceItem.TravelGroup_ID,
                HotelPrice = TotalHotelPrice,
                AnotherPrice = TotalAnotherPrice,
                ServicePrice = TotalServicePrice,
                TransportPrice = TotalTransportPrice,
                TravelCostDescription = TravelCost_Description,
                TotalPrice = TotalPrice
            };
        }
        private bool IsExcuteSavePrice()
        {
            if (string.IsNullOrEmpty(TravelCost_Description))
            {
                return false;
            }
            if (TravelCost_Description != PriceItem.TravelCostDescription)
            {
                return true;
            }
            if (TotalPrice != PriceItem.TotalPrice)
            {
                return true;
            }
            return false;
        }
        #endregion Price

        #region Traveller
        public int IndexCount { get; set; }
        public ObservableCollection<TravellerModel> AllTravellerList { get; set; }

        private async void GetTravellerList()
        {
            await Task.Delay(2000);
            AllTravellerList = TravelGroupHandleModel.GetTravellerList();
        }
        private async void SetTravellerList(int travelgroup_id)
        {
            await Task.Delay(2000);
            BindableCollection<TravellerModel> travellerList = TravelGroupHandleModel.GetTravellerListWithID(travelgroup_id);
            TravellerList = new BindableCollection<TravellerModel>();

            TravellerCount = travellerList.Count;

            foreach (var item in travellerList)
            {
                TravellerModel traveller = new TravellerModel()
                {

                    Traveller_Enable = IsEnable,
                    Traveller_ID = item.Traveller_ID,
                    Traveller_Name = item.Traveller_Name,
                    Traveller_Address = item.Traveller_Address,
                    Traveller_Birth = item.Traveller_Birth,
                    Traveller_BirthString = item.Traveller_BirthString,
                    Traveller_CitizenIdentity = item.Traveller_CitizenIdentity,
                    Traveller_PhoneNumber = item.Traveller_PhoneNumber,
                    Traveller_Star = item.Traveller_Star,
                    Traveller_Notify = "",
                    Traveller_Index = IndexCount,
                    Traveller_Check = true,
                    Traveller_Select = 0,
                    Traveller_StarEnable = IsEnableForTravellerStar(Tour_End)
                };
                IndexCount++;
                TravellerList.Add(traveller);
            }
        }

        private bool IsEnableForTravellerStar(DateTime dateend)
        {
            if (DateTime.Now < dateend)
            {
                return false;
            }
            TimeSpan span = DateTime.Now - dateend;
            if (span.Days < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ObservableCollection<TravellerModel> SearchTravellerList(string traveller_name)
        {
            ObservableCollection<TravellerModel> searchList = new ObservableCollection<TravellerModel>();

            searchList = new ObservableCollection<TravellerModel>(AllTravellerList.Where(x => x.Traveller_Name.Contains(traveller_name) ||
                                                                                                            x.Traveller_Name.ToLower().Contains(traveller_name) ||
                                                                                                            x.Traveller_Name.ToUpper().Contains(traveller_name)));

            return searchList;
        }

        private ICommand _SearchTravellerCommand;
        public ICommand SearchTravellerCommand
        {
            get
            {
                if (_SearchTravellerCommand == null)
                {
                    _SearchTravellerCommand = new RelayCommand<int>(p =>
                    {
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
                        if (traveller == null)
                        {
                            return false;
                        }
                        return traveller.Traveller_Check;
                    }, p =>
                    {
                        ExcuteSearchTravellerCommand(p);
                    });
                }
                return _SearchTravellerCommand;
            }
        }

        private void ExcuteSearchTravellerCommand(int index)
        {
            TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == index).First();
            if (traveller == null)
            {
                return;
            }
            traveller.TravelSearchList = SearchTravellerList(traveller.Traveller_Name);
            int count = traveller.TravelSearchList.Count;
            if (count > 0)
            {
                TravellerModel selectTraveller = traveller.TravelSearchList.First();
                traveller.Traveller_Name = selectTraveller.Traveller_Name;
                traveller.Traveller_ID = selectTraveller.Traveller_ID;
                traveller.Traveller_Type = selectTraveller.Traveller_Type;
                traveller.Traveller_Address = selectTraveller.Traveller_Address;
                traveller.Traveller_PhoneNumber = selectTraveller.Traveller_PhoneNumber;
                traveller.Traveller_CitizenIdentity = selectTraveller.Traveller_CitizenIdentity;
                traveller.Traveller_Sex = selectTraveller.Traveller_Sex;
                traveller.Traveller_Birth = selectTraveller.Traveller_Birth;
                traveller.Traveller_BirthString = selectTraveller.Traveller_Birth.ToString("dd/MM/yyyy");
                traveller.Traveller_Notify = count == 1 ? string.Format("1") : string.Format("1/{0}", count);
                traveller.Traveller_Check = false;
                traveller.Traveller_Select = 0;
                traveller.Traveller_CheckCommand = count == 1 ? false : true;
            }
            else
            {
                traveller.Traveller_ID = 0;
                traveller.Traveller_Notify = string.Format("0");
                traveller.Traveller_Check = false;
                traveller.Traveller_Select = -1;
                traveller.Traveller_CheckCommand = false;
            }
        }

        private ICommand _BackTravellerCommand;
        public ICommand BackTravellerCommand
        {
            get
            {
                if (_BackTravellerCommand == null)
                {
                    _BackTravellerCommand = new RelayCommand<int>(p =>
                    {
                        if (!IsEnable)
                        {
                            return false;
                        }
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
                        if (traveller == null)
                        {
                            return false;
                        }
                        if (!traveller.Traveller_CheckCommand)
                        {
                            return false;
                        }
                        int min = traveller.Traveller_Select + 1;
                        if (traveller.TravelSearchList.Count == 1)
                        {
                            return false;
                        }
                        if (min > 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }, p =>
                    {
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
                        TravellerModel back_traveller = traveller.TravelSearchList[traveller.Traveller_Select - 1];
                        traveller.Traveller_Name = back_traveller.Traveller_Name;
                        traveller.Traveller_ID = back_traveller.Traveller_ID;
                        traveller.Traveller_Type = back_traveller.Traveller_Type;
                        traveller.Traveller_Address = back_traveller.Traveller_Address;
                        traveller.Traveller_PhoneNumber = back_traveller.Traveller_PhoneNumber;
                        traveller.Traveller_CitizenIdentity = back_traveller.Traveller_CitizenIdentity;
                        traveller.Traveller_Sex = back_traveller.Traveller_Sex;
                        traveller.Traveller_Birth = back_traveller.Traveller_Birth;
                        traveller.Traveller_BirthString = back_traveller.Traveller_Birth.ToString("dd/MM/yyyy");
                        traveller.Traveller_Notify = string.Format("{0}/{1}", traveller.Traveller_Select, traveller.TravelSearchList.Count);
                        traveller.Traveller_Select--;
                        traveller.Traveller_Check = false;
                    });
                }
                return _BackTravellerCommand;
            }
        }

        private ICommand _NextTravellerCommand;
        public ICommand NextTravellerCommand
        {
            get
            {
                if (_NextTravellerCommand == null)
                {
                    _NextTravellerCommand = new RelayCommand<int>(p =>
                    {
                        if (!IsEnable)
                        {
                            return false;
                        }
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
                        if (traveller == null)
                        {
                            return false;
                        }
                        if (!traveller.Traveller_CheckCommand)
                        {
                            return false;
                        }
                        int max = traveller.TravelSearchList.Count;
                        if (max == 1)
                        {
                            return false;
                        }
                        if (traveller.Traveller_Select + 1 < max)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }, p =>
                    {
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
                        TravellerModel next_traveller = traveller.TravelSearchList[traveller.Traveller_Select + 1];
                        traveller.Traveller_Name = next_traveller.Traveller_Name;
                        traveller.Traveller_ID = next_traveller.Traveller_ID;
                        traveller.Traveller_Type = next_traveller.Traveller_Type;
                        traveller.Traveller_Address = next_traveller.Traveller_Address;
                        traveller.Traveller_PhoneNumber = next_traveller.Traveller_PhoneNumber;
                        traveller.Traveller_CitizenIdentity = next_traveller.Traveller_CitizenIdentity;
                        traveller.Traveller_Sex = next_traveller.Traveller_Sex;
                        traveller.Traveller_Birth = next_traveller.Traveller_Birth;
                        traveller.Traveller_BirthString = next_traveller.Traveller_Birth.ToString("dd/MM/yyyy");
                        traveller.Traveller_Select++;
                        traveller.Traveller_Notify = string.Format("{0}/{1}", traveller.Traveller_Select + 1, traveller.TravelSearchList.Count);
                        traveller.Traveller_Check = false;
                    });
                }
                return _NextTravellerCommand;
            }
        }

        private ICommand _AddTravellerCommand;
        public ICommand AddTravellerCommand
        {
            get
            {
                if (_AddTravellerCommand == null)
                {
                    _AddTravellerCommand = new RelayCommand<object>(p => TravellerCount <= 20 && IsEnable, p => AddTraveller());
                }
                return _AddTravellerCommand;
            }
        }
        private void AddTraveller()
        {
            TravellerList.Add(new TravellerModel()
            {
                Traveller_ID = 0,
                Traveller_Index = IndexCount,
                Traveller_Name = "",
                Traveller_Address = "",
                Traveller_Birth = DateTime.Now,
                Traveller_CitizenIdentity = "",
                Traveller_Notify = "",
                Traveller_PhoneNumber = "",
                Traveller_Sex = "",
                Traveller_Type = "",
                Traveller_Check = false,
                Traveller_Enable = true,
                Traveller_Select = 0,
            });
            IndexCount++;
            TravellerCount++;
            SetPriceBaseOnCustomer(TravellerCount);
        }

        private ICommand _RemoveTravellerCommand;
        public ICommand RemoveTravellerCommand
        {
            get
            {
                if (_RemoveTravellerCommand == null)
                {
                    _RemoveTravellerCommand = new RelayCommand<int>(_ => TravellerCount > 0 && IsEnable, p => RemoveTraveller(p));
                }
                return _RemoveTravellerCommand;
            }
        }

        private void RemoveTraveller(int index)
        {
            TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == index).FirstOrDefault();
            TravellerList.Remove(traveller);
            TravellerCount--;
            SetPriceBaseOnCustomer(TravellerCount);
        }

        private ICommand _CheckTravellerCommand;
        public ICommand CheckTravellerCommand
        {
            get
            {
                if (_CheckTravellerCommand == null)
                {
                    _CheckTravellerCommand = new RelayCommand<object>(p => TravellerCount > 0 && IsEnable, p =>
                    {
                        int max = TravellerList.Count;
                        for (int i = 1; i < max; i++)
                        {
                            if (string.IsNullOrEmpty(TravellerList[i].Traveller_Name))
                            {
                                TravellerList_Notify = "Can't have invalid customer in customer list";
                                return;
                            }
                            for (int j = 0; j < i; j++)
                            {
                                if (TravellerList[i].Traveller_Name.Equals(TravellerList[j].Traveller_Name) &&
                                TravellerList[i].Traveller_CitizenIdentity.Equals(TravellerList[j].Traveller_CitizenIdentity))
                                {
                                    TravellerList_Notify = "Can't have the same customer in customer list";
                                    return;
                                }
                            }
                        }

                        TravellerList_Notify = "Valid Customer List";
                    });
                }
                return _CheckTravellerCommand;
            }
        }

        private ICommand _SaveTravellerCommand;
        public ICommand SaveTravellerCommand
        {
            get
            {
                if (_SaveTravellerCommand == null)
                {
                    _SaveTravellerCommand = new RelayCommand<object>(p => IsExcuteSaveTraveller(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteSaveTraveller();
                    });
                }
                return _SaveTravellerCommand;
            }
        }

        private async void ExcuteSaveTraveller()
        {
            await Task.Delay(6000);
            if (TravelGroupHandleModel.InsertOrUpdateTravellerList(TravellerList, User_ID))
            {
                if (TravelGroupHandleModel.DeleteTravellerDetail(TravelGroup_ID))
                {
                    if (TravelGroupHandleModel.InsertTravellerDetailList(TravellerList, TravelGroup_ID, User_ID, false))
                    {
                        if (IsEnable)
                        {
                            if (TravelGroupHandleModel.UpdateTravelGroupCost(UpdateTraveGroupCost(), User_ID))
                            {
                                MessageBox.Show("Update Traveller List successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                                TravellerList_Notify = "";
                                PriceItem = UpdateTraveGroupCost();
                                foreach (var item in TravellerList)
                                {
                                    item.Traveller_CheckCommand = false;
                                }
                                ProgressBarVisbility = Visibility.Hidden;
                            }
                            else
                            {
                                MessageBox.Show("Update Traveller List failed! Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                                ProgressBarVisbility = Visibility.Hidden;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Update Traveller List (Rating) successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                            TravellerList_Notify = "";
                            TourHandleModel.GetTourStar(TourData.TOUR_ID);
                            foreach (var item in TravellerList)
                            {
                                item.Traveller_CheckCommand = false;
                            }
                            ProgressBarVisbility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Update Traveller List failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        ProgressBarVisbility = Visibility.Hidden;
                    }
                }
                else
                {
                    MessageBox.Show("There is something wrong when update Traveller list. Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProgressBarVisbility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("There is something wrong when update Traveller list. Please try again!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private bool IsExcuteSaveTraveller()
        {
            if (TravellerCount == 0)
            {
                return false;
            }

            if ((TravellerList_Notify.Contains("Can't") || TravellerList_Notify.Equals("")) && IsEnable)
            {
                return false;
            }

            int count = 0;
            int total = 0;
            int countstar = 0;
            foreach (var item in TravellerList)
            {
                if (!string.IsNullOrEmpty(item.Traveller_Name) && !string.IsNullOrEmpty(item.Traveller_Type) &&
                    !string.IsNullOrEmpty(item.Traveller_Sex) && !string.IsNullOrEmpty(item.Traveller_CitizenIdentity) &&
                    !string.IsNullOrEmpty(item.Traveller_PhoneNumber) && IsEnable)
                {
                    count++;
                }
                if (item.Traveller_CheckCommand)
                {
                    countstar++;
                }
                if (!item.Traveller_Check)
                {
                    total++;
                }
            }

            if (count == TravellerCount)
            {
                return true;
            }

            if (countstar > 0 && total == TravellerCount)
            {
                return true;
            }
            return false;
        }
        #endregion Traveller

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new TravelGroupViewModel(User_ID));
                }

                return _CancelCommand;
            }
        }
    }
}
