using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class TourInformation
    {
        public int TourInformationID { get; set; }
        public PriceModel Price { get; set; }
        public TimeModel Time { get; set; }
        public List<ScheduleModel> Schedule { get; set; }

        public static List<TourInformation> getTourInformation(int tour_id)
        {
            List<TourInformation> tourInformations = new List<TourInformation>();
            var informationList = from information in DataProvider.Ins.DB.TOUR_INFORMATION
                                  where information.TOUR_ID == tour_id && information.TOUR_TIME.FirstOrDefault().TOUR_TIME_DEPARTMENT_DATE > DateTime.Now
                                  select information;
            foreach (var item in informationList)
            {
                TourInformation informationModel = new TourInformation()
                {
                    TourInformationID = item.TOUR_INFORMATION_ID,
                };
                informationModel.Schedule = new List<ScheduleModel>();

                foreach (var itemSchedule in item.TOUR_SCHEDULE)
                {
                    ScheduleModel scheduleModel = new ScheduleModel()
                    {
                        ScheduleId = itemSchedule.TOUR_SCHEDULE_ID,
                        ScheduleDay = itemSchedule.TOUR_SCHEDULE_DAY,
                        ScheduleContent = itemSchedule.TOUR_SCHEDULE_CONTENT
                    };
                    informationModel.Schedule.Add(scheduleModel);
                }

                var price = item.TOUR_PRICE.FirstOrDefault();
                informationModel.Price = new PriceModel()
                {
                    PriceId = price.TOUR_PRICE_ID,
                    PriceHotel = (double)price.TOUR_PRICE_COST_HOTEL,
                    PriceService = (double)price.TOUR_PRICE_COST_SERVICE,
                    PriceTransport = (double)price.TOUR_PRICE_COST_TRANSPORT,
                    PriceTotal = (double)price.TOUR_PRICE_COST_TOTAL
                };

                var time = item.TOUR_TIME.FirstOrDefault();
                informationModel.Time = new TimeModel()
                {
                    TimeId = time.TOUR_TIME_ID,
                    TimeDay = (int)time.TOUR_TIME_DAY,
                    TimeNight = (int)time.TOUR_TIME_NIGHT,
                    StartDate = (DateTime)time.TOUR_TIME_DEPARTMENT_DATE,
                    EndDate = (DateTime)time.TOUR_TIME_END_DATE,
                    TimeStartString = ((DateTime)time.TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy"),
                    TimeEndString = ((DateTime)time.TOUR_TIME_END_DATE).ToString("dd/MM/yyyy"),
                    TimeString = string.Format("{0} day(s) {1} night(s)", (int)time.TOUR_TIME_DAY, (int)time.TOUR_TIME_NIGHT)
                };
                tourInformations.Add(informationModel);
            }

            return tourInformations;
        }
    }
}