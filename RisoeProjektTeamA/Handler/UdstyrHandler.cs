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

            UdstyrViewModel.Logbog.Facade.IndsætUdstyr(udstyr);

            //ListView opdatering

            var Udstyr = OpgaveViewModel.Logbog.Facade.HentAltUdstyr();

            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.Add(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void OpdaterUdstyr()
        {
            Udstyr udstyr = UdstyrViewModel.NytUdstyr;
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.ID;

            UdstyrViewModel.Logbog.Facade.OpdaterEtUdstyr(UdstyrID, Udstyr);

            //ListView opdatering
            var Udstyr = UdstyrViewModel.Logbog.Facade.HentAltUdstyr();

            UdstyrViewModel.Logbog.OpgaveListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.Add(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void SletUdstyr()
        {
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.ID;

            UdstyrViewModel.Logbog.Facade.SletUdstyr(UdstyrID);

            //ListView opdatering
            var Udstyr = UdstyrViewModel.Logbog.Facade.HentAltUdstyr();

            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            foreach (var u in Udstyr)
            {
                UdstyrViewModel.Logbog.Add(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }
    }
}
