using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TourManagementSystem.View
{
    /// <summary>
    /// Interaction logic for TourUC.xaml
    /// </summary>
    public partial class TourUC : UserControl
    {
        private ObservableCollection<TourModel> _ListTour;
        public ObservableCollection<TourModel> ListTour { get => _ListTour; set => _ListTour = value; }
        public TourUC()
        {
            InitializeComponent();
            ListTour = new ObservableCollection<TourModel>();
            ListTour.Add(new TourModel() { ID = 1,TourName = "ABCXYZ",Price = "500$/person",TripLength = "30 days",Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 2, TourName = "Nha Trang", Price = "700$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 3, TourName = "Vung Tau", Price = "800$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 4, TourName = "HCM", Price = "1100$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 5, TourName = "Ha Noi", Price = "300$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 6, TourName = "ABCXYZ", Price = "200$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 7, TourName = "ABCXYZ", Price = "400$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            ListTour.Add(new TourModel() { ID = 8, TourName = "ABCXYZ", Price = "700$/person", TripLength = "30 days", Host = "Hoang Quoc Trong" });
            dgTourList.ItemsSource = ListTour;
        }

        
    }
}
