using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data binding
        private string _FilterText;
        public string FilterText
        {
            get => _FilterText; set
            {
                _FilterText = value;
                OnPropertyChanged("FilterText");
                RecordItems_Filter();
            }
        }

        private ObservableCollection<ComboBoxModel> _CB_RecordList;
        public ObservableCollection<ComboBoxModel> CB_RecordList { get => _CB_RecordList; set { _CB_RecordList = value; OnPropertyChanged(); } }

        private ComboBoxModel _CB_RecordSelected;
        public ComboBoxModel CB_RecordSelected { get => _CB_RecordSelected; set { _CB_RecordSelected = value; OnPropertyChanged(); } }

        private bool _Checkbox_DisplayAllRecord;
        public bool Checkbox_DisplayAllRecord
        {
            get => _Checkbox_DisplayAllRecord; set
            {
                _Checkbox_DisplayAllRecord = value;
                OnPropertyChanged();
                CheckBoxDisplay();
            }
        }

        private DateTime _Select_Date = DateTime.Now;
        public DateTime Select_Date
        {
            get => _Select_Date; set
            {
                _Select_Date = value;
                OnPropertyChanged();
                RecordList = RecordHandleModel.GetRecordListByDate(Select_Date);
                RefreshRecordList = RecordHandleModel.GetRecordListByDate(Select_Date);
                Record_Amount = RecordList.Count;
            }
        }

        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        private DateTime _EndDate = DateTime.Now;
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; OnPropertyChanged(); } }

        private BindableCollection<RecordModel> _RecordList;
        public BindableCollection<RecordModel> RecordList { get => _RecordList; set { _RecordList = value; OnPropertyChanged(); } }

        private BindableCollection<RecordModel> _RefreshRecordList;
        public BindableCollection<RecordModel> RefreshRecordList { get => _RefreshRecordList; set { _RefreshRecordList = value; OnPropertyChanged(); } }

        private int _Record_Amount;
        public int Record_Amount { get => _Record_Amount; set { _Record_Amount = value; OnPropertyChanged(); } }
        #endregion Data binding


        public DashboardViewModel(int user_id)
        {
            User_ID = user_id;
            Checkbox_DisplayAllRecord = true;
            IsEnable = false;
            CheckBoxDisplay();
        }

        private void LoadRecordComboBox()
        {
            if (Checkbox_DisplayAllRecord)
            {
                CB_RecordList = new ObservableCollection<ComboBoxModel>
                {
                    new ComboBoxModel("Name", true),
                    new ComboBoxModel("ID", false),
                    new ComboBoxModel("Date", false)
                };
            }
            else
            {
                CB_RecordList = new ObservableCollection<ComboBoxModel>
                {
                    new ComboBoxModel("Name", true),
                    new ComboBoxModel("ID", false)
                };
            }

            CB_RecordSelected = CB_RecordList.FirstOrDefault(x => x.IsSelected);
        }

        private void CheckBoxDisplay()
        {
            LoadRecordComboBox();
            if (Checkbox_DisplayAllRecord)
            {
                IsEnable = false;
                RecordList = RecordHandleModel.GetRecordList();
                RefreshRecordList = RecordHandleModel.GetRecordList();
                Record_Amount = RecordList.Count;
            }
            else
            {
                IsEnable = true;
                RecordList = RecordHandleModel.GetRecordListByDate(Select_Date);
                RefreshRecordList = RecordHandleModel.GetRecordListByDate(Select_Date);
                Record_Amount = RecordList.Count;
            }
        }

        private void RecordItems_Filter()
        {
            RecordList = RefreshRecordList;

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (CB_RecordSelected.CB_Name)
                {
                    case "Name":
                        RecordList = new BindableCollection<RecordModel>(RecordList.Where(x => x.Staff_Name.Contains(FilterText) ||
                                                                                                            x.Staff_Name.ToLower().Contains(FilterText) ||
                                                                                                            x.Staff_Name.ToUpper().Contains(FilterText)));
                        Record_Amount = RecordList.Count;
                        break;
                    case "Date":
                        RecordList = new BindableCollection<RecordModel>(RecordList.Where(x => x.Record_Date_String.Contains(FilterText)));
                        Record_Amount = RecordList.Count;
                        break;
                    case "ID":
                        RecordList = new BindableCollection<RecordModel>(RecordList.Where(x => x.Staff_ID.ToString().Contains(FilterText)));
                        Record_Amount = RecordList.Count;
                        break;
                }
            }
        }
    }
}
