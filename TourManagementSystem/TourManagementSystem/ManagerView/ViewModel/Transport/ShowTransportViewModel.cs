using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class ShowTransportViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        private TransportModel _TransportSelected;
        public TransportModel TransportSelected
        { get => _TransportSelected; set { _TransportSelected = value; OnPropertyChanged(); } }

        private Visibility _IsVisibility;
        public Visibility IsVisibility
        { get => _IsVisibility; set { _IsVisibility = value; OnPropertyChanged("IsVisibility"); } }

        #region Data Binding

        private int _Transport_ID;
        public int Transport_ID
        { get => _Transport_ID; set { _Transport_ID = value; OnPropertyChanged(); } }
        private string _Transport_Name;
        public string Transport_Name
        { get => _Transport_Name; set { _Transport_Name = value; OnPropertyChanged(); } }

        private int _Transport_Amount_Max;
        public int Transport_Amount_Max
        { get => _Transport_Amount_Max; set { _Transport_Amount_Max = value; OnPropertyChanged(); } }

        private string _Transport_Company;
        public string Transport_Company
        { get => _Transport_Company; set { _Transport_Company = value; OnPropertyChanged(); } }

        private string _Transport_Type;
        public string Transport_Type
        { get => _Transport_Type; set { _Transport_Type = value; OnPropertyChanged(); } }

        private string _Transport_Description;
        public string Transport_Description
        { get => _Transport_Description; set { _Transport_Description = value; OnPropertyChanged(); } }

        private string _Transport_String_Date;
        public string Transport_String_Date
        { get => _Transport_String_Date; set { _Transport_String_Date = value; OnPropertyChanged(); } }

        private DateTime _Transport_Date = DateTime.Now;
        public DateTime Transport_Date
        { get => _Transport_Date; set { _Transport_Date = value; OnPropertyChanged(); } }

        private bool _Transport_Is_Delete;
        public bool Transport_Is_Delete
        { get => _Transport_Is_Delete; set { _Transport_Is_Delete = value; OnPropertyChanged(); } }

        private bool _Transport_TypeTrans_Choose;

        public bool Transport_TypeTrans_Choose
        {
            get
            {
                if (!_Transport_TypeTrans_Choose)
                {
                    if (!Transport_Is_Delete)
                    {
                        _Transport_TypeTrans_Choose = false;
                    }
                }
                return _Transport_TypeTrans_Choose;
            }
            set
            {
                _Transport_TypeTrans_Choose = value;
                OnPropertyChanged();
            }
        }

        private string _Transport_TypeTrans;

        public string Transport_TypeTrans
        {
            get => _Transport_TypeTrans;
            set
            {
                _Transport_TypeTrans = value;
                OnPropertyChanged();
                if (Transport_TypeTrans.Contains("Road") && Transport_Is_Delete)
                {
                    Transport_TypeTrans_Choose = true;
                }
                else
                {
                    Transport_TypeTrans_Choose = false;
                    Transport_Amount_Max = 0;
                }
            }
        }

        private double _Transport_Price;
        public double Transport_Price
        { get => _Transport_Price; set { _Transport_Price = value; OnPropertyChanged(); } }

        #endregion Data Binding

        public ShowTransportViewModel(int user_id, TransportModel transport, Visibility visibility)
        {
            User_ID = user_id;
            IsVisibility = visibility;
            TransportSelected = transport;
            SetTransportInView(transport);
            LoadTourTransportDetailComboBox();
            ObservableCollection<TourTransportDetailModel> tourtransportdetailItems = TravelGroupHandleModel.GetTravelGroupListWithTransportID(transport.TRANSPORT_ID);
            TourTransportDetailItemsCollection = new CollectionViewSource { Source = tourtransportdetailItems };
            TourTransportDetailItemsCollection.Filter += TourTransportDetailItem_Filter;
        }

        private void SetTransportInView(TransportModel transport)
        {
            Transport_ID = transport.TRANSPORT_ID;
            Transport_Name = transport.TRANSPORT_NAME;
            Transport_Type = transport.TRANSPORT_TYPE;
            Transport_Date = transport.TRANSPORT_DATE;
            Transport_String_Date = Transport_Date.ToString("dd/MM/yyyy");
            Transport_TypeTrans = transport.TRANSPORT_TYPETRANS;
            Transport_Amount_Max = transport.TRANSPORT_AMOUNT_MAX;
            Transport_Price = transport.TRANSPORT_PRICE;
            Transport_Company = transport.TRANSPORT_COMPANY;
            Transport_Description = transport.TRANSPORT_DESCRIPTION;
            Transport_Is_Delete = !transport.TRANSPORT_IS_DELETE && IsVisibility == Visibility.Visible;
        }

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand<ContentControl>(p => true, p => p.Content = new TransportViewModel(User_ID, IsVisibility));
                }
                return _CancelCommand;
            }
        }

        private ICommand _SaveChangeCommand;

        public ICommand SaveChangeCommand
        {
            get
            {
                if (_SaveChangeCommand == null)
                {
                    _SaveChangeCommand = new RelayCommand<object>(p => IsExcuteSaveChangeCommand(), p => ExcuteSaveChangeCommand());
                }
                return _SaveChangeCommand;
            }
        }

        private void ExcuteSaveChangeCommand()
        {
            TransportSelected = InsertDataToTransportSelect();
            if (TransportHandleModel.UpdateTransport(TransportSelected, User_ID))
            {
                string messageDisplay = string.Format("Update Transport Successfully!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
            else
            {
                string messageDisplay = string.Format("Update Transport Failed! Please try again!");
                MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                messageWindow.ShowDialog();
            }
        }

        private bool IsExcuteSaveChangeCommand()
        {
            if (!Transport_Is_Delete)
            {
                return false;
            }

            Transport_String_Date = Transport_Date.ToString("dd/MM/yyyy");
            if (Transport_Name != TransportSelected.TRANSPORT_NAME)
            {
                return true;
            }
            if (Transport_Amount_Max != TransportSelected.TRANSPORT_AMOUNT_MAX)
            {
                return true;
            }
            if (Transport_TypeTrans != TransportSelected.TRANSPORT_TYPETRANS)
            {
                return true;
            }
            if (Transport_Price != TransportSelected.TRANSPORT_PRICE)
            {
                return true;
            }
            if (Transport_Company != TransportSelected.TRANSPORT_COMPANY)
            {
                return true;
            }
            if (Transport_Type != TransportSelected.TRANSPORT_TYPE)
            {
                return true;
            }
            if (Transport_String_Date != TransportSelected.TRANSPORT_STRING_DATE)
            {
                return true;
            }
            if (Transport_Description != TransportSelected.TRANSPORT_DESCRIPTION)
            {
                return true;
            }
            return false;
        }

        private TransportModel InsertDataToTransportSelect()
        {
            return new TransportModel()
            {
                TRANSPORT_ID = Transport_ID,
                TRANSPORT_NAME = Transport_Name,
                TRANSPORT_TYPE = Transport_Type,
                TRANSPORT_TYPETRANS = Transport_TypeTrans,
                TRANSPORT_DATE = Transport_Date,
                TRANSPORT_COMPANY = Transport_Company,
                TRANSPORT_AMOUNT_MAX = Transport_Amount_Max,
                TRANSPORT_PRICE = Transport_Price,
                TRANSPORT_DESCRIPTION = Transport_Description,
                TRANSPORT_STRING_DATE = Transport_Date.ToString("dd/MM/yyyy")
            };
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand<ContentControl>(p => Transport_Is_Delete, p => ExcuteDeleteCommand(p));
                }
                return _DeleteCommand;
            }
        }

        private void ExcuteDeleteCommand(ContentControl p)
        {
            bool? Result = new MessageWindow("Do you want to delete this transport?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result == true)
            {
                if (TransportHandleModel.DeleteTransport(Transport_ID, User_ID))
                {
                    string messageDisplay = string.Format("Delete Transport Successfully!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    p.Content = new TransportViewModel(User_ID, IsVisibility);
                }
                else
                {
                    string messageDisplay = string.Format("Delete Transport Failed! Please try again!");
                    MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                }
            }
        }

        private CollectionViewSource TourTransportDetailItemsCollection;

        public ICollectionView TourTransportDetailCollection => TourTransportDetailItemsCollection.View;

        private ObservableCollection<ComboBoxModel> _CB_TourTransportDetailList;
        public ObservableCollection<ComboBoxModel> CB_TourTransportDetailList
        { get => _CB_TourTransportDetailList; set { _CB_TourTransportDetailList = value; OnPropertyChanged("CB_HistoryList"); } }

        private ComboBoxModel _CB_TourTransportDetailSelected;
        public ComboBoxModel CB_TourTransportDetailSelected
        { get => _CB_TourTransportDetailSelected; set { _CB_TourTransportDetailSelected = value; OnPropertyChanged("CB_HistorySelected"); } }

        private void LoadTourTransportDetailComboBox()
        {
            CB_TourTransportDetailList = new ObservableCollection<ComboBoxModel>
            {
                new ComboBoxModel("Tour Name", true),
                new ComboBoxModel("Travel Group Name", false),
                new ComboBoxModel("Start Date", false),
                new ComboBoxModel("End Date", false)
            };
            CB_TourTransportDetailSelected = CB_TourTransportDetailList.FirstOrDefault(x => x.IsSelected);
        }

        //Text Search Filter
        private string _TourTransportDetailFilterText;

        public string TourTransportDetailFilterText
        {
            get => _TourTransportDetailFilterText;
            set
            {
                _TourTransportDetailFilterText = value;
                TourTransportDetailItemsCollection.View.Refresh();
                OnPropertyChanged("TourTransportDetailFilterText");
            }
        }

        private void TourTransportDetailItem_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(TourTransportDetailFilterText))
            {
                e.Accepted = true;
                return;
            }

            TourTransportDetailModel _items = e.Item as TourTransportDetailModel;
            switch (CB_TourTransportDetailSelected.CB_Name)
            {
                case "Tour Name":
                    if (_items.TOUR_NAME.IndexOf(TourTransportDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "Travel Group Name":
                    if (_items.TRAVEL_GROUP_NAME.IndexOf(TourTransportDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "Start Date":
                    if (_items.STRING_START_DATE.IndexOf(TourTransportDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;

                case "End Date":
                    if (_items.STRING_END_DATE.IndexOf(TourTransportDetailFilterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                    break;
            }
        }
    }
}