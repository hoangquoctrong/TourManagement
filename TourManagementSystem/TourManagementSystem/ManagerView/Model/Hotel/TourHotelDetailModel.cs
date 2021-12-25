using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourHotelDetailModel : BaseViewModel
    {
        private int _TOUR_HOTEL_DETAIL_ID;

        public int TOUR_HOTEL_DETAIL_ID
        { get => _TOUR_HOTEL_DETAIL_ID; set { _TOUR_HOTEL_DETAIL_ID = value; OnPropertyChanged(); } }

        private int _HOTEL_ID;

        public int HOTEL_ID
        { get => _HOTEL_ID; set { _HOTEL_ID = value; OnPropertyChanged(); } }

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

        private int _HOTEL_DAY;

        public int HOTEL_DAY
        { get => _HOTEL_DAY; set { _HOTEL_DAY = value; OnPropertyChanged(); } }

        public TourHotelDetailModel(int id, string tour_name, string group_name, int day, DateTime start_date, DateTime end_date)
        {
            TOUR_HOTEL_DETAIL_ID = id;
            TOUR_NAME = tour_name;
            TRAVEL_GROUP_NAME = group_name;
            START_DATE = start_date;
            END_DATE = end_date;
            STRING_START_DATE = start_date.ToString("dd/MM/yyyy");
            STRING_END_DATE = end_date.ToString("dd/MM/yyyy");
            HOTEL_DAY = day;
        }
    }
}