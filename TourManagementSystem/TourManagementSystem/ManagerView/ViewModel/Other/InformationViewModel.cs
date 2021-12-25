using System.Windows;
using System.Windows.Input;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class InformationViewModel : BaseViewModel
    {
        private int _User_ID;

        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        public InformationViewModel(int user_id)
        {
            User_ID = user_id;
            ProgressBarVisbility = Visibility.Hidden;
        }

        private string _TextMail;

        public string TextMail
        { get => _TextMail; set { _TextMail = value; OnPropertyChanged(); } }

        private Visibility _ProgressBarVisbility;

        public Visibility ProgressBarVisbility
        { get => _ProgressBarVisbility; set { _ProgressBarVisbility = value; OnPropertyChanged("ProgressBarVisbility"); } }

        private ICommand _SendEmailCommand;

        public ICommand SendEmailCommand
        {
            get
            {
                if (_SendEmailCommand == null)
                {
                    _SendEmailCommand = new RelayCommand<object>(p => !string.IsNullOrEmpty(TextMail), async p =>
                    {
                        ProgressBarVisbility = Visibility.Visible;
                        if (await GlobalFunction.IsSendEmail(TextMail))
                        {
                            ProgressBarVisbility = Visibility.Hidden;
                            string messageDisplay = string.Format("Send Email Successful!");
                            MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                            messageWindow.ShowDialog();
                            TextMail = "";
                        }
                        else
                        {
                            ProgressBarVisbility = Visibility.Hidden;
                            string messageDisplay = string.Format("Send Email failed!");
                            MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                            messageWindow.ShowDialog();
                        }
                    });
                }
                return _SendEmailCommand;
            }
        }
    }
}