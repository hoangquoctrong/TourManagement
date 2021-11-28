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

        public static string GetPlaceName(int place_id)
        {
            var place = DataProvider.Ins.DB.PLACE.Where(x => x.PLACE_ID == place_id).FirstOrDefault();
            return place.PLACE_NAME;
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

        public static bool InsertPlaceDetail(BindableCollection<PlaceModel> places, string tour_name, int user_id, bool IsFirst)
        {
            try
            {
                TOUR tour = DataProvider.Ins.DB.TOUR.Where(x => x.TOUR_NAME.Equals(tour_name)).FirstOrDefault();
                int tour_id = tour.TOUR_ID;
                int countPlace = 0;
                foreach (PlaceModel item in places)
                {
                    TOUR_PLACE_DETAILED place_detail = new TOUR_PLACE_DETAILED()
                    {
                        PLACE_ID = item.PLACE_ID,
                        TOUR_ID = tour_id,
                    };
                    countPlace++;
                    DataProvider.Ins.DB.TOUR_PLACE_DETAILED.Add(place_detail);
                }
                TOUR tourdb = DataProvider.Ins.DB.TOUR.Where(x => x.TOUR_ID == tour_id).FirstOrDefault();
                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("{0} {1} places in {2}", IsFirst ? "Add" : "Update", countPlace, tourdb.TOUR_NAME)
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

        public static ObservableCollection<PlaceDetailModel> GetPlaceDetail(int tour_id)
        {
            ObservableCollection<PlaceDetailModel> PlaceDetailList = new ObservableCollection<PlaceDetailModel>();
            var placeDetailList = from placedetail in DataProvider.Ins.DB.TOUR_PLACE_DETAILED
                                  where placedetail.TOUR_ID == tour_id
                                  select placedetail;

            foreach (var item in placeDetailList)
            {
                PlaceDetailModel placeDetailModel = new PlaceDetailModel()
                {
                    Place_Detail_ID = item.TOUR_PLACE_DETAILED_ID,
                    Place_ID = item.PLACE_ID,
                    Tour_ID = item.TOUR_ID
                };

                PlaceDetailList.Add(placeDetailModel);
            }

            return PlaceDetailList;
        }

        public static BindableCollection<PlaceModel> GetPlaceFromPlaceDetail(int tour_id)
        {
            BindableCollection<PlaceModel> PlaceList = new BindableCollection<PlaceModel>();

            ObservableCollection<PlaceDetailModel> placeDetailList = GetPlaceDetail(tour_id);
            if (placeDetailList.Count == 0)
            {
                return PlaceList;
            }

            ObservableCollection<PlaceModel> PlaceListAll = GetPlaceList();

            foreach (var item in placeDetailList)
            {
                PlaceModel place = PlaceListAll.Where(x => x.PLACE_ID == item.Place_ID).FirstOrDefault();

                PlaceList.Add(place);
            }

            return PlaceList;
        }

        public static bool DeletePlaceDetail(int tour_id)
        {
            try
            {
                var placedetails = DataProvider.Ins.DB.TOUR_PLACE_DETAILED.Where(x => x.TOUR_ID == tour_id);
                DataProvider.Ins.DB.TOUR_PLACE_DETAILED.RemoveRange(placedetails);
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

        public static ObservableCollection<TourLocationModel> GetTourLocationList(int location_id)
        {
            ObservableCollection<TourLocationModel> TourLocationList = new ObservableCollection<TourLocationModel>();

            var tourLocationList = from locationdetail in DataProvider.Ins.DB.TOUR_LOCATION_DETAILED
                                   where locationdetail.TOUR_LOCATION_ID == location_id
                                   select locationdetail;

            if (tourLocationList.Count() == 0)
            {
                return TourLocationList;
            }

            foreach (var item in tourLocationList)
            {
                TourLocationModel tourLocationModel = new TourLocationModel()
                {
                    LOCATION_ID = item.TOUR_LOCATION_ID,
                    TOUR_ID = item.TOUR_INFORMATION.TOUR_ID,
                    TOUR_NAME = item.TOUR_INFORMATION.TOUR.TOUR_NAME,
                    TOUR_DATE = (DateTime)item.TOUR_INFORMATION.TOUR_TIME.TOUR_TIME_DEPARTMENT_DATE,
                    TOUR_STRING_DATE = ((DateTime)item.TOUR_INFORMATION.TOUR_TIME.TOUR_TIME_DEPARTMENT_DATE).ToString("dd/MM/yyyy")
                };

                TourLocationList.Add(tourLocationModel);
            }
            return TourLocationList;
        }

        public static BindableCollection<LocationModel> GetLocationFromPlaceList(ObservableCollection<PlaceModel> places)
        {
            BindableCollection<LocationModel> LocationList = new BindableCollection<LocationModel>();

            foreach (var item in places)
            {
                var locationList = from location in DataProvider.Ins.DB.TOUR_LOCATION
                                   where location.PLACE_ID == item.PLACE_ID
                                   select location;

                foreach (var itemlocation in locationList)
                {
                    LocationModel locationModel = new LocationModel()
                    {
                        LOCATION_ID = itemlocation.TOUR_LOCATION_ID,
                        LOCATION_NAME = itemlocation.TOUR_LOCATION_NAME,
                        LOCATION_ADDRESS = itemlocation.TOUR_LOCATION_ADDRESS,
                        LOCATION_DESCRIPTION = itemlocation.TOUR_LOCATION_DESCRIPTION,
                        PLACE_ID = itemlocation.PLACE_ID,
                        PLACE_NAME = itemlocation.PLACE.PLACE_NAME
                    };

                    LocationList.Add(locationModel);
                }
            }

            return LocationList;
        }

        public static bool InsertLocationDetail(BindableCollection<LocationModel> locations, int tour_informationID, int user_id, bool IsFirst)
        {
            try
            {
                int countLocation = 0;
                foreach (LocationModel item in locations)
                {
                    TOUR_LOCATION_DETAILED location_detail = new TOUR_LOCATION_DETAILED()
                    {
                        TOUR_INFORMATION_ID = tour_informationID,
                        TOUR_LOCATION_ID = item.LOCATION_ID
                    };
                    countLocation++;
                    DataProvider.Ins.DB.TOUR_LOCATION_DETAILED.Add(location_detail);
                }
                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("{0} {1} locations in tour information with id is {2}", IsFirst ? "Add" : "Update", countLocation, tour_informationID)
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

        public static ObservableCollection<LocationDetailModel> GetLocationDetail(int tour_information_id)
        {
            ObservableCollection<LocationDetailModel> LocationDetailList = new ObservableCollection<LocationDetailModel>();
            var locationDetailList = from locationdetail in DataProvider.Ins.DB.TOUR_LOCATION_DETAILED
                                     where locationdetail.TOUR_INFORMATION_ID == tour_information_id
                                     select locationdetail;

            foreach (var item in locationDetailList)
            {
                LocationDetailModel locationDetailModel = new LocationDetailModel()
                {
                    Location_Detail_ID = item.TOUR_LOCATION_DETAILED_ID,
                    Location_ID = item.TOUR_LOCATION_ID,
                    Tour_Information_ID = item.TOUR_INFORMATION_ID
                };

                LocationDetailList.Add(locationDetailModel);
            }

            return LocationDetailList;
        }

        public static BindableCollection<LocationModel> GetLocationFromLocationDetail(int tour_information_id, ObservableCollection<PlaceModel> places)
        {
            BindableCollection<LocationModel> LocationList = new BindableCollection<LocationModel>();

            ObservableCollection<LocationDetailModel> locationDetailList = GetLocationDetail(tour_information_id);
            if (locationDetailList.Count == 0)
            {
                return LocationList;
            }

            foreach (var itemplace in places)
            {
                ObservableCollection<LocationModel> LocationListAll = GetLocationList(itemplace.PLACE_ID);

                foreach (var item in locationDetailList)
                {
                    LocationModel location = LocationListAll.Where(x => x.LOCATION_ID == item.Location_ID).FirstOrDefault();

                    LocationList.Add(location);
                }
            }

            return LocationList;
        }

        public static bool DeleteLocationDetail(int tour_information_id)
        {
            try
            {
                var locationdetails = DataProvider.Ins.DB.TOUR_LOCATION_DETAILED.Where(x => x.TOUR_INFORMATION_ID == tour_information_id);
                DataProvider.Ins.DB.TOUR_LOCATION_DETAILED.RemoveRange(locationdetails);
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
