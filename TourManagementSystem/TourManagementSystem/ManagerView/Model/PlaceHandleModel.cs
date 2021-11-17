using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class PlaceHandleModel : BaseViewModel
    {
        public static ObservableCollection<PlaceModel> GetPlace()
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
    }
}
