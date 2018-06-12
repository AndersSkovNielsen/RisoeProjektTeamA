using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTRisoe.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil.Tests
{
    [TestClass()]
    public class ManageOpgaveTests        
    {
        ManageOpgave opgaveTester = new ManageOpgave();
        List<ModelLibrary.Model.Opgave> testListe = new List<Opgave>();
       

        [TestMethod()]
        public void HentOpgaveFraIdTest()
        {
            //arrange
            //det eneste arrangement denne test kræver er er "opgaveTester" der allerede er lavet. og
            
            //act/Assert
            Assert.IsNotNull(opgaveTester.HentOpgaveFraId(1));
        }

        [TestMethod()]
        public void IndsætOgHentOpgaverTest()
        {//arrange
            int listelængde1;
            int listelængde2;
         //act
            Opgave testOpgave = new Opgave(999,"testopgave", StatusType.IkkeLøst,2, new Udstyr(2));
            testListe = opgaveTester.HentAlleOpgaver();
            listelængde1 = testListe.Count;
            opgaveTester.IndsætOpgave(testOpgave);
            testListe = opgaveTester.HentAlleOpgaver();
            listelængde2 = testListe.Count;
            opgaveTester.SletOpgave(999);

            //assert
            Assert.AreEqual(listelængde1 + 1, listelængde2);
        }

        [TestMethod()]
        public void OpdaterOpgaveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SletOpgaveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckEnumParseTest()
        {
            //arrange
            
            //act

            //assert
           
        }
    }
}