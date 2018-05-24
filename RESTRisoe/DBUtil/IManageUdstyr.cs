using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public interface IManageUdstyr
    {
        List<Udstyr> HentAltUdstyr();
        List<Udstyr> HentAltUdstyrForStation(int stationId);
        Udstyr HentUdstyrFraId(int id);
        bool IndsætUdstyr(Udstyr udstyr);
        bool OpdaterUdstyr(Udstyr udstyr, int id);
        Udstyr SletUdstyr(int id);
    }
}

