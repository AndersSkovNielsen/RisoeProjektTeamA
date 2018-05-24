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
    class UdstyrViewModel : INotifyPropertyChanged
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
        public UdstyrViewModel()
        {
            UdstyrHandler = new UdstyrHandler(this);
            Logbog = LogbogSingleton.Instance;
            
            AddCommand = new RelayCommand(UdstyrHandler.IndsætUdstyr);
            UpdateCommand = new RelayCommand(UdstyrHandler.OpdaterUdstyr);
            RemoveCommand = new RelayCommand(UdstyrHandler.SletUdstyr);

            NytUdstyr = new Udstyr();

            TypeListe = new List<uType>() {uType.Filter, uType.Termometer, uType.Lufttrykmåler};
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }

        //Ikke relavant for 1. iteration, måske senere
        private Udstyr _nytUdstyr;
        public Udstyr NytUdstyr
        {
            get { return _nytUdstyr; }
            set
            {
                if (value != null)
                {
                    _nytUdstyr = new Udstyr(value);
                }
                else
                {
                    _nytUdstyr = null;
                }
                OnPropertyChanged();
            }
        }

        private Udstyr _valgtUdstyr;
        public Udstyr ValgtUdstyr
        {
            get { return _valgtUdstyr; }
            set
            {
                if (value != null)
                {
                    _valgtUdstyr = new Udstyr(value);
                    NytUdstyr = value;
                    UdstyrErValgt = true;
                }
                else
                {
                    _valgtUdstyr = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _udstyrErValgt;
        public bool UdstyrErValgt
        {
            get { return _udstyrErValgt; }
            set
            {
                _udstyrErValgt = value;
                OnPropertyChanged();
            }
        }

        public List<uType> TypeListe { get; set; }

        
    
        //Ikke brugt i 2. iteration
        public int ValgtIndex { get; set; }
    }
}
