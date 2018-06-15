using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using ModelLibrary.Model;
using RisoeProjektTeamA.View;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    class BrugerHandler
    {
        public BrugerViewModel BrugerViewModel { get; set; }

        public BrugerHandler(BrugerViewModel brugerViewModel)
        {
            BrugerViewModel = brugerViewModel;
        }

        public void HentOpgaver()
        {
            if (BrugerViewModel.ValgtBruger != null)
            {
                BrugerViewModel.Logbog.Brugerliste.Clear();

                //int udstyrID = OpgaveViewModel.ValgtUdstyr.UdstyrId;
                //OpgaveViewModel.AdminUdstyr = OpgaveViewModel.ValgtUdstyr;
                //OpgaveViewModel.AdminUdstyrErValgt = true;
                //OpgaveViewModel.OpgaveErValgt = false;

                var brugere = BrugerViewModel.Logbog.BFacade.HentAlleBrugere();

                foreach (var b in brugere)
                {
                    BrugerViewModel.Logbog.AddB(b);
                }
            }
            else
            {
                MessageDialogHandler.Show("Intet udstyr valgt", "Du skal vælge et udstyr fra menuen");
            }
        }

        public void IndsætBruger()
        {
            Bruger bruger = BrugerViewModel.NyBruger;
            string BrugerInitialer = BrugerViewModel.NyBruger.Initialer;
            string BrugerKodeOrd = BrugerViewModel.NyBruger.KodeOrd;

            string BKodeord = BrugerViewModel.BKodeord;


            if (BKodeord == BrugerKodeOrd)
            {
                BrugerViewModel.Logbog.BFacade.IndsætBruger(bruger);
            }
            else
            {
                MessageDialogHandler.Show("Kig igen", "Kodeord ikke ens");
            }


            //sammenlign de 2 kodeord. f.eks. execption
            //kan hente den enkelte bruger.
            //bruger.Initialer = BrugerInitialer;


            BrugerViewModel.Logbog.Brugerliste.Clear();

            var brugere = BrugerViewModel.Logbog.BFacade.HentAlleBrugere();

            foreach (var b in brugere)
            {
                BrugerViewModel.Logbog.AddB(b);
            }

            //BrugerViewModel.OpgaveErValgt = false;
        }


        public void SletBruger()
        {
            Bruger bruger = BrugerViewModel.ValgtBruger;

            BrugerViewModel.Logbog.BFacade.SletBruger(bruger.Initialer);

            BrugerViewModel.Logbog.Brugerliste.Clear();

            var brugere = BrugerViewModel.Logbog.BFacade.HentAlleBrugere();

            foreach (var b in brugere)
            {
                BrugerViewModel.Logbog.AddB(b);
            }

        }










    }
}
