using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public OpgaveHandler(OpgaveViewModel opgaveViewModel)
        {
            OpgaveViewModel = opgaveViewModel;
        }

        public void IndsætOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;
            int udstyrID = OpgaveViewModel.AdminUdstyr.UdstyrId;

            int opgaveID = OpgaveViewModel.AdminUdstyr.UdstyrId;
            opgave.Udstyr.UdstyrId = opgaveID;

            OpgaveViewModel.Logbog.OFacade.IndsætOpgave(opgave);

            //ListView opdatering
            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            var opgaver = OpgaveViewModel.Logbog.OFacade.HentOpgaverForUdstyrID(udstyrID);

            foreach (var o in opgaver)
            {
                OpgaveViewModel.Logbog.AddO(o);
            }

            OpgaveViewModel.OpgaveErValgt = false;
        }

        //2. iteration, men skrevet i 1. iteration
        public void OpdaterOpgave()
        {
            Opgave opgave = OpgaveViewModel.NyOpgave;
            int udstyrID = OpgaveViewModel.AdminUdstyr.UdstyrId;

            int opgaveID = OpgaveViewModel.ValgtOpgave.ID;

            OpgaveViewModel.Logbog.OFacade.OpdaterEnOpgave(opgaveID, opgave);

            //ListView opdatering
            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            var opgaver = OpgaveViewModel.Logbog.OFacade.HentOpgaverForUdstyrID(udstyrID);

            foreach (var o in opgaver)
            {
                OpgaveViewModel.Logbog.AddO(o);
            }

            OpgaveViewModel.OpgaveErValgt = false;
        }

        public void SletOpgave()
        {
            int opgaveID = OpgaveViewModel.ValgtOpgave.ID;

            int udstyrID = OpgaveViewModel.AdminUdstyr.UdstyrId;

            OpgaveViewModel.Logbog.OFacade.SletOpgave(opgaveID);

            //ListView opdatering
            OpgaveViewModel.Logbog.OpgaveListe.Clear();

            var opgaver = OpgaveViewModel.Logbog.OFacade.HentOpgaverForUdstyrID(udstyrID);

            foreach (var o in opgaver)
            {
                OpgaveViewModel.Logbog.AddO(o);
            }

            OpgaveViewModel.OpgaveErValgt = false;
        }

        //3. iteration
        public void HentOpgaver()
        {
            if (OpgaveViewModel.ValgtUdstyr != null)
            {
                OpgaveViewModel.Logbog.OpgaveListe.Clear();

                int udstyrID = OpgaveViewModel.ValgtUdstyr.UdstyrId;
                OpgaveViewModel.AdminUdstyr = OpgaveViewModel.ValgtUdstyr;
                OpgaveViewModel.AdminUdstyrErValgt = true;

                var opgaver = OpgaveViewModel.Logbog.OFacade.HentOpgaverForUdstyrID(udstyrID);

                foreach (var o in opgaver)
                {
                    OpgaveViewModel.Logbog.AddO(o);
                }
            }
            else
            {
                MessageDialogHandler.Show("Intet udstyr valgt", "Du skal vælge et udstyr fra menuen");
            }
        }

        //Experiment af pop up
        public void TestPopUp()
        {
            MessageDialogHandler.Show("Test", "Dette er en test!");
        }
    }
}
