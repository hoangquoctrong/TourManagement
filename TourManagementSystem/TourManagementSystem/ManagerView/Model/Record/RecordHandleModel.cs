using Caliburn.Micro;
using System;
using System.Linq;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public static class RecordHandleModel
    {
        public static BindableCollection<RecordModel> GetRecordList()
        {
            BindableCollection<RecordModel> RecordList = new BindableCollection<RecordModel>();

            var recordList = from record in DataProvider.Ins.DB.TOUR_RECORD
                             orderby record.TOUR_RECORD_DATE descending
                             select record;

            foreach (var item in recordList)
            {
                RecordModel record = new RecordModel()
                {
                    Record_ID = item.TOUR_RECORD_ID,
                    Staff_ID = item.TOUR_STAFF_ID,
                    Staff_Name = item.TOUR_STAFF.TOUR_STAFF_NAME,
                    Record_Content = item.TOUR_RECORD_CONTENT,
                    Record_Date = (DateTime)item.TOUR_RECORD_DATE,
                    Record_Date_String = ((DateTime)item.TOUR_RECORD_DATE).ToString("HH:mm:ss dd/MM/yyyy")
                };

                RecordList.Add(record);
            }
            return RecordList;
        }

        public static BindableCollection<RecordModel> GetRecordListByDate(DateTime date)
        {
            BindableCollection<RecordModel> RecordList = new BindableCollection<RecordModel>();

            var recordList = DataProvider.Ins.DB.TOUR_RECORD.Where(x => x.TOUR_RECORD_DATE.Value.Day == date.Day &&
                                                    x.TOUR_RECORD_DATE.Value.Month == date.Month &&
                                                    x.TOUR_RECORD_DATE.Value.Year == date.Year).OrderByDescending(x => x.TOUR_RECORD_DATE);

            foreach (var item in recordList)
            {
                RecordModel record = new RecordModel()
                {
                    Record_ID = item.TOUR_RECORD_ID,
                    Staff_ID = item.TOUR_STAFF_ID,
                    Staff_Name = item.TOUR_STAFF.TOUR_STAFF_NAME,
                    Record_Content = item.TOUR_RECORD_CONTENT,
                    Record_Date = (DateTime)item.TOUR_RECORD_DATE,
                    Record_Date_String = ((DateTime)item.TOUR_RECORD_DATE).ToString("HH:mm:ss dd/MM/yyyy")
                };

                RecordList.Add(record);
            }

            return RecordList;
        }
    }
}