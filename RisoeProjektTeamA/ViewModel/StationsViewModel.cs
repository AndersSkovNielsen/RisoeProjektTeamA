using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.Annotations;
using RisoeProjektTeamA.Common;
using RisoeProjektTeamA.Handler;
using RisoeProjektTeamA.Model;

namespace RisoeProjektTeamA.ViewModel
{
    class StationsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LogbogSingleton Logbog { get; set; }
        public UdstyrHandler UdstyrHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public StationsViewModel()
        {
            StationsHandler StationsHandler = new StationsHandler(this);
            Logbog = LogbogSingleton.Instance;

            //Ikke relavant for 1. iteration. Måske senere
            AddCommand = new RelayCommand(StationsHandler.IndsætStation);
            UpdateCommand = new RelayCommand(StationsHandler.OpdaterStation);
            RemoveCommand = new RelayCommand(StationsHandler.SletStation);

            NyStation = new Station();

        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }

        //Ikke relavant for 1. iteration, måske senere
        private Station _nyStation;
        public Station NyStation
        {
            get { return _nyStation; }
            set
            {
                if (value != null)
                {
                    _nyStation = new Station (value);
                }
                else
                {
                    _nyStation = null;
                }
                OnPropertyChanged();
            }
        }

        private Station _valgtStation;
        public Station ValgtStation
        {
            get { return _valgtStation; }
            set
            {
                if (value != null)
                {
                    _valgtStation = new Station (value);
                    NyStation = value;
                    StationErValgt = true;
                }
                else
                {
                    _valgtStation = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _stationErValgt;

        public bool StationErValgt
        {
            get { return _stationErValgt; }
            set
            {
                _stationErValgt = value;
                OnPropertyChanged();
            }
        }

        //Ikke brugt i 2. iteration
        public int ValgtIndex { get; set; }
    }
}
