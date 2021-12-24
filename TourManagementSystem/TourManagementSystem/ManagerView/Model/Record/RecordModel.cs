using System;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class RecordModel : BaseViewModel
    {
        private int _Record_ID;
        public int Record_ID
        { get => _Record_ID; set { _Record_ID = value; OnPropertyChanged(); } }

        private int _Staff_ID;
        public int Staff_ID
        { get => _Staff_ID; set { _Staff_ID = value; OnPropertyChanged(); } }

        private string _Record_Content;
        public string Record_Content
        { get => _Record_Content; set { _Record_Content = value; OnPropertyChanged(); } }

        private string _Record_Date_String;
        public string Record_Date_String
        { get => _Record_Date_String; set { _Record_Date_String = value; OnPropertyChanged(); } }

        private string _Staff_Name;
        public string Staff_Name
        { get => _Staff_Name; set { _Staff_Name = value; OnPropertyChanged(); } }

        private DateTime _Record_Date;
        public DateTime Record_Date
        { get => _Record_Date; set { _Record_Date = value; OnPropertyChanged(); } }
    }
}