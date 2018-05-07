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
    public class OpgaveViewModel:INotifyPropertyChanged
    {
        //Gem logbog til senere iteration
        //public ModelLibrary.Model.LogbogSingleton TheLogbogSingleton { get; set; }
        public int SelectedIndex { get; set; }
        public static ModelLibrary.Model.Opgave _selectedOpgave;

        
        public Model.LogbogSingleton Logbog { get; set; }
        public OpgaveHandler OpgaveHandler { get; set; }

        public Opgave NyOpgave { get; set; }

        public Opgave SelectedOpgave
        {
            get { return _selectedOpgave; }
            set
            {
                _selectedOpgave = new Opgave( value.Beskrivelse, value.Status, value.VentetidIDage);
                OnPropertyChanged();
            }
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
            Logbog = Model.LogbogSingleton.Instance;
            NyOpgave = new Opgave();
            
            UpdateCommand = new RelayCommand(OpgaveHandler.OpdaterOpgave);
            AddCommand = new RelayCommand(OpgaveHandler.IndsætOpgave);
            UpdateCommand = new RelayCommand(OpgaveHandler.SletOpgave);

            //Gem logbog til senere iteration
            //ModelLibrary.Model.LogbogSingleton TheLogbogSingleton = ModelLibrary.Model.LogbogSingleton.Instance();
        } 
        


         

        public event PropertyChangedEventHandler PropertyChanged;

       

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
