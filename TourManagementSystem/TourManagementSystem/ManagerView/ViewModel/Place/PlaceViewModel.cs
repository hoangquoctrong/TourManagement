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
    public class PlaceViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged("User_ID"); } }

        private ObservableCollection<PlaceModel> _PlaceItems;
        public ObservableCollection<PlaceModel> PlaceItems { get => _PlaceItems; set { _PlaceItems = value; OnPropertyChanged("PlaceItems"); } }

        private ObservableCollection<PlaceModel> _Refresh_PlaceItems;
        public ObservableCollection<PlaceModel> Refresh_PlaceItems { get => _Refresh_PlaceItems; set { _Refresh_PlaceItems = value; OnPropertyChanged("Refresh_PlaceItems"); } }

        private PlaceModel _PlaceSelected;
        public PlaceModel PlaceSelected { get => _PlaceSelected; set { _PlaceSelected = value; OnPropertyChanged("PlaceSelected"); } }
        public PlaceViewModel(int user_id)
        {
            LoadPlaceComboBox();
            User_ID = user_id;
            PlaceItems = PlaceHandleModel.GetPlaceList();
            Refresh_PlaceItems = PlaceHandleModel.GetPlaceList();
        }

        private ObservableCollection<ComboBoxModel> _CB_PlaceList;
        public ObservableCollection<ComboBoxModel> CB_PlaceList { get => _CB_PlaceList; set { _CB_PlaceList = value; OnPropertyChanged("CB_PlaceList"); } }

        private ComboBoxModel _CB_PlaceSelected;
        public ComboBoxModel CB_PlaceSelected { get => _CB_PlaceSelected; set { _CB_PlaceSelected = value; OnPropertyChanged("CB_PlaceSelected"); } }

        private void LoadPlaceComboBox()
        {
            CB_PlaceList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Name", true),
                new ComboBoxModel("Nation", false),
                new ComboBoxModel("Number Of Location", false)
            };
            CB_PlaceSelected = CB_PlaceList.FirstOrDefault(x => x.IsSelected);
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
                PlaceItems_Filter();
            }
        }

        private void PlaceItems_Filter()
        {
            PlaceItems = Refresh_PlaceItems;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_PlaceSelected.CB_Name)
                {
                    case "Name":
                        PlaceItems = new ObservableCollection<PlaceModel>(PlaceItems.Where(x => x.PLACE_NAME.Contains(FilterText) ||
                                                                                                            x.PLACE_NAME.ToLower().Contains(FilterText) ||
                                                                                                            x.PLACE_NAME.ToUpper().Contains(FilterText)));
                        break;
                    case "Nation":
                        PlaceItems = new ObservableCollection<PlaceModel>(PlaceItems.Where(x => x.PLACE_NATION.Contains(FilterText) ||
                                                                                                            x.PLACE_NATION.ToLower().Contains(FilterText) ||
                                                                                                            x.PLACE_NATION.ToUpper().Contains(FilterText)));
                        break;
                    case "Number Of Location":
                        PlaceItems = new ObservableCollection<PlaceModel>(PlaceItems.Where(x => x.PLACE_LOCATION.ToString().Contains(FilterText)));
                        break;
                }
            }
        }

        private ICommand _ShowDetailPlaceCommand;
        public ICommand ShowDetailPlaceCommand
        {
            get
            {
                if (_ShowDetailPlaceCommand == null)
                {
                    _ShowDetailPlaceCommand = new RelayCommand<ContentControl>(_ => PlaceSelected != null, p => p.Content = new ShowPlaceViewModel(User_ID, PlaceSelected.PLACE_ID));
                }
                return _ShowDetailPlaceCommand;
            }
        }

        private ICommand _AddPlaceCommand;
        public ICommand AddPlaceCommand
        {
            get
            {
                if (_AddPlaceCommand == null)
                {
                    _AddPlaceCommand = new RelayCommand<ContentControl>(_ => true, p => p.Content = new AddPlaceViewModel(User_ID));
                }
                return _AddPlaceCommand;
            }
        }
    }
}
