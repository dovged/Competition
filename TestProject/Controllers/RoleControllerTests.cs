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
    public class RoleControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            RoleController con = new RoleController();
            var response = con.Get("Org1", 5);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}