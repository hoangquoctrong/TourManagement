using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourManagementSystem.ManagerView.Model
{
    public static class TourDetailHandleModel
    {
        public static ObservableCollection<TourMissionModel> GetTourMissionList()
        {
            return new ObservableCollection<TourMissionModel>()
            {
                new TourMissionModel(1,"Da Lat - Nha Trang", "Nhom Phuot 1", "Huong dan vien", "Huong dan vien", "1/11/2021", "3/11/2021"),
                new TourMissionModel(2,"Da Lat - Nha Trang", "Nhom Phuot 2", "Huong dan vien", "Huong dan vien", "2/11/2021", "5/11/2021"),
                new TourMissionModel(3,"Da Lat - Nha Trang", "Nhom Phuot 3", "Huong dan vien", "Huong dan vien", "3/11/2021", "6/11/2021"),
                new TourMissionModel(4,"Da Lat - Nha Trang", "Nhom Phuot 4", "Huong dan vien", "Huong dan vien", "4/11/2021", "7/11/2021"),
                new TourMissionModel(5,"Da Lat - Nha Trang", "Nhom Phuot 5", "Huong dan vien", "Huong dan vien", "5/11/2021", "8/11/2021"),
                new TourMissionModel(6,"Da Lat - Nha Trang", "Nhom Phuot 6", "Huong dan vien", "Huong dan vien", "6/11/2021", "9/11/2021"),
                new TourMissionModel(7,"Da Lat - Nha Trang", "Nhom Phuot 7", "Huong dan vien", "Huong dan vien", "7/11/2021", "10/11/2021"),
                new TourMissionModel(8,"Da Lat - Nha Trang", "Nhom Phuot 8", "Huong dan vien", "Huong dan vien", "8/11/2021", "11/11/2021"),
                new TourMissionModel(9,"Da Lat - Nha Trang", "Nhom Phuot 9", "Huong dan vien", "Huong dan vien", "9/11/2021", "12/11/2021"),
                new TourMissionModel(10,"Da Lat - Nha Trang", "Nhom Phuot 10", "Huong dan vien", "Huong dan vien", "10/11/2021", "13/11/2021")
            };
        }

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

        public static ObservableCollection<TourTransportDetailModel> GetTourTransportDetailList()
        {
            return new ObservableCollection<TourTransportDetailModel>()
            {
                new TourTransportDetailModel(1, "Ba Ria - Ben Tre", "Nhom Phuot", new DateTime(2021,10,28), new DateTime(2021,11,1)),
                new TourTransportDetailModel(2, "Ben Tre - Tien Giang", "R&B", new DateTime(2021,11,3), new DateTime(2021,11,6)),
                new TourTransportDetailModel(3, "Da Nang - Hue", "Dai gia dinh", new DateTime(2021,11,11), new DateTime(2021,11,13)),
                new TourTransportDetailModel(4, "Ha Noi - Quang Ninh", "GVR5", new DateTime(2021,11,19), new DateTime(2021,11,21)),
                new TourTransportDetailModel(6, "Ba Ria - Da Lat", "BCTM", new DateTime(2021,11,22), new DateTime(2021,11,24)),
                new TourTransportDetailModel(7, "TPHCM - Dong Nai", "Under the hood", new DateTime(2021,12,1), new DateTime(2021,12,4))
            };
        }
    }
}
