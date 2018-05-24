using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    interface IManageStation
    {
        List<Station> HentAlleStationer();
        Station HentStationFraId(int id);
        bool IndsætStation(Station station);
        bool OpdaterStationr(Station station, int id);
        Station SletStation(int id);
    }
}
