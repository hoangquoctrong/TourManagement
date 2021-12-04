using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class MissionModel : BaseViewModel
    {
        private int _Mission_ID;
        public int Mission_ID { get => _Mission_ID; set { _Mission_ID = value; OnPropertyChanged(); } }

        private string _Mission_Responsibility;
        public string Mission_Responsibility { get => _Mission_Responsibility; set { _Mission_Responsibility = value; OnPropertyChanged(); } }

        private string _Mission_Description;
        public string Mission_Description { get => _Mission_Description; set { _Mission_Description = value; OnPropertyChanged(); } }

        private double _Mission_Price;
        public double Mission_Price { get => _Mission_Price; set { _Mission_Price = value; OnPropertyChanged(); } }

        private int _Mission_Count;
        public int Mission_Count { get => _Mission_Count; set { _Mission_Count = value; OnPropertyChanged(); } }
    }
}
