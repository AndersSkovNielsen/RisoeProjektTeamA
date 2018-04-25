using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RisoeProjektTeamA.Model;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    class OpgaveHandler
    {
        private OpgaveViewModel opgaveViewModel;
        private LogbogSingleton Logbog;

        public OpgaveHandler(OpgaveViewModel opgaveViewModel, string beskrivelse)
        {
            this.opgaveViewModel = opgaveViewModel;
            _beskrivelse = beskrivelse;
            Logbog = LogbogSingleton.Instance(); // er dette det rigtige sted at contsruere vores LogbogSIngleton?
        }
        private string _beskrivelse;

        public string Beskrivelse
        {
            get { return _beskrivelse; }
            set { _beskrivelse = value; }
        }

    }
}
