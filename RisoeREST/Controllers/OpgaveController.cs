using ModelLibrary.Model;
using RisoeREST.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RisoeREST.Controllers
{
    public class OpgaveController : ApiController
    {
        IManageOpgave manager = new ManageOpgave();

        // GET: api/Opgaver
        [Route("api/Opgaver")]
        public IEnumerable<TestOpgave> Get()
        {
            return manager.GetAllTestOpgave();
        }

        // GET: api/Opgave/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Opgave
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Opgave/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Opgave/5
        public void Delete(int id)
        {
        }
    }
}
