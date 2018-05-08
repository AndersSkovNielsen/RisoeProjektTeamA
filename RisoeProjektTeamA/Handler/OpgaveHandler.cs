using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    public class OpgaveHandler
    {
        public OpgaveViewModel OpgaveViewModel { get; set; }

        //Gem logbog til senere iteration
        //private LogbogSingleton Logbog;

        public OpgaveHandler(OpgaveViewModel opgaveViewModel)
        {
            OpgaveViewModel = opgaveViewModel;
        }

        public void IndsætOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;

            OpgaveViewModel.Logbog.Facade.IndsætOpgave(opgave);

            //ListView opdatering

            var opgaver = OpgaveViewModel.Logbog.Facade.HentAlleOpgaver();

            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            foreach (var o in opgaver)
            {
                OpgaveViewModel.Logbog.Add(o);
            }
            //kan måske nøjes med bare at tilføje (Hvad menes der med denne kommentar?).
        }

        public void OpdaterOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;

            OpgaveViewModel.Logbog.Facade.OpdaterEnOpgave(opgave.ID, opgave);

            //ListView opdatering
            var opgaver = OpgaveViewModel.Logbog.Facade.HentAlleOpgaver();

            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            foreach (var g in opgaver)
            {
                OpgaveViewModel.Logbog.Add(g);
            }
        }

        public void SletOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;

            OpgaveViewModel.Logbog.Facade.SletOpgave(opgave.ID);

            //ListView opdatering
            var opgaver = OpgaveViewModel.Logbog.Facade.HentAlleOpgaver();

            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            foreach (var g in opgaver)
            {
                OpgaveViewModel.Logbog.Add(g);
            }
        }
    }
}
