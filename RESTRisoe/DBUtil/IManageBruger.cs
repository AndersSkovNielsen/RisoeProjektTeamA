using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public interface IManageBruger
    {
        List<Bruger> HentAlleBruger();
        Bruger HentBrugerFraInitialer(string initialer);
        bool indsætBruger(Bruger bruger);
        Bruger SletBruger(string initialer);
    }
}