using System.Linq;

namespace TourManagementSystem.Global.Model
{
    public static class LoginHandleModel
    {
        public static int IsLoginAccount(string username, string password, out int user_id)
        {
            string passwordAfter = GlobalFunction.CreateMD5(GlobalFunction.Base64Encode(password));
            var user = (from u in DataProvider.Ins.DB.TOUR_ACCOUNT
                        where u.TOUR_ACCOUNT_NAME == username && u.TOUR_ACCOUNT_PASSWORD == passwordAfter
                        select u).ToArray();
            if (user.Length > 0)
            {
                switch (user[0].TOUR_STAFF.TOUR_STAFF_ROLE)
                {
                    case "Manager":
                        user_id = user[0].TOUR_STAFF_ID;
                        return 1;

                    case "Staff":
                    case "Director":
                        user_id = user[0].TOUR_STAFF_ID;
                        return -1;

                    default:
                        user_id = 0;
                        return 0;
                }
            }
            user_id = 0;
            return 0;
        }

        public static void SaveAccount(string username, string password, int user_id)
        {
            Properties.Settings.Default.User_ID = user_id;
            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.UpdatePassword = "";
            Properties.Settings.Default.IsCheck = true;
            Properties.Settings.Default.Save();
        }

        public static void SaveID(int user_id)
        {
            Properties.Settings.Default.User_ID = user_id;
            Properties.Settings.Default.Username = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.UpdatePassword = "";
            Properties.Settings.Default.IsCheck = false;
            Properties.Settings.Default.Save();
        }
    }
}