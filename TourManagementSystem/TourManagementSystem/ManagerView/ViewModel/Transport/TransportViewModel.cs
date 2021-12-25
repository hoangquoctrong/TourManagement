using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class TransportViewModel : BaseViewModel
    {
        private int _User_ID;

        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private Visibility _ProgressBarVisbility;

        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        private Visibility _IsVisibility;

        public Visibility IsVisibility
        { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        private ObservableCollection<TransportModel> _TransportItems;

        public ObservableCollection<TransportModel> TransportItems
        { get => _TransportItems; set { _TransportItems = value; OnPropertyChanged("TransportItems"); } }

        private ObservableCollection<TransportModel> _Refresh_TransportItems;

        public ObservableCollection<TransportModel> Refresh_TransportItems
        { get => _Refresh_TransportItems; set { _Refresh_TransportItems = value; OnPropertyChanged("Refresh_TransportItems"); } }

        private TransportModel _TransportSelected;

        public TransportModel TransportSelected
        { get => _TransportSelected; set { _TransportSelected = value; OnPropertyChanged("TransportSelected"); } }

        public TransportViewModel(int user_id, Visibility visibility)
        {
            IsVisibility = visibility;
            ProgressBarVisbility = Visibility.Visible;
            User_ID = user_id;
            LoadTransportComboBox();
            TransportItems = new ObservableCollection<TransportModel>();
            Refresh_TransportItems = new ObservableCollection<TransportModel>();
            LoadDataToUI();
        }

        private async void LoadDataToUI()
        {
            await Task.Delay(3000);
            TransportItems = TransportHandleModel.GetTransportList();
            Refresh_TransportItems = TransportHandleModel.GetTransportList();
            ProgressBarVisbility = Visibility.Hidden;
        }

        private ObservableCollection<ComboBoxModel> _CB_TransportList;

        public ObservableCollection<ComboBoxModel> CB_TransportList
        { get => _CB_TransportList; set { _CB_TransportList = value; OnPropertyChanged("CB_TransportList"); } }

        private ComboBoxModel _CB_TransportSelected;

        public ComboBoxModel CB_TransportSelected
        { get => _CB_TransportSelected; set { _CB_TransportSelected = value; OnPropertyChanged("CB_TransportSelected"); } }

        private void LoadTransportComboBox()
        {
            CB_TransportList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Type", false),
                new ComboBoxModel("Amount Max", false),
                new ComboBoxModel("Price", false),
                new ComboBoxModel("Status", false)
            };
            CB_TransportSelected = CB_TransportList.FirstOrDefault(x => x.IsSelected);
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
                TransportItems_Filter();
            }
        }

        private void TransportItems_Filter()
        {
            TransportItems = Refresh_TransportItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_TransportSelected.CB_Name)
                {
                    case "Name":
                        TransportItems = new ObservableCollection<TransportModel>(TransportItems.Where(x => x.TRANSPORT_NAME.Contains(FilterText) ||
                                                                                                            x.TRANSPORT_NAME.ToLower().Contains(FilterText) ||
                                                                                                            x.TRANSPORT_NAME.ToUpper().Contains(FilterText)));
                        break;

                    case "Type":
                        TransportItems = new ObservableCollection<TransportModel>(TransportItems.Where(x => x.TRANSPORT_TYPETRANS.Contains(FilterText) ||
                                                                                                            x.TRANSPORT_TYPETRANS.ToLower().Contains(FilterText) ||
                                                                                                            x.TRANSPORT_TYPETRANS.ToUpper().Contains(FilterText)));
                        break;

                    case "Amount Max":
                        TransportItems = new ObservableCollection<TransportModel>(TransportItems.Where(x => x.TRANSPORT_AMOUNT_MAX.ToString().Contains(FilterText)));
                        break;

                    case "Price":
                        TransportItems = new ObservableCollection<TransportModel>(TransportItems.Where(x => x.TRANSPORT_PRICE.ToString().Contains(FilterText)));
                        break;

                    case "Status":
                        TransportItems = new ObservableCollection<TransportModel>(TransportItems.Where(x => x.TRANSPORT_TYPE.Contains(FilterText) ||
                                                                                                            x.TRANSPORT_TYPE.ToLower().Contains(FilterText) ||
                                                                                                            x.TRANSPORT_TYPE.ToUpper().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailTransportCommand;

        public ICommand ShowDetailTransportCommand
        {
            get
            {
                if (_ShowDetailTransportCommand == null)
                {
                    _ShowDetailTransportCommand = new RelayCommand<ContentControl>(_ => TransportSelected != null, p => p.Content = new ShowTransportViewModel(User_ID, TransportSelected, IsVisibility));
                }
                return _ShowDetailTransportCommand;
            }
        }

        private ICommand _AddTransportCommand;

        public ICommand AddTransportCommand
        {
            get
            {
                if (_AddTransportCommand == null)
                {
                    _AddTransportCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddTransportViewModel(User_ID));
                }
                return _AddTransportCommand;
            }
        }
    }
}