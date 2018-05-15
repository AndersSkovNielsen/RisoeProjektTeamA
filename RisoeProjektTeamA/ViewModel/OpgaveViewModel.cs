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

            //Pop up test, experiment
            PopUpTestCommand = new RelayCommand(OpgaveHandler.TestPopUp);
        }

        //Pop up test, experiment
        public RelayCommand PopUpTestCommand { get; set; }

        //Ikke relavant for 1. iteration, måske senere
        private Opgave _selectedOpgave = null;
        public Opgave SelectedOpgave
        {
            get { return _selectedOpgave; }
            set
            {
                if (value != null)
                {
                    _selectedOpgave = new Opgave(value);
                }
                else
                {
                    _selectedOpgave = null;
                }
                OnPropertyChanged();
            }
        }

        public int SelectedIndex { get; set; }
        
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
