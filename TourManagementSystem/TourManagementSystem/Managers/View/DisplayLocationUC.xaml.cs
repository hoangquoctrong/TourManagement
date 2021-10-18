using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourManagementSystem.Managers.Model;
using TourManagementSystem.Managers.ViewModel;

namespace TourManagementSystem.Managers.View
{
    /// <summary>
    /// Interaction logic for DisplayLocationUC.xaml
    /// </summary>
    public partial class DisplayLocationUC : UserControl
    {
        public DisplayLocationUC(PlaceModel placeModel, LocationModel locationModel, int user_id)
        {
            InitializeComponent();
            DataContext = new PlaceViewModel(placeModel, locationModel, user_id);
        }
    }
}
