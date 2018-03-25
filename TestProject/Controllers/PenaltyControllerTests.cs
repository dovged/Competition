using Microsoft.VisualStudio.TestTools.UnitTesting;
using Competition.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition.Models;

namespace Competition.Controllers.Tests
{
    [TestClass()]
    public class PenaltyControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var con = new PenaltyController();
            var response = con.Get();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod()]
        public void GetTest1()
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

        [TestMethod()]
        public void DeleteTest()
        {
            var con = new PenaltyController();
            var response = con.Delete(100);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
            Assert.Fail();
        }
    }
}