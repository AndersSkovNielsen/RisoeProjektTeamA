using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.View;
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

            OpgaveViewModel.OpgaveErValgt = false;
        }

        public void OpdaterOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;
            int opgaveID = OpgaveViewModel.ValgtOpgave.ID;

            OpgaveViewModel.Logbog.Facade.OpdaterEnOpgave(opgaveID, opgave);

            //ListView opdatering
            var opgaver = OpgaveViewModel.Logbog.Facade.HentAlleOpgaver();

            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            foreach (var g in opgaver)
            {
                OpgaveViewModel.Logbog.Add(g);
            }

            OpgaveViewModel.OpgaveErValgt = false;
        }

        public void SletOpgave()
        {
            int opgaveID = OpgaveViewModel.ValgtOpgave.ID;

            OpgaveViewModel.Logbog.Facade.SletOpgave(opgaveID);

            //ListView opdatering
            var opgaver = OpgaveViewModel.Logbog.Facade.HentAlleOpgaver();

            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            foreach (var g in opgaver)
            {
                OpgaveViewModel.Logbog.Add(g);
            }

            OpgaveViewModel.OpgaveErValgt = false;
        }

        //Pop Up Test, experiment
        public void TestPopUp()
        {
            MessageDialogHandler.Show("Test", "Dette er en test!");
        }
    }
}
