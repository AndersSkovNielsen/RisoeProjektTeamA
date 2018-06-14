using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelLibrary.Model;
using RESTRisoe.DBUtil;

namespace RESTRisoe.Controllers
{
    public class BrugerController : ApiController
    {
        IManageBruger manager = new ManageBruger();
        // GET: api/Bruger
        public IEnumerable<string> Get()
        {
            return manager.HentAlleBruger();
        }

        // GET: api/Bruger/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bruger
        public bool Post([FromBody]Bruger value)
        {
            return manager.indsætBruger(value);
        }

        // PUT: api/Bruger/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bruger/5
        public void Delete(int id)
        {
        }
    }
}
