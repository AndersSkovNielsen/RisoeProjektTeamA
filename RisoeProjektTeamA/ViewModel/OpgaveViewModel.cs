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

namespace RisoeProjektTeamA.ViewModel
{
    class OpgaveViewModel:INotifyPropertyChanged
    {
        //hey, virker det?
        public ModelLibrary.Model.LogbogSingleton TheLogbogSingleton { get; set; }
        public int SelectedIndex { get; set; }
        public static ModelLibrary.Model.Opgave _selectedOpgave;

        public ModelLibrary.Model.Opgave SelectedOpgave
        {
            get { return _selectedOpgave; }
            set
            {
                _selectedOpgave=new Opgave(value.Udstyr,value.Løst,value.VentetidIDage,value.Beskrivelse);
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
            OpgaveHandler TheOpgaveHandler = new OpgaveHandler(this);
            ModelLibrary.Model.LogbogSingleton TheLogbogSingleton = ModelLibrary.Model.LogbogSingleton.Instance();
            UpdateCommand=new RelayCommand(TheOpgaveHandler.UpdateSelectedOpgave);
        } 
        


         

        public event PropertyChangedEventHandler PropertyChanged;

       

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
