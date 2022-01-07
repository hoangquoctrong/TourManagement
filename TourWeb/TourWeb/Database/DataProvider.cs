﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourWeb.Database;

namespace TourWeb.Models
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                }

                return _ins;
            }
            set => _ins = value;
        }

        public Tour_Mangement_DatabaseEntities DB { get; set; }

        private DataProvider()
        {
            DB = new Tour_Mangement_DatabaseEntities();
        }
    }
}