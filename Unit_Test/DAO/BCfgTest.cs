using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.DAO.BCfg;

namespace Unit_Test.DAO
{
    /// <summary>
    /// Summary description for CargoCliDaoTest
    /// </summary>
    [TestClass]
    public class BCfgTest
    {
        public BCfgTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance; 

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BusinessConfigDao_All()
        {            
            var cfg = new BusinessConfigDao();
            var result = cfg.GetBConfig(1, "SCBME", true);
            Assert.IsTrue(result != null);
        }
        [TestMethod]
        public void BusinessConfigDao()
        {
            var cfg = new BusinessConfigDao();
            var result = cfg.GetBConfig("N",1, "SCBME", true);
            Assert.IsTrue(result != null);
        }
        [TestMethod]
        public void CargosCliDao()
        {
            var cfg = new CargosCliDao();
            var result = cfg.GetCargos("65281502", 7460, 3);
            Assert.IsTrue(result != null);
        }
        [TestMethod]
        public void VolMax()
        {
            var cfg = new CargosCliDao();
            var result = cfg.GetVolMax("65281502", 7460, 3);
            Assert.IsTrue(result != null);
        }

    }
}
