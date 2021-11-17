using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourDetailHandleModel
    {
        public static ObservableCollection<TourHotelDetailModel> GetTourHotelDetailList()
        {
            return new ObservableCollection<TourHotelDetailModel>()
            {
                new TourHotelDetailModel(1, "Ba Ria - Ben Tre", "Nhom Phuot", new DateTime(2021,10,28), new DateTime(2021,11,1)),
                new TourHotelDetailModel(2, "Ben Tre - Tien Giang", "R&B", new DateTime(2021,11,3), new DateTime(2021,11,6)),
                new TourHotelDetailModel(3, "Da Nang - Hue", "Dai gia dinh", new DateTime(2021,11,11), new DateTime(2021,11,13)),
                new TourHotelDetailModel(4, "Ha Noi - Quang Ninh", "GVR5", new DateTime(2021,11,19), new DateTime(2021,11,21)),
                new TourHotelDetailModel(6, "Ba Ria - Da Lat", "BCTM", new DateTime(2021,11,22), new DateTime(2021,11,24)),
                new TourHotelDetailModel(7, "TPHCM - Dong Nai", "Under the hood", new DateTime(2021,12,1), new DateTime(2021,12,4))
            };
        }
    }
}
