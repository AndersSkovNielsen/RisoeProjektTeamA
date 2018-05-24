using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;
using RESTRisoe.DBUtil;

namespace RESTRisoe.Controllers
{
    public class UdstyrController: ApiController
    {
        IManageUdstyr manager = new ManageUdstyr();

        // GET: api/Udstyr/
        public IEnumerable<Udstyr> Get()
        {
            try
            {
                return manager.HentAltUdstyr();
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe
            }
        }

        //Get: api/Udstyr/HentAlleOpgaverForUdstyr/1
        [Route("api/Opgave/HentAltUdstyrForStation/{stationNr:int}")]
        public IEnumerable<Opgave> GetFromUdstyr(int stationNr)
        {
            //try
            //{
            //    return manager.HentAlleOpgaverForUdstyr(udstyrId);
            //}
            //catch (ParseToEnumException ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    throw ex; //ex  håndteres i MVVMRisoe 
            //}
            return null;
        }

        // GET: api/Udstyr/5
        public Udstyr Get(int id)
        {
            try
            {
                return manager.HentUdstyrFraId(id);
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        // POST: api/Udstyr/
        public bool Post([FromBody] Udstyr value)
        {
            return manager.IndsætUdstyr(value);
        }

        // PUT: api/Udstyr/5
        public bool Put(int id, [FromBody] Udstyr value)
        {
            return manager.OpdaterUdstyr(value, id);
        }

        // DELETE: api/Udstyr/5
        public Udstyr Delete(int id)
        {
            return null; /*manager.SletUdstyr(id);*/
        }
    }
}
