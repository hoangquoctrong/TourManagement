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
    public class StatisticTourViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        public StatisticTourViewModel(int user_id)
        {
            User_ID = user_id;
            Checkbox_DisplayAll = true;
        }

        private async Task<ObservableCollection<TourStatisticModel>> GetTourList()
        {
            await Task.Delay(5000);
            ObservableCollection<TourStatisticModel> tourListBeforeSort = TourHandleModel.GetTourStatisticList();
            var tourlist = from tour in tourListBeforeSort
                           orderby tour.Tour_NumberVisitGroup descending, tour.Tour_ID ascending
                           select tour;
            ObservableCollection<TourStatisticModel> tourList = new ObservableCollection<TourStatisticModel>();
            foreach (var item in tourlist)
            {
                tourList.Add(item);
            }

            Refresh_TourItems = tourList;
            return tourList;
        }

        private async void CheckBoxDisplay()
        {
            if (Checkbox_DisplayAll)
            {
                IsEnable = false;
                ProgressBarVisbility = Visibility.Visible;
                TourItems = await GetTourList();
                ProgressBarVisbility = Visibility.Hidden;
            }
            else
            {
                IsEnable = true;
                ProgressBarVisbility = Visibility.Visible;
                TourItems = await GetFillterList(StartDate, EndDate);
                ProgressBarVisbility = Visibility.Hidden;
            }
        }

        #region Data Binding
        private bool _Checkbox_DisplayAll;
        public bool Checkbox_DisplayAll { get => _Checkbox_DisplayAll; set { _Checkbox_DisplayAll = value; OnPropertyChanged(); CheckBoxDisplay(); } }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        private DateTime _DateTimeNow = DateTime.Now;
        public DateTime DateTimeNow { get => _DateTimeNow; set { _DateTimeNow = value; OnPropertyChanged(); } }

        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate { get => _StartDate; set { _StartDate = value; OnPropertyChanged(); } }

        private DateTime _EndDate = DateTime.Now;
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; OnPropertyChanged(); } }

        private ObservableCollection<TourStatisticModel> _TourItems;
        public ObservableCollection<TourStatisticModel> TourItems { get => _TourItems; set { _TourItems = value; OnPropertyChanged(); } }

        private ObservableCollection<TourStatisticModel> _Refresh_TourItems;
        public ObservableCollection<TourStatisticModel> Refresh_TourItems { get => _Refresh_TourItems; set { _Refresh_TourItems = value; OnPropertyChanged(); } }
        #endregion Data Binding

        private ICommand _FilterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_FilterCommand == null)
                {
                    _FilterCommand = new RelayCommand<object>(p => IsExcuteFilter(), p => ExcuteFilter());
                }
                return _FilterCommand;
            }
        }

        private async void ExcuteFilter()
        {
            ProgressBarVisbility = Visibility.Visible;
            TourItems = await GetFillterList(StartDate, EndDate);
            ProgressBarVisbility = Visibility.Hidden;
        }

        private bool IsExcuteFilter()
        {
            return IsEnable;
        }

        private async Task<ObservableCollection<TourStatisticModel>> GetFillterList(DateTime datestart, DateTime dateend)
        {
            await Task.Delay(3000);
            ObservableCollection<TourStatisticModel> filterList = new ObservableCollection<TourStatisticModel>();
            foreach (var item in Refresh_TourItems)
            {
                if (item.Tour_NumberVisitGroup == 0)
                {
                    filterList.Add(item);
                }
                else
                {
                    TourStatisticModel tourStatistic = new TourStatisticModel()
                    {
                        Tour_ID = item.Tour_ID,
                        Tour_Name = item.Tour_Name
                    };
                    ObservableCollection<TourDetailStatisticModel> detailstatisticlist = new ObservableCollection<TourDetailStatisticModel>();
                    foreach (var itemDetail in item.DetailStatistic)
                    {
                        if (itemDetail.Tour_Department > datestart && itemDetail.Tour_End < dateend)
                        {
                            tourStatistic.Tour_NumberVisitGroup++;
                            tourStatistic.Tour_NumberVisitTraveller += itemDetail.Tour_NumberTraveller;
                            tourStatistic.Tour_TotalCost += itemDetail.Tour_Cost;
                            detailstatisticlist.Add(itemDetail);
                        }
                    }

                    tourStatistic.DetailStatistic = detailstatisticlist;
                    filterList.Add(tourStatistic);
                }
            }
            return filterList;
        }


        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(null, p => p.Content = new TourViewModel(User_ID));
                }
                return _CancelCommand;
            }
        }
    }
}
