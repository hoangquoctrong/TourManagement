using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourWeb.Models
{
    public class DetailModel
    {
        public int DetailId { get; set; }
        public List<TourInformation> tourInformation { get; set; }
        public List<RatingModel> ratingModels { get; set; }
        public RatingModel ratingModel { get; set; }
        public TourModel tourModel { get; set; }
        public RegisterModel registerModel { get; set; }

        public void getDetailModel(int id, DetailModel detailModel)
        {
            RatingModel.SaveTourStar(id);
            detailModel = new DetailModel();
            detailModel.DetailId = id;
            detailModel.tourInformation = new List<TourInformation>();
            detailModel.tourInformation = TourInformation.getTourInformation(id);
            detailModel.tourModel = new TourModel();
            detailModel.tourModel.getTour(id);
            detailModel.ratingModels = new List<RatingModel>();
            detailModel.ratingModels = RatingModel.getRating(id);
            detailModel.ratingModel = new RatingModel();
            detailModel.tourModel.ImagesGallery = new List<string>();
            detailModel.tourModel.getImageList(detailModel.tourModel);
        }
    }
}