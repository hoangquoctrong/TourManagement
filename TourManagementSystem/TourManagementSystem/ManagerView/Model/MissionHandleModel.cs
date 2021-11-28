using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourManagementSystem.ManagerView.Model
{
    public static class MissionHandleModel
    {
        public static double CalculateTotalMissionPrice(ObservableCollection<MissionModel> missions)
        {
            double total = 0;

            if (missions.Count == 0)
            {
                return total;
            }

            foreach (var item in missions)
            {
                total += item.Mission_Price * item.Mission_Count;
            }

            return total;
        }
    }
}
