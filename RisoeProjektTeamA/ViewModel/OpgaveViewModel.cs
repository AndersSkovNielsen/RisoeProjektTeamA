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
        public int SelectedIndex { get; set; }
        public static Opgave _selectedOpgave;
        public Opgave SelectedOpgave
        {
            get { return _selectedOpgave; }
            set
            {
                _selectedOpgave = new Opgave(value.Beskrivelse, value.Status, value.VentetidIDage);
                OnPropertyChanged();
            }
        }
        
        public LogbogSingleton Logbog { get; set; }
        public OpgaveHandler OpgaveHandler { get; set; }
        public Opgave NyOpgave
        {
            get { return NyOpgave; }
            set
            {
                if (value != null)
                {
                    NyOpgave = new Opgave(value);
                }
                else
                {
                    NyOpgave = null;
                }
                OnPropertyChanged(); }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public OpgaveViewModel()
        {
            OpgaveHandler = new OpgaveHandler(this);
            Logbog = LogbogSingleton.Instance;
            NyOpgave = new Opgave();

            AddCommand = new RelayCommand(OpgaveHandler.IndsætOpgave);
            UpdateCommand = new RelayCommand(OpgaveHandler.OpdaterOpgave);
            RemoveCommand = new RelayCommand(OpgaveHandler.SletOpgave);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
