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
    public static class TourInformationHandleModel
    {
        public static ObservableCollection<TourInformationModel> GetTourInformationList(int tour_id)
        {
            ObservableCollection<TourInformationModel> InformationList = new ObservableCollection<TourInformationModel>();

            var informationList = from information in DataProvider.Ins.DB.TOUR_INFORMATION
                                  where information.TOUR_ID == tour_id
                                  select information;

            foreach (var item in informationList)
            {
                TourInformationModel informationModel = new TourInformationModel()
                {
                    INFORMATION_ID = item.TOUR_INFORMATION_ID,
                    TOUR_ID = item.TOUR_ID
                };

                informationModel.INFORMATION_LOCATION_LIST = new ObservableCollection<LocationModel>();
                informationModel.INFORMATION_TRANSPORT_LIST = new ObservableCollection<TransportModel>();
                informationModel.INFORMATION_HOTEL_LIST = new ObservableCollection<HotelModel>();
                informationModel.INFORMATION_SCHEDULE_LIST = new ObservableCollection<TourScheduleModel>();
                informationModel.INFORMATION_MISSION_LIST = new ObservableCollection<MissionModel>();

                foreach (var itemLocation in item.TOUR_LOCATION_DETAILED)
                {
                    LocationModel location = new LocationModel()
                    {
                        LOCATION_ADDRESS = itemLocation.TOUR_LOCATION.TOUR_LOCATION_ADDRESS,
                        LOCATION_DESCRIPTION = itemLocation.TOUR_LOCATION.TOUR_LOCATION_DESCRIPTION,
                        LOCATION_ID = itemLocation.TOUR_LOCATION_ID,
                        LOCATION_NAME = itemLocation.TOUR_LOCATION.TOUR_LOCATION_NAME,
                        PLACE_ID = itemLocation.TOUR_LOCATION.PLACE_ID,
                        PLACE_NAME = itemLocation.TOUR_LOCATION.PLACE.PLACE_NAME
                    };

                    informationModel.INFORMATION_LOCATION_LIST.Add(location);
                }

                foreach (var itemTransport in item.TOUR_TRANSPORT_DETAIL)
                {
                    TransportModel transport = new TransportModel()
                    {
                        TRANSPORT_ID = itemTransport.TOUR_TRANSPORT_ID,
                        TRANSPORT_NAME = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_NAME,
                        TRANSPORT_AMOUNT_MAX = (int)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_AMOUNT_MAX,
                        TRANSPORT_COMPANY = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_COMPANY,
                        TRANSPORT_DESCRIPTION = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_DESCRIPTION,
                        TRANSPORT_TYPE = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPE,
                        TRANSPORT_DATE = (DateTime)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_START_DATE,
                        TRANSPORT_PRICE = (double)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_PRICE,
                        TRANSPORT_TYPETRANS = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPETRANS,
                        TRANSPORT_IS_DELETE = (bool)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_DELETE,
                        TRANSPORT_AMOUNT = (int)itemTransport.TOUR_TRANSPORT_DETAIL_AMOUNT,
                        TRANSPORT_IS_AMOUNT = ((int)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_AMOUNT_MAX == 0) ? false : true,
                        TRANSPORT_STRING_DATE = ((DateTime)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_START_DATE).ToString("dd/MM/yyyy"),
                        CB_TransportAmount = LoadTransportComboBox((int)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_AMOUNT_MAX)
                    };

                    informationModel.INFORMATION_TRANSPORT_LIST.Add(transport);
                }

                foreach (var itemHotel in item.TOUR_HOTEL_DETAIL)
                {
                    HotelModel hotel = new HotelModel()
                    {
                        HOTEL_ID = itemHotel.TOUR_HOTEL_ID,
                        HOTEL_NAME = itemHotel.TOUR_HOTEL.TOUR_HOTEL_NAME,
                        HOTEL_ADDRESS = itemHotel.TOUR_HOTEL.TOUR_HOTEL_ADDRESS,
                        HOTEL_PHONE_NUMBER = itemHotel.TOUR_HOTEL.TOUR_HOTEL_PHONE_NUMBER,
                        HOTEL_DESCRIPTION = itemHotel.TOUR_HOTEL.TOUR_HOTEL_DESCRIPTION,
                        HOTEL_TYPE = itemHotel.TOUR_HOTEL.TOUR_HOTEL_TYPE,
                        HOTEL_IS_RESTAURANT = itemHotel.TOUR_HOTEL.TOUR_HOTEL_IS_RESTAURANT,
                        HOTEL_PRICE = (double)itemHotel.TOUR_HOTEL.TOUR_HOTEL_PRICE,
                        HOTEL_EMAIL = itemHotel.TOUR_HOTEL.TOUR_HOTEL_EMAIL,
                        HOTEL_IS_DELETE = (bool)itemHotel.TOUR_HOTEL.TOUR_HOTEL_DELETE,
                        PLACE_ID = itemHotel.TOUR_HOTEL.PLACE_ID,
                        PLACE_NAME = itemHotel.TOUR_HOTEL.PLACE.PLACE_NAME,
                        HOTEL_DAY = (int)itemHotel.TOUR_HOTEL_DETAIL_DAY
                    };

                    informationModel.INFORMATION_HOTEL_LIST.Add(hotel);
                }

                foreach (var itemSchedule in item.TOUR_SCHEDULE)
                {
                    TourScheduleModel scheduleModel = new TourScheduleModel()
                    {
                        SCHEDULE_ID = itemSchedule.TOUR_SCHEDULE_ID,
                        SCHEDULE_DAY = itemSchedule.TOUR_SCHEDULE_DAY,
                        SCHEDULE_CONTENT = itemSchedule.TOUR_SCHEDULE_CONTENT
                    };

                    informationModel.INFORMATION_SCHEDULE_LIST.Add(scheduleModel);
                }

                foreach (var itemMission in item.TOUR_MISSION)
                {
                    MissionModel mission = new MissionModel()
                    {
                        Mission_ID = itemMission.TOUR_MISSION_ID,
                        Mission_Responsibility = itemMission.TOUR_MISSION_RESPONSIBILITY,
                        Mission_Description = itemMission.TOUR_MISSION_DESCRIPTION,
                        Mission_Price = (double)itemMission.TOUR_MISSION_PRICE,
                        Mission_Count = (int)itemMission.TOUR_MISSION_COUNT
                    };

                    informationModel.INFORMATION_MISSION_LIST.Add(mission);
                }

                TOUR_PRICE price = item.TOUR_PRICE.FirstOrDefault();
                informationModel.INFORMATION_PRICE = new TourPriceModel()
                {
                    PRICE_ID = price.TOUR_PRICE_ID,
                    PRICE_COST_HOTEL = (double)price.TOUR_PRICE_COST_HOTEL,
                    PRICE_COST_SERVICE = (double)price.TOUR_PRICE_COST_SERVICE,
                    PRICE_COST_TRANSPORT = (double)price.TOUR_PRICE_COST_TRANSPORT,
                    PRICE_COST_TOTAL = (double)price.TOUR_PRICE_COST_TOTAL
                };

                TOUR_TIME time = item.TOUR_TIME.FirstOrDefault();
                informationModel.INFORMATION_TIME = new TourTimeModel()
                {
                    TIME_ID = time.TOUR_TIME_ID,
                    TIME_DAY = (int)time.TOUR_TIME_DAY,
                    TIME_NIGHT = (int)time.TOUR_TIME_NIGHT,
                    TIME_DEPARTMENT_TIME = (DateTime)time.TOUR_TIME_DEPARTMENT_DATE,
                    TIME_END_TIME = (DateTime)time.TOUR_TIME_END_DATE,
                    TIME_DEPARTMENT_STRING = ((DateTime)time.TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy"),
                    TIME_END_STRING = ((DateTime)time.TOUR_TIME_END_DATE).ToString("dd/MM/yyyy"),
                    TIME_STRING = string.Format("{0} day(s) {1} night(s)", (int)time.TOUR_TIME_DAY, (int)time.TOUR_TIME_NIGHT)
                };

                InformationList.Add(informationModel);
            }

            return InformationList;
        }

        public static TourInformationModel GetTourInformation(int tourinformation_id)
        {
            TOUR_INFORMATION item = DataProvider.Ins.DB.TOUR_INFORMATION.Where(x => x.TOUR_INFORMATION_ID == tourinformation_id).FirstOrDefault();

            TourInformationModel informationModel = new TourInformationModel()
            {
                INFORMATION_ID = item.TOUR_INFORMATION_ID,
                TOUR_ID = item.TOUR_ID
            };

            informationModel.INFORMATION_LOCATION_LIST = new ObservableCollection<LocationModel>();
            informationModel.INFORMATION_TRANSPORT_LIST = new ObservableCollection<TransportModel>();
            informationModel.INFORMATION_HOTEL_LIST = new ObservableCollection<HotelModel>();
            informationModel.INFORMATION_SCHEDULE_LIST = new ObservableCollection<TourScheduleModel>();
            informationModel.INFORMATION_MISSION_LIST = new ObservableCollection<MissionModel>();

            foreach (var itemLocation in item.TOUR_LOCATION_DETAILED)
            {
                LocationModel location = new LocationModel()
                {
                    LOCATION_ADDRESS = itemLocation.TOUR_LOCATION.TOUR_LOCATION_ADDRESS,
                    LOCATION_DESCRIPTION = itemLocation.TOUR_LOCATION.TOUR_LOCATION_DESCRIPTION,
                    LOCATION_ID = itemLocation.TOUR_LOCATION_ID,
                    LOCATION_NAME = itemLocation.TOUR_LOCATION.TOUR_LOCATION_NAME,
                    PLACE_ID = itemLocation.TOUR_LOCATION.PLACE_ID,
                    PLACE_NAME = itemLocation.TOUR_LOCATION.PLACE.PLACE_NAME
                };

                informationModel.INFORMATION_LOCATION_LIST.Add(location);
            }

            foreach (var itemTransport in item.TOUR_TRANSPORT_DETAIL)
            {
                TransportModel transport = new TransportModel()
                {
                    TRANSPORT_ID = itemTransport.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_NAME,
                    TRANSPORT_AMOUNT_MAX = (int)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_AMOUNT_MAX,
                    TRANSPORT_COMPANY = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_PRICE = (double)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_PRICE,
                    TRANSPORT_TYPETRANS = itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPETRANS,
                    TRANSPORT_IS_DELETE = (bool)itemTransport.TOUR_TRANSPORT.TOUR_TRANSPORT_DELETE
                };

                informationModel.INFORMATION_TRANSPORT_LIST.Add(transport);
            }

            foreach (var itemHotel in item.TOUR_HOTEL_DETAIL)
            {
                HotelModel hotel = new HotelModel()
                {
                    HOTEL_ID = itemHotel.TOUR_HOTEL_ID,
                    HOTEL_NAME = itemHotel.TOUR_HOTEL.TOUR_HOTEL_NAME,
                    HOTEL_ADDRESS = itemHotel.TOUR_HOTEL.TOUR_HOTEL_ADDRESS,
                    HOTEL_PHONE_NUMBER = itemHotel.TOUR_HOTEL.TOUR_HOTEL_PHONE_NUMBER,
                    HOTEL_DESCRIPTION = itemHotel.TOUR_HOTEL.TOUR_HOTEL_DESCRIPTION,
                    HOTEL_TYPE = itemHotel.TOUR_HOTEL.TOUR_HOTEL_TYPE,
                    HOTEL_IS_RESTAURANT = itemHotel.TOUR_HOTEL.TOUR_HOTEL_IS_RESTAURANT,
                    HOTEL_PRICE = (double)itemHotel.TOUR_HOTEL.TOUR_HOTEL_PRICE,
                    HOTEL_EMAIL = itemHotel.TOUR_HOTEL.TOUR_HOTEL_EMAIL,
                    HOTEL_IS_DELETE = (bool)itemHotel.TOUR_HOTEL.TOUR_HOTEL_DELETE,
                    PLACE_ID = itemHotel.TOUR_HOTEL.PLACE_ID,
                    PLACE_NAME = itemHotel.TOUR_HOTEL.PLACE.PLACE_NAME
                };

                informationModel.INFORMATION_HOTEL_LIST.Add(hotel);
            }

            foreach (var itemSchedule in item.TOUR_SCHEDULE)
            {
                TourScheduleModel scheduleModel = new TourScheduleModel()
                {
                    SCHEDULE_ID = itemSchedule.TOUR_SCHEDULE_ID,
                    SCHEDULE_DAY = itemSchedule.TOUR_SCHEDULE_DAY,
                    SCHEDULE_CONTENT = itemSchedule.TOUR_SCHEDULE_CONTENT
                };

                informationModel.INFORMATION_SCHEDULE_LIST.Add(scheduleModel);
            }

            foreach (var itemMission in item.TOUR_MISSION)
            {
                MissionModel mission = new MissionModel()
                {
                    Mission_ID = itemMission.TOUR_MISSION_ID,
                    Mission_Responsibility = itemMission.TOUR_MISSION_RESPONSIBILITY,
                    Mission_Description = itemMission.TOUR_MISSION_DESCRIPTION,
                    Mission_Price = (double)itemMission.TOUR_MISSION_PRICE,
                    Mission_Count = (int)itemMission.TOUR_MISSION_COUNT
                };

                informationModel.INFORMATION_MISSION_LIST.Add(mission);
            }

            TOUR_PRICE price = item.TOUR_PRICE.FirstOrDefault();
            informationModel.INFORMATION_PRICE = new TourPriceModel()
            {
                PRICE_ID = price.TOUR_PRICE_ID,
                PRICE_COST_HOTEL = (double)price.TOUR_PRICE_COST_HOTEL,
                PRICE_COST_SERVICE = (double)price.TOUR_PRICE_COST_SERVICE,
                PRICE_COST_TRANSPORT = (double)price.TOUR_PRICE_COST_TRANSPORT,
                PRICE_COST_TOTAL = (double)price.TOUR_PRICE_COST_TOTAL
            };

            TOUR_TIME time = item.TOUR_TIME.FirstOrDefault();
            informationModel.INFORMATION_TIME = new TourTimeModel()
            {
                TIME_ID = time.TOUR_TIME_ID,
                TIME_DAY = (int)time.TOUR_TIME_DAY,
                TIME_NIGHT = (int)time.TOUR_TIME_NIGHT,
                TIME_DEPARTMENT_TIME = (DateTime)time.TOUR_TIME_DEPARTMENT_DATE,
                TIME_END_TIME = (DateTime)time.TOUR_TIME_END_DATE,
                TIME_DEPARTMENT_STRING = ((DateTime)time.TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy"),
                TIME_END_STRING = ((DateTime)time.TOUR_TIME_END_DATE).ToString("dd/MM/yyyy"),
                TIME_STRING = string.Format("{0} day(s) {1} night(s)", (int)time.TOUR_TIME_DAY, (int)time.TOUR_TIME_NIGHT)
            };

            return informationModel;
        }

        public static int InsertTourInformation(TourInformationModel information, int user_id)
        {
            try
            {
                TOUR_INFORMATION tourInformation = new TOUR_INFORMATION()
                {
                    TOUR_ID = information.TOUR_ID
                };
                DataProvider.Ins.DB.TOUR_INFORMATION.Add(tourInformation);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Add new information with id is {0} of tour {1}", tourInformation.TOUR_INFORMATION_ID, tourInformation.TOUR.TOUR_NAME)
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
                DataProvider.Ins.DB.SaveChanges();

                return tourInformation.TOUR_INFORMATION_ID;
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

        public static bool InsertTourScheduleList(ObservableCollection<TourScheduleModel> schedules, int tour_informationID, int user_id, bool IsFirst)
        {
            try
            {
                int countSchedule = 0;
                foreach (TourScheduleModel item in schedules)
                {
                    TOUR_SCHEDULE schedule = new TOUR_SCHEDULE()
                    {
                        TOUR_SCHEDULE_DAY = item.SCHEDULE_DAY,
                        TOUR_SCHEDULE_CONTENT = item.SCHEDULE_CONTENT,
                        TOUR_INFORMATION_ID = tour_informationID
                    };
                    countSchedule++;
                    DataProvider.Ins.DB.TOUR_SCHEDULE.Add(schedule);
                }
                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("{0} {1} schudules in tour information with id is {2}", IsFirst ? "Add" : "Update", countSchedule, tour_informationID)
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

        public static bool DeleteScheduleDetail(int tour_information_id)
        {
            try
            {
                var schedule = DataProvider.Ins.DB.TOUR_SCHEDULE.Where(x => x.TOUR_INFORMATION_ID == tour_information_id);
                DataProvider.Ins.DB.TOUR_SCHEDULE.RemoveRange(schedule);
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

        public static bool InsertTourTime(TourTimeModel time, int tourinformation_id, int user_id)
        {
            try
            {
                TOUR_TIME tourtime = new TOUR_TIME()
                {
                    TOUR_INFORMATION_ID = tourinformation_id,
                    TOUR_TIME_DAY = time.TIME_DAY,
                    TOUR_TIME_NIGHT = time.TIME_NIGHT,
                    TOUR_TIME_DEPARTMENT_DATE = time.TIME_DEPARTMENT_TIME,
                    TOUR_TIME_END_DATE = time.TIME_END_TIME
                };
                DataProvider.Ins.DB.TOUR_TIME.Add(tourtime);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new tour time with tour information id is : " + tourinformation_id
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
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

        public static bool UpdateTourTime(TourTimeModel time, int tourinformation_id, int user_id)
        {
            try
            {
                TOUR_TIME tourtime = DataProvider.Ins.DB.TOUR_TIME.Where(x => x.TOUR_INFORMATION_ID == tourinformation_id).FirstOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;
                if (tourtime.TOUR_TIME_DAY != time.TIME_DAY)
                {
                    changeToSave += string.Format("Day Change ({0} -> {1})   ", tourtime.TOUR_TIME_DAY, time.TIME_DAY);
                    tourtime.TOUR_TIME_DAY = time.TIME_DAY;
                    countChangeToSave++;
                }

                if (tourtime.TOUR_TIME_NIGHT != time.TIME_NIGHT)
                {
                    changeToSave += string.Format("Night Change ({0} -> {1})   ", tourtime.TOUR_TIME_NIGHT, time.TIME_NIGHT);
                    tourtime.TOUR_TIME_NIGHT = time.TIME_NIGHT;
                    countChangeToSave++;
                }

                string Department_Date_String = ((DateTime)tourtime.TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy");
                if (Department_Date_String != time.TIME_DEPARTMENT_STRING)
                {
                    changeToSave += string.Format("Department date Change ({0} -> {1})   ", Department_Date_String, time.TIME_DEPARTMENT_STRING);
                    tourtime.TOUR_TIME_DEPARTMENT_DATE = time.TIME_DEPARTMENT_TIME;
                    countChangeToSave++;
                }

                string End_Date_String = ((DateTime)tourtime.TOUR_TIME_END_DATE).ToString("dd/MM/yyyy");
                if (End_Date_String != time.TIME_END_STRING)
                {
                    changeToSave += string.Format("End date Change ({0} -> {1})   ", End_Date_String, time.TIME_END_STRING);
                    tourtime.TOUR_TIME_END_DATE = time.TIME_END_TIME;
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

        public static bool InsertTourPrice(TourPriceModel price, int tourinformation_id, int user_id)
        {
            try
            {
                TOUR_PRICE tourprice = new TOUR_PRICE()
                {
                    TOUR_INFORMATION_ID = tourinformation_id,
                    TOUR_PRICE_COST_HOTEL = price.PRICE_COST_HOTEL,
                    TOUR_PRICE_COST_SERVICE = price.PRICE_COST_SERVICE,
                    TOUR_PRICE_COST_TRANSPORT = price.PRICE_COST_TRANSPORT,
                    TOUR_PRICE_COST_TOTAL = price.PRICE_COST_TOTAL
                };
                DataProvider.Ins.DB.TOUR_PRICE.Add(tourprice);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new tour price with tour information id is " + tourinformation_id
                };
                DataProvider.Ins.DB.TOUR_RECORD.Add(tour_record);
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

        public static bool UpdateTourPrice(TourPriceModel price, int tourinformation_id, int user_id)
        {
            try
            {
                TOUR_PRICE tourprice = DataProvider.Ins.DB.TOUR_PRICE.Where(x => x.TOUR_INFORMATION_ID == tourinformation_id).FirstOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;
                if (tourprice.TOUR_PRICE_COST_HOTEL != price.PRICE_COST_HOTEL)
                {
                    changeToSave += string.Format("Total Hotel Price Change ({0} -> {1})   ", tourprice.TOUR_PRICE_COST_HOTEL, price.PRICE_COST_HOTEL);
                    tourprice.TOUR_PRICE_COST_HOTEL = price.PRICE_COST_HOTEL;
                    countChangeToSave++;
                }

                if (tourprice.TOUR_PRICE_COST_TRANSPORT != price.PRICE_COST_TRANSPORT)
                {
                    changeToSave += string.Format("Total Transport Price Change ({0} -> {1})   ", tourprice.TOUR_PRICE_COST_TRANSPORT, price.PRICE_COST_TRANSPORT);
                    tourprice.TOUR_PRICE_COST_TRANSPORT = price.PRICE_COST_TRANSPORT;
                    countChangeToSave++;
                }

                if (tourprice.TOUR_PRICE_COST_SERVICE != price.PRICE_COST_SERVICE)
                {
                    changeToSave += string.Format("Total Service Price Change ({0} -> {1})   ", tourprice.TOUR_PRICE_COST_SERVICE, price.PRICE_COST_SERVICE);
                    tourprice.TOUR_PRICE_COST_SERVICE = price.PRICE_COST_SERVICE;
                    countChangeToSave++;
                }

                if (tourprice.TOUR_PRICE_COST_TOTAL != price.PRICE_COST_TOTAL)
                {
                    changeToSave += string.Format("Total Price Change ({0} -> {1})   ", tourprice.TOUR_PRICE_COST_TOTAL, price.PRICE_COST_TOTAL);
                    tourprice.TOUR_PRICE_COST_TOTAL = price.PRICE_COST_TOTAL;
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

        private static ObservableCollection<ComboBoxModel> LoadTransportComboBox(int amount_max)
        {
            ObservableCollection<ComboBoxModel> CB_TransportAmount = new ObservableCollection<ComboBoxModel>();
            if (amount_max == 0)
            {
                return CB_TransportAmount;
            }

            for (int i = 1; i <= amount_max; i++)
            {
                ComboBoxModel comboBox = new ComboBoxModel(i.ToString(), false);
                CB_TransportAmount.Add(comboBox);
            }

            return CB_TransportAmount;
        }
    }
}
