using DTO_Adapter.WS;
using Persistence.AppWS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Test.WS
{


    /// <summary>
    ///This is a test class for SOrderTest and is intended
    ///to contain all SOrderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SOrderTest
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
        public void SOrderTest_Execution_Regular()
        {
            string wsUrl = "http://mxoccsapmrp02.noam.cemexnet.com:8000/sap/bc/srt/xip/sap/zsi_is_external_to_ordertaking/500/zs_ordtakingcrm/zb_ordtakingcrm"; // TODO: Initialize to an appropriate value

            SOrder target = new SOrder(wsUrl);
            OrdHeaderDto header = new OrdHeaderDto()
                                      {
                                          doc_Type = "ZTA",
                                          sales_Org = "7460",
                                          channel = "00",
                                          division = "03",
                                          ship_Cond = "01",
                                          country = "CO",
                                          currency = "COP",
                                          purch_Ord = "",
                                          order_Dte = "20130725",
                                          inbo_Outb = "",
                                          agent = "",
                                          cond_Man = "",
                                          cap_Min = "",
                                          cap_Max = "",
                                          ship_Method = ""
                                      };

            OrdItemDto[] items = new OrdItemDto[2]
                                     {
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "20018265",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F002"
                                             },
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "60003350",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F002"
                                             }
                                     };

            OrdPartnerDto[] partners = new OrdPartnerDto[1]
                                           {
                                               new OrdPartnerDto()
                                                   {
                                                       Part_Role = "SH",
                                                       Part_Number = "65301717"
                                                   }
                                           };

            SoParametersDto parameters = new SoParametersDto
                                             {
                                                 Header = header,
                                                 Item = items,
                                                 Partner = partners
                                             };


            var actual = CustomerStatementTest_LogicExec(wsUrl, "ITOPERCOL", "cemex2011", parameters);
            Assert.IsTrue(actual.ExceptionMessageList == null);
        }

        [TestMethod()]
        public void SOrderTest_Execution_Failure()
        {
            string wsUrl = "http://mxoccsapmrp02.noam.cemexnet.com:8000/sap/bc/srt/xip/sap/zsi_is_external_to_ordertaking/500/zs_ordtakingcrm/zb_ordtakingcrm"; // TODO: Initialize to an appropriate value

            SOrder target = new SOrder(wsUrl);
            OrdHeaderDto header = new OrdHeaderDto()
            {
                doc_Type = "ZTA",
                sales_Org = "7460",
                channel = "00",
                division = "03",
                ship_Cond = "01",
                country = "CO",
                currency = "COP",
                purch_Ord = "",
                order_Dte = "20130725",
                inbo_Outb = "",
                agent = "",
                cond_Man = "",
                cap_Min = "",
                cap_Max = "",
                ship_Method = ""
            };

            OrdItemDto[] items = new OrdItemDto[2]
                                     {
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "20018265",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F002"
                                             },
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "60003350",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F002"
                                             }
                                     };

            OrdPartnerDto[] partners = new OrdPartnerDto[1]
                                           {
                                               new OrdPartnerDto()
                                                   {
                                                       Part_Role = "SH",
                                                       Part_Number = ""
                                                   }
                                           };

            SoParametersDto parameters = new SoParametersDto
            {
                Header = header,
                Item = items,
                Partner = partners
            };



            var actual = CustomerStatementTest_LogicExec(wsUrl, "ITOPERCOL", "cemex2011", parameters);
            Assert.IsTrue(actual.ExceptionMessageList != null);
        }

        [TestMethod()]
        public void SOrderTest_Execution_Regular_Mock()
        {
            string wsUrl = "http://10.0.1.7:8088/mockZB_ORDTAKINGCRM"; // TODO: Initialize to an appropriate value

            SOrder target = new SOrder(wsUrl);
            OrdHeaderDto header = new OrdHeaderDto()
            {
                doc_Type = "ZTA",
                sales_Org = "7460",
                channel = "00",
                division = "03",
                ship_Cond = "01",
                country = "CO",
                currency = "COP",
                purch_Ord = "",
                order_Dte = "20130725",
                inbo_Outb = "",
                agent = "",
                cond_Man = "",
                cap_Min = "",
                cap_Max = "",
                ship_Method = ""
            };

            OrdItemDto[] items = new OrdItemDto[2]
                                     {
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "20018265",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F072"
                                             },
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "60003350",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F072"
                                             }
                                     };

            OrdPartnerDto[] partners = new OrdPartnerDto[1]
                                           {
                                               new OrdPartnerDto()
                                                   {
                                                       Part_Role = "SH",
                                                       Part_Number = "65603243"
                                                   }
                                           };

            SoParametersDto parameters = new SoParametersDto
            {
                Header = header,
                Item = items,
                Partner = partners
            };

            var actual = CustomerStatementTest_LogicExec(wsUrl, "ITOPERCOL", "cemex2011", parameters);

            var expected = new SoResponseDto()
                               {
                                   ExceptionMessageList = null,
                                   OutList = new SoRespOutList[2]
                                                 {
                                                    new SoRespOutList()
                                                        {                                                            
                                                            Item = "",
                                                            Material = "",
                                                            NetVal = 0,
                                                            NetValSpecified = true,
                                                            TaxVal = 0,
                                                            TaxValSpecified = true,
                                                            Currency = "",
                                                            SalesUnit = "",
                                                            DelDate = DateTime.Now,
                                                            DelDateSpecified = true,
                                                            SubTot = 304720400,
                                                            SubTotSpecified = true,
                                                            TaxTot = 0,
                                                            TaxTotSpecified = true,
                                                            ItemCat = "",
                                                            Plant = "",
                                                            ShipPoint = "",
                                                            Conditions = new SoRespCondList[1]
                                                            {
                                                                new SoRespCondList()
                                                                {
                                                                    Cond_Code ="",
                                                                    Cond_Name ="",
                                                                    Cond_Value ="",
                                                                    Cond_Type ="",
                                                                    Value_Cond ="",
                                                                    Cond_Curr ="",                                   
                                                                }
                                                            }

                                                        },
                                                    new SoRespOutList()
                                                        {
                                                            Item = "",
                                                            Material = "",
                                                            NetVal = 0,
                                                            NetValSpecified = true,
                                                            TaxVal = 0,
                                                            TaxValSpecified = true,
                                                            Currency = "",
                                                            SalesUnit = "",
                                                            DelDate = DateTime.Now,
                                                            DelDateSpecified = true,
                                                            SubTot = 0,
                                                            SubTotSpecified = true,
                                                            TaxTot = 0,
                                                            TaxTotSpecified = true,
                                                            ItemCat = "",
                                                            Plant = "",
                                                            ShipPoint = "",
                                                            Conditions = new SoRespCondList[1]
                                                            {
                                                                new SoRespCondList()
                                                                {
                                                                    Cond_Code ="",
                                                                    Cond_Name ="",
                                                                    Cond_Value ="",
                                                                    Cond_Type ="",
                                                                    Value_Cond ="",
                                                                    Cond_Curr ="",                                   
                                                                }
                                                            }

                                                        }
                                                 }
                               };


            Assert.AreEqual(expected.OutList[0].SubTot, actual.OutList[0].SubTot);
            Assert.AreEqual(expected.OutList[1].SubTot, actual.OutList[1].SubTot);
        }

        [TestMethod()]
        public void SOrderTest_Execution_Failure_Mock()
        {
            string wsUrl = "http://10.0.1.7:8080/mock_Failure"; // TODO: Initialize to an appropriate value
            SOrder target = new SOrder(wsUrl);
            OrdHeaderDto header = new OrdHeaderDto()
            {
                doc_Type = "ZTA",
                sales_Org = "7460",
                channel = "00",
                division = "03",
                ship_Cond = "01",
                country = "CO",
                currency = "COP",
                purch_Ord = "",
                order_Dte = "20130725",
                inbo_Outb = "",
                agent = "",
                cond_Man = "",
                cap_Min = "",
                cap_Max = "",
                ship_Method = ""
            };

            OrdItemDto[] items = new OrdItemDto[2]
                                     {
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "20018265",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F072"
                                             },
                                         new OrdItemDto()
                                             {
                                                 Item = "",
                                                 Material = "60003350",
                                                 Dlv_Grp = "",
                                                 Tar_Qty = "",
                                                 Tar_QU = "",
                                                 Req_Qty = "10",
                                                 Req_Dte = "",
                                                 Req_Time = "",
                                                 Item_typ = "",
                                                 Paym_Trm = "",
                                                 Plant = "F072"
                                             }
                                     };

            OrdPartnerDto[] partners = new OrdPartnerDto[1]
                                           {
                                               new OrdPartnerDto()
                                                   {
                                                       Part_Role = "SH",
                                                       Part_Number = ""
                                                   }
                                           };

            SoParametersDto parameters = new SoParametersDto
            {
                Header = header,
                Item = items,
                Partner = partners
            };

            var actual = CustomerStatementTest_LogicExec(wsUrl, "ITOPERCOL", "cemex2011", parameters);

            var expected = new SoResponseDto()
            {
                ExceptionMessageList = new SoRespErrorMsgList[1]
                                           {
                                               new SoRespErrorMsgList()
                                                   {
                                                       Type ="",
                                                       Id ="",
                                                       Number = "211",
                                                       Message ="",
                                                       MessageV1 ="",
                                                       MessageV2 ="",
                                                       MessageV3 ="",
                                                       MessageV4 =""
                                                   }
                                           },
                OutList = null
            };

            //var expected = new CsResponseDto()
            //{
            //    Exception = "SHIPTO_NOT_FOUND"
            //};
            Assert.IsTrue(actual.ExceptionMessageList != null);
            Assert.AreEqual(actual.ExceptionMessageList[0].Number, expected.ExceptionMessageList[0].Number);
        }

        public SoResponseDto CustomerStatementTest_LogicExec(string wsUrl, string usr, string password, SoParametersDto param)
        {
            //CustomerStatement target = new CustomerStatement(wsUrl);
            IWService<SoParametersDto, SoResponseDto> target = new SOrder(wsUrl);
            target.RequestParameters(param);
            target.Credentials(usr, password);
            return target.Execute();
        }



    }
}
