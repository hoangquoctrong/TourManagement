using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public static class TravelGroupHandleModel
    {
        public static ObservableCollection<TravelGroupModel> GetTravelGroupList()
        {
            ObservableCollection<TravelGroupModel> TravelGroupList = new ObservableCollection<TravelGroupModel>();

            var travelgroupList = from travel in DataProvider.Ins.DB.TRAVEL_GROUP
                                  select travel;

            foreach (var item in travelgroupList)
            {
                TravelGroupModel travelgroup = new TravelGroupModel()
                {
                    TourInformation_ID = item.TOUR_INFORMATION_ID,
                    TravelGroup_ID = item.TRAVEL_GROUP_ID,
                    TravelGroup_Name = item.TRAVEL_GROUP_NAME,
                    TravelGroup_Type = item.TRAVEL_GROUP_CONTENT_DETAIL,
                    Tour_Name = item.TOUR_INFORMATION.TOUR.TOUR_NAME,
                    Tour_Start = (DateTime)item.TOUR_INFORMATION.TOUR_TIME.First().TOUR_TIME_DEPARTMENT_DATE,
                    Tour_End = (DateTime)item.TOUR_INFORMATION.TOUR_TIME.First().TOUR_TIME_END_DATE,
                    Tour_StartString = ((DateTime)item.TOUR_INFORMATION.TOUR_TIME.First().TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy"),
                    Tour_EndString = ((DateTime)item.TOUR_INFORMATION.TOUR_TIME.First().TOUR_TIME_END_DATE).ToString("dd/MM/yyyy")
                };

                TravelGroupList.Add(travelgroup);
            }

            return TravelGroupList;
        }

        public static int InsertTravelGroup(TravelGroupModel travelGroup, int user_id)
        {
            try
            {
                TRAVEL_GROUP travelgroupdb = new TRAVEL_GROUP()
                {
                    TOUR_INFORMATION_ID = travelGroup.TourInformation_ID,
                    TRAVEL_GROUP_NAME = travelGroup.TravelGroup_Name,
                    TRAVEL_GROUP_CONTENT_DETAIL = travelGroup.TravelGroup_Type
                };
                DataProvider.Ins.DB.TRAVEL_GROUP.Add(travelgroupdb);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Add new travel group with name is {1}", travelGroup.TravelGroup_Name)
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                DataProvider.Ins.DB.SaveChanges();

                TRAVEL_GROUP newtravelgroup = DataProvider.Ins.DB.TRAVEL_GROUP.Where(x => x.TRAVEL_GROUP_NAME == travelGroup.TravelGroup_Name).FirstOrDefault();
                return newtravelgroup.TRAVEL_GROUP_ID;
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
