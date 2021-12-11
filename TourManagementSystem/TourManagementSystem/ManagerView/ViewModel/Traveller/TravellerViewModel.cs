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
    public class TravellerViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private Visibility _IsVisibility;
        public Visibility IsVisibility { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }
        public TravellerViewModel(int user_id, Visibility visibility)
        {
            User_ID = user_id;
            IsVisibility = visibility;
            ProgressBarVisbility = Visibility.Visible;
            LoadTravellerComboBox();
            LoadDataToUC();
        }

        private async void LoadDataToUC()
        {
            await Task.Delay(3000);
            TravellerItems = GetTravellerList();
            Refresh_TravellerItems = GetTravellerList();
            ProgressBarVisbility = Visibility.Hidden;
        }

        private ObservableCollection<TravellerModel> _TravellerItems = new ObservableCollection<TravellerModel>();
        public ObservableCollection<TravellerModel> TravellerItems { get => _TravellerItems; set { _TravellerItems = value; OnPropertyChanged("TravellerItems"); } }

        private ObservableCollection<TravellerModel> _Refresh_TravellerItems = new ObservableCollection<TravellerModel>();
        public ObservableCollection<TravellerModel> Refresh_TravellerItems { get => _Refresh_TravellerItems; set { _Refresh_TravellerItems = value; OnPropertyChanged("Refresh_TravellerItems"); } }

        private TravellerModel _TravellerSelected;
        public TravellerModel TravellerSelected { get => _TravellerSelected; set { _TravellerSelected = value; OnPropertyChanged("TravellerSelected"); } }

        private ObservableCollection<ComboBoxModel> _CB_TravellerList;
        public ObservableCollection<ComboBoxModel> CB_TravellerList { get => _CB_TravellerList; set { _CB_TravellerList = value; OnPropertyChanged("CB_TravellerList"); } }

        private ComboBoxModel _CB_TravellerSelected;
        public ComboBoxModel CB_TravellerSelected { get => _CB_TravellerSelected; set { _CB_TravellerSelected = value; OnPropertyChanged("CB_TravellerSelected"); } }

        private ObservableCollection<TravellerModel> GetTravellerList()
        {
            ObservableCollection<TravellerModel> travellerList = TravelGroupHandleModel.GetTravellerList();

            foreach (var item in travellerList)
            {
                item.Traveller_Number_Tour = TravelGroupHandleModel.GetTravellerNumberTour(item.Traveller_ID);
            }

            return travellerList;
        }
        private void LoadTravellerComboBox()
        {
            CB_TravellerList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Type", false),
                new ComboBoxModel("Gender", false)
            };
            CB_TravellerSelected = CB_TravellerList.FirstOrDefault(x => x.IsSelected);
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
                TravellerItems_Filter();
            }
        }

        private void TravellerItems_Filter()
        {
            TravellerItems = Refresh_TravellerItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_TravellerSelected.CB_Name)
                {
                    case "Name":
                        TravellerItems = new ObservableCollection<TravellerModel>(TravellerItems.Where(x => x.Traveller_Name.Contains(FilterText) ||
                                                                                                            x.Traveller_Name.ToLower().Contains(FilterText) ||
                                                                                                            x.Traveller_Name.ToUpper().Contains(FilterText)));
                        break;
                    case "Type":
                        TravellerItems = new ObservableCollection<TravellerModel>(TravellerItems.Where(x => x.Traveller_Type.Contains(FilterText) ||
                                                                                                            x.Traveller_Type.ToLower().Contains(FilterText) ||
                                                                                                            x.Traveller_Type.ToUpper().Contains(FilterText)));
                        break;
                    case "Gender":
                        TravellerItems = new ObservableCollection<TravellerModel>(TravellerItems.Where(x => x.Traveller_Sex.Contains(FilterText) ||
                                                                                                            x.Traveller_Sex.ToLower().Contains(FilterText) ||
                                                                                                            x.Traveller_Sex.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailTravellerCommand;
        public ICommand ShowDetailTravellerCommand
        {
            get
            {
                if (_ShowDetailTravellerCommand == null)
                {
                    _ShowDetailTravellerCommand = new RelayCommand<ContentControl>(_ => TravellerSelected != null, p => p.Content = new ShowTravellerViewModel(User_ID, TravellerSelected, IsVisibility));
                }
                return _ShowDetailTravellerCommand;
            }
        }

        private ICommand _AddTravellerCommand;
        public ICommand AddTravellerCommand
        {
            get
            {
                if (_AddTravellerCommand == null)
                {
                    _AddTravellerCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddTravellerViewModel(User_ID));
                }
                return _AddTravellerCommand;
            }
        }
    }
}
