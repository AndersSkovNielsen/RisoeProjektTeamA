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
        bool indsætBruger(Bruger bruger);
        Bruger HentBrugerFraInitialer(string initialer);

        Bruger SletBruger(string initialer);
    }
}