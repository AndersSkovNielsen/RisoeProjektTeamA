
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;
using RisoeProjektTeamA.Persistency;


namespace RisoeMVVMUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //public OpgavePersistenceFacade TestFacade = new OpgavePersistenceFacade();
        //[TestMethod]
        //public void Hentenopgave()
        //{
        //    Opgave testOpgave = null;
        //    testOpgave=TestFacade.HentEnOpgave(1);

        //    Assert.IsNotNull(testOpgave);        
        //}

        ////Opgave Exception Tests
        //[TestMethod]
        //public void SuccesfulparsefromDB()
        //{
            
        //    bool exceptionThrown=false;

        //    try
        //    {
        //        TestFacade.HentAlleOpgaver();
        //    }
        //    catch (ParseToEnumException e)
        //    {
        //        Console.WriteLine(e);
        //        exceptionThrown = true;
                
        //    }

        //    Assert.IsFalse(exceptionThrown==true);
        //}

        [TestMethod]
        public void test()
        {
            Assert.Fail();
        }
    }
}
