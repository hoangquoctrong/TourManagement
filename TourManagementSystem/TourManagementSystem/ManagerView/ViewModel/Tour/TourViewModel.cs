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
    public class TourViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private ObservableCollection<TourModel> _TourItems;
        public ObservableCollection<TourModel> TourItems { get => _TourItems; set { _TourItems = value; OnPropertyChanged("TourItems"); } }

        private ObservableCollection<TourModel> _Refresh_TourItems;
        public ObservableCollection<TourModel> Refresh_TourItems { get => _Refresh_TourItems; set { _Refresh_TourItems = value; OnPropertyChanged("Refresh_TourItems"); } }

        private TourModel _TourSelected;
        public TourModel TourSelected { get => _TourSelected; set { _TourSelected = value; OnPropertyChanged("TourSelected"); } }

        public TourViewModel(int user_id)
        {
            User_ID = user_id;
            LoadTourComboBox();
            TourItems = new ObservableCollection<TourModel>();
            Refresh_TourItems = new ObservableCollection<TourModel>();
            TourItems = TourHandleModel.GetTourList();
            Refresh_TourItems = TourHandleModel.GetTourList();
        }

        private ObservableCollection<ComboBoxModel> _CB_TourList;
        public ObservableCollection<ComboBoxModel> CB_TourList { get => _CB_TourList; set { _CB_TourList = value; OnPropertyChanged("CB_TourList"); } }

        private ComboBoxModel _CB_TourSelected;
        public ComboBoxModel CB_TourSelected { get => _CB_TourSelected; set { _CB_TourSelected = value; OnPropertyChanged("CB_TourSelected"); } }

        private void LoadTourComboBox()
        {
            CB_TourList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Type", false)
            };
            CB_TourSelected = CB_TourList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _FilterText;
        public string FilterText
        {
            get => _FilterText;
            set
            {
                _FilterText = value;
                OnPropertyChanged("FilterText");
                TourItems_Filter();
            }
        }

        private void TourItems_Filter()
        {
            TourItems = Refresh_TourItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_TourSelected.CB_Name)
                {
                    case "Name":
                        TourItems = new ObservableCollection<TourModel>(TourItems.Where(x => x.TOUR_NAME.Contains(FilterText) ||
                                                                                                            x.TOUR_NAME.ToLower().Contains(FilterText) ||
                                                                                                            x.TOUR_NAME.ToUpper().Contains(FilterText)));
                        break;
                    case "Type":
                        TourItems = new ObservableCollection<TourModel>(TourItems.Where(x => x.TOUR_TYPE.Contains(FilterText) ||
                                                                                                            x.TOUR_TYPE.ToLower().Contains(FilterText) ||
                                                                                                            x.TOUR_TYPE.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailTourCommand;
        public ICommand ShowDetailTourCommand
        {
            get
            {
                if (_ShowDetailTourCommand == null)
                {
                    _ShowDetailTourCommand = new RelayCommand<ContentControl>(_ => TourSelected != null, p => p.Content = new ShowTourViewModel(User_ID, TourSelected.TOUR_ID));
                }
                return _ShowDetailTourCommand;
            }
        }

        private ICommand _AddTourCommand;
        public ICommand AddTourCommand
        {
            get
            {
                if (_AddTourCommand == null)
                {
                    _AddTourCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddTourViewModel(User_ID));
                }
                return _AddTourCommand;
            }
        }

        private ICommand _StatisticTourCommand;
        public ICommand StatisticTourCommand
        {
            get
            {
                if (_StatisticTourCommand == null)
                {
                    _StatisticTourCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new StatisticTourViewModel(User_ID));
                }
                return _StatisticTourCommand;
            }
        }
    }
}
