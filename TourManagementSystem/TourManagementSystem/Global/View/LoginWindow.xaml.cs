using System.Windows;
using System.Windows.Controls;

namespace TourManagementSystem.Global.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            passwordBox.Password = Properties.Settings.Default.Password;
        }
    }
}