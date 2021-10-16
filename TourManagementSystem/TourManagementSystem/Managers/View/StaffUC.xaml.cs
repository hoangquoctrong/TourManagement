﻿using System;
using System.Collections.Generic;
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
using TourManagementSystem.Managers.ViewModel;

namespace TourManagementSystem.Managers.View
{
    /// <summary>
    /// Interaction logic for EmployeeUC.xaml
    /// </summary>
    public partial class StaffUC : UserControl
    {
        public StaffUC(int user_id)
        {
            InitializeComponent();
            DataContext = new StaffViewModel(user_id);
        }
    }
}
