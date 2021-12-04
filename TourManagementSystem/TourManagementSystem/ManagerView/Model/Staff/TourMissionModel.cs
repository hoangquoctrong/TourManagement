using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourMissionModel : BaseViewModel
    {
        private int _TOUR_MISSION_ID;
        public int TOUR_MISSION_ID { get => _TOUR_MISSION_ID; set { _TOUR_MISSION_ID = value; OnPropertyChanged(); } }

        private int _STAFF_ID;
        public int STAFF_ID { get => _STAFF_ID; set { _STAFF_ID = value; OnPropertyChanged(); } }

        private string _STAFF_NAME;
        public string STAFF_NAME { get => _STAFF_NAME; set { _STAFF_NAME = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_RESPONSIBILITY;
        public string TOUR_MISSION_RESPONSIBILITY { get => _TOUR_MISSION_RESPONSIBILITY; set { _TOUR_MISSION_RESPONSIBILITY = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_DESCRIPTION;
        public string TOUR_MISSION_DESCRIPTION { get => _TOUR_MISSION_DESCRIPTION; set { _TOUR_MISSION_DESCRIPTION = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_TOURNAME;
        public string TOUR_MISSION_TOURNAME { get => _TOUR_MISSION_TOURNAME; set { _TOUR_MISSION_TOURNAME = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_TRAVELGROUPNAME;
        public string TOUR_MISSION_TRAVELGROUPNAME { get => _TOUR_MISSION_TRAVELGROUPNAME; set { _TOUR_MISSION_TRAVELGROUPNAME = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_STRING_STARTDATE;
        public string TOUR_MISSION_STRING_STARTDATE { get => _TOUR_MISSION_STRING_STARTDATE; set { _TOUR_MISSION_STRING_STARTDATE = value; OnPropertyChanged(); } }

        private string _TOUR_MISSION_STRING_ENDDATE;
        public string TOUR_MISSION_STRING_ENDDATE { get => _TOUR_MISSION_STRING_ENDDATE; set { _TOUR_MISSION_STRING_ENDDATE = value; OnPropertyChanged(); } }

        private DateTime _TOUR_MISSION_STARTDATE = DateTime.Now;
        public DateTime TOUR_MISSION_STARTDATE { get => _TOUR_MISSION_STARTDATE; set { _TOUR_MISSION_STARTDATE = value; OnPropertyChanged(); } }

        private DateTime _TOUR_MISSION_ENDDATE = DateTime.Now;
        public DateTime TOUR_MISSION_ENDDATE { get => _TOUR_MISSION_ENDDATE; set { _TOUR_MISSION_ENDDATE = value; OnPropertyChanged(); } }

        private double _TOUR_MISSION_PRICE;
        public double TOUR_MISSION_PRICE { get => _TOUR_MISSION_PRICE; set { _TOUR_MISSION_PRICE = value; OnPropertyChanged(); } }


        public TourMissionModel(int ID, string tourname, string travelgroupname, string responsibility, string description, string startdate, string enddate)
        {
            this.TOUR_MISSION_ID = ID;
            this.TOUR_MISSION_TOURNAME = tourname;
            this.TOUR_MISSION_TRAVELGROUPNAME = travelgroupname;
            this.TOUR_MISSION_DESCRIPTION = description;
            this.TOUR_MISSION_RESPONSIBILITY = responsibility;
            this.TOUR_MISSION_STRING_STARTDATE = startdate;
            this.TOUR_MISSION_STRING_ENDDATE = enddate;
        }
    }
}
