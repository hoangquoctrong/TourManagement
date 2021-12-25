using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.Model
{
    public class TourImageModel : BaseViewModel
    {
        private byte[] _TOUR_IMAGE_BYTE_SOURCE;

        public byte[] TOUR_IMAGE_BYTE_SOURCE
        { get => _TOUR_IMAGE_BYTE_SOURCE; set { _TOUR_IMAGE_BYTE_SOURCE = value; OnPropertyChanged(); } }

        private int _TOUR_ID;

        public int TOUR_ID
        { get => _TOUR_ID; set { _TOUR_ID = value; OnPropertyChanged(); } }

        private int _IMAGE_ID;

        public int IMAGE_ID
        { get => _IMAGE_ID; set { _IMAGE_ID = value; OnPropertyChanged(); } }
    }
}