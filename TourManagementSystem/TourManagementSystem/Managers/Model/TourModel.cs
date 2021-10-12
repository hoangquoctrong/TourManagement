using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourManagementSystem.Managers.Model
{
    public class TourModel
    {
        private int _ID;
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _TourName;
        public string TourName
        {
            get { return _TourName; }
            set { _TourName = value; }
        }
        private byte[] _IMAGETOUR;
        public byte[] IMAGETOUR { get => _IMAGETOUR; set => _IMAGETOUR = value; }

        private BitmapImage _IMAGE_SOURCE;
        public BitmapImage IMAGE_SOURCE { get => _IMAGE_SOURCE; set { _IMAGE_SOURCE = value; } }

        public string Host { get => _Host; set => _Host = value; }
        public string TripLength { get => _TripLength; set => _TripLength = value; }
        public string Price { get => _Price; set => _Price = value; }

        private string _TripLength;
        private string _Host;
        private string _Price;


    }
}
