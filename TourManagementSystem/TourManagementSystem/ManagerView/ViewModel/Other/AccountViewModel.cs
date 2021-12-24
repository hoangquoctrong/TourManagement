using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.Global.Model;
using TourManagementSystem.Global.View;
using TourManagementSystem.ManagerView.Model;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.ManagerView.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        private int _User_ID;
        public int User_ID
        { get => _User_ID; set { _User_ID = value; OnPropertyChanged(); } }

        #region Data Binding

        private int _Staff_ID;
        public int Staff_ID
        { get => _Staff_ID; set { _Staff_ID = value; OnPropertyChanged(); } }

        private string _Staff_Name;
        public string Staff_Name
        { get => _Staff_Name; set { _Staff_Name = value; OnPropertyChanged(); } }

        private string _Staff_Role;
        public string Staff_Role
        { get => _Staff_Role; set { _Staff_Role = value; OnPropertyChanged(); } }

        private string _Staff_ID_Card;
        public string Staff_ID_Card
        { get => _Staff_ID_Card; set { _Staff_ID_Card = value; OnPropertyChanged(); } }

        private string _Staff_ID_Card_Place;
        public string Staff_ID_Card_Place
        { get => _Staff_ID_Card_Place; set { _Staff_ID_Card_Place = value; OnPropertyChanged(); } }

        private DateTime _Staff_ID_Card_Date = DateTime.Now;
        public DateTime Staff_ID_Card_Date
        { get => _Staff_ID_Card_Date; set { _Staff_ID_Card_Date = value; OnPropertyChanged(); } }

        private string _Staff_String_ID_Card_Date;
        public string Staff_String_ID_Card_Date
        { get => _Staff_String_ID_Card_Date; set { _Staff_String_ID_Card_Date = value; OnPropertyChanged(); } }

        private DateTime _Staff_Birthday = DateTime.Now;
        public DateTime Staff_Birthday
        { get => _Staff_Birthday; set { _Staff_Birthday = value; OnPropertyChanged(); } }

        private string _Staff_String_Birthday;
        public string Staff_String_Birthday
        { get => _Staff_String_Birthday; set { _Staff_String_Birthday = value; OnPropertyChanged(); } }

        private string _Staff_Birth_Place;
        public string Staff_Birth_Place
        { get => _Staff_Birth_Place; set { _Staff_Birth_Place = value; OnPropertyChanged(); } }

        private string _Staff_Address;
        public string Staff_Address
        { get => _Staff_Address; set { _Staff_Address = value; OnPropertyChanged(); } }

        private string _Staff_Gender;
        public string Staff_Gender
        { get => _Staff_Gender; set { _Staff_Gender = value; OnPropertyChanged(); } }

        private string _Staff_Academic_Level;
        public string Staff_Academic_Level
        { get => _Staff_Academic_Level; set { _Staff_Academic_Level = value; OnPropertyChanged(); } }

        private string _Staff_Email;
        public string Staff_Email
        { get => _Staff_Email; set { _Staff_Email = value; OnPropertyChanged(); } }

        private string _Staff_Phone_Number;
        public string Staff_Phone_Number
        { get => _Staff_Phone_Number; set { _Staff_Phone_Number = value; OnPropertyChanged(); } }

        private string _Staff_Username;
        public string Staff_Username
        { get => _Staff_Username; set { _Staff_Username = value; OnPropertyChanged(); } }

        private string _Staff_Password;
        public string Staff_Password
        { get => _Staff_Password; set { _Staff_Password = value; OnPropertyChanged(); } }

        private string _Staff_Note;
        public string Staff_Note
        { get => _Staff_Note; set { _Staff_Note = value; OnPropertyChanged(); } }

        private DateTime _Staff_Start_Date = DateTime.Now;
        public DateTime Staff_Start_Date
        { get => _Staff_Start_Date; set { _Staff_Start_Date = value; OnPropertyChanged(); } }

        private byte[] _Staff_Image_Byte_Source;
        public byte[] Staff_Image_Byte_Source
        { get => _Staff_Image_Byte_Source; set { _Staff_Image_Byte_Source = value; OnPropertyChanged(); } }

        private BitmapImage _Staff_Image_Source;
        public BitmapImage Staff_Image_Source
        { get => _Staff_Image_Source; set { _Staff_Image_Source = value; OnPropertyChanged(); } }

        #endregion Data Binding

        public AccountViewModel(int user_id)
        {
            User_ID = user_id;
            SetStaffInView(StaffHandleModel.GetStaffFromID(user_id));
        }

        private async void SetStaffInView(StaffModel staff)
        {
            await Task.Delay(1000);
            Staff_ID = staff.STAFF_ID;
            Staff_Name = staff.STAFF_NAME;
            Staff_Role = staff.STAFF_ROLE;
            Staff_ID_Card = staff.STAFF_CITIZEN_CARD;
            Staff_ID_Card_Date = staff.STAFF_CITIZEN_CARD_DATE;
            Staff_ID_Card_Place = staff.STAFF_CITIZEN_CARD_PLACE;
            Staff_Birthday = staff.STAFF_BIRTH_DATE;
            Staff_String_Birthday = staff.STAFF_STRING_BIRTH_DATE;
            Staff_Birth_Place = staff.STAFF_BIRTH_PLACE;
            Staff_Address = staff.STAFF_ADDRESS;
            Staff_Gender = staff.STAFF_GENDER;
            Staff_Academic_Level = staff.STAFF_ACADEMIC_LEVEL;
            Staff_Email = staff.STAFF_EMAIL;
            Staff_Start_Date = staff.STAFF_START_DATE;
            Staff_Note = staff.STAFF_NOTE;
            Staff_Phone_Number = staff.STAFF_PHONE_NUMBER;
            Staff_Username = staff.STAFF_USERNAME;
            Staff_Image_Byte_Source = staff.STAFF_IMAGE_BYTE_SOURCE;
            Staff_Image_Source = Staff_Image_Byte_Source == null
                ? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/User.png", UriKind.Absolute))
                : GlobalFunction.ToImage(Staff_Image_Byte_Source);
        }

        private ICommand _ChangePasswordCommand;

        public ICommand ChangePasswordCommand
        {
            get
            {
                if (_ChangePasswordCommand == null)
                {
                    _ChangePasswordCommand = new RelayCommand<object>(p => !string.IsNullOrEmpty(Staff_Password), p =>
                    {
                        if (StaffHandleModel.ChangePassword(User_ID, Staff_Password))
                        {
                            string messageDisplay = string.Format("Change Password Successfully!");
                            MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Success, MessageButtons.Ok);
                            messageWindow.ShowDialog();
                            Staff_Password = "";
                        }
                        else
                        {
                            string messageDisplay = string.Format("Change Password Failed! Please try again!");
                            MessageWindow messageWindow = new MessageWindow(messageDisplay, MessageType.Error, MessageButtons.Ok);
                            messageWindow.ShowDialog();
                        }
                    });
                }
                return _ChangePasswordCommand;
            }
        }
    }
}