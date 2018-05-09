using ModelLibrary.Model;
using RESTRisoe.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTRisoe.Exceptions;

namespace RESTRisoe.Controllers
{
    public class OpgaveController : ApiController
    {
        IManageOpgave manager = new ManageOpgave();

        // GET: api/Opgave
        public IEnumerable<Opgave> Get()
        {
            try
            {
                return manager.HentAlleOpgaver();
            }
            catch (ParseToEnumException e)
            {
                // hvordan udtrykker vi at vi leder efter Status strings der ikke kan konverteres til Enum?
                Console.WriteLine(e);
                throw e;
            }
            
        }

        // GET: api/Opgave/5
        public Opgave Get(int id)
        {
            return manager.HentOpgaveFraId(id);
        }

        // POST: api/Opgave
        public bool Post([FromBody] Opgave value)
        {
            return manager.IndsætOpgave(value);
        }

        // PUT: api/Opgave/5
        public bool Put(int id, [FromBody] Opgave value)
        {
            return manager.OpdaterOpgave(value, id);
        }

        // DELETE: api/Opgave/5
        public Opgave Delete(int id)
        {
            return manager.SletOpgave(id);
        }
    }
}
