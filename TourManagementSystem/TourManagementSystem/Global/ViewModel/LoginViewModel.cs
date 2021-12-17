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

        private bool _IsCheck;
        public bool IsCheck { get => _IsCheck; set { _IsCheck = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;
        public Visibility ProgressBarVisbility { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        public LoginViewModel()
        {
            Username = Properties.Settings.Default.Username;
            UserPassword = Properties.Settings.Default.Password;
            IsCheck = Properties.Settings.Default.IsCheck;
            ProgressBarVisbility = Visibility.Hidden;
        }
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand<Window>(p => IsExcuteLoginCommand(), p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        Task LoginTask = ExcuteLoginCommand(p);
                    });
                }
                return _LoginCommand;
            }
        }
        private bool IsExcuteLoginCommand()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(UserPassword);
        }
        private async Task ExcuteLoginCommand(Window p)
        {
            await Task.Delay(6000);
            int User_ID;
            int loginWindow = LoginHandleModel.IsLoginAccount(Username, UserPassword, out User_ID);
            if (loginWindow == 1)
            {
                if (StaffHandleModel.IsStaffDelete(User_ID))
                {
                    MessageWindow messageWindow = new MessageWindow("Account have been deleted", MessageType.Info, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    ProgressBarVisbility = Visibility.Hidden;
                }
                else
                {
                    if (IsCheck)
                    {
                        LoginHandleModel.SaveAccount(Username, UserPassword, User_ID);
                    }
                    else
                    {
                        LoginHandleModel.SaveID(User_ID);
                    }
                    ManagerWindow window = new ManagerWindow();
                    p.Close();
                    ProgressBarVisbility = Visibility.Hidden;
                    window.ShowDialog();
                }

            }
            else if (loginWindow == -1)
            {
                if (StaffHandleModel.IsStaffDelete(User_ID))
                {
                    MessageWindow messageWindow = new MessageWindow("Account have been deleted", MessageType.Info, MessageButtons.Ok);
                    messageWindow.ShowDialog();
                    ProgressBarVisbility = Visibility.Hidden;
                }
                else
                {
                    if (IsCheck)
                    {
                        LoginHandleModel.SaveAccount(Username, UserPassword, User_ID);
                    }
                    else
                    {
                        LoginHandleModel.SaveID(User_ID);
                    }
                    EmployeeWindow window = new EmployeeWindow();
                    p.Close();
                    ProgressBarVisbility = Visibility.Hidden;
                    window.ShowDialog();
                }
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Username or Password wrong!", MessageType.Info, MessageButtons.Ok);
                messageWindow.ShowDialog();
                ProgressBarVisbility = Visibility.Hidden;
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
                    _CloseCommand = new RelayCommand<Window>(null, p =>
                    {
                        bool? Result = new MessageWindow("Do you want to close?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                        if (Result == true)
                        {
                            p.Close();
                        }
                    });
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
