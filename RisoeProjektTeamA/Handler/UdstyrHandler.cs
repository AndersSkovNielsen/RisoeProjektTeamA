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
            int stationID = UdstyrViewModel.AdminStation.StationsId;

            //int udstyrID = UdstyrViewModel.AdminStation.StationsId;
            
            udstyr.Station.StationsId = stationID;

            //Hvis man ikke skal indtaste dato
            udstyr.Installationsdato = DateTime.Now;

            UdstyrViewModel.Logbog.UFacade.IndsætUdstyr(udstyr);

            //ListView opdatering
            UdstyrViewModel.Logbog.UdstyrsListe.Clear();
            
            var udstyrliste = UdstyrViewModel.Logbog.UFacade.HentUdstyrForStationID(stationID);

            foreach (var u in udstyrliste)
            {
                UdstyrViewModel.Logbog.AddU(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void OpdaterUdstyr()
        {
            Udstyr udstyr = UdstyrViewModel.NytUdstyr;
            int stationID = UdstyrViewModel.AdminStation.StationsId;

            int udstyrID = UdstyrViewModel.ValgtUdstyr.UdstyrId;

            UdstyrViewModel.Logbog.UFacade.OpdaterEtUdstyr(udstyrID, udstyr);

            //ListView opdatering
            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            var udstyrliste = UdstyrViewModel.Logbog.UFacade.HentUdstyrForStationID(stationID);

            foreach (var u in udstyrliste)
            {
                UdstyrViewModel.Logbog.AddU(u);
            }

            UdstyrViewModel.UdstyrErValgt = false;
        }

        public void SletUdstyr()
        {
            int udstyrID = UdstyrViewModel.ValgtUdstyr.UdstyrId;

            int stationID = UdstyrViewModel.AdminStation.StationsId;

            UdstyrViewModel.Logbog.UFacade.SletUdstyr(udstyrID);

            //ListView opdatering
            UdstyrViewModel.Logbog.UdstyrsListe.Clear();

            var udstyrliste = UdstyrViewModel.Logbog.UFacade.HentUdstyrForStationID(stationID);

            foreach (var u in udstyrliste)
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

                int stationID = UdstyrViewModel.ValgtStation.StationsId;
                UdstyrViewModel.AdminStation = UdstyrViewModel.ValgtStation;
                UdstyrViewModel.AdminStationErValgt = true;
                UdstyrViewModel.UdstyrErValgt = false;

                var udstyrliste = UdstyrViewModel.Logbog.UFacade.HentUdstyrForStationID(stationID);

                foreach (var u in udstyrliste)
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
