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
    public class ClubControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            ClubController con = new ClubController();
            var response = con.Get();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}