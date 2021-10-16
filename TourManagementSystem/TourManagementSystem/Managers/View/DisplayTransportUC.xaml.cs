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
    /// Interaction logic for DisplayTransportUC.xaml
    /// </summary>
    public partial class DisplayTransportUC : UserControl
    {
        public DisplayTransportUC(TransportModel transportModel, int user_id)
        {
            InitializeComponent();
            DataContext = new TransportViewModel(transportModel, user_id);
        }
    }
}
