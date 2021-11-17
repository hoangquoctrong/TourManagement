using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class HotelHandleModel : BaseViewModel
    {
        public static ObservableCollection<HotelModel> GetHotelList()
        {
            ObservableCollection<HotelModel> HotelList = new ObservableCollection<HotelModel>();

            var hotelList = from hotel in DataProvider.Ins.DB.TOUR_HOTEL
                            join place in DataProvider.Ins.DB.PLACE on hotel.PLACE_ID equals place.PLACE_ID
                            select new
                            {
                                hotel,
                                place
                            };

            foreach (var item in hotelList)
            {
                HotelModel hotelModel = new HotelModel
                {
                    HOTEL_ID = item.hotel.TOUR_HOTEL_ID,
                    HOTEL_NAME = item.hotel.TOUR_HOTEL_NAME,
                    HOTEL_ADDRESS = item.hotel.TOUR_HOTEL_ADDRESS,
                    HOTEL_PHONE_NUMBER = item.hotel.TOUR_HOTEL_PHONE_NUMBER,
                    HOTEL_DESCRIPTION = item.hotel.TOUR_HOTEL_DESCRIPTION,
                    HOTEL_TYPE = item.hotel.TOUR_HOTEL_TYPE,
                    HOTEL_IS_RESTAURANT = item.hotel.TOUR_HOTEL_IS_RESTAURANT,
                    HOTEL_PRICE = (float)item.hotel.TOUR_HOTEL_PRICE,
                    HOTEL_EMAIL = item.hotel.TOUR_HOTEL_EMAIL,
                    HOTEL_IMAGE_BYTE_SOURCE = item.hotel.TOUR_HOTEL_IMAGE,
                    HOTEL_IS_DELETE = (bool)item.hotel.TOUR_HOTEL_DELETE,
                    PLACE_ID = item.hotel.PLACE_ID,
                    PLACE_NAME = item.place.PLACE_NAME
                };

                HotelList.Add(hotelModel);
            }

            return HotelList;
        }

        public static bool InsertHotel(HotelModel hotel, int user_id)
        {
            try
            {
                TOUR_HOTEL tour_hotel = new TOUR_HOTEL()
                {
                    TOUR_HOTEL_NAME = hotel.HOTEL_NAME,
                    TOUR_HOTEL_PHONE_NUMBER = hotel.HOTEL_PHONE_NUMBER,
                    TOUR_HOTEL_ADDRESS = hotel.HOTEL_ADDRESS,
                    TOUR_HOTEL_TYPE = hotel.HOTEL_TYPE,
                    TOUR_HOTEL_EMAIL = hotel.HOTEL_EMAIL,
                    TOUR_HOTEL_IS_RESTAURANT = hotel.HOTEL_IS_RESTAURANT,
                    TOUR_HOTEL_PRICE = hotel.HOTEL_PRICE,
                    TOUR_HOTEL_DESCRIPTION = string.IsNullOrEmpty(hotel.HOTEL_DESCRIPTION) ? "" : hotel.HOTEL_DESCRIPTION,
                    TOUR_HOTEL_IMAGE = hotel.HOTEL_IMAGE_BYTE_SOURCE,
                    TOUR_HOTEL_DELETE = false,
                    PLACE_ID = hotel.PLACE_ID
                };
                DataProvider.Ins.DB.TOUR_HOTEL.Add(tour_hotel);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new hotel with Name: " + tour_hotel.TOUR_HOTEL_NAME
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

        public static bool UpdateHotel(HotelModel hotel, int user_id)
        {
            try
            {
                TOUR_HOTEL tour_hotel = DataProvider.Ins.DB.TOUR_HOTEL.Where(x => x.TOUR_HOTEL_ID == hotel.HOTEL_ID).SingleOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;

                if (tour_hotel.TOUR_HOTEL_NAME != hotel.HOTEL_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_NAME, hotel.HOTEL_NAME);
                    tour_hotel.TOUR_HOTEL_NAME = hotel.HOTEL_NAME;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_ADDRESS != hotel.HOTEL_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_ADDRESS, hotel.HOTEL_ADDRESS);
                    tour_hotel.TOUR_HOTEL_ADDRESS = hotel.HOTEL_ADDRESS;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_PHONE_NUMBER != hotel.HOTEL_PHONE_NUMBER)
                {
                    changeToSave += string.Format("Phone Number Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_PHONE_NUMBER, hotel.HOTEL_PHONE_NUMBER);
                    tour_hotel.TOUR_HOTEL_PHONE_NUMBER = hotel.HOTEL_PHONE_NUMBER;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_TYPE != hotel.HOTEL_TYPE)
                {
                    changeToSave += string.Format("Type Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_TYPE, hotel.HOTEL_TYPE);
                    tour_hotel.TOUR_HOTEL_TYPE = hotel.HOTEL_TYPE;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_EMAIL != hotel.HOTEL_EMAIL)
                {
                    changeToSave += string.Format("Email Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_EMAIL, hotel.HOTEL_EMAIL);
                    tour_hotel.TOUR_HOTEL_EMAIL = hotel.HOTEL_EMAIL;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_DESCRIPTION != hotel.HOTEL_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_DESCRIPTION, hotel.HOTEL_DESCRIPTION);
                    tour_hotel.TOUR_HOTEL_DESCRIPTION = hotel.HOTEL_DESCRIPTION;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_IS_RESTAURANT != hotel.HOTEL_IS_RESTAURANT)
                {
                    changeToSave += string.Format("Hotel has restaurant Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_IS_RESTAURANT, hotel.HOTEL_IS_RESTAURANT);
                    tour_hotel.TOUR_HOTEL_IS_RESTAURANT = hotel.HOTEL_IS_RESTAURANT;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_PRICE != hotel.HOTEL_PRICE)
                {
                    changeToSave += string.Format("Price Change ({0} -> {1})   ", tour_hotel.TOUR_HOTEL_PRICE, hotel.HOTEL_PRICE);
                    tour_hotel.TOUR_HOTEL_PRICE = hotel.HOTEL_PRICE;
                    countChangeToSave++;
                }
                if (tour_hotel.TOUR_HOTEL_IMAGE != hotel.HOTEL_IMAGE_BYTE_SOURCE)
                {
                    changeToSave += string.Format("Image Change");
                    tour_hotel.TOUR_HOTEL_IMAGE = hotel.HOTEL_IMAGE_BYTE_SOURCE;
                    countChangeToSave++;
                }
                if (tour_hotel.PLACE_ID != hotel.PLACE_ID)
                {
                    changeToSave += string.Format("Place Change ({0} -> {1})    ", tour_hotel.PLACE.PLACE_NAME, hotel.PLACE_NAME);
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

        public static bool DeleteHotel(int hotel_id, int user_id)
        {
            try
            {
                TOUR_HOTEL tour_hotel = DataProvider.Ins.DB.TOUR_HOTEL.Where(x => x.TOUR_HOTEL_ID == hotel_id).SingleOrDefault();
                tour_hotel.TOUR_HOTEL_DELETE = true;

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Remove Hotel {0} with {1}", tour_hotel.TOUR_HOTEL_NAME, tour_hotel.TOUR_HOTEL_ID)
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
