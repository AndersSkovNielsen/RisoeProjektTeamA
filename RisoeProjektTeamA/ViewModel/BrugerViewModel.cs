using System;
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
    class BrugerViewModel : INotifyPropertyChanged
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


        private string _bKodeOrd;

        public string BKodeOrd
        {
            get { return _bKodeOrd; }
            set { _bKodeOrd = value; BekræftKode(); }
        }


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

        public ObservableCollection<Bruger> Brugerliste { get; set; }

        public BrugerViewModel()
        {
            BrugerHandler = new BrugerHandler(this);
            Logbog = LogbogSingleton.Instance;

            Brugerliste = new ObservableCollection<Bruger>(Logbog.BFacade.HentAlleBrugere());

            NyBruger = new Bruger();
            AddCommand = new RelayCommand(BrugerHandler.IndsætBruger);
            //UpdateCommand = new RelayCommand(BrugerHandler.OpdaterBruger);
            //RemoveCommand = new RelayCommand(BrugerHandler.SletBruger);
        }

        private Bruger _valgtBruger;

        public Bruger ValgtBruger
        {
            get { return _valgtBruger; }
            set
            {
                if (value != null)
                {
                    _valgtBruger = new Bruger(value);
                }
                else
                {
                    _valgtBruger = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _kodeErRigtig = false;
        public bool KodeErRigtig
        {
            get { return _kodeErRigtig; }
            set
            {
                if (value == true)
                {
                    _kodeErRigtig = true;
                }
                else
                {
                    _kodeErRigtig = false;
                }
                OnPropertyChanged();
            }
        }

        private bool BekræftKode()
        {
            Logbog.BFacade.HentEnBruger(ValgtBruger.Initialer);

            if (ValgtBruger.KodeOrd == BKodeOrd)
            {
                return KodeErRigtig = true;
            }
            else return false;
        }
    }
}
