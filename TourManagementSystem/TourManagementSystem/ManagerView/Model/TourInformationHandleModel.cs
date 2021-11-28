using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public static class TourInformationHandleModel
    {
        public static ObservableCollection<TourScheduleModel> GetTourScheduleList(int tourinformation_id)
        {
            ObservableCollection<TourScheduleModel> ScheduleList = new ObservableCollection<TourScheduleModel>();

            var scheduleList = from schedule in DataProvider.Ins.DB.TOUR_SCHEDULE
                               where schedule.TOUR_INFORMATION_ID == tourinformation_id
                               select schedule;

            foreach (var item in scheduleList)
            {
                TourScheduleModel scheduleModel = new TourScheduleModel()
                {
                    SCHEDULE_ID = item.TOUR_SCHEDULE_ID,
                    SCHEDULE_DAY = item.TOUR_SCHEDULE_DAY,
                    SCHEDULE_CONTENT = item.TOUR_SCHEDULE_CONTENT
                };

                ScheduleList.Add(scheduleModel);
            }

            return ScheduleList;
        }
    }
}
