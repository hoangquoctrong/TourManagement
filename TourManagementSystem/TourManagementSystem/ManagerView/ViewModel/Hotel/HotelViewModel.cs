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
    public class HotelViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private ObservableCollection<HotelModel> _HotelItems;
        public ObservableCollection<HotelModel> HotelItems { get => _HotelItems; set { _HotelItems = value; OnPropertyChanged("HotelItems"); } }

        private ObservableCollection<HotelModel> _Refresh_HotelItems;
        public ObservableCollection<HotelModel> Refresh_HotelItems { get => _Refresh_HotelItems; set { _Refresh_HotelItems = value; OnPropertyChanged("Refresh_HotelItems"); } }

        private HotelModel _HotelSelected;
        public HotelModel HotelSelected { get => _HotelSelected; set { _HotelSelected = value; OnPropertyChanged("HotelSelected"); } }
        public HotelViewModel(int user_id)
        {
            LoadHotelComboBox();
            User_ID = user_id;
            HotelItems = new ObservableCollection<HotelModel>();
            Refresh_HotelItems = new ObservableCollection<HotelModel>();
            HotelItems = HotelHandleModel.GetHotelList();
            Refresh_HotelItems = HotelHandleModel.GetHotelList();
        }

        private ObservableCollection<ComboBoxModel> _CB_HotelList;
        public ObservableCollection<ComboBoxModel> CB_HotelList { get => _CB_HotelList; set { _CB_HotelList = value; OnPropertyChanged("CB_HotelList"); } }

        private ComboBoxModel _CB_HotelSelected;
        public ComboBoxModel CB_HotelSelected { get => _CB_HotelSelected; set { _CB_HotelSelected = value; OnPropertyChanged("CB_HotelSelected"); } }

        private void LoadHotelComboBox()
        {
            CB_HotelList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Email", false),
                new ComboBoxModel("Phone Number", false),
                new ComboBoxModel("Address", false)
            };
            CB_HotelSelected = CB_HotelList.FirstOrDefault(x => x.IsSelected);
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
                HotelItems_Filter();
            }
        }

        private void HotelItems_Filter()
        {
            HotelItems = Refresh_HotelItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_HotelSelected.CB_Name)
                {
                    case "Name":
                        HotelItems = new ObservableCollection<HotelModel>(HotelItems.Where(x => x.HOTEL_NAME.Contains(FilterText) ||
                                                                                                            x.HOTEL_NAME.ToLower().Contains(FilterText) ||
                                                                                                            x.HOTEL_NAME.ToUpper().Contains(FilterText)));
                        break;
                    case "Email":
                        HotelItems = new ObservableCollection<HotelModel>(HotelItems.Where(x => x.HOTEL_EMAIL.Contains(FilterText) ||
                                                                                                            x.HOTEL_EMAIL.ToLower().Contains(FilterText) ||
                                                                                                            x.HOTEL_EMAIL.ToUpper().Contains(FilterText)));
                        break;
                    case "Phone Number":
                        HotelItems = new ObservableCollection<HotelModel>(HotelItems.Where(x => x.HOTEL_PHONE_NUMBER.Contains(FilterText) ||
                                                                                                            x.HOTEL_PHONE_NUMBER.ToLower().Contains(FilterText) ||
                                                                                                            x.HOTEL_PHONE_NUMBER.ToUpper().Contains(FilterText)));
                        break;
                    case "Address":
                        HotelItems = new ObservableCollection<HotelModel>(HotelItems.Where(x => x.HOTEL_ADDRESS.Contains(FilterText) ||
                                                                                                            x.HOTEL_ADDRESS.ToLower().Contains(FilterText) ||
                                                                                                            x.HOTEL_ADDRESS.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailHotelCommand;
        public ICommand ShowDetailHotelCommand
        {
            get
            {
                if (_ShowDetailHotelCommand == null)
                {
                    _ShowDetailHotelCommand = new RelayCommand<ContentControl>(_ => HotelSelected != null, p => p.Content = new ShowHotelViewModel(User_ID, HotelSelected));
                }
                return _ShowDetailHotelCommand;
            }
        }

        private ICommand _AddHotelCommand;
        public ICommand AddHotelCommand
        {
            get
            {
                if (_AddHotelCommand == null)
                {
                    _AddHotelCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddHotelViewModel(User_ID));
                }
                return _AddHotelCommand;
            }
        }

    }
}
