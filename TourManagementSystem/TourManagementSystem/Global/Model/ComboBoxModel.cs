﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagementSystem.ViewModel;

namespace TourManagementSystem.Global.Model
{
    public class ComboBoxModel : BaseViewModel
    {
        /*
         * IsSelected is bool value to define what object is displayed in combobox
         */
        private bool _IsSelected;
        public bool IsSelected { get => _IsSelected; set { _IsSelected = value; OnPropertyChanged(); } }

        /*
         * Datagrid Combobox
         */
        private string _CB_Name;
        public string CB_Name { get => _CB_Name; set { _CB_Name = value; OnPropertyChanged(); } }

        public ComboBoxModel(string cb_name, bool is_selected)
        {
            this.IsSelected = is_selected;
            this.CB_Name = cb_name;
        }
    }
}