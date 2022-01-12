using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class TourModel
    {
        public int TourID { get; set; }
        public String TourName { get; set; }
        public String Tour_Character { get; set; }
        public double Tour_Star { get; set; }
        public string imagesData { get; set; }
        public List<string> ImagesGallery { get; set; }
        public int RatingNumber { get; set; }




        public void getImageList(TourModel tourModel)
        {
            var imageList = from images in DataProvider.Ins.DB.TOUR_IMAGE
                            where images.TOUR_ID == tourModel.TourID
                            select images;
            foreach (var item in imageList)
            {
                byte[] bytes = item.TOUR_IMAGE_BYTE;
                string imageBase64Data = Convert.ToBase64String(bytes);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                tourModel.ImagesGallery.Add(imageDataURL);
            }
        }

        public TourModel getTour(int tour_id)
        {
            var tourdb = DataProvider.Ins.DB.TOURs.Where(x => x.TOUR_ID == tour_id).FirstOrDefault();
            byte[] bytes = tourdb.TOUR_MAIN_IMAGE;
            string imageBase64Data = Convert.ToBase64String(bytes);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

            var informationList = from information in DataProvider.Ins.DB.TOUR_INFORMATION
                                  where information.TOUR_ID == tour_id && information.TOUR_TIME.FirstOrDefault().TOUR_TIME_DEPARTMENT_DATE > DateTime.Now
                                  select information;
            return new TourModel()
            {
                TourID = tour_id,
                Tour_Character = tourdb.TOUR_CHARACTERISTIS,
                TourName = tourdb.TOUR_NAME,
                Tour_Star = (double)tourdb.TOUR_STAR,
                imagesData = imageDataURL,
            };
        }

        public static List<TourModel> getTourList()
        {
            List<TourModel> tourModels = new List<TourModel>();
            tourModels.Clear();
            var tourlist = from tour in DataProvider.Ins.DB.TOURs
                           where tour.TOUR_IS_EXIST == "No"
                           select tour;
            foreach (var item in tourlist)
            {
                byte[] bytes = item.TOUR_MAIN_IMAGE;
                string imageBase64Data = Convert.ToBase64String(bytes);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                TourModel tourModel = new TourModel()
                {
                    TourID = item.TOUR_ID,
                    Tour_Character = item.TOUR_CHARACTERISTIS,
                    TourName = item.TOUR_NAME,
                    Tour_Star = (double)item.TOUR_STAR,
                    imagesData = imageDataURL
                };

                tourModels.Add(tourModel);
            }
            return tourModels;
        }
    }
}