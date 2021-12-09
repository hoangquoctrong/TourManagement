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
    public class StatisticStaffViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        public StatisticStaffViewModel(int user_id)
        {
            User_ID = user_id;
            LoadRefreshList();
            Checkbox_DisplayAll = true;
        }
        private async void LoadRefreshList()
        {
            await Task.Delay(1000);
            Refresh_StaffItems = GetStaffList();
        }
        private ObservableCollection<StaffStatisticModel> GetStaffList()
        {
            ObservableCollection<StaffStatisticModel> staffListBeforeSort = StaffHandleModel.GetStaffListForStatistic();
            var stafflist = from staff in staffListBeforeSort
                            orderby staff.Staff_Tour descending, staff.Staff_ID ascending
                            select staff;
            ObservableCollection<StaffStatisticModel> staffList = new ObservableCollection<StaffStatisticModel>();
            foreach (var item in stafflist)
            {
                staffList.Add(item);
            }
            return staffList;
        }

        private async void CheckBoxDisplay()
        {
            if (Checkbox_DisplayAll)
            {
                await Task.Delay(3000);
                IsEnable = false;
                StaffItems = GetStaffList();
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                await Task.Delay(2000);
                IsEnable = true;
                StaffItems = GetFillterList(StartDate, EndDate);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        #region Data Binding
        private bool _Checkbox_DisplayAll;
        public bool Checkbox_DisplayAll
        {
            get => _Checkbox_DisplayAll; set
            {
                _Checkbox_DisplayAll = value;
                OnPropertyChanged();
                ProgressBarVisbility = Visibility.Visible;
                CheckBoxDisplay();
            }
        }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        private DateTime _DateTimeNow = DateTime.Now;
        public DateTime DateTimeNow { get => _DateTimeNow; set { _DateTimeNow = value; OnPropertyChanged(); } }

        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate { get => _StartDate; set { _StartDate = value; OnPropertyChanged(); } }

        private DateTime _EndDate = DateTime.Now;
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; OnPropertyChanged(); } }

        private ObservableCollection<StaffStatisticModel> _StaffItems;
        public ObservableCollection<StaffStatisticModel> StaffItems { get => _StaffItems; set { _StaffItems = value; OnPropertyChanged(); } }

        private ObservableCollection<StaffStatisticModel> _Refresh_StaffItems;
        public ObservableCollection<StaffStatisticModel> Refresh_StaffItems { get => _Refresh_StaffItems; set { _Refresh_StaffItems = value; OnPropertyChanged(); } }
        #endregion Data Binding

        private ICommand _FilterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_FilterCommand == null)
                {
                    _FilterCommand = new RelayCommand<object>(p => IsExcuteFilter(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        ExcuteFilter();
                    });
                }
                return _FilterCommand;
            }
        }

        private async void ExcuteFilter()
        {
            await Task.Delay(1500);
            StaffItems = GetFillterList(StartDate, EndDate);
            ProgressBarVisbility = Visibility.Hidden;
        }

        private ObservableCollection<StaffStatisticModel> GetFillterList(DateTime datestart, DateTime dateend)
        {
            ObservableCollection<StaffStatisticModel> filterList = new ObservableCollection<StaffStatisticModel>();
            foreach (var item in Refresh_StaffItems)
            {
                if (item.Staff_Tour == 0)
                {
                    filterList.Add(item);
                }
                else
                {
                    ObservableCollection<TourTimeModel> TimeList = new ObservableCollection<TourTimeModel>();
                    foreach (var itemTime in item.TimeList)
                    {
                        if (itemTime.TIME_DEPARTMENT_TIME > datestart && itemTime.TIME_END_TIME < dateend)
                        {
                            TimeList.Add(itemTime);
                        }
                    }
                    StaffStatisticModel staffStatistic = new StaffStatisticModel()
                    {
                        Staff_ID = item.Staff_ID,
                        Staff_Name = item.Staff_Name,
                        TimeList = TimeList,
                        Staff_Tour = TimeList.Count
                    };

                    filterList.Add(staffStatistic);
                }
            }
            return filterList;
        }

        private bool IsExcuteFilter()
        {
            return IsEnable;
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new StaffViewModel(User_ID));
                }
                return _CancelCommand;
            }
        }
    }
}
