using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model.Register
{
    internal class RegisterHandleModel
    {
        public static BindableCollection<RegisterModel> GetRegisterList()
        {
            BindableCollection<RegisterModel> RecordList = new BindableCollection<RegisterModel>();

            var registerList = from register in DataProvider.Ins.DB.REGISTER
                               orderby register.REGISTER_ID descending
                               select register;

            foreach (var item in registerList)
            {
                RegisterModel register = new RegisterModel()
                {
                    Register_ID = item.REGISTER_ID,
                    Register_Address = item.REGISTER_ADDRESS,
                    Register_Detail = item.REGISTER_DETAIL,
                    Register_Email = item.REGISTER_EMAILL,
                    Register_PhoneNumber = item.REGISTER_PHONE_NUMBER,
                    Tour_ID = item.TOUR_ID,
                    Register_Name = item.REGISTER_NAME
                };

                RecordList.Add(register);
            }
            return RecordList;
        }

        public static bool DeleteRegister(RegisterModel register, int user_id)
        {
            try
            {
                var Register = DataProvider.Ins.DB.REGISTER.Where(x => x.REGISTER_ID == register.Register_ID).SingleOrDefault();
                DataProvider.Ins.DB.REGISTER.Remove(Register);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Delete register form of {0} with phone number is {1}", register.Register_Name, register.Register_PhoneNumber)
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (DbEntityValidationResult eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (DbValidationError ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}