using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    class OpgaveHandler
    {
        private OpgaveViewModel opgaveViewModel;
        private LogbogSingleton Logbog;

        public OpgaveHandler(OpgaveViewModel opgaveViewModel)
        {
            this.opgaveViewModel = opgaveViewModel;
            Logbog = LogbogSingleton.Instance(); // er dette det rigtige sted at contsruere vores LogbogSIngleton?
        }
       

    }
}
