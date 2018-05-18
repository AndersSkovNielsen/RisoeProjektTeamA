using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    class UdstyrHandler
    {
        public UdstyrViewModel UdstyrViewModel { get; set; }

        //Gem logbog til senere iteration
        //private LogbogSingleton Logbog;

        public UdstyrHandler(UdstyrViewModel udstyrViewModel)
        {
            UdstyrViewModel = udstyrViewModel;
        }

        public void IndsætUdstyr()
        {
            Udstyr udstyr = UdstyrViewModel.NytUdstyr;

            UdstyrViewModel.Logbog.UFacade.IndsætUdstyr(udstyr);

            //ListView opdatering

            var Udstyr = UdstyrViewModel.Logbog.UFacade.HentAltUdstyr();

            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.AddU(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void OpdaterUdstyr()
        {
            Udstyr udstyr = UdstyrViewModel.NytUdstyr;
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.ID;

            UdstyrViewModel.Logbog.UFacade.OpdaterEtUdstyr(UdstyrID, Udstyr);

            //ListView opdatering
            var Udstyr = UdstyrViewModel.Logbog.UFacade.HentAltUdstyr();

            UdstyrViewModel.Logbog.OpgaveListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.AddU(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void SletUdstyr()
        {
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.ID;

            UdstyrViewModel.Logbog.UFacade.SletUdstyr(UdstyrID);

            //ListView opdatering
            var Udstyr = UdstyrViewModel.Logbog.UFacade.HentAltUdstyr();

            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.AddU(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }
    }
}
