using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TourWeb.Database;

namespace TourWeb.Models
{
    public class RatingModel
    {
        public int RatingID { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Your group ID")]
        [Range(1, Int32.MaxValue,ErrorMessage ="Must be greater than 1")]
        public int GroupID { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Your Name")]
        public string TravellerName { get; set; }
        public int TravellerID { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Your phone number")]
        [RegularExpression(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Not a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^.{1,}$", ErrorMessage = "Minimum 1 characters required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(1, 5)]
        public int Rating { get; set; }

        public static List<RatingModel> getRating(int tour_id)
        {
            List<RatingModel> ratingModels = new List<RatingModel>();
            var ratingList = from x in DataProvider.Ins.DB.TRAVELLER_DETAIL
                             where x.TRAVEL_GROUP.TOUR_INFORMATION.TOUR_ID == tour_id && x.TRAVELLER_DETAIL_STAR != 0
                             select x;
            foreach (var item in ratingList)
            {
                RatingModel ratingModel = new RatingModel()
                {
                    RatingID = item.TRAVELLER_DETAIL_ID,
                    Rating = (int)item.TRAVELLER_DETAIL_STAR,
                    Comment = item.TRAVELLER_DETAIL_COMMENT,
                    TravellerName = item.TRAVELLER.TRAVELLER_NAME,

                };

                ratingModels.Add(ratingModel);
            }
            return ratingModels;
        }

        public static void SaveTourStar(int tour_id)
        {
            var tourinformationlist = from travelgroup in DataProvider.Ins.DB.TRAVEL_GROUP
                                      join tgdetail in DataProvider.Ins.DB.TRAVELLER_DETAIL on travelgroup.TRAVEL_GROUP_ID equals tgdetail.TRAVEL_GROUP_ID
                                      where travelgroup.TOUR_INFORMATION.TOUR_ID == tour_id
                                      select new
                                      {
                                          tgdetail.TRAVELLER_DETAIL_STAR
                                      };

            double tour_star = 0;
            int total = 0;
            int count = 0;
            foreach (var item in tourinformationlist)
            {
                if (item.TRAVELLER_DETAIL_STAR != 0)
                {
                    int star = (int)item.TRAVELLER_DETAIL_STAR;
                    total += star;
                    count++;
                }
            }
            tour_star = total * 1.0 / count;
            tour_star = Math.Round(tour_star, 2);
            TOUR tour = DataProvider.Ins.DB.TOURs.Where(x => x.TOUR_ID == tour_id).First();
            tour.TOUR_STAR = tour_star;
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}