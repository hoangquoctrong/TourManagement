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
using TourManagementSystem.ManagerView.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Global.ViewModel
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged("Username"); } }

        private string _UserEmail;
        public string UserEmail { get => _UserEmail; set { _UserEmail = value; OnPropertyChanged("Username"); } }

        private int _NumberConfirm;
        public int NumberConfirm { get => _NumberConfirm; set { _NumberConfirm = value; OnPropertyChanged(); } }

        private int _ValidateNumber;
        public int ValidateNumber { get => _ValidateNumber; set { _ValidateNumber = value; OnPropertyChanged(); } }
        public ForgetPasswordViewModel()
        {
            Username = Properties.Settings.Default.Username;
            Random random = new Random();
            ValidateNumber = random.Next(100000, 999999);
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

        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand<Window>(null, p =>
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        p.Close();
                        loginWindow.ShowDialog();
                    });
                }
                return _LoginCommand;
            }
        }

        private ICommand _SendEmailCommand;
        public ICommand SendEmailCommand
        {
            get
            {
                if (_SendEmailCommand == null)
                {
                    _SendEmailCommand = new RelayCommand<object>(null, p =>
                    {
                        if (ForgetPasswordHandleModel.IsSendEmail(UserEmail, ValidateNumber))
                        {
                            MessageBox.Show("Send email successfully!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Send email failed!", "Notify", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                return _SendEmailCommand;
            }
        }

        private ICommand _ConfirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (_ConfirmCommand == null)
                {
                    _ConfirmCommand = new RelayCommand<Window>(p => IsExcuteConfirmCommand(), p => ExcuteConfirmCommand(p));
                }
                return _ConfirmCommand;
            }
        }

        private void ExcuteConfirmCommand(Window p)
        {
            int User_ID;
            int loginWindow = ForgetPasswordHandleModel.ConfirmForgetPassword(Username, out User_ID);
            if (loginWindow == 1)
            {
                ForgetPasswordHandleModel.SaveAccount(Username, User_ID);
                ManagerWindow window = new ManagerWindow();
                p.Close();
                window.ShowDialog();
            }
            else if (loginWindow == -1)
            {
                ForgetPasswordHandleModel.SaveAccount(Username, User_ID);
                EmployeeWindow window = new EmployeeWindow();
                p.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }

        private bool IsExcuteConfirmCommand()
        {
            return !string.IsNullOrEmpty(UserEmail) &&
                !string.IsNullOrEmpty(Username) &&
                NumberConfirm == ValidateNumber;
        }
    }
}
