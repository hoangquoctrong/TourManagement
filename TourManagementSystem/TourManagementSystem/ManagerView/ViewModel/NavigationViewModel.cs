using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    class NavigationViewModel : BaseViewModel
    {
        // CollectionViewSource enables XAML code to set the commonly used CollectionView properties,
        // passing these settings to the underlying view.
        private CollectionViewSource MenuItemsCollection;

        // ICollectionView enables collections to have the functionalities of current record management,
        // custom sorting, filtering, and grouping.
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        public NavigationViewModel()
        {
            User_ID = Properties.Settings.Default.User_ID;
            MessageBox.Show(string.Format("{0}, {1}, {2}", Properties.Settings.Default.User_ID, Properties.Settings.Default.Username, Properties.Settings.Default.Password));

            // ObservableCollection represents a dynamic data collection that provides notifications when items
            // get added, removed, or when the whole list is refreshed.
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>()
            {
                new MenuItems {MenuName = "Record", MenuImage = @"Assets/home.png"},
                new MenuItems {MenuName = "Tour", MenuImage = @"Assets/tour.png"},
                new MenuItems {MenuName = "Travel Group", MenuImage = @"Assets/travelgroup.png"},
                new MenuItems {MenuName = "Customer", MenuImage = @"Assets/customer.png"},
                new MenuItems {MenuName = "Place", MenuImage = @"Assets/place.png"},
                new MenuItems {MenuName = "Staff", MenuImage = @"Assets/staff.png"},
                new MenuItems {MenuName = "Transports", MenuImage = @"Assets/transport.png"},
                new MenuItems {MenuName = "Hotel", MenuImage = @"Assets/hotel.png"},
                new MenuItems {MenuName = "Account", MenuImage = @"Assets/account.png"}
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            MenuItemsCollection.Filter += MenuItem_Filter;

            // Set Startup Page
            SelectedViewModel = new DashboardViewModel(User_ID);
        }

        //Text Search Filter
        private string _FilterText;
        public string FilterText
        {
            get => _FilterText;
            set
            {
                _FilterText = value;
                MenuItemsCollection.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private void MenuItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            MenuItems _items = e.Item as MenuItems;
            if (_items.MenuName.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        // Select ViewModel
        private object _SelectedViewModel;
        public object SelectedViewModel
        {
            get => _SelectedViewModel;
            set { _SelectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }


        // Switch Views
        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Dashboard":
                    SelectedViewModel = new DashboardViewModel(User_ID);
                    break;
                case "Tour":
                    SelectedViewModel = new TourViewModel(User_ID);
                    break;
                case "Travel Group":
                    SelectedViewModel = new TravelGroupViewModel(User_ID);
                    break;
                case "Customer":
                    SelectedViewModel = new TravellerViewModel(User_ID);
                    break;
                case "Place":
                    SelectedViewModel = new PlaceViewModel(User_ID);
                    break;
                case "Staff":
                    SelectedViewModel = new StaffViewModel(User_ID);
                    break;
                case "Transports":
                    SelectedViewModel = new TransportViewModel(User_ID);
                    break;
                case "Hotel":
                    SelectedViewModel = new HotelViewModel(User_ID);
                    break;
                case "Account":
                    SelectedViewModel = new AccountViewModel(User_ID);
                    break;
                default:
                    SelectedViewModel = new DashboardViewModel(User_ID);
                    break;
            }
        }

        // Menu Button Command
        private ICommand _Menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_Menucommand == null)
                {
                    _Menucommand = new RelayCommand<object>(p => true, p => SwitchViews(p));
                }
                return _Menucommand;
            }
        }

        // Close App Command
        private ICommand _CloseCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new RelayCommand<Window>(p => true, p => p.Close());
                }
                return _CloseCommand;
            }
        }
    }
}
