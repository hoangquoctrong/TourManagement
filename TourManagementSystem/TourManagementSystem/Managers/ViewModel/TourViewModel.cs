using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Managers.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Managers.ViewModel
{
    public class TourViewModel : BaseViewModel
    {
        #region Data
        #region Data Binding for TourUC
        /*
        * User is manager of system
        */
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        /*
         * TourList binding ItemSource on DataGridView of TourUC
         */
        private ObservableCollection<TourModel> _TourList;
        public ObservableCollection<TourModel> TourList { get => _TourList; set { _TourList = value; OnPropertyChanged(); } }

        /*
         * Refresh_TourList is refresh TourList
         */
        private ObservableCollection<TourModel> _Refresh_TourList;
        public ObservableCollection<TourModel> Refresh_TourList { get => _Refresh_TourList; set { _Refresh_TourList = value; OnPropertyChanged(); } }


        /*
         * Tour_Selected binding SelectedItem on DataGridView of TourUC
         */
        private TourModel _Tour_Selected;
        public TourModel Tour_Selected
        {
            get => _Tour_Selected;
            set
            {
                _Tour_Selected = value;
                OnPropertyChanged();

                /*
                 * This step is insert select staff into display TourUC
                 */
                if (Tour_Selected != null)
                {

                }
            }
        }


        /*
        * Search_Tour_Text binding to Search button of TourUC
        */
        private string _Search_Tour_Text;
        public string Search_Tour_Text
        {
            get => _Search_Tour_Text;
            set
            {
                _Search_Tour_Text = value;
                OnPropertyChanged();

                /*
                 * Step 1: Refresh TourList
                 * Step 2: Check SearchText is null or not
                 * Step 3: Check ComboBox is null or not
                 * Step 4: Choose ComboBox type to select                 
                 */
                //Step 1
                TourList = Refresh_TourList;

                //Step 2
                if (!string.IsNullOrEmpty(Search_Tour_Text))
                {
                    //Step 3
                    if (CB_TourSelected != null)
                    {
                        //Step 4
                        switch (CB_TourSelected.CB_Name)
                        {
                            case "Name":
                                TourList = new ObservableCollection<TourModel>(TourList.Where(x => x.TOUR_NAME.Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_NAME.ToLower().Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_NAME.ToUpper().Contains(Search_Tour_Text)));
                                break;
                            case "Type":
                                TourList = new ObservableCollection<TourModel>(TourList.Where(x => x.TOUR_TYPE.Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_TYPE.ToLower().Contains(Search_Tour_Text) ||
                                                                                                        x.TOUR_TYPE.ToUpper().Contains(Search_Tour_Text)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /*
         * CB_TourList binding ItemSource of ComboBox of TourUC
         */
        private ObservableCollection<ComboBoxModel> _CB_TourList;
        public ObservableCollection<ComboBoxModel> CB_TourList { get => _CB_TourList; set { _CB_TourList = value; OnPropertyChanged(); } }

        /*
         * CB_TourSelected binding SelectedValue of ComboBox of TourUC
         */
        private ComboBoxModel _CB_TourSelected;
        public ComboBoxModel CB_TourSelected { get => _CB_TourSelected; set { _CB_TourSelected = value; OnPropertyChanged(); } }

        #endregion Data Binding for TourUC
        #endregion Data
    }
}
