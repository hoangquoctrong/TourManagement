using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class StatisticStaffViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding

        #endregion Data Binding

        public StatisticStaffViewModel(int user_id)
        {
            User_ID = user_id;
        }
    }
}
