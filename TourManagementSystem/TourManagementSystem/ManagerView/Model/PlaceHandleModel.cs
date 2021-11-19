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
    public class PlaceHandleModel : BaseViewModel
    {
        public static ObservableCollection<PlaceModel> GetPlaceList()
        {
            ObservableCollection<PlaceModel> PlaceList = new ObservableCollection<PlaceModel>();

            var placeList = from place in DataProvider.Ins.DB.PLACE
                            select place;

            foreach (var item in placeList)
            {
                PlaceModel place = new PlaceModel()
                {
                    PLACE_ID = item.PLACE_ID,
                    PLACE_LOCATION = item.TOUR_LOCATION.Count,
                    PLACE_NAME = item.PLACE_NAME,
                    PLACE_NATION = item.PLACE_NATION
                };

                PlaceList.Add(place);
            }

            return PlaceList;
        }

        public static PlaceModel GetPlace(int place_id)
        {
            var place = DataProvider.Ins.DB.PLACE.Where(x => x.PLACE_ID == place_id).FirstOrDefault();

            return new PlaceModel()
            {
                PLACE_ID = place_id,
                PLACE_LOCATION = place.TOUR_LOCATION.Count,
                PLACE_NAME = place.PLACE_NAME,
                PLACE_NATION = place.PLACE_NATION
            };
        }

        public static bool InsertPlace(PlaceModel place, int user_id)
        {
            try
            {
                PLACE tour_place = new PLACE()
                {
                    PLACE_NAME = place.PLACE_NAME,
                    PLACE_NATION = place.PLACE_NATION
                };
                DataProvider.Ins.DB.PLACE.Add(tour_place);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new place with Name: " + tour_place.PLACE_NAME
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

        public static bool UpdatePlace(PlaceModel place, int user_id)
        {
            try
            {
                PLACE tour_place = DataProvider.Ins.DB.PLACE.Where(x => x.PLACE_ID == place.PLACE_ID).SingleOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;
                if (tour_place.PLACE_NAME != place.PLACE_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", tour_place.PLACE_NAME, place.PLACE_NAME);
                    tour_place.PLACE_NAME = place.PLACE_NAME;
                    countChangeToSave++;
                }
                if (tour_place.PLACE_NATION != place.PLACE_NATION)
                {
                    changeToSave += string.Format("Nation Change ({0} -> {1})   ", tour_place.PLACE_NATION, place.PLACE_NATION);
                    tour_place.PLACE_NATION = place.PLACE_NATION;
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

        public static ObservableCollection<LocationModel> GetLocationList(int place_id)
        {
            ObservableCollection<LocationModel> LocationList = new ObservableCollection<LocationModel>();

            var locationlist = from location in DataProvider.Ins.DB.TOUR_LOCATION
                               where location.PLACE_ID == place_id
                               select location;

            foreach (var item in locationlist)
            {
                LocationModel locationModel = new LocationModel
                {
                    LOCATION_ID = item.TOUR_LOCATION_ID,
                    LOCATION_NAME = item.TOUR_LOCATION_NAME,
                    LOCATION_ADDRESS = item.TOUR_LOCATION_ADDRESS,
                    LOCATION_DESCRIPTION = item.TOUR_LOCATION_DESCRIPTION,
                    PLACE_ID = item.PLACE_ID
                };

                LocationList.Add(locationModel);
            }

            return LocationList;
        }

        public static bool InsertLocation(LocationModel location, int user_id)
        {
            try
            {
                TOUR_LOCATION tour_location = new TOUR_LOCATION()
                {
                    TOUR_LOCATION_NAME = location.LOCATION_NAME,
                    TOUR_LOCATION_ADDRESS = location.LOCATION_ADDRESS,
                    TOUR_LOCATION_DESCRIPTION = location.LOCATION_DESCRIPTION,
                    PLACE_ID = location.PLACE_ID
                };
                DataProvider.Ins.DB.TOUR_LOCATION.Add(tour_location);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new location with Name: " + tour_location.TOUR_LOCATION_NAME
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

        public static bool UpdateLocation(LocationModel location, int user_id)
        {
            try
            {
                TOUR_LOCATION tour_location = DataProvider.Ins.DB.TOUR_LOCATION.Where(x => x.TOUR_LOCATION_ID == location.LOCATION_ID).SingleOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;
                if (tour_location.TOUR_LOCATION_NAME != location.LOCATION_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", tour_location.TOUR_LOCATION_NAME, location.LOCATION_NAME);
                    tour_location.TOUR_LOCATION_NAME = location.LOCATION_NAME;
                    countChangeToSave++;
                }
                if (tour_location.TOUR_LOCATION_ADDRESS != location.LOCATION_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", tour_location.TOUR_LOCATION_ADDRESS, location.LOCATION_ADDRESS);
                    tour_location.TOUR_LOCATION_ADDRESS = location.LOCATION_ADDRESS;
                    countChangeToSave++;
                }
                if (tour_location.TOUR_LOCATION_DESCRIPTION != location.LOCATION_DESCRIPTION)
                {
                    changeToSave += string.Format("Description Change ({0} -> {1})   ", tour_location.TOUR_LOCATION_DESCRIPTION, location.LOCATION_DESCRIPTION);
                    tour_location.TOUR_LOCATION_DESCRIPTION = location.LOCATION_DESCRIPTION;
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
