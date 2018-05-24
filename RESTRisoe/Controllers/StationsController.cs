using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelLibrary.Exceptions;
using RESTRisoe.DBUtil;

namespace RESTRisoe.Controllers
{
    public class StationsController : ApiController
    {
        IManageStation manager = new ManageStation();
        // GET: api/Stations
        public IEnumerable<string> HentAlleStationer()
        {
            try
            {
                return manager.HentAlleStationer();
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe
            }
        }

        // GET: api/Stations/5
        public string HentStationFraId(int id)
        {
            return "value";
        }

        // POST: api/Stations
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Stations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stations/5
        public void SletStation(int id)
        {
        }
    }
}
