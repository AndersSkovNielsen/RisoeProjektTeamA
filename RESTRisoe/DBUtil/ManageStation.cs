using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public class ManageStation:IManageStation
    {
        public List<Station> HentAlleStationer()
        {
            throw new NotImplementedException();
        }

        public Station HentStationFraId(int id)
        {
            throw new NotImplementedException();
        }

        public bool IndsætStation(Station station)
        {
            throw new NotImplementedException();
        }

        public bool OpdaterStationr(Station station, int id)
        {
            throw new NotImplementedException();
        }

        public Station SletStation(int id)
        {
            throw new NotImplementedException();
        }
    }
}