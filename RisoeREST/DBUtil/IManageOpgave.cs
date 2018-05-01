using ModelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RisoeREST.DBUtil
{
    public interface IManageOpgave
    {
        List<TestOpgave> GetAllTestOpgave();
    }
}