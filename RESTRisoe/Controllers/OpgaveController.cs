﻿using ModelLibrary.Model;
using ModelLibrary.Exceptions;
using RESTRisoe.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;


namespace RESTRisoe.Controllers
{
    public class OpgaveController : ApiController
    {
        IManageOpgave manager = new ManageOpgave();

        // GET: api/Opgave/
        public IEnumerable<Opgave> Get()
        {
            try
            {
                return manager.HentAlleOpgaver();
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe
            }  
        }

        //Bliver ikke brugt i 3. iteration
        //Get: api/Opgave/HentAlleOpgaverForUdstyr/1
        [Route("api/Opgave/HentAlleOpgaverForUdstyr/{udstyrId:int}")]
        [HttpGet]
        public IEnumerable<Opgave> GetAlleOpgaverForUdstyr(int udstyrId)
        {
            try
            {
                return manager.HentAlleOpgaverForUdstyr(udstyrId);
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe 
            }
        }

        //3. iteration
        //Get: api/Opgave/HentOpgaverForUdstyrID/1
        [Route("api/Opgave/HentOpgaverForUdstyrID/{udstyrId:int}")]
        [HttpGet]
        public IEnumerable<Opgave> GetOpgaverForID(int udstyrID)
        {
            try
            {
                return manager.HentOpgaverForUdstyrID(udstyrID);
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex; //ex  håndteres i MVVMRisoe 
            }
        }

        // GET: api/Opgave/5
        public Opgave Get(int id)
        {
            try
            {
                return manager.HentOpgaveFraId(id);
            }
            catch (ParseToEnumException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        // POST: api/Opgave/
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