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

        [TestMethod()]
        public void HentAlleOpgaverTest()
        {
            //arrange
            ManageOpgave opgaveTester = new ManageOpgave();
            //act
            List<ModelLibrary.Model.Opgave> testListe=new List<Opgave>(null);
          
            testListe = opgaveTester.HentAlleOpgaver();

            //assert
            foreach (var opg in testListe)
            {
             Assert.IsNotNull(opg);   
            }
            ;
        }

        [TestMethod()]
        public void HentOpgaveFraIdTest()
        {
            //arrange
            //det eneste arrangement denne test kræver er er "opgaveTester" der allerede er lavet. og
            
            //act/Assert
            Assert.IsNotNull(opgaveTester.HentOpgaveFraId(1));
        }

        [TestMethod()]
        public void IndsætOpgaveTest()
        {
            Assert.Fail();
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