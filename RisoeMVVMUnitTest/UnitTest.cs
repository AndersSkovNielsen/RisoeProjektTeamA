
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RisoeProjektTeamA.Persistency;


namespace RisoeMVVMUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

      //Opgave Exception Tests
        public bool SuccesfulparsefromDB()
        {
            OpgavePersistenceFacade testface=new OpgavePersistenceFacade();

            testface.HentAlleOpgaver();


           return true ;
        }
    }
}
