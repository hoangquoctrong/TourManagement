using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.Global.Model;

namespace TourManagementSystem.ManagerView.Model
{
    public class ComboBoxMissionModel : ComboBoxModel
    {
        private MissionModel _MissionItem;
        public MissionModel MissionItem { get => _MissionItem; set { _MissionItem = value; OnPropertyChanged(); } }
        public ComboBoxMissionModel(string cb_name, bool is_selected, MissionModel mission) : base(cb_name, is_selected)
        {
            this.MissionItem = mission;
        }

        public ComboBoxMissionModel(string cb_name, int id, bool is_selected, MissionModel mission) : base(cb_name, id, is_selected)
        {
            this.MissionItem = mission;
        }
    }
}
