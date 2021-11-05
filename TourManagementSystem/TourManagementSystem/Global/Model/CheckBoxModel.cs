using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourManagementSystem.Managers.ViewModel;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Global.Model
{
    public class CheckBoxModel : BaseViewModel
    {
        /*
         * IsSelected is bool value to define what object is displayed in checkbox
         */
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }

        /*
         * checkbox name
         */
        private string _CB_Name;
        public string CB_Name { get => _CB_Name; set { _CB_Name = value; OnPropertyChanged(); } }

        /*
         * checkbox sub name
         */
        private string _CB_Sub_Name;
        public string CB_Sub_Name { get => _CB_Sub_Name; set { _CB_Sub_Name = value; OnPropertyChanged(); } }

        /*
         * ID checkbox
         */
        private int _CB_ID;
        public int CB_ID { get => _CB_ID; set { _CB_ID = value; OnPropertyChanged(); } }

        public ICommand CB_Click_Command { get; set; }

        public CheckBoxModel(string name, string sub_name, int id, bool is_selected)
        {
            this.IsSelected = is_selected;
            this.CB_ID = id;
            this.CB_Name = name;
            this.CB_Sub_Name = sub_name;
        }
    }
}
