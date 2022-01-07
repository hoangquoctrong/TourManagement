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
    }
}