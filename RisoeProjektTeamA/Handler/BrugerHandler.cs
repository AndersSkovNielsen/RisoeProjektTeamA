using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
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


            bruger.Initialer = BrugerInitialer;

            

            //ListView opdatering
            BrugerViewModel.Logbog.Brugerliste.Clear();

            var brugere = BrugerViewModel.Logbog.BFacade.HentAlleBrugere();

            foreach (var b in brugere)
            {
                BrugerViewModel.Logbog.AddB(b);
            }

            //BrugerViewModel.BrugerErValgt = false;
        }













    }
}
