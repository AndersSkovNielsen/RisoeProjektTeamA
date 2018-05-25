using ModelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTRisoe.DBUtil
{
    public interface IManageOpgave
    {
        List<Opgave> HentAlleOpgaver();
        List<Opgave> HentAlleOpgaverForUdstyr(int udstyrId);
        List<Opgave> HentUdstyrIDForOpgaver(int udstyrId);
        Opgave HentOpgaveFraId(int id);
        bool IndsætOpgave(Opgave opgave);
        bool OpdaterOpgave(Opgave opgave, int id);
        Opgave SletOpgave(int id);
    }
}
