using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model.Register
{
    class RegisterHandleModel
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
    }
}
