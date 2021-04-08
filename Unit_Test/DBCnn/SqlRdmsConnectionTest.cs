using System.Data;
using System.Data.SqlClient;
using DTO_Adapter.DBCnn;
using Persistence.DAO.DBcnn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unit_Test.DBCnn
{


    /// <summary>
    ///This is a test class for SqlRdmsConnectionTest and is intended
    ///to contain all SqlRdmsConnectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SqlRdmsConnectionTest
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

        #region Execute StoreProcedure DML Command
        [TestMethod()]
        public void SqlRdmsConnectionTestExecSP_Select()
        {
            String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            //String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdTConfig", "SCBME"));
            //param.Add(new SqlParameter("@IdBCfg", "N"));
            param.Add(new SqlParameter("@IdPais", 1));
            param.Add(new SqlParameter("@IsActive", true));
            IRdmsConnection<BConfigDto> cnn = new SqlRdmsConnection<BConfigDto>(new SqlConnection(StrCnn), "bcfg.SPS_BsConfig", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            Assert.IsTrue(result != null);
        }

        [TestMethod()]
        public void SqlRdmsConnectionTestExecSP_Insert()
        {
            //String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            var param = new List<SqlParameter>();

            //param.Add(new SqlParameter("@IdBCfg", "SCCM"));
            //param.Add(new SqlParameter("@IdPais", 1));
            //param.Add(new SqlParameter("@NCfg", "Sobre Cargo Carga Minima"));
            //param.Add(new SqlParameter("@Value", 60003171));
            //param.Add(new SqlParameter("@IsActive", true));

            //param.Add(new SqlParameter("@IdBCfg", "N"));
            //param.Add(new SqlParameter("@IdPais", 1));
            //param.Add(new SqlParameter("@NCfg", "Cargo Adicional Concreto Tipo N"));
            //param.Add(new SqlParameter("@IdTConfig", 1));
            //param.Add(new SqlParameter("@Value", 20));
            //param.Add(new SqlParameter("@IsActive", true));

            param.Add(new SqlParameter("@IdBCfg", "P"));
            param.Add(new SqlParameter("@IdPais", 1));
            param.Add(new SqlParameter("@NCfg", "Cargo Adicional Concreto Tipo N"));
            param.Add(new SqlParameter("@IdTConfig", 1));
            param.Add(new SqlParameter("@Value", 15));
            param.Add(new SqlParameter("@IsActive", true));

            SqlRdmsConnection<EmpDto> cnn = new SqlRdmsConnection<EmpDto>(
                new SqlConnection(StrCnn), "bcfg.SPI_BsConfig", param);

            var result = cnn.Execute(false, CommandType.StoredProcedure);
            Assert.IsTrue(result != null);
        }

        [TestMethod()]
        public void SqlRdmsConnectionTestExecSP_Update()
        {
            //String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdCfg", "N"));
            param.Add(new SqlParameter("@IdPais", 1));
            param.Add(new SqlParameter("@NCfg", "Cargo Adicional Modificado"));
            param.Add(new SqlParameter("@Value", "15"));
            param.Add(new SqlParameter("@DFin", DateTime.Today.AddDays(2)));
            SqlRdmsConnection<EmpDto> cnn = new SqlRdmsConnection<EmpDto>(
                new SqlConnection(StrCnn), "bcfg.SPU_BsConfig", param);
            var result = cnn.Execute(false, CommandType.StoredProcedure);
            Assert.IsTrue(result != null);
        }


        [TestMethod()]
        public void SqlRdmsConnectionTestExecSP_TestObject()
        {
            String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            //String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdTConfig", "SCBME"));
            //param.Add(new SqlParameter("@IdBCfg", "N"));
            param.Add(new SqlParameter("@IdPais", 1));
            param.Add(new SqlParameter("@IsActive", true));
            IRdmsConnection<Object> cnn = new SqlRdmsConnection<Object>(new SqlConnection(StrCnn), "bcfg.SPS_BsConfig", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            Assert.IsTrue(result != null);
        }

        #endregion

        #region Execute DML Command
        [TestMethod()]
        public void SqlRdmsConnectionTestExecCmd_Select()
        {
            //String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";

            SqlRdmsConnection<EmpDto> cnn = new SqlRdmsConnection<EmpDto>(new SqlConnection(StrCnn), "select * from hr.Emp");
            var result = cnn.Execute(true, CommandType.Text);
            Assert.IsTrue(result != null);
        }


        [TestMethod()]
        public void SqlRdmsConnectionTestExecCmd_Insert()
        {
            //String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";

            SqlRdmsConnection<EmpDto> cnn = new SqlRdmsConnection<EmpDto>(
                new SqlConnection(StrCnn), "INSERT INTO bcfg.tbConfig (IdCfg, IdPais, NCfg, IdTCfg, Value, DFin)" +
                                            "VALUES ('Z', 1, 'Cargo Z', 'N', 8, '2013-07-30')");
            var result = cnn.Execute(false, CommandType.Text);
            Assert.IsTrue(result != null);
        }

        [TestMethod()]
        public void SqlRdmsConnectionTestExecCmd_Update()
        {
            //String StrCnn = @"Data Source=GBERNAL;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";
            String StrCnn = @"Data Source=DevSrv2008R2;Initial Catalog=SCDBDev;Persist Security Info=True;User ID=scappuser;Password=scappuser$;WSID=IP(WS_Name)\WS_Usr\App_Usr;App=SINCO";

            SqlRdmsConnection<EmpDto> cnn = new SqlRdmsConnection<EmpDto>
                (new SqlConnection(StrCnn), "UPDATE bcfg.tbConfig " +
                    "SET NCfg = 'Cargo P Mod', IdTCfg = 'Z', Value = '100', DFin = '2013-08-01'" +
                    "WHERE IdCfg = 'P' AND IdPais = 1");
            var result = cnn.Execute(true, CommandType.Text);
            Assert.IsTrue(result != null);
        }
        #endregion

    }
}
