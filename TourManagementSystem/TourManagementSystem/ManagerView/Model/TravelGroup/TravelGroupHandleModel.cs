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
                    TOUR_RECORD_CONTENT = string.Format("Add new travel group with name is {0}", travelGroup.TravelGroup_Name)
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                DataProvider.Ins.DB.SaveChanges();

                return travelgroupdb.TRAVEL_GROUP_ID;
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

        public static bool UpdateTravelGroup(TravelGroupModel travelGroup, int user_id)
        {
            try
            {
                TRAVEL_GROUP travelgroupdb = DataProvider.Ins.DB.TRAVEL_GROUP.Where(x => x.TRAVEL_GROUP_ID == travelGroup.TravelGroup_ID).FirstOrDefault();

                string changeToSave = string.Format("Update Travel Group with id {0}: ", travelGroup.TravelGroup_ID);
                int countChangeToSave = 0;
                if (travelgroupdb.TRAVEL_GROUP_NAME != travelGroup.TravelGroup_Name)
                {
                    changeToSave += string.Format("Travel Group Name Change ({0} -> {1})   ", travelgroupdb.TRAVEL_GROUP_NAME, travelGroup.TravelGroup_Name);
                    travelgroupdb.TRAVEL_GROUP_NAME = travelGroup.TravelGroup_Name;
                    countChangeToSave++;
                }
                if (travelgroupdb.TRAVEL_GROUP_CONTENT_DETAIL != travelGroup.TravelGroup_Type)
                {
                    changeToSave += string.Format("Travel Group Type Change ({0} -> {1})   ", travelgroupdb.TRAVEL_GROUP_CONTENT_DETAIL, travelGroup.TravelGroup_Type);
                    travelgroupdb.TRAVEL_GROUP_CONTENT_DETAIL = travelGroup.TravelGroup_Type;
                    countChangeToSave++;
                }

                if (countChangeToSave != 0)
                {
                    TOUR_RECORD tour_record = new TOUR_RECORD
                    {
                        TOUR_STAFF_ID = user_id,
                        TOUR_RECORD_DATE = DateTime.Now,
                        TOUR_RECORD_CONTENT = changeToSave
                    };
                    DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                    DataProvider.Ins.DB.SaveChanges();
                    return true;
                }

                return false;
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

        public static ObservableCollection<TravellerModel> GetTravellerList()
        {
            ObservableCollection<TravellerModel> TravellerList = new ObservableCollection<TravellerModel>();

            var travellerlist = from traveller in DataProvider.Ins.DB.TRAVELLER
                                select traveller;

            if (travellerlist.Count() == 0)
            {
                return TravellerList;
            }

            foreach (var item in travellerlist)
            {
                TravellerModel traveller = new TravellerModel()
                {
                    Traveller_ID = item.TRAVELLER_ID,
                    Traveller_Name = item.TRAVELLER_NAME,
                    Traveller_Address = item.TRAVELLER_ADDRESS,
                    Traveller_Birth = (DateTime)item.TRAVELLER_BIRTH,
                    Traveller_BirthString = ((DateTime)item.TRAVELLER_BIRTH).ToString("dd/MM/yyyy"),
                    Traveller_CitizenIdentity = item.TRAVELLER_CITIZEN_IDENTITY,
                    Traveller_PhoneNumber = item.TRAVELLER_PHONE_NUMBER,
                    Traveller_Sex = item.TRAVELLER_SEX,
                    Traveller_Type = item.TRAVELLER_TYPE
                };

                TravellerList.Add(traveller);
            }

            return TravellerList;
        }

        public static bool InsertTraveller(TravellerModel Traveller, int user_id, bool isOnly)
        {
            try
            {
                TRAVELLER traveller = new TRAVELLER()
                {
                    TRAVELLER_NAME = Traveller.Traveller_Name,
                    TRAVELLER_TYPE = Traveller.Traveller_Type,
                    TRAVELLER_ADDRESS = Traveller.Traveller_Address,
                    TRAVELLER_CITIZEN_IDENTITY = Traveller.Traveller_CitizenIdentity,
                    TRAVELLER_PHONE_NUMBER = Traveller.Traveller_PhoneNumber,
                    TRAVELLER_SEX = Traveller.Traveller_Sex,
                    TRAVELLER_BIRTH = Traveller.Traveller_Birth
                };
                DataProvider.Ins.DB.TRAVELLER.Add(traveller);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Add new traveller with name is {0}", Traveller.Traveller_Name)
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                if (isOnly)
                {
                    DataProvider.Ins.DB.SaveChanges();
                }

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

        public static bool UpdateTraveller(TravellerModel Traveller, int user_id, bool isOnly)
        {
            try
            {
                TRAVELLER travellerdb = DataProvider.Ins.DB.TRAVELLER.Where(x => x.TRAVELLER_ID == Traveller.Traveller_ID).FirstOrDefault();

                string changeToSave = string.Format("Update Traveller with id {0}: ", Traveller.Traveller_ID);
                int countChangeToSave = 0;
                if (travellerdb.TRAVELLER_NAME != Traveller.Traveller_Name)
                {
                    changeToSave += string.Format("Traveller Name Change ({0} -> {1})   ", travellerdb.TRAVELLER_NAME, Traveller.Traveller_Name);
                    travellerdb.TRAVELLER_NAME = Traveller.Traveller_Name;
                    countChangeToSave++;
                }
                if (travellerdb.TRAVELLER_TYPE != Traveller.Traveller_Type)
                {
                    changeToSave += string.Format("Traveller Type Change ({0} -> {1})   ", travellerdb.TRAVELLER_TYPE, Traveller.Traveller_Type);
                    travellerdb.TRAVELLER_TYPE = Traveller.Traveller_Type;
                    countChangeToSave++;
                }
                if (travellerdb.TRAVELLER_ADDRESS != Traveller.Traveller_Address)
                {
                    changeToSave += string.Format("Traveller Address Change ({0} -> {1})   ", travellerdb.TRAVELLER_ADDRESS, Traveller.Traveller_Address);
                    travellerdb.TRAVELLER_ADDRESS = Traveller.Traveller_Address;
                    countChangeToSave++;
                }
                if (travellerdb.TRAVELLER_CITIZEN_IDENTITY != Traveller.Traveller_CitizenIdentity)
                {
                    changeToSave += string.Format("Traveller Citizen Identity Change ({0} -> {1})   ", travellerdb.TRAVELLER_CITIZEN_IDENTITY, Traveller.Traveller_CitizenIdentity);
                    travellerdb.TRAVELLER_CITIZEN_IDENTITY = Traveller.Traveller_CitizenIdentity;
                    countChangeToSave++;
                }
                if (travellerdb.TRAVELLER_PHONE_NUMBER != Traveller.Traveller_PhoneNumber)
                {
                    changeToSave += string.Format("Traveller Phone Number Change ({0} -> {1})   ", travellerdb.TRAVELLER_PHONE_NUMBER, Traveller.Traveller_PhoneNumber);
                    travellerdb.TRAVELLER_PHONE_NUMBER = Traveller.Traveller_PhoneNumber;
                    countChangeToSave++;
                }
                if (travellerdb.TRAVELLER_SEX != Traveller.Traveller_Sex)
                {
                    changeToSave += string.Format("Traveller Gender Change ({0} -> {1})   ", travellerdb.TRAVELLER_SEX, Traveller.Traveller_Sex);
                    travellerdb.TRAVELLER_SEX = Traveller.Traveller_Sex;
                    countChangeToSave++;
                }
                string TravellerDB_Birth_String = ((DateTime)travellerdb.TRAVELLER_BIRTH).ToString("dd/MM/yyyy");
                string Traveller_Birth_String = Traveller.Traveller_Birth.ToString("dd/MM/yyyy");
                if (TravellerDB_Birth_String != Traveller_Birth_String)
                {
                    changeToSave += string.Format("Traveller Birth Change ({0} -> {1})   ", TravellerDB_Birth_String, Traveller_Birth_String);
                    travellerdb.TRAVELLER_BIRTH = Traveller.Traveller_Birth;
                    countChangeToSave++;
                }

                if (countChangeToSave != 0)
                {
                    TOUR_RECORD tour_record = new TOUR_RECORD
                    {
                        TOUR_STAFF_ID = user_id,
                        TOUR_RECORD_DATE = DateTime.Now,
                        TOUR_RECORD_CONTENT = changeToSave
                    };
                    DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                    if (isOnly)
                    {
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    return true;
                }

                return false;
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

        public static bool InsertOrUpdateTravellerList(ObservableCollection<TravellerModel> TravellerDetailList, int user_id)
        {
            try
            {
                foreach (var item in TravellerDetailList)
                {
                    if (item.Traveller_ID == 0)
                    {
                        InsertTraveller(item, user_id, false);
                    }
                    else
                    {
                        UpdateTraveller(item, user_id, false);
                    }
                }
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

        public static bool InsertTravellerDetailList(ObservableCollection<TravellerModel> TravellerDetailList, int travelgroup_id, int user_id, bool IsFirst)
        {
            try
            {
                int countTraveller = 0;
                foreach (var item in TravellerDetailList)
                {
                    TRAVELLER traveller = DataProvider.Ins.DB.TRAVELLER.Where(x => x.TRAVELLER_NAME.Equals(item.Traveller_Name)).FirstOrDefault();
                    TRAVELLER_DETAIL travellerdetail = new TRAVELLER_DETAIL()
                    {
                        TRAVELLER_DETAIL_STAR = 0,
                        TRAVELLER_ID = traveller.TRAVELLER_ID,
                        TRAVEL_GROUP_ID = travelgroup_id
                    };
                    countTraveller++;
                    DataProvider.Ins.DB.TRAVELLER_DETAIL.Add(travellerdetail);
                }
                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("{0} {1} travellers in travel group with id is {2}", IsFirst ? "Add" : "Update", countTraveller, travelgroup_id)
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

        public static bool DeleteTravellerDetail(int travelgroup_id)
        {
            try
            {
                var travellerdetail = DataProvider.Ins.DB.TRAVELLER_DETAIL.Where(x => x.TRAVEL_GROUP_ID == travelgroup_id);
                DataProvider.Ins.DB.TRAVELLER_DETAIL.RemoveRange(travellerdetail);
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

        public static bool InsertTravelGroupCost(TravelCostModel TravelCost, int travelgroup_id, int user_id)
        {
            try
            {
                TRAVEL_COST travelcost = new TRAVEL_COST()
                {
                    TRAVEL_GROUP_ID = travelgroup_id,
                    TOTAL_HOTEL_COST = TravelCost.HotelPrice,
                    TOTAL_SERVICE_COST = TravelCost.ServicePrice,
                    TOTAL_TRANSPORT_COST = TravelCost.TransportPrice,
                    ANOTHER_COST = TravelCost.AnotherPrice,
                    TRAVEL_COST_DESCRIPTION = TravelCost.TravelCostDescription
                };
                DataProvider.Ins.DB.TRAVEL_COST.Add(travelcost);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Add new travel cost with travel group id is {0}", travelgroup_id)
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

        public static bool UpdateTravelGroupCost(TravelCostModel TravelCost, int user_id)
        {
            try
            {
                TRAVEL_COST travelcost = DataProvider.Ins.DB.TRAVEL_COST.Where(x => x.TRAVEL_COST_ID == TravelCost.TravelCost_ID).FirstOrDefault();

                string changeToSave = string.Format("Update Travel Cost with ID {0}: ", TravelCost.TravelCost_ID);
                int countChangeToSave = 0;
                if (travelcost.TOTAL_HOTEL_COST != TravelCost.HotelPrice)
                {
                    changeToSave += string.Format("Total Hotel Price Change ({0} -> {1})   ", travelcost.TOTAL_HOTEL_COST, TravelCost.HotelPrice);
                    travelcost.TOTAL_HOTEL_COST = TravelCost.HotelPrice;
                    countChangeToSave++;
                }
                if (travelcost.TOTAL_TRANSPORT_COST != TravelCost.TransportPrice)
                {
                    changeToSave += string.Format("Total Transport Price Change ({0} -> {1})   ", travelcost.TOTAL_TRANSPORT_COST, TravelCost.TransportPrice);
                    travelcost.TOTAL_TRANSPORT_COST = TravelCost.TransportPrice;
                    countChangeToSave++;
                }
                if (travelcost.TOTAL_SERVICE_COST != TravelCost.ServicePrice)
                {
                    changeToSave += string.Format("Total Service Price Change ({0} -> {1})   ", travelcost.TOTAL_SERVICE_COST, TravelCost.ServicePrice);
                    travelcost.TOTAL_SERVICE_COST = TravelCost.ServicePrice;
                    countChangeToSave++;
                }
                if (travelcost.ANOTHER_COST != TravelCost.AnotherPrice)
                {
                    changeToSave += string.Format("Another Price Change ({0} -> {1})   ", travelcost.ANOTHER_COST, TravelCost.AnotherPrice);
                    travelcost.ANOTHER_COST = TravelCost.AnotherPrice;
                    countChangeToSave++;
                }
                if (travelcost.TRAVEL_COST_DESCRIPTION != TravelCost.TravelCostDescription)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", travelcost.TRAVEL_COST_DESCRIPTION, TravelCost.TravelCostDescription);
                    travelcost.TRAVEL_COST_DESCRIPTION = TravelCost.TravelCostDescription;
                    countChangeToSave++;
                }

                if (countChangeToSave != 0)
                {
                    TOUR_RECORD tour_record = new TOUR_RECORD
                    {
                        TOUR_STAFF_ID = user_id,
                        TOUR_RECORD_DATE = DateTime.Now,
                        TOUR_RECORD_CONTENT = changeToSave
                    };
                    DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                    DataProvider.Ins.DB.SaveChanges();
                    return true;
                }

                return false;
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
