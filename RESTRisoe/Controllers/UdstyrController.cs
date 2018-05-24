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

        //Get: api/Udstyr/HentAltUdstyrForStation/1
        [Route("api/Udstyr/HentAltUdstyrForStation/{stationId:int}")]
        public IEnumerable<Udstyr> GetFromStation(int stationId)
        {
            try
            {
                return manager.HentAltUdstyrForStation(stationId);
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe 
            }
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
