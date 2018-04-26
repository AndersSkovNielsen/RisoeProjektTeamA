using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class OpgaveController : ApiController
    {
        // GET: api/Opgave
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
