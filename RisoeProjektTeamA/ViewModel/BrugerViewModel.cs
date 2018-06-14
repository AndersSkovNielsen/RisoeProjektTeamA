﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class BrugerViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
        public LogbogSingleton Logbog { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public BrugerHandler BrugerHandler { get; set; }
        public ObservableCollection<Bruger> BrugerListe { get; set; }
        
        public string BKodeOrd { get; set; }

        private Bruger _nyBruger;
        public Bruger NyBruger
        {
          get { return _nyBruger; }

            set
            {
                if (value != null)
                {
                    _nyBruger = new Bruger(value);
                }
                else
                {
                    _nyBruger = null;
                }
                OnPropertyChanged();

            }
            
        }

        private Bruger _ValgtBruger;

        public Bruger ValgtBruger
        {
            get { return _ValgtBruger; }
            set
            {
                if (_ValgtBruger == null)
                {
                    _ValgtBruger = new Bruger(value);
                }
                else
                {
                    _ValgtBruger = null;
                }
                OnPropertyChanged();
            }
        }

        private List<string> KodeOrdsListe { get; set; }
        private List<string> Initialerliste { get; set; }

        public BrugerViewModel()
        {
            BrugerHandler = new BrugerHandler(this);
            Logbog = LogbogSingleton.Instance;
            BrugerListe = new ObservableCollection<Bruger>(Logbog.BFacade.HentAlleBrugere());

            NyBruger = new Bruger();
            AddCommand = new RelayCommand(BrugerHandler.IndsætBruger);
            //UpdateCommand = new RelayCommand(BrugerHandler.OpdaterBruger);
            //RemoveCommand = new RelayCommand(BrugerHandler.SletBruger);


        }





    }
}
