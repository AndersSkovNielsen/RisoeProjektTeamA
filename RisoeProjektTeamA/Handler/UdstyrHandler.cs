﻿using System;
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
    class UdstyrHandler
    {
        public UdstyrViewModel UdstyrViewModel { get; set; }

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
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.UdstyrId;

            UdstyrViewModel.Logbog.UFacade.OpdaterEtUdstyr(UdstyrID, udstyr);

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
            int UdstyrID = UdstyrViewModel.ValgtUdstyr.UdstyrId;

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

        public void HentUdstyr()
        {
            if (UdstyrViewModel.ValgtStation != null)
            {
                UdstyrViewModel.Logbog.UdstyrsListe.Clear();

                int StationID = UdstyrViewModel.ValgtStation.StationsId;

                var udstyr = UdstyrViewModel.Logbog.UFacade.HentAltUdstyrFraStation(StationID);
                foreach (var u in udstyr)
                {
                    UdstyrViewModel.Logbog.AddU(u);
                }
            }
            else
            {
                MessageDialogHandler.Show("Intet udstyr valgt", "Du skal vælge et udstyr fra menuen");
            }
        }
    }
}
