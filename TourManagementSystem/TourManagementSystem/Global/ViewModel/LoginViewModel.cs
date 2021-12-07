using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManagementSystem.EmployeeView.View;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ManagerView.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Global.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged("Username"); } }

        private string _UserPassword;
        public string UserPassword { get => _UserPassword; set { _UserPassword = value; OnPropertyChanged(); } }

        public LoginViewModel()
        {
            Username = Properties.Settings.Default.Username;
            UserPassword = Properties.Settings.Default.Password;
        }
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand<Window>(p => IsExcuteLoginCommand(), p => ExcuteLoginCommand(p));
                }
                return _LoginCommand;
            }
        }
        private bool IsExcuteLoginCommand()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(UserPassword);
        }
        private void ExcuteLoginCommand(Window p)
        {
            int User_ID;
            int loginWindow = LoginHandleModel.IsLoginAccount(Username, UserPassword, out User_ID);
            if (StaffHandleModel.IsStaffDelete(User_ID))
            {
                MessageBox.Show("Account have been deleted", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (loginWindow == 1)
            {
                LoginHandleModel.SaveAccount(Username, UserPassword, User_ID);
                ManagerWindow window = new ManagerWindow();
                p.Close();
                window.ShowDialog();
            }
            else if (loginWindow == -1)
            {
                LoginHandleModel.SaveAccount(Username, UserPassword, User_ID);
                EmployeeWindow window = new EmployeeWindow();
                p.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username or Password wrong!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private ICommand _PasswordChangedCommand;
        public ICommand PasswordChangedCommand
        {
            get
            {
                if (_PasswordChangedCommand == null)
                {
                    _PasswordChangedCommand = new RelayCommand<PasswordBox>(null, p =>
                    {
                        UserPassword = p.Password;
                    });
                }
                return _PasswordChangedCommand;
            }
        }

        private ICommand _CloseCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new RelayCommand<Window>(null, p => p.Close());
                }
                return _CloseCommand;
            }
        }

        private ICommand _ForgetPasswordCommand;
        public ICommand ForgetPasswordCommand
        {
            get
            {
                if (_ForgetPasswordCommand == null)
                {
                    _ForgetPasswordCommand = new RelayCommand<Window>(null, p =>
                    {
                        ForgetPasswordWindow forgetPasswordWindow = new ForgetPasswordWindow();
                        p.Close();
                        forgetPasswordWindow.ShowDialog();
                    });
                }
                return _ForgetPasswordCommand;
            }
        }
    }
}
