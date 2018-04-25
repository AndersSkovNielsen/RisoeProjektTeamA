﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace RisoeProjektTeamA.Model
{
    class LogbogSingleton
    {
        public static LogbogSingleton _instance;
        public ObservableCollection<Opgave> OpgaveListe { get; set; }
        public ObservableCollection<Log> Logliste { get; set; }



        public static LogbogSingleton Instance()
        {
            return _instance ?? (_instance = new LogbogSingleton());
        }

        private LogbogSingleton()
        {
            OpgaveListe = new ObservableCollection<Opgave>();
            Logliste = new ObservableCollection<Log>();
        }
    }
}
