using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary.Model;
using MVVMRisoe;

namespace MVVMTester
{
    [TestClass]
    public class UnitTest1
    {
        OpgavePersistenceFacade
        public OpgavePersistenceFacade TestFacade = new OpgavePersistenceFacade();
        [TestMethod]
        public void Hentenopgave()
        {
            Opgave testOpgave = null;
            testOpgave = TestFacade.HentEnOpgave(1);

            Assert.IsNotNull(testOpgave);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.Fail();
        }
    }
}
