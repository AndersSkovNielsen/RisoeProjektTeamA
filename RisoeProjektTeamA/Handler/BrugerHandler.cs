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

        public void IndsætBruger()
        {
            Bruger bruger = BrugerViewModel.NyBruger;
            string BrugerInitialer = BrugerViewModel.NyBruger.Initialer;
            string BrugerKodeOrd = BrugerViewModel.NyBruger.KodeOrd;

            string BKodeord = BrugerViewModel.BKodeOrd;


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

            
            //var brugere = BrugerViewModel.Logbog.BFacade.HentAlleBrugere();
            
        }













    }
}
