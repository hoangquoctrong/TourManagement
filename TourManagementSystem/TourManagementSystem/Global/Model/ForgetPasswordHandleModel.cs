using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TourManagementSystem.Global.Model
{
    public static class ForgetPasswordHandleModel
    {
        public static bool IsSendEmail(string email, int validate_number)
        {
            string from = "pplthdt.uit.team3@gmail.com";
            string pass = "UIT@team3";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from);
                mail.To.Add(email);
                mail.Subject = "Confirm Email Code";
                mail.Body = string.Format("Verification code exists for your email : {0}", validate_number);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(from, pass);
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
                }
            }
        }

        public static int ConfirmForgetPassword(string username, out int user_id)
        {
            var user = (from u in DataProvider.Ins.DB.TOUR_ACCOUNT
                        where u.TOUR_ACCOUNT_NAME == username
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

        public static void SaveAccount(string username, int user_id)
        {
            Properties.Settings.Default.User_ID = user_id;
            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
        }
    }
}
