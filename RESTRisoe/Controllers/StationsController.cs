using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;
using RESTRisoe.DBUtil;

namespace RESTRisoe.Controllers
{
    public class StationsController : ApiController
    {
        IManageStation manager = new ManageStation();
        // GET: api/Stations
        public IEnumerable<Station> Get()
        {
            return manager.HentAlleStationer();   
        }

        // GET: api/Stations/5
        public Station Get(int id)
        {
            {
                return manager.HentStationFraId(id);    
            }
        }

        // POST: api/Stations
        public bool Post([FromBody]Station value)
        {
            return manager.IndsætStation(value);
        }

        // PUT: api/Stations/5
        public bool Put(int id, [FromBody]Station value)
        {
            return manager.OpdaterStationr(value, id);
        }

        // DELETE: api/Stations/5
        public Station Delete(int id)
        {
            return manager.SletStation(id);
        }
    }
}
