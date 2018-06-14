using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public interface IManageBruger
    {
        List<Bruger> HentAlleBrugere();
        Bruger HentBrugerFraID(int id);
        bool indsætBruger(Bruger bruger);
    }
}