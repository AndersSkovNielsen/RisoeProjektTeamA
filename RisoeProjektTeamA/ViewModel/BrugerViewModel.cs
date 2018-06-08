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

        private string _initialer;
        public BrugerHandler BrugerHandler { get; set; }
        private string Initialer
        {
            get { return _initialer; }
            set { _initialer = value; OnPropertyChanged();}
            
        }
        private string _kodeOrd;
        private string KodeOrd
        {
            get { return _kodeOrd; }
            set { _kodeOrd = value; OnPropertyChanged();}
        }

        private Bruger _nyBruger;
        public Bruger NyBruger
        {
          get { return _nyBruger; }

          set { _nyBruger = value; }
            
        }


        private List<string> KodeOrdsListe { get; set; }
        private List<string> Initialerliste { get; set; }

        public BrugerViewModel()
        {
            BrugerHandler = new BrugerHandler(this);


            NyBruger = new Bruger();
            AddCommand = new RelayCommand(BrugerHandler.IndsætBruger);
            UpdateCommand = new RelayCommand(BrugerHandler.OpdaterBruger);
            RemoveCommand = new RelayCommand(BrugerHandler.SletBruger);


        }





    }
}
