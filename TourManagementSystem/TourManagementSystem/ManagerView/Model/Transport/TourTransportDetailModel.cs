using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourTransportDetailModel : BaseViewModel
    {
        private int _TOUR_TRANSPORT_DETAIL_ID;
        public int TOUR_TRANSPORT_DETAIL_ID
        { get => _TOUR_TRANSPORT_DETAIL_ID; set { _TOUR_TRANSPORT_DETAIL_ID = value; OnPropertyChanged(); } }

        private int _TRANSPORT_ID;
        public int TRANSPORT_ID
        { get => _TRANSPORT_ID; set { _TRANSPORT_ID = value; OnPropertyChanged(); } }

        private string _TOUR_NAME;
        public string TOUR_NAME
        { get => _TOUR_NAME; set { _TOUR_NAME = value; OnPropertyChanged(); } }

        private string _TRAVEL_GROUP_NAME;
        public string TRAVEL_GROUP_NAME
        { get => _TRAVEL_GROUP_NAME; set { _TRAVEL_GROUP_NAME = value; OnPropertyChanged(); } }

        private DateTime _START_DATE = DateTime.Now;
        public DateTime START_DATE
        { get => _START_DATE; set { _START_DATE = value; OnPropertyChanged(); } }

        private DateTime _END_DATE = DateTime.Now;
        public DateTime END_DATE
        { get => _END_DATE; set { _END_DATE = value; OnPropertyChanged(); } }

        private string _STRING_START_DATE;
        public string STRING_START_DATE
        { get => _STRING_START_DATE; set { _STRING_START_DATE = value; OnPropertyChanged(); } }

        private string _STRING_END_DATE;
        public string STRING_END_DATE
        { get => _STRING_END_DATE; set { _STRING_END_DATE = value; OnPropertyChanged(); } }

        private int _TRANSPORT_AMOUNT;
        public int TRANSPORT_AMOUNT
        { get => _TRANSPORT_AMOUNT; set { _TRANSPORT_AMOUNT = value; OnPropertyChanged(); } }

        public TourTransportDetailModel(int id, string tour_name, string group_name, int amount, DateTime start_date, DateTime end_date)
        {
            TOUR_TRANSPORT_DETAIL_ID = id;
            TOUR_NAME = tour_name;
            TRAVEL_GROUP_NAME = group_name;
            START_DATE = start_date;
            END_DATE = end_date;
            STRING_START_DATE = start_date.ToString("dd/MM/yyyy");
            STRING_END_DATE = end_date.ToString("dd/MM/yyyy");
            TRANSPORT_AMOUNT = amount;
        }
    }
}