using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RisoeProjektTeamA.Annotations;
using RisoeProjektTeamA.Handler;

namespace RisoeProjektTeamA.ViewModel
{
    class OpgaveViewModel:INotifyPropertyChanged
    {
        //hey, virker det?
        OpgaveHandler TheOpgaveHandler=new OpgaveHandler(this);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
