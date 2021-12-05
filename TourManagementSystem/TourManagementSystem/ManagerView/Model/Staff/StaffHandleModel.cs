using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using TourManagementSystem.Global;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class StaffHandleModel : BaseViewModel
    {
        public static ObservableCollection<StaffModel> GetStaffList(bool chooseAll)
        {
            ObservableCollection<StaffModel> StaffList = new ObservableCollection<StaffModel>();

            var staffList = chooseAll ?
                            from staff in DataProvider.Ins.DB.TOUR_STAFF
                            join staff_account in DataProvider.Ins.DB.TOUR_ACCOUNT on staff.TOUR_STAFF_ID equals staff_account.TOUR_STAFF_ID
                            join staff_delete in DataProvider.Ins.DB.TOUR_STAFF_DELETE on staff.TOUR_STAFF_ID equals staff_delete.TOUR_STAFF_ID
                            orderby staff.TOUR_STAFF_ID ascending
                            select new
                            {
                                staff,
                                USERNAME = staff_account.TOUR_ACCOUNT_NAME,
                                staff_delete
                            }
                            :
                            from staff in DataProvider.Ins.DB.TOUR_STAFF
                            join staff_account in DataProvider.Ins.DB.TOUR_ACCOUNT on staff.TOUR_STAFF_ID equals staff_account.TOUR_STAFF_ID
                            join staff_delete in DataProvider.Ins.DB.TOUR_STAFF_DELETE on staff.TOUR_STAFF_ID equals staff_delete.TOUR_STAFF_ID
                            where staff_delete.TOUR_STAFF_DELETE_ISDELETED == false
                            orderby staff.TOUR_STAFF_ID ascending
                            select new
                            {
                                staff,
                                USERNAME = staff_account.TOUR_ACCOUNT_NAME,
                                staff_delete
                            };

            foreach (var item in staffList)
            {
                StaffModel staffModel = new StaffModel
                {
                    STAFF_ID = item.staff.TOUR_STAFF_ID,
                    STAFF_NAME = item.staff.TOUR_STAFF_NAME,
                    STAFF_ROLE = item.staff.TOUR_STAFF_ROLE,
                    STAFF_BIRTH_DATE = (DateTime)item.staff.TOUR_STAFF_BIRTH_DATE,
                    STAFF_BIRTH_PLACE = item.staff.TOUR_STAFF_BIRTH_PLACE,
                    STAFF_GENDER = item.staff.TOUR_STAFF_GENDER,
                    STAFF_CITIZEN_CARD = item.staff.TOUR_STAFF_CITIZEN_IDENTITY,
                    STAFF_CITIZEN_CARD_DATE = (DateTime)item.staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE,
                    STAFF_CITIZEN_CARD_PLACE = item.staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE,
                    STAFF_ADDRESS = item.staff.TOUR_STAFF_ADDRESS,
                    STAFF_START_DATE = (DateTime)item.staff.TOUR_STAFF_START_DATE,
                    STAFF_PHONE_NUMBER = item.staff.TOUR_STAFF_PHONE_NUMBER,
                    STAFF_ACADEMIC_LEVEL = item.staff.TOUR_STAFF_ACADEMIC_LEVEL,
                    STAFF_EMAIL = item.staff.TOUR_STAFF_EMAIL,
                    STAFF_NOTE = item.staff.TOUR_STAFF_NOTE,
                    STAFF_IMAGE_BYTE_SOURCE = item.staff.TOUR_STAFF_IMAGE,
                    STAFF_IS_DELETE = (bool)item.staff_delete.TOUR_STAFF_DELETE_ISDELETED,
                    STAFF_DELETE_NOTE = item.staff_delete.TOUR_STAFF_DELETE_CONTENT,
                    STAFF_USERNAME = item.USERNAME,
                    STAFF_DELETE_DATE = (DateTime)((bool)item.staff_delete.TOUR_STAFF_DELETE_ISDELETED ? item.staff_delete.TOUR_STAFF_DELETE_DATE : DateTime.Now)
                };
                staffModel.STAFF_STRING_BIRTH_DATE = staffModel.STAFF_BIRTH_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_START_DATE = staffModel.STAFF_START_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_CITIZEN_CARD_DATE = staffModel.STAFF_CITIZEN_CARD_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_DELETE_STRING_DATE = staffModel.STAFF_DELETE_DATE.ToString("dd/MM/yyyy");

                StaffList.Add(staffModel);
            }

            return StaffList;
        }

        public static bool InsertStaff(StaffModel staff, int user_id)
        {
            try
            {
                TOUR_STAFF tour_staff = new TOUR_STAFF()
                {
                    TOUR_STAFF_NAME = staff.STAFF_NAME,
                    TOUR_STAFF_ROLE = staff.STAFF_ROLE,
                    TOUR_STAFF_CITIZEN_IDENTITY = staff.STAFF_CITIZEN_CARD,
                    TOUR_STAFF_CITIZEN_IDENTITY_DATE = staff.STAFF_CITIZEN_CARD_DATE,
                    TOUR_STAFF_CITIZEN_IDENTITY_PLACE = staff.STAFF_CITIZEN_CARD_PLACE,
                    TOUR_STAFF_BIRTH_DATE = staff.STAFF_BIRTH_DATE,
                    TOUR_STAFF_BIRTH_PLACE = staff.STAFF_BIRTH_PLACE,
                    TOUR_STAFF_GENDER = staff.STAFF_GENDER,
                    TOUR_STAFF_ADDRESS = staff.STAFF_ADDRESS,
                    TOUR_STAFF_ACADEMIC_LEVEL = staff.STAFF_ACADEMIC_LEVEL,
                    TOUR_STAFF_PHONE_NUMBER = staff.STAFF_PHONE_NUMBER,
                    TOUR_STAFF_EMAIL = staff.STAFF_EMAIL,
                    TOUR_STAFF_NOTE = string.IsNullOrEmpty(staff.STAFF_NOTE) ? "" : staff.STAFF_NOTE,
                    TOUR_STAFF_START_DATE = DateTime.Now,
                    TOUR_STAFF_IMAGE = staff.STAFF_IMAGE_BYTE_SOURCE
                };
                DataProvider.Ins.DB.TOUR_STAFF.Add(tour_staff);

                TOUR_ACCOUNT tour_account = new TOUR_ACCOUNT()
                {
                    TOUR_STAFF_ID = tour_staff.TOUR_STAFF_ID,
                    TOUR_ACCOUNT_NAME = staff.STAFF_USERNAME,
                    TOUR_ACCOUNT_PASSWORD = GlobalFunction.CreateMD5(GlobalFunction.Base64Encode(staff.STAFF_PASSWORD))
                };
                DataProvider.Ins.DB.TOUR_ACCOUNT.Add(tour_account);

                TOUR_STAFF_DELETE tour_staff_delete = new TOUR_STAFF_DELETE()
                {
                    TOUR_STAFF_ID = tour_staff.TOUR_STAFF_ID,
                    TOUR_STAFF_DELETE_ISDELETED = false
                };
                DataProvider.Ins.DB.TOUR_STAFF_DELETE.Add(tour_staff_delete);

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = "Add new staff with Name: " + staff.STAFF_NAME
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

        public static bool UpdateStaff(StaffModel staff, int user_id)
        {
            try
            {
                TOUR_STAFF tour_staff = DataProvider.Ins.DB.TOUR_STAFF.Where(x => x.TOUR_STAFF_ID == staff.STAFF_ID).SingleOrDefault();

                string changeToSave = "";
                int countChangeToSave = 0;

                DateTime staff_birthdate = (DateTime)tour_staff.TOUR_STAFF_BIRTH_DATE;
                string Staff_String_Birthday = staff_birthdate.ToString("dd/MM/yyyy");

                DateTime staff_IDdate = (DateTime)tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE;
                string Staff_String_ID_Card_Date = staff_IDdate.ToString("dd/MM/yyyy");

                if (staff.STAFF_NAME != tour_staff.TOUR_STAFF_NAME)
                {
                    changeToSave += string.Format("Name Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_NAME, staff.STAFF_NAME);
                    tour_staff.TOUR_STAFF_NAME = staff.STAFF_NAME;
                    countChangeToSave++;
                }
                if (staff.STAFF_ROLE != tour_staff.TOUR_STAFF_ROLE)
                {
                    changeToSave += string.Format("Role Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_ROLE, staff.STAFF_ROLE);
                    tour_staff.TOUR_STAFF_ROLE = staff.STAFF_ROLE;
                    countChangeToSave++;
                }
                if (staff.STAFF_CITIZEN_CARD != tour_staff.TOUR_STAFF_CITIZEN_IDENTITY)
                {
                    changeToSave += string.Format("ID-Card Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_CITIZEN_IDENTITY, staff.STAFF_CITIZEN_CARD);
                    tour_staff.TOUR_STAFF_CITIZEN_IDENTITY = staff.STAFF_CITIZEN_CARD;
                    countChangeToSave++;
                }
                if (staff.STAFF_STRING_CITIZEN_CARD_DATE != Staff_String_ID_Card_Date)
                {
                    changeToSave += string.Format("ID-Card Date Change ({0} -> {1})   ", Staff_String_ID_Card_Date, staff.STAFF_STRING_CITIZEN_CARD_DATE);
                    tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE = staff.STAFF_CITIZEN_CARD_DATE;
                    countChangeToSave++;
                }
                if (staff.STAFF_CITIZEN_CARD_PLACE != tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE)
                {
                    changeToSave += string.Format("ID-Card Place Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE, staff.STAFF_CITIZEN_CARD_PLACE);
                    tour_staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE = staff.STAFF_CITIZEN_CARD_PLACE;
                    countChangeToSave++;
                }
                if (staff.STAFF_STRING_BIRTH_DATE != Staff_String_Birthday)
                {
                    changeToSave += string.Format("Birthday Change ({0} -> {1})   ", Staff_String_Birthday, staff.STAFF_STRING_BIRTH_DATE);
                    tour_staff.TOUR_STAFF_BIRTH_DATE = staff.STAFF_BIRTH_DATE;
                    countChangeToSave++;
                }
                if (staff.STAFF_BIRTH_PLACE != tour_staff.TOUR_STAFF_BIRTH_PLACE)
                {
                    changeToSave += string.Format("Birth Place Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_BIRTH_PLACE, staff.STAFF_BIRTH_PLACE);
                    tour_staff.TOUR_STAFF_BIRTH_PLACE = staff.STAFF_BIRTH_PLACE;
                    countChangeToSave++;
                }
                if (staff.STAFF_GENDER != tour_staff.TOUR_STAFF_GENDER)
                {
                    changeToSave += string.Format("Gender Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_GENDER, staff.STAFF_GENDER);
                    tour_staff.TOUR_STAFF_GENDER = staff.STAFF_GENDER;
                    countChangeToSave++;
                }
                if (staff.STAFF_ADDRESS != tour_staff.TOUR_STAFF_ADDRESS)
                {
                    changeToSave += string.Format("Address Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_ADDRESS, staff.STAFF_ADDRESS);
                    tour_staff.TOUR_STAFF_ADDRESS = staff.STAFF_ADDRESS;
                    countChangeToSave++;
                }
                if (staff.STAFF_ACADEMIC_LEVEL != tour_staff.TOUR_STAFF_ACADEMIC_LEVEL)
                {
                    changeToSave += string.Format("Academic Level Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_ACADEMIC_LEVEL, staff.STAFF_ACADEMIC_LEVEL);
                    tour_staff.TOUR_STAFF_ACADEMIC_LEVEL = staff.STAFF_ACADEMIC_LEVEL;
                    countChangeToSave++;
                }
                if (staff.STAFF_EMAIL != tour_staff.TOUR_STAFF_EMAIL)
                {
                    changeToSave += string.Format("Email Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_EMAIL, staff.STAFF_EMAIL);
                    tour_staff.TOUR_STAFF_EMAIL = staff.STAFF_EMAIL;
                    countChangeToSave++;
                }
                if (staff.STAFF_PHONE_NUMBER != tour_staff.TOUR_STAFF_PHONE_NUMBER)
                {
                    changeToSave += string.Format("Phone Number Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_PHONE_NUMBER, staff.STAFF_PHONE_NUMBER);
                    tour_staff.TOUR_STAFF_PHONE_NUMBER = staff.STAFF_PHONE_NUMBER;
                    countChangeToSave++;
                }
                if (staff.STAFF_NOTE != tour_staff.TOUR_STAFF_NOTE)
                {
                    changeToSave += string.Format("Note Change ({0} -> {1})   ", tour_staff.TOUR_STAFF_NOTE, staff.STAFF_NOTE);
                    tour_staff.TOUR_STAFF_NOTE = staff.STAFF_NOTE;
                    countChangeToSave++;
                }
                if (staff.STAFF_IMAGE_BYTE_SOURCE != tour_staff.TOUR_STAFF_IMAGE)
                {
                    changeToSave += string.Format("Image Change");
                    tour_staff.TOUR_STAFF_IMAGE = staff.STAFF_IMAGE_BYTE_SOURCE;
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

        public static bool DeleteStaff(int staff_id, string staff_name, string staff_remove_note, int user_id)
        {
            try
            {
                TOUR_STAFF_DELETE tour_staff_delete = DataProvider.Ins.DB.TOUR_STAFF_DELETE.Where(x => x.TOUR_STAFF_ID == staff_id).SingleOrDefault();
                tour_staff_delete.TOUR_STAFF_DELETE_ISDELETED = true;
                tour_staff_delete.TOUR_STAFF_DELETE_DATE = DateTime.Now;
                tour_staff_delete.TOUR_STAFF_DELETE_CONTENT = staff_remove_note;

                TOUR_RECORD tour_record = new TOUR_RECORD()
                {
                    TOUR_STAFF_ID = user_id,
                    TOUR_RECORD_DATE = DateTime.Now,
                    TOUR_RECORD_CONTENT = string.Format("Remove Staff {0} with {1} because {2}", staff_name, staff_id, staff_remove_note)
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

        public static bool CheckAccountStaff(string user_name)
        {
            var user = DataProvider.Ins.DB.TOUR_ACCOUNT.Where(x => x.TOUR_ACCOUNT_NAME == user_name);
            return user?.Count() == 0;
        }

        public static bool ChangePassword(int staff_id, string password)
        {
            try
            {
                TOUR_ACCOUNT tour_account = DataProvider.Ins.DB.TOUR_ACCOUNT.Where(x => x.TOUR_STAFF_ID == staff_id).SingleOrDefault();
                tour_account.TOUR_ACCOUNT_PASSWORD = GlobalFunction.CreateMD5(GlobalFunction.Base64Encode(password));
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

        public static StaffModel GetStaffFromID(int staff_id)
        {
            var staffItem = (from staff in DataProvider.Ins.DB.TOUR_STAFF
                             join staff_account in DataProvider.Ins.DB.TOUR_ACCOUNT on staff.TOUR_STAFF_ID equals staff_account.TOUR_STAFF_ID
                             where staff.TOUR_STAFF_ID == staff_id
                             select new
                             {
                                 staff,
                                 staff_account
                             }).FirstOrDefault();

            return new StaffModel()
            {
                STAFF_ID = staffItem.staff.TOUR_STAFF_ID,
                STAFF_NAME = staffItem.staff.TOUR_STAFF_NAME,
                STAFF_ROLE = staffItem.staff.TOUR_STAFF_ROLE,
                STAFF_BIRTH_DATE = (DateTime)staffItem.staff.TOUR_STAFF_BIRTH_DATE,
                STAFF_STRING_BIRTH_DATE = ((DateTime)staffItem.staff.TOUR_STAFF_BIRTH_DATE).ToString("dd/MM/yyyy"),
                STAFF_BIRTH_PLACE = staffItem.staff.TOUR_STAFF_BIRTH_PLACE,
                STAFF_GENDER = staffItem.staff.TOUR_STAFF_GENDER,
                STAFF_CITIZEN_CARD = staffItem.staff.TOUR_STAFF_CITIZEN_IDENTITY,
                STAFF_CITIZEN_CARD_DATE = (DateTime)staffItem.staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE,
                STAFF_STRING_CITIZEN_CARD_DATE = ((DateTime)staffItem.staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE).ToString("dd/MM/yyyy"),
                STAFF_CITIZEN_CARD_PLACE = staffItem.staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE,
                STAFF_ADDRESS = staffItem.staff.TOUR_STAFF_ADDRESS,
                STAFF_START_DATE = (DateTime)staffItem.staff.TOUR_STAFF_START_DATE,
                STAFF_STRING_START_DATE = ((DateTime)staffItem.staff.TOUR_STAFF_START_DATE).ToString("dd/MM/yyyy"),
                STAFF_PHONE_NUMBER = staffItem.staff.TOUR_STAFF_PHONE_NUMBER,
                STAFF_ACADEMIC_LEVEL = staffItem.staff.TOUR_STAFF_ACADEMIC_LEVEL,
                STAFF_EMAIL = staffItem.staff.TOUR_STAFF_EMAIL,
                STAFF_NOTE = staffItem.staff.TOUR_STAFF_NOTE,
                STAFF_IMAGE_BYTE_SOURCE = staffItem.staff.TOUR_STAFF_IMAGE,
                STAFF_USERNAME = staffItem.staff_account.TOUR_ACCOUNT_NAME
            };
        }

        public static bool IsStaffDelete(int user_id)
        {
            var staffdelete = DataProvider.Ins.DB.TOUR_STAFF_DELETE.Where(x => x.TOUR_STAFF_ID == user_id).FirstOrDefault();

            return staffdelete.TOUR_STAFF_DELETE_ISDELETED == true;
        }

        public static ObservableCollection<StaffModel> GetStaffListForTraveller()
        {
            ObservableCollection<StaffModel> StaffList = new ObservableCollection<StaffModel>();

            var staffList =
                            from staff in DataProvider.Ins.DB.TOUR_STAFF
                            join staff_account in DataProvider.Ins.DB.TOUR_ACCOUNT on staff.TOUR_STAFF_ID equals staff_account.TOUR_STAFF_ID
                            join staff_delete in DataProvider.Ins.DB.TOUR_STAFF_DELETE on staff.TOUR_STAFF_ID equals staff_delete.TOUR_STAFF_ID
                            where staff_delete.TOUR_STAFF_DELETE_ISDELETED == false && staff.TOUR_STAFF_ROLE.Contains("Staff")
                            orderby staff.TOUR_STAFF_ID ascending
                            select new
                            {
                                staff,
                                USERNAME = staff_account.TOUR_ACCOUNT_NAME,
                                staff_delete
                            };

            foreach (var item in staffList)
            {
                StaffModel staffModel = new StaffModel
                {
                    STAFF_ID = item.staff.TOUR_STAFF_ID,
                    STAFF_NAME = item.staff.TOUR_STAFF_NAME,
                    STAFF_ROLE = item.staff.TOUR_STAFF_ROLE,
                    STAFF_BIRTH_DATE = (DateTime)item.staff.TOUR_STAFF_BIRTH_DATE,
                    STAFF_BIRTH_PLACE = item.staff.TOUR_STAFF_BIRTH_PLACE,
                    STAFF_GENDER = item.staff.TOUR_STAFF_GENDER,
                    STAFF_CITIZEN_CARD = item.staff.TOUR_STAFF_CITIZEN_IDENTITY,
                    STAFF_CITIZEN_CARD_DATE = (DateTime)item.staff.TOUR_STAFF_CITIZEN_IDENTITY_DATE,
                    STAFF_CITIZEN_CARD_PLACE = item.staff.TOUR_STAFF_CITIZEN_IDENTITY_PLACE,
                    STAFF_ADDRESS = item.staff.TOUR_STAFF_ADDRESS,
                    STAFF_START_DATE = (DateTime)item.staff.TOUR_STAFF_START_DATE,
                    STAFF_PHONE_NUMBER = item.staff.TOUR_STAFF_PHONE_NUMBER,
                    STAFF_ACADEMIC_LEVEL = item.staff.TOUR_STAFF_ACADEMIC_LEVEL,
                    STAFF_EMAIL = item.staff.TOUR_STAFF_EMAIL,
                    STAFF_NOTE = item.staff.TOUR_STAFF_NOTE,
                    STAFF_IMAGE_BYTE_SOURCE = item.staff.TOUR_STAFF_IMAGE,
                    STAFF_IS_DELETE = (bool)item.staff_delete.TOUR_STAFF_DELETE_ISDELETED,
                    STAFF_DELETE_NOTE = item.staff_delete.TOUR_STAFF_DELETE_CONTENT,
                    STAFF_USERNAME = item.USERNAME,
                    STAFF_DELETE_DATE = (DateTime)((bool)item.staff_delete.TOUR_STAFF_DELETE_ISDELETED ? item.staff_delete.TOUR_STAFF_DELETE_DATE : DateTime.Now)
                };
                staffModel.STAFF_STRING_BIRTH_DATE = staffModel.STAFF_BIRTH_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_START_DATE = staffModel.STAFF_START_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_STRING_CITIZEN_CARD_DATE = staffModel.STAFF_CITIZEN_CARD_DATE.ToString("dd/MM/yyyy");
                staffModel.STAFF_DELETE_STRING_DATE = staffModel.STAFF_DELETE_DATE.ToString("dd/MM/yyyy");

                StaffList.Add(staffModel);
            }

            return StaffList;
        }
    }
}
