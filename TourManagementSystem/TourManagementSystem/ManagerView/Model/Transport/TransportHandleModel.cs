using Caliburn.Micro;
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
    public static class TransportHandleModel
    {
        public static ObservableCollection<TransportModel> GetTransportList()
        {
            ObservableCollection<TransportModel> TransportList = new ObservableCollection<TransportModel>();

            var transportList = from trans in DataProvider.Ins.DB.TOUR_TRANSPORT
                                select trans;

            foreach (var item in transportList)
            {
                TransportModel transportModel = new TransportModel
                {
                    TRANSPORT_ID = item.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = item.TOUR_TRANSPORT_NAME,
                    TRANSPORT_AMOUNT_MAX = (int)item.TOUR_TRANSPORT_AMOUNT_MAX,
                    TRANSPORT_COMPANY = item.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = item.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = item.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)item.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_PRICE = (double)item.TOUR_TRANSPORT_PRICE,
                    TRANSPORT_TYPETRANS = item.TOUR_TRANSPORT_TYPETRANS,
                    TRANSPORT_IS_DELETE = (bool)item.TOUR_TRANSPORT_DELETE
                };

                TransportList.Add(transportModel);
            }

            return TransportList;
        }

        public static ObservableCollection<TransportModel> GetTransportListWithoutDelete()
        {
            ObservableCollection<TransportModel> TransportList = new ObservableCollection<TransportModel>();

            var transportList = from trans in DataProvider.Ins.DB.TOUR_TRANSPORT
                                where trans.TOUR_TRANSPORT_DELETE == false
                                select trans;

            foreach (var item in transportList)
            {
                TransportModel transportModel = new TransportModel
                {
                    TRANSPORT_ID = item.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = item.TOUR_TRANSPORT_NAME,
                    TRANSPORT_AMOUNT_MAX = (int)item.TOUR_TRANSPORT_AMOUNT_MAX,
                    TRANSPORT_COMPANY = item.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = item.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = item.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)item.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_PRICE = (double)item.TOUR_TRANSPORT_PRICE,
                    TRANSPORT_TYPETRANS = item.TOUR_TRANSPORT_TYPETRANS,
                    TRANSPORT_IS_DELETE = (bool)item.TOUR_TRANSPORT_DELETE
                };

                TransportList.Add(transportModel);
            }

            return TransportList;
        }

        public static bool InsertTransport(TransportModel transport, int user_id)
        {
            try
            {
                TOUR_TRANSPORT tour_transport = new TOUR_TRANSPORT()
                {
                    TOUR_TRANSPORT_NAME = transport.TRANSPORT_NAME,
                    TOUR_TRANSPORT_COMPANY = transport.TRANSPORT_COMPANY,
                    TOUR_TRANSPORT_AMOUNT_MAX = transport.TRANSPORT_AMOUNT_MAX,
                    TOUR_TRANSPORT_TYPE = transport.TRANSPORT_TYPE,
                    TOUR_TRANSPORT_DESCRIPTION = string.IsNullOrEmpty(transport.TRANSPORT_DESCRIPTION) ? "" : transport.TRANSPORT_DESCRIPTION,
                    TOUR_TRANSPORT_START_DATE = DateTime.Now,
                    TOUR_TRANSPORT_PRICE = transport.TRANSPORT_PRICE,
                    TOUR_TRANSPORT_TYPETRANS = transport.TRANSPORT_TYPETRANS,
                    TOUR_TRANSPORT_DELETE = transport.TRANSPORT_IS_DELETE
                };
                DataProvider.Ins.DB.TOUR_TRANSPORT.Add(tour_transport);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new transport with Name: " + tour_transport.TOUR_TRANSPORT_NAME
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

        public static bool UpdateTransport(TransportModel transport, int user_id)
        {
            try
            {
                TOUR_TRANSPORT tour_transport = DataProvider.Ins.DB.TOUR_TRANSPORT.Where(x => x.TOUR_TRANSPORT_ID == transport.TRANSPORT_ID).FirstOrDefault();

                string changeToSave = string.Format("Update Transport with id {0}: ", transport.TRANSPORT_ID);
                int countChangeToSave = 0;
                DateTime Transport_Start_Date = (DateTime)tour_transport.TOUR_TRANSPORT_START_DATE;
                string Transport_String_Date = Transport_Start_Date.ToString("dd/MM/yyyy");
                if (tour_transport.TOUR_TRANSPORT_NAME != transport.TRANSPORT_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_NAME, transport.TRANSPORT_NAME);
                    tour_transport.TOUR_TRANSPORT_NAME = transport.TRANSPORT_NAME;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_AMOUNT_MAX != transport.TRANSPORT_AMOUNT_MAX)
                {
                    changeToSave += string.Format("License Plate Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_AMOUNT_MAX, transport.TRANSPORT_AMOUNT_MAX);
                    tour_transport.TOUR_TRANSPORT_AMOUNT_MAX = transport.TRANSPORT_AMOUNT_MAX;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_COMPANY != transport.TRANSPORT_COMPANY)
                {
                    changeToSave += string.Format("Company Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_COMPANY, transport.TRANSPORT_COMPANY);
                    tour_transport.TOUR_TRANSPORT_COMPANY = transport.TRANSPORT_COMPANY;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_TYPE != transport.TRANSPORT_TYPE)
                {
                    changeToSave += string.Format("Type Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_TYPE, transport.TRANSPORT_TYPE);
                    tour_transport.TOUR_TRANSPORT_TYPE = transport.TRANSPORT_TYPE;
                    countChangeToSave++;
                }
                if (Transport_String_Date != transport.TRANSPORT_STRING_DATE)
                {
                    changeToSave += string.Format("Start Date Change ({0} -> {1})   ", Transport_String_Date, transport.TRANSPORT_STRING_DATE);
                    tour_transport.TOUR_TRANSPORT_START_DATE = transport.TRANSPORT_DATE;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_DESCRIPTION != transport.TRANSPORT_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_DESCRIPTION, transport.TRANSPORT_DESCRIPTION);
                    tour_transport.TOUR_TRANSPORT_DESCRIPTION = transport.TRANSPORT_DESCRIPTION;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_TYPETRANS != transport.TRANSPORT_TYPETRANS)
                {
                    changeToSave += string.Format("Transport Type Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_TYPETRANS, transport.TRANSPORT_TYPETRANS);
                    tour_transport.TOUR_TRANSPORT_TYPETRANS = transport.TRANSPORT_TYPETRANS;
                    countChangeToSave++;
                }
                if (tour_transport.TOUR_TRANSPORT_PRICE != transport.TRANSPORT_PRICE)
                {
                    changeToSave += string.Format("Price Change ({0} -> {1})   ", tour_transport.TOUR_TRANSPORT_PRICE, transport.TRANSPORT_PRICE);
                    tour_transport.TOUR_TRANSPORT_PRICE = transport.TRANSPORT_PRICE;
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

        public static bool DeleteTransport(int transport_id, int user_id)
        {
            try
            {
                TOUR_TRANSPORT tour_transport = DataProvider.Ins.DB.TOUR_TRANSPORT.Where(x => x.TOUR_TRANSPORT_ID == transport_id).SingleOrDefault();
                tour_transport.TOUR_TRANSPORT_DELETE = true;

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Remove Transport {0} with {1}", tour_transport.TOUR_TRANSPORT_NAME, tour_transport.TOUR_TRANSPORT_ID)
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

        public static bool InsertTransportDetail(BindableCollection<TransportModel> transports, int tour_informationID, int user_id, bool IsFirst)
        {
            try
            {
                int countTransport = 0;
                foreach (TransportModel item in transports)
                {
                    TOUR_TRANSPORT_DETAIL transport_detail = new TOUR_TRANSPORT_DETAIL()
                    {
                        TOUR_TRANSPORT_ID = item.TRANSPORT_ID,
                        TOUR_INFORMATION_ID = tour_informationID,
                        TOUR_TRANSPORT_DETAIL_AMOUNT = item.TRANSPORT_AMOUNT
                    };
                    countTransport++;
                    DataProvider.Ins.DB.TOUR_TRANSPORT_DETAIL.Add(transport_detail);
                }
                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("{0} {1} transports in tour information with id is {2}", IsFirst ? "Add" : "Update", countTransport, tour_informationID)
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

        public static bool DeleteTransportDetail(int tour_information_id)
        {
            try
            {
                var transport = DataProvider.Ins.DB.TOUR_TRANSPORT_DETAIL.Where(x => x.TOUR_INFORMATION_ID == tour_information_id);
                DataProvider.Ins.DB.TOUR_TRANSPORT_DETAIL.RemoveRange(transport);
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

        public static BindableCollection<TransportModel> GetTransportFromTourInformation(int tourinformation_id)
        {
            BindableCollection<TransportModel> TransportDetailList = new BindableCollection<TransportModel>();
            var transportDetailList = from transportdetail in DataProvider.Ins.DB.TOUR_TRANSPORT_DETAIL
                                      where transportdetail.TOUR_INFORMATION_ID == tourinformation_id
                                      select transportdetail;

            foreach (var item in transportDetailList)
            {
                TransportModel transport = new TransportModel()
                {
                    TRANSPORT_ID = item.TOUR_TRANSPORT.TOUR_TRANSPORT_ID,
                    TRANSPORT_NAME = item.TOUR_TRANSPORT.TOUR_TRANSPORT_NAME,
                    TRANSPORT_AMOUNT_MAX = (int)item.TOUR_TRANSPORT.TOUR_TRANSPORT_AMOUNT_MAX,
                    TRANSPORT_COMPANY = item.TOUR_TRANSPORT.TOUR_TRANSPORT_COMPANY,
                    TRANSPORT_DESCRIPTION = item.TOUR_TRANSPORT.TOUR_TRANSPORT_DESCRIPTION,
                    TRANSPORT_TYPE = item.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPE,
                    TRANSPORT_DATE = (DateTime)item.TOUR_TRANSPORT.TOUR_TRANSPORT_START_DATE,
                    TRANSPORT_PRICE = (double)item.TOUR_TRANSPORT.TOUR_TRANSPORT_PRICE,
                    TRANSPORT_TYPETRANS = item.TOUR_TRANSPORT.TOUR_TRANSPORT_TYPETRANS,
                    TRANSPORT_IS_DELETE = (bool)item.TOUR_TRANSPORT.TOUR_TRANSPORT_DELETE,
                    TRANSPORT_AMOUNT = (int)item.TOUR_TRANSPORT_DETAIL_AMOUNT
                };

                TransportDetailList.Add(transport);
            }

            return TransportDetailList;
        }

        public static double CalculateTotalTransportPrice(ObservableCollection<TransportModel> transports)
        {
            double total = 0;

            if (transports.Count == 0)
            {
                return total;
            }

            foreach (var item in transports)
            {
                if (item.TRANSPORT_TYPETRANS.Contains("Road"))
                {
                    total += item.TRANSPORT_PRICE * item.TRANSPORT_AMOUNT;
                }
                else
                {
                    total += item.TRANSPORT_PRICE;
                }

            }

            return total;
        }

        public static ObservableCollection<TourTransportDetailModel> GetTransportDetailWithTravelGroup(int transport_id)
        {
            ObservableCollection<TourTransportDetailModel> TransportList = new ObservableCollection<TourTransportDetailModel>();

            var transportList = from transport in DataProvider.Ins.DB.TOUR_TRANSPORT_DETAIL
                                join travelgroup in DataProvider.Ins.DB.TRAVEL_GROUP on transport.TOUR_INFORMATION_ID equals travelgroup.TOUR_INFORMATION_ID
                                where transport.TOUR_TRANSPORT_ID == transport_id
                                select new
                                {
                                    transport,
                                    travelgroup
                                };

            foreach (var item in transportList)
            {
                TourTransportDetailModel transportDetailModel = new TourTransportDetailModel(item.transport.TOUR_TRANSPORT_DETAIL_ID,
                                                                            item.transport.TOUR_INFORMATION.TOUR.TOUR_NAME,
                                                                            item.travelgroup.TRAVEL_GROUP_NAME,
                                                                            (int)item.transport.TOUR_TRANSPORT_DETAIL_AMOUNT,
                                                                            (DateTime)item.transport.TOUR_INFORMATION.TOUR_TIME.FirstOrDefault().TOUR_TIME_DEPARTMENT_DATE,
                                                                            (DateTime)item.transport.TOUR_INFORMATION.TOUR_TIME.FirstOrDefault().TOUR_TIME_END_DATE);
                TransportList.Add(transportDetailModel);
            }

            return TransportList;
        }
    }
}
