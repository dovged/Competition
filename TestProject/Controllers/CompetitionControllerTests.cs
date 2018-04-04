using Microsoft.VisualStudio.TestTools.UnitTesting;
using Competition.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Controllers.Tests
{
    [TestClass()]
    public class CompetitionControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var con = new CompetitionController();
            var response = con.Get();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod()]
        public void GetTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutTest()
        {
            Assert.Fail();
        }
    }
}