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
    public class OpgaveViewModel : INotifyPropertyChanged
    {
        public LogbogSingleton Logbog { get; set; }
        public OpgaveHandler OpgaveHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OpgaveViewModel()
        {
            OpgaveHandler = new OpgaveHandler(this);
            Logbog = LogbogSingleton.Instance;
            
            //2. iteration
            AddCommand = new RelayCommand(OpgaveHandler.IndsætOpgave);
            UpdateCommand = new RelayCommand(OpgaveHandler.OpdaterOpgave);
            RemoveCommand = new RelayCommand(OpgaveHandler.SletOpgave);

            NyOpgave = new Opgave();

            StatusListe = new List<StatusType>() { StatusType.Løst, StatusType.Fejlet, StatusType.IkkeLøst };

            //3. iteration
            UdstyrsListe = Logbog.UdstyrsListe;

            HentCommand = new RelayCommand(OpgaveHandler.HentOpgaver);
            
            //Pop up test, experiment
            PopUpTestCommand = new RelayCommand(OpgaveHandler.TestPopUp);
        }
        
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }

        //2. iteration, måske senere
        private Opgave _nyOpgave;
        public Opgave NyOpgave
        {
            get { return _nyOpgave; }
            set
            {
                if (value != null)
                {
                    _nyOpgave = new Opgave(value);
                }
                else
                {
                    _nyOpgave = null;
                }
                OnPropertyChanged();
            }
        }

        private Opgave _valgtOpgave;
        public Opgave ValgtOpgave
        {
            get { return _valgtOpgave; }
            set
            {
                if (value != null)
                {
                    _valgtOpgave = new Opgave(value);
                    NyOpgave = value;
                    OpgaveErValgt = true;
                }
                else
                {
                    _valgtOpgave = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _opgaveErValgt;
        public bool OpgaveErValgt
        {
            get { return _opgaveErValgt; }
            set
            {
                if (AdminUdstyrErValgt == true)
                {
                    _opgaveErValgt = value;
                }
                else
                {
                    _opgaveErValgt = false;
                }
                OnPropertyChanged();
            }
        }

        public List<StatusType> StatusListe { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //3. iteration
        public ObservableCollection<Udstyr> UdstyrsListe { get; set; }

        public RelayCommand HentCommand { get; set; }

        private Udstyr _valgtUdstyr;
        public Udstyr ValgtUdstyr
        {
            get { return _valgtUdstyr; }
            set
            {
                if (value != null)
                {
                    _valgtUdstyr = new Udstyr(value);
                    UdstyrErValgt = true;
                }
                else
                {
                    _valgtUdstyr = null;
                    UdstyrErValgt = false;
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

        private Udstyr _adminUdstyr;
        public Udstyr AdminUdstyr
        {
            get { return _adminUdstyr; }
            set
            {
                if (value != null)
                {
                    _adminUdstyr = new Udstyr(value);
                }
                else
                {
                    _adminUdstyr = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _adminUdstyrErValgt;
        public bool AdminUdstyrErValgt
        {
            get { return _adminUdstyrErValgt; }
            set
            {
                _adminUdstyrErValgt = value;
                OnPropertyChanged();
            }
        }

        //Ikke relevant i 3. iteration
        //Pop up test, experiment
        public RelayCommand PopUpTestCommand { get; set; }

        //Ikke brugt i 2. iteration
        public int ValgtIndex { get; set; }

        
    }
}
