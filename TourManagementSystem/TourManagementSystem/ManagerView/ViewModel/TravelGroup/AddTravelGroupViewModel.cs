using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AddTravelGroupViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        #region Data Binding

        #region Information

        private string _TravelGroup_Name;
        public string TravelGroup_Name
        { get => _TravelGroup_Name; set { _TravelGroup_Name = value; OnPropertyChanged(); } }

        private string _TravelGroup_Type;
        public string TravelGroup_Type
        { get => _TravelGroup_Type; set { _TravelGroup_Type = value; OnPropertyChanged(); } }

        private ObservableCollection<ComboBoxTourModel> _CB_TourList;
        public ObservableCollection<ComboBoxTourModel> CB_TourList
        { get => _CB_TourList; set { _CB_TourList = value; OnPropertyChanged(); } }

        private ComboBoxTourModel _CB_TourSelected;

        public ComboBoxTourModel CB_TourSelected
        {
            get => _CB_TourSelected; set
            {
                _CB_TourSelected = value;
                OnPropertyChanged();
                ProgressBarVisbility = Visibility.Visible;
                SetTourInformationComboBox();
            }
        }

        private ObservableCollection<ComboBoxInformationModel> _CB_TourInformationList;
        public ObservableCollection<ComboBoxInformationModel> CB_TourInformationList
        { get => _CB_TourInformationList; set { _CB_TourInformationList = value; OnPropertyChanged(); } }

        private ComboBoxInformationModel _CB_TourInformationSelected;

        public ComboBoxInformationModel CB_TourInformationSelected
        {
            get => _CB_TourInformationSelected; set
            {
                _CB_TourInformationSelected = value;
                OnPropertyChanged();
                ProgressBarVisbility = Visibility.Visible;
                HandleWhenChooseTourInformation();
            }
        }

        #endregion Information

        #region Traveller

        private int _TravellerCount;
        public int TravellerCount
        { get => _TravellerCount; set { _TravellerCount = value; OnPropertyChanged(); } }

        private string _TravellerList_Notify;
        public string TravellerList_Notify
        { get => _TravellerList_Notify; set { _TravellerList_Notify = value; OnPropertyChanged(); } }

        private BindableCollection<TravellerModel> _TravellerList;
        public BindableCollection<TravellerModel> TravellerList
        { get => _TravellerList; set { _TravellerList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxTravellerModel> _CB_TravellerList;
        public BindableCollection<ComboBoxTravellerModel> CB_TravellerList
        { get => _CB_TravellerList; set { _CB_TravellerList = value; OnPropertyChanged(); } }

        #endregion Traveller

        #region Staff

        private int _StaffCount;
        public int StaffCount
        { get => _StaffCount; set { _StaffCount = value; OnPropertyChanged(); } }

        private BindableCollection<StaffDetailModel> _StaffList;
        public BindableCollection<StaffDetailModel> StaffList
        { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }

        private BindableCollection<ComboBoxStaffModel> _CB_StaffList;
        public BindableCollection<ComboBoxStaffModel> CB_StaffList
        { get => _CB_StaffList; set { _CB_StaffList = value; OnPropertyChanged(); } }

        #endregion Staff

        #region Price

        private double _TotalHotelPrice;
        public double TotalHotelPrice
        { get => _TotalHotelPrice; set { _TotalHotelPrice = value; OnPropertyChanged(); } }

        private double _TotalTransportPrice;
        public double TotalTransportPrice
        { get => _TotalTransportPrice; set { _TotalTransportPrice = value; OnPropertyChanged(); } }

        private double _TotalServicePrice;
        public double TotalServicePrice
        { get => _TotalServicePrice; set { _TotalServicePrice = value; OnPropertyChanged(); } }

        private double _TotalAnotherPrice;
        public double TotalAnotherPrice
        { get => _TotalAnotherPrice; set { _TotalAnotherPrice = value; OnPropertyChanged(); SetPriceBaseOnCustomer(TravellerCount); } }

        private double _TotalPrice;
        public double TotalPrice
        { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private bool _IsEnable;
        public bool IsEnable
        { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        private string _TravelCost_Description;
        public string TravelCost_Description
        { get => _TravelCost_Description; set { _TravelCost_Description = value; OnPropertyChanged(); } }

        #endregion Price

        #endregion Data Binding

        public AddTravelGroupViewModel(int user_id)
        {
            User_ID = user_id;
            ProgressBarVisbility = Visibility.Hidden;

            //Information
            SetTourComboBox();
            SetTourInformationComboBox();
            SetComboBoxStaffList();

            TravellerList = new BindableCollection<TravellerModel>();
            TravellerCount = 0;
            GetTravellerList();
            TravellerList_Notify = "";

            StaffList = new BindableCollection<StaffDetailModel>();
            StaffCount = 0;
        }

        #region Command Information

        private async void SetTourComboBox()
        {
            await Task.Delay(1000);
            CB_TourList = new ObservableCollection<ComboBoxTourModel>();

            ObservableCollection<TourModel> TourList = TourInformationHandleModel.GetTourListHaveInformation();

            if (TourList.Count == 0)
            {
                return;
            }

            foreach (var item in TourList)
            {
                ComboBoxTourModel cbTour = new ComboBoxTourModel(item.TOUR_NAME, item.TOUR_ID, false, item);
                CB_TourList.Add(cbTour);
            }
        }

        private async void SetTourInformationComboBox()
        {
            await Task.Delay(3000);
            CB_TourInformationList = new ObservableCollection<ComboBoxInformationModel>();

            if (CB_TourSelected == null)
            {
                return;
            }

            ObservableCollection<TourInformationModel> TourInformationList = TourInformationHandleModel.GetTourInformationListForTravelGroup(CB_TourSelected.CB_ID);

            if (TourInformationList.Count == 0)
            {
                return;
            }

            foreach (var item in TourInformationList)
            {
                ComboBoxInformationModel cbInformation = new ComboBoxInformationModel(item.INFORMATION_TIME.TIME_DEPARTMENT_STRING, item.INFORMATION_ID, false, item);
                CB_TourInformationList.Add(cbInformation);
            }
            ProgressBarVisbility = Visibility.Hidden;
        }

        private async void HandleWhenChooseTourInformation()
        {
            await Task.Delay(3000);
            CreateStaffListBaseOnMission();
            SetPriceEnable();
            ProgressBarVisbility = Visibility.Hidden;
        }

        #endregion Command Information

        #region Command Price

        public double HotelPrice { get; set; }
        public double TransportPrice { get; set; }
        public double ServicePrice { get; set; }

        private void SetPriceEnable()
        {
            if (CB_TourInformationSelected == null)
            {
                IsEnable = false;
                HotelPrice = 0;
                TransportPrice = 0;
                ServicePrice = 0;
            }
            else
            {
                IsEnable = true;
                TourInformationModel information = CB_TourInformationSelected.InformationItem;
                HotelPrice = information.INFORMATION_PRICE.PRICE_COST_HOTEL;
                TransportPrice = information.INFORMATION_PRICE.PRICE_COST_TRANSPORT;
                ServicePrice = information.INFORMATION_PRICE.PRICE_COST_SERVICE;
            }
        }

        private void SetPriceBaseOnCustomer(int count)
        {
            TotalHotelPrice = HotelPrice * count;
            TotalTransportPrice = TransportPrice * count;
            TotalServicePrice = ServicePrice * count;
            TotalPrice = TotalHotelPrice + TotalTransportPrice + TotalServicePrice + TotalAnotherPrice;
        }

        #endregion Command Price

        #region Command Staff

        public int MissionCount { get; set; }

        private void CreateStaffListBaseOnMission()
        {
            StaffList = new BindableCollection<StaffDetailModel>();
            StaffCount = 0;
            if (CB_TourInformationSelected == null)
            {
                return;
            }
            BindableCollection<MissionModel> MissionList = MissionHandleModel.GetMissionFromTourInformation(CB_TourInformationSelected.CB_ID);

            if (MissionList.Count == 0)
            {
                return;
            }

            MissionCount = MissionList.Count;

            int count = 0;
            foreach (var item in MissionList)
            {
                for (int i = 0; i < item.Mission_Count; i++)
                {
                    StaffDetailModel staffdetail = new StaffDetailModel()
                    {
                        StaffDetailID = count,
                        MissionID = item.Mission_ID,
                        MissionResponsibility = item.Mission_Responsibility,
                        Staff_Notify = "",
                        CB_StaffList = CB_StaffList
                    };

                    count++;
                    StaffList.Add(staffdetail);
                }
            }

            StaffCount = StaffList.Count;
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
                    _SaveStaffCommand = new RelayCommand<object>(p => CB_TourInformationSelected != null, p =>
                    {
                    });
                }
                return _SaveStaffCommand;
            }
        }

        #endregion Command Staff

        #region Command Traveller

        public int IndexCount { get; set; }
        public ObservableCollection<TravellerModel> AllTravellerList { get; set; }

        private void GetTravellerList()
        {
            AllTravellerList = TravelGroupHandleModel.GetTravellerList();
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
                        TravellerModel traveller = TravellerList.Where(x => x.Traveller_Index == p).FirstOrDefault();
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
                    });
                }
                return _SearchTravellerCommand;
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
                    _AddTravellerCommand = new RelayCommand<object>(p => TravellerCount <= 20, p => AddTraveller());
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
                    _RemoveTravellerCommand = new RelayCommand<int>(_ => TravellerCount > 0, p => RemoveTraveller(p));
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

        private ICommand _SaveTravellerCommand;

        public ICommand SaveTravellerCommand
        {
            get
            {
                if (_SaveTravellerCommand == null)
                {
                    _SaveTravellerCommand = new RelayCommand<object>(p => TravellerCount > 0, p =>
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
                return _SaveTravellerCommand;
            }
        }

        #endregion Command Traveller

        private ICommand _AddTravelGroupCommand;

        public ICommand AddTravelGroupCommand
        {
            get
            {
                if (_AddTravelGroupCommand == null)
                {
                    _AddTravelGroupCommand = new RelayCommand<ContentControl>(p => IsExucteAddTravelGroupCommand(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteAddTravelGroupCommand(p);
                    });
                }

                return _AddTravelGroupCommand;
            }
        }

        #region IsExcute Save

        private bool IsExucteAddTravelGroupCommand()
        {
            return IsExcuteInformation() && IsExcuteStaff() && IsExcuteTraveller();
        }

        private bool IsExcuteInformation()
        {
            return (!string.IsNullOrEmpty(TravelGroup_Name)) && (!string.IsNullOrEmpty(TravelGroup_Type)) &&
                (CB_TourSelected != null) && (CB_TourInformationSelected != null);
        }

        private bool IsExcuteStaff()
        {
            if (StaffCount == 0)
            {
                return false;
            }
            int count = 0;
            foreach (var item in StaffList)
            {
                if (!item.Staff_Check && item.Staff_Notify.Equals("Staff Valid"))
                {
                    count++;
                }
            }
            return count == StaffCount;
        }

        private bool IsExcuteTraveller()
        {
            if (TravellerCount == 0)
            {
                return false;
            }

            if (TravellerList_Notify.Contains("Can't") || TravellerList_Notify.Equals(""))
            {
                return false;
            }

            int count = 0;
            foreach (var item in TravellerList)
            {
                if (!string.IsNullOrEmpty(item.Traveller_Name) && !string.IsNullOrEmpty(item.Traveller_Type) &&
                    !string.IsNullOrEmpty(item.Traveller_Sex) && !string.IsNullOrEmpty(item.Traveller_CitizenIdentity) &&
                    !string.IsNullOrEmpty(item.Traveller_PhoneNumber) && !item.Traveller_Check)
                {
                    count++;
                }
            }

            return count == TravellerCount;
        }

        #endregion IsExcute Save

        #region Excute Save

        private async void ExcuteAddTravelGroupCommand(ContentControl p)
        {
            await Task.Delay(6000);
            int countSuccess = 0;
            //Traveller
            if (TravelGroupHandleModel.InsertOrUpdateTravellerList(InsertTravellerModelDatabase(), User_ID))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("Add Or Update Traveller Failed! Please try again.");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }
            int travelgroup_id = TravelGroupHandleModel.InsertTravelGroup(InsertTravelGroupDatabase(), User_ID);

            if (travelgroup_id <= 0)
            {
                string messageDisplay = string.Format("Add Travel Group failed! Please try again.");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }
            //Staff List
            if (MissionHandleModel.InsertStaffDetailList(InsertStaffListDatabase(), travelgroup_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("There is something wrong (Staff List failed) when add Travel Group. Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Price
            if (TravelGroupHandleModel.InsertTravelGroupCost(InsertTravelCostModelDatabase(), travelgroup_id, User_ID))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("There is something wrong (Travel Group Cost failed) when add Travel Group. Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            //Traveller
            if (TravelGroupHandleModel.InsertTravellerDetailList(InsertTravellerModelDatabase(), travelgroup_id, User_ID, true))
            {
                countSuccess++;
            }
            else
            {
                string messageDisplay = string.Format("There is something wrong (Traveller List failed) when add Travel Group. Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
                return;
            }

            if (countSuccess == 4)
            {
                string messageDisplay = string.Format("Add Travel Group Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
                p.Content = new TravelGroupViewModel(User_ID, Visibility.Visible);
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                string messageDisplay = string.Format("There is something wrong (Undifinte Now) when add Travel Group. Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        private TravelGroupModel InsertTravelGroupDatabase()
        {
            return new TravelGroupModel
            {
                TourInformation_ID = CB_TourInformationSelected.CB_ID,
                TravelGroup_Name = TravelGroup_Name,
                TravelGroup_Type = TravelGroup_Type
            };
        }

        private ObservableCollection<StaffDetailModel> InsertStaffListDatabase()
        {
            ObservableCollection<StaffDetailModel> staffList = new ObservableCollection<StaffDetailModel>();

            foreach (var item in StaffList)
            {
                staffList.Add(item);
            }
            return staffList;
        }

        private TravelCostModel InsertTravelCostModelDatabase()
        {
            return new TravelCostModel()
            {
                HotelPrice = TotalHotelPrice,
                TransportPrice = TotalTransportPrice,
                ServicePrice = TotalServicePrice,
                AnotherPrice = TotalAnotherPrice,
                TravelCostDescription = string.IsNullOrEmpty(TravelCost_Description) ? "" : TravelCost_Description,
                TotalPrice = TotalPrice
            };
        }

        private ObservableCollection<TravellerModel> InsertTravellerModelDatabase()
        {
            return TravellerList;
        }

        #endregion Excute Save

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new TravelGroupViewModel(User_ID, Visibility.Visible));
                }

                return _CancelCommand;
            }
        }
    }
}