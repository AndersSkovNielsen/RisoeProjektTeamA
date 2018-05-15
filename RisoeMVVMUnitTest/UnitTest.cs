
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary.Exceptions;
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
            OpgavePersistenceFacade testface = new OpgavePersistenceFacade();
            bool exceptionThrown=false;

            try
            {
                testface.HentAlleOpgaver();
            }
            catch (ParseToEnumException e)
            {
                Console.WriteLine(e);
                exceptionThrown = true;
                
            }

            Assert.IsFalse(exceptionThrown==true);

            return exceptionThrown;




        }
    }
}
