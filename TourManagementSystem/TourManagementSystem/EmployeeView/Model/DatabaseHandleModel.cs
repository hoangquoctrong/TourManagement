using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.EmployeeView.Model
{
    public static class DatabaseHandleModel
    {
        public static int IsTypeAccount(int user_id)
        {
            var user = (from u in DataProvider.Ins.DB.TOUR_ACCOUNT
                        where u.TOUR_STAFF_ID == user_id
                        select u).ToArray();
            if (user.Length > 0)
            {
                switch (user[0].TOUR_STAFF.TOUR_STAFF_ROLE)
                {
                    case "Staff":
                        return 1;
                    case "Director":
                        return -1;
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}
