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
        List<TestOpgave> GetAllTestOpgave();
    }
}
