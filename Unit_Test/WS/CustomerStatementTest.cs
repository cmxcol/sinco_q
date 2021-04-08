using DTO_Adapter.WS;
using Persistence.AppWS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;


namespace Unit_Test.WS
{


    /// <summary>
    ///This is a test class for CustomerStatementTest and is intended
    ///to contain all CustomerStatementTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CustomerStatementTest
    {

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void CustomerStatementTest_Execution_Regular()
        {
            string wsUrl = "http://mxoccsapeip01.noam.cemexnet.com:8180/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_SINCO_PRD_GBL&receiverParty=&receiverService=&interface=SI_OS_CustomerStatement&interfaceNamespace=http%3A%2F%2Fcemex.com%2FSINCO%2FECCMEX%2FCustomerStatement"; // TODO: Initialize to an appropriate value
            CustomerStatement target = new CustomerStatement(wsUrl); // TODO: Initialize to an appropriate value
            CsParametersDto parameters = new CsParametersDto()
            {
                IdBusinessEntity = "65603243",
                CDist = "00",
                IdSOrg = "7460",
                IdSector = "03",
                IdDCurr = "",
                IdFCurr = ""
            }; // TODO: Initialize to an appropriate value

            var actual = CustomerStatementTest_LogicExec(wsUrl, "WSPISINCO", "inicio01", parameters);
            Assert.IsTrue(actual.Exception == null,actual.IdBlqBusinessEntity);
        }

        [TestMethod()]
        public void CustomerStatementTest_Execution_Failure()
        {
            string wsUrl = "http://mxoccsapeip01.noam.cemexnet.com:8180/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_SINCO_PRD_GBL&receiverParty=&receiverService=&interface=SI_OS_CustomerStatement&interfaceNamespace=http%3A%2F%2Fcemex.com%2FSINCO%2FECCMEX%2FCustomerStatement"; // TODO: Initialize to an appropriate value
            CustomerStatement target = new CustomerStatement(wsUrl); // TODO: Initialize to an appropriate value
            CsParametersDto parameters = new CsParametersDto()
            {
                IdBusinessEntity = "",
                CDist = "00",
                IdSOrg = "7460",
                IdSector = "03",
                IdDCurr = "",
                IdFCurr = ""
            }; // TODO: Initialize to an appropriate value

            var actual = CustomerStatementTest_LogicExec(wsUrl, "WSPISINCO", "inicio01", parameters);
            Assert.IsTrue(actual.Exception != null,actual.Exception);
        }

        [TestMethod()]
        public void CustomerStatementTest_Execution_Regular_Mock()
        {
            string wsUrl = "http://10.0.1.7:8088/mockSI_OS_CustomerStatementBinding"; // TODO: Initialize to an appropriate value
            CustomerStatement target = new CustomerStatement(wsUrl); // TODO: Initialize to an appropriate value
            CsParametersDto parameters = new CsParametersDto()
            {
                IdBusinessEntity = "65603243",
                CDist = "00",
                IdSOrg = "7460",
                IdSector = "03",
                IdDCurr = "",
                IdFCurr = ""
            }; // TODO: Initialize to an appropriate value

            var actual = CustomerStatementTest_LogicExec(wsUrl, "WSPISINCO", "inicio01", parameters);
            var expected = new CsResponseDto()
            {
                NBusinessEntity = "MESA DE YEGUAS V 70",
                IdBlqBusinessEntity = "00",
                NBlqBusinessEntity = "Not blocked customer",
                IdCliente = "0050301790",
                NCliente = "JOSOA SAS",
                IdBlqCliente = "00",
                NBlqCliente = "Not blocked customer",
                IdCpObra = "ZCOC",
                NCpObra = "(AR) Payable immediately Due net",
                CartT = "0.0000",
                SldAf = "1502051.0000-",
                CartN = "1502051.0000-",
                LimCr = "0.0000",
                FConvCurr = "42.00",
                VComp = "0.0000",
                Exception = null
            };

            Assert.IsTrue(expected.NBusinessEntity == actual.NBusinessEntity, "(Expected)" + expected.NBusinessEntity + "=" + "(Actual)" + actual.NBusinessEntity);
        }

        [TestMethod()]
        public void CustomerStatementTest_Execution_Failure_Mock()
        {
            string wsUrl = "http://10.0.1.7:8080/mockSI_OS_CustomerStatementBinding_Failure"; // TODO: Initialize to an appropriate value
            CustomerStatement target = new CustomerStatement(wsUrl); // TODO: Initialize to an appropriate value
            CsParametersDto parameters = new CsParametersDto()
            {
                IdBusinessEntity = "65603243",
                CDist = "00",
                IdSOrg = "7460",
                IdSector = "03",
                IdDCurr = "",
                IdFCurr = ""
            }; // TODO: Initialize to an appropriate value

            var actual = CustomerStatementTest_LogicExec(wsUrl, "WSPISINCO", "inicio01", parameters);
            var expected = new CsResponseDto()
            {
                Exception = "SHIPTO_NOT_FOUND"
            };
            //Assert.IsTrue(actual.Exception != null);
            Assert.AreEqual(actual.Exception, expected.Exception, "(Actual)" + actual.Exception + "=" + "(Expected)" + expected.Exception);
        }

        public CsResponseDto CustomerStatementTest_LogicExec(string wsUrl, string usr, string password, CsParametersDto param)
        {
            //CustomerStatement target = new CustomerStatement(wsUrl);
            IWService<CsParametersDto, CsResponseDto> target = new CustomerStatement(wsUrl);
            target.RequestParameters(param);
            target.Credentials(usr, password);
            return target.Execute();

        }

    }
}
