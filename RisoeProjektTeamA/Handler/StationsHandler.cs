using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Handler
{
    class StationsHandler
    {
        public StationsViewModel StationsViewModel { get; set; }

        public StationsHandler(StationsViewModel stationsViewModel)
        {
            StationsViewModel = stationsViewModel;
        }

        public void IndsætStation()
        {
            Station station = StationsViewModel.NyStation;

            StationsViewModel.Logbog.SFacade.IndsætStation(station);

            //ListView opdatering
            var Stationer = StationsViewModel.Logbog.SFacade.HentAlleStationer();

            StationsViewModel.Logbog.StationsListe.Clear();

            foreach (var s in Stationer)
            {
                StationsViewModel.Logbog.AddS(s);
            }

            StationsViewModel.StationErValgt = false;
        }

        public void OpdaterStation()
        {
            Station Station = StationsViewModel.NyStation;
            int StationsID = StationsViewModel.ValgtStation.StationsId;

            StationsViewModel.Logbog.SFacade.OpdaterEnStation(StationsID, Station);

            //ListView opdatering
            var Stationer = StationsViewModel.Logbog.SFacade.HentAlleStationer();

            StationsViewModel.Logbog.StationsListe.Clear();

            foreach (var s in Stationer)
            {
                StationsViewModel.Logbog.AddS(s);
            }

            StationsViewModel.StationErValgt = false;
        }

        public void SletStation()
        {
            int StationsID = StationsViewModel.ValgtStation.StationsId;

            StationsViewModel.Logbog.SFacade.SletStation(StationsID);

            //ListView opdatering
            var Stationer = StationsViewModel.Logbog.SFacade.HentAlleStationer();

            StationsViewModel.Logbog.StationsListe.Clear();

            foreach (var s in Stationer)
            {
                StationsViewModel.Logbog.AddS(s);
            }

            StationsViewModel.StationErValgt = false;
        }
    }
}
