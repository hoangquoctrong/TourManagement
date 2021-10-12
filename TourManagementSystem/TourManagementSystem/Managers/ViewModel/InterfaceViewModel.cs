using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TourManagementSystem.Model;
using TourManagementSystem.View;

namespace TourManagementSystem.ViewModel
{
    class InterfaceViewModel : BaseViewModel
    {
        public InterfaceViewModel() { LoadCommand(); }

        #region Data Binding cho Side bar
        private SideBarModel _TOURTAB;
        public SideBarModel TOURTAB { get => _TOURTAB;set { _TOURTAB = value;OnPropertyChanged(); } }
        private SideBarModel _GROUPTAB;
        public SideBarModel GROUPTAB { get => _GROUPTAB; set { _GROUPTAB = value; OnPropertyChanged(); } }
        private SideBarModel _PLACETAB;
        public SideBarModel PLACETAB { get => _PLACETAB; set { _PLACETAB = value; OnPropertyChanged(); } }
        private SideBarModel _EMPLOYEETAB;
        public SideBarModel EMPLOYEETAB { get => _EMPLOYEETAB; set { _EMPLOYEETAB = value; OnPropertyChanged(); } }
        private SideBarModel _TRANSPORTTAB;
        public SideBarModel TRANSPORTTAB { get => _TRANSPORTTAB; set { _TRANSPORTTAB = value; OnPropertyChanged(); } }
        private SideBarModel _HOTELTAB;
        public SideBarModel HOTELTAB { get => _HOTELTAB; set { _HOTELTAB = value; OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDTOUR;
        public SolidColorBrush FOREGROUNDTOUR { get => _FOREGROUNDTOUR; set { _FOREGROUNDTOUR = value;OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDGROUP;
        public SolidColorBrush FOREGROUNDGROUP { get => _FOREGROUNDGROUP; set { _FOREGROUNDGROUP = value; OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDPLACE;
        public SolidColorBrush FOREGROUNDPLACE { get => _FOREGROUNDPLACE; set { _FOREGROUNDPLACE = value; OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDEMPLOYEE;
        public SolidColorBrush FOREGROUNDEMPLOYEE { get => _FOREGROUNDEMPLOYEE; set { _FOREGROUNDEMPLOYEE = value; OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDTRANSPORT;
        public SolidColorBrush FOREGROUNDTRANSPORT { get => _FOREGROUNDTRANSPORT; set { _FOREGROUNDTRANSPORT = value; OnPropertyChanged(); } }
        private SolidColorBrush _FOREGROUNDHOTEL;
        public SolidColorBrush FOREGROUNDHOTEL { get => _FOREGROUNDHOTEL; set { _FOREGROUNDHOTEL = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDTOUR;
        public SolidColorBrush BACKGROUNDTOUR { get => _BACKGROUNDTOUR; set { _BACKGROUNDTOUR = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDGROUP;
        public SolidColorBrush BACKGROUNDGROUP { get => _BACKGROUNDGROUP; set { _BACKGROUNDGROUP = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDPLACE;
        public SolidColorBrush BACKGROUNDPLACE { get => _BACKGROUNDPLACE; set { _BACKGROUNDPLACE = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDEMPLOYEE;
        public SolidColorBrush BACKGROUNDEMPLOYEE { get => _BACKGROUNDEMPLOYEE; set { _BACKGROUNDEMPLOYEE = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDTRANSPORT;
        public SolidColorBrush BACKGROUNDTRANSPORT { get => _BACKGROUNDTRANSPORT; set { _BACKGROUNDTRANSPORT = value; OnPropertyChanged(); } }
        private SolidColorBrush _BACKGROUNDHOTEL;
        public SolidColorBrush BACKGROUNDHOTEL { get => _BACKGROUNDHOTEL; set { _BACKGROUNDHOTEL = value; OnPropertyChanged(); } }
        private ContentControl _CONTENTCONTROL;
        public ContentControl CONTENTCONTROL { get => _CONTENTCONTROL; set { _CONTENTCONTROL = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand TourCommand { get; set; }
        public ICommand GroupCommand { get; set; }
        public ICommand PlaceCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand TransportCommand { get; set; }
        public ICommand HotelCommand { get; set; }
        private void LoadCommand()
        {
            LoadNameSideBar();
            TourCommand = new RelayCommand<object>(p => { return true; }, p => TourClick());
            GroupCommand = new RelayCommand<object>(p => { return true; }, p => GroupClick());
            PlaceCommand = new RelayCommand<object>(p => { return true; }, p => PlaceClick());
            EmployeeCommand = new RelayCommand<object>(p => { return true; }, p => EmployeeClick());
            TransportCommand = new RelayCommand<object>(p => { return true; }, p => TransportClick());
            HotelCommand = new RelayCommand<object>(p => { return true; }, p => HotelClick());
        }


        private void LoadNameSideBar()
        {
            TOURTAB = new SideBarModel("Travel", "Tour");
            GROUPTAB = new SideBarModel("UserGroup", "Group");
            PLACETAB = new SideBarModel("Place", "Place");
            EMPLOYEETAB = new SideBarModel("User", "Employee");
            TRANSPORTTAB = new SideBarModel("Transportation", "Transport");
            HOTELTAB = new SideBarModel("Hotel", "Hotel");

            BACKGROUNDTOUR = new SolidColorBrush(Color.FromArgb(45, 255,255,255));
            FOREGROUNDTOUR = new SolidColorBrush(Colors.White);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);

            CONTENTCONTROL = new TourUC();
        }
        private void TourClick()
        {
            BACKGROUNDTOUR = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));
            FOREGROUNDTOUR = new SolidColorBrush(Colors.White);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);
        }
        private void GroupClick()
        {
            FOREGROUNDTOUR = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTOUR = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.White);
            BACKGROUNDGROUP = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);
        }
        private void PlaceClick()
        {
            FOREGROUNDTOUR = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTOUR = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.White);
            BACKGROUNDPLACE = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);
        }
        private void EmployeeClick()
        {
            FOREGROUNDTOUR = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTOUR = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.White);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);
        }
        private void TransportClick()
        {
            FOREGROUNDTOUR = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTOUR = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.White);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDHOTEL = new SolidColorBrush(Colors.Transparent);
        }
        private void HotelClick()
        {
            FOREGROUNDTOUR = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTOUR = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDGROUP = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDGROUP = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDPLACE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDPLACE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDEMPLOYEE = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDEMPLOYEE = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDTRANSPORT = new SolidColorBrush(Colors.WhiteSmoke);
            BACKGROUNDTRANSPORT = new SolidColorBrush(Colors.Transparent);

            FOREGROUNDHOTEL = new SolidColorBrush(Colors.White);
            BACKGROUNDHOTEL = new SolidColorBrush(Color.FromArgb(45, 255, 255, 255));
        }
        #endregion


    }
}
