
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
        public OpgavePersistenceFacade TestFacade = new OpgavePersistenceFacade();



        [TestMethod] //til at sikre at testprojected kan give "passed" resultat
        public void SuccesTest()
        {
            int i = 1;
            Assert.AreEqual(i,1);
        }

        //[TestMethod]
        //public void Hentenopgave()
        //{
        //    Opgave testOpgave = null;
        //    testOpgave = TestFacade.HentEnOpgave(1);

        //    Assert.IsNotNull(testOpgave);
        //}

        //Opgave Exception Tests udkommenteret pga. kompleks forbindelse til REST. 
        //Se REST unit test klassen, for bedre eksempler på unit tests.
        //[TestMethod]
        //public void SuccesfulparsefromDB()
        //{

        //    bool exceptionThrown = false;

        //    try
        //    {
        //        TestFacade.HentAlleOpgaver();
        //    }
        //    catch (ParseToEnumException e)
        //    {
        //        Console.WriteLine(e);
        //        Assert.Fail();

        //    }
        //    //hvis vi når hertil er testen Passed
        //    Assert.AreEqual(1,1);
        //}

        [TestMethod]
        public void InputTest()
        { 
            //arrange
            StatusType st = StatusType.Fejlet;
            int id = 1;
            string ts = "Beksrivese";
            int uds = 1;
            int vt = 1;

            //act
            Opgave TestOpgave=new Opgave(id,ts,st,uds,vt);
        
            //assert
            Assert.AreEqual(id,TestOpgave.ID);
            Assert.AreEqual(vt,TestOpgave.VentetidIDage);
            Assert.AreEqual(ts,TestOpgave.Beskrivelse);
            Assert.AreEqual(st, TestOpgave.Status);
            Assert.AreEqual(TestOpgave.UdstyrId,uds);
        }
        

        

        //[TestMethod]
        //public void HentEnOpgave()
        //{
        //    Opgave testOpgave = null;
        //    testOpgave = TestFacade.HentEnOpgave(1);

        //    Assert.IsNotNull(testOpgave);
        //}

        [TestMethod] //til at sikre at test kan resultater i "Failed"
        public void FailTest()
        {
            Assert.Fail();
        }
    }
}

