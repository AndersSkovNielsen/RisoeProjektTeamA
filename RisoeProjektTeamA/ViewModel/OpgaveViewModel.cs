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
    public class OpgaveViewModel : INotifyPropertyChanged
    {
        private Opgave _nyOpgave = null;
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

        public LogbogSingleton Logbog { get; set; }
        public OpgaveHandler OpgaveHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OpgaveViewModel()
        {
            OpgaveHandler = new OpgaveHandler(this);
            Logbog = LogbogSingleton.Instance;
            NyOpgave = new Opgave();

            //Ikke relavant for 1. iteration. Måske senere
            AddCommand = new RelayCommand(OpgaveHandler.IndsætOpgave);
            UpdateCommand = new RelayCommand(OpgaveHandler.OpdaterOpgave);
            RemoveCommand = new RelayCommand(OpgaveHandler.SletOpgave);

            StatusListe = new List<StatusType>() { StatusType.Løst, StatusType.Fejlet, StatusType.IkkeLøst };

            //Pop up test, experiment
            PopUpTestCommand = new RelayCommand(OpgaveHandler.TestPopUp);
        }

        //Pop up test, experiment
        public RelayCommand PopUpTestCommand { get; set; }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        private bool _opgaveErValgt;
        public bool OpgaveErValgt
        {
            get { return _opgaveErValgt; }
            set
            {
                _opgaveErValgt = value;
                OnPropertyChanged();
            }
        }

        //Ikke relavant for 1. iteration, måske senere
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

        public List<StatusType> StatusListe { get; set; }

        public int ValgtIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
