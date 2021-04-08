using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;
using SAP.Middleware.Connector;

namespace Persistence.SAP.Proxy
{
    public class BAPI_Access : ISAP_BAPI
    {

        


        private readonly ConfigParameters SAPConfig = new ConfigParameters();
        public String ConfigurationName { get; set; }

        public Dictionary<String, Object> Execute_VA01(VA01_DTO simulator)
        {
            try
            {
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                var rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;

                IRfcFunction customerList = rfcRep.CreateFunction("ABAP4_CALL_TRANSACTION");

                customerList.SetValue("TCODE", "VA01");
                customerList.SetValue("MODE_VAL", "N");
                customerList.SetValue("UPDATE_VAL", "S");


                IRfcTable USIGN_TAB = customerList.GetTable("USING_TAB");

                #region ClasePedido

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "0101");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "VBAK-AUART");
                USIGN_TAB.SetValue("FVAL", simulator.CPedido);

                #endregion

                #region OrgVentas

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "0101");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "VBAK-VKORG");
                USIGN_TAB.SetValue("FVAL", simulator.OrgVentas);

                #endregion

                #region Canal

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "0101");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "VBAK-VTWEG");
                USIGN_TAB.SetValue("FVAL", simulator.Canal);

                #endregion

                #region Sector

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "0101");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "VBAK-SPART");
                USIGN_TAB.SetValue("FVAL", simulator.Sector);

                #endregion


                #region Obra

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "4701");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "KUWEV-KUNNR");
                USIGN_TAB.SetValue("FVAL", simulator.CodObra);

                #endregion

                #region Fecha

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "4401");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "RV45A-KETDAT");
                USIGN_TAB.SetValue("FVAL", simulator.Fecha);//DD.MM.AAAA

                #endregion



                #region Material

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "4900");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "VBAP-MATNR");
                USIGN_TAB.SetValue("FVAL", simulator.Material);

                #endregion

                #region Volumen

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMV45A");
                USIGN_TAB.SetValue("DYNPRO", "4900");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "RV45A-KWMENG");
                USIGN_TAB.SetValue("FVAL", simulator.Vol);

                #endregion
                IRfcTable MESS_TAB = customerList.GetTable("MESS_TAB");

                customerList.Invoke(rfcDest);
                IRfcStructure Data = customerList.GetStructure("KOMP");
                /* IRfcFunction customprice = rfcRep.CreateFunction("RFC_GETPRICING");
                customprice.*/
                IEnumerator<IRfcField> list = Data.GetEnumerator();
                string[] strArray = null;
                int i = 0;

                while (list.MoveNext())
                {
                    strArray[i] = list.Current.GetValue().ToString();
                }

                /* DataTable dt = new DataTable();
                FillRowsFromSapTable(dt, Data);
                GetColumnsFromSapTable(dt, Data);*/

                var res = Data.Last().ToString();

                /* #region Guardar:

                USIGN_TAB.Append();
                USIGN_TAB.SetValue("PROGRAM", "SAPMZCX_CFIAR0MX104");
                USIGN_TAB.SetValue("DYNPRO", "0101");
                USIGN_TAB.SetValue("DYNBEGIN", "X");
                USIGN_TAB.SetValue("FNAM", "BDC_OKCODE");
                USIGN_TAB.SetValue("FVAL", "=SAVE");

                #endregion

                IRfcTable MESS_TAB = customerList.GetTable("MESS_TAB");

                customerList.Invoke(rfcDest);*/
            }
            catch (RfcLogonException le)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return new Dictionary<string, object>() { { "Response", true }, { "Message", "Success" } };
        }


        public Dictionary<String, Object> GetSAPPrecio(Int64 ObraId, Int64 MaterialId, int Volumen)
        {
            Dictionary<String, Object> response = null;
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "A502");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "KNUMH");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "(KSCHL EQ 'ZCAK' OR  KSCHL EQ 'ZCA1') AND ");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KUNWE EQ '" + CompleteCodes(ObraId.ToString(), 10) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "MATNR LIKE '%" + MaterialId + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATBI GT '" + (DateTime.Now.Year + (DateTime.Now.Month > 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day > 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATAB LE '" + (DateTime.Now.Year + (DateTime.Now.Month > 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day > 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "'");
                IRfcTable DATA = qry.GetTable("DATA");

                qry.Invoke(rfcDest);
                response = new Dictionary<string, object>();
                if (DATA.ToList().Count > 0)
                {
                    var result = DATA.ToList().First()["WA"];
                    var value = result.GetValue();
                    response.Add("Response", true);
                    response.Add("SAPKNUMHId", value.ToString());
                }
                else
                {
                    response.Add("Response", false);
                }
            }
            catch (RfcLogonException le)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);

            }
            return response;
        }


        public Dictionary<String, Object> GetSAPPrecioUnitario(Int64 ResponseId, Int64 MaterialId, int Volumen)
        {
            Dictionary<String, Object> response = null;
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "KONP");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "KBETR");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KNUMH EQ '" + CompleteCodes(ResponseId.ToString(), 10) + "'");
                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                response = new Dictionary<string, object>();
                if (DATA.ToList().Count > 0)
                {
                    var result = DATA.ToList().First()["WA"];
                    var value = result.GetValue();
                    response.Add("Response", true);
                    response.Add("SAPPrecio", Int64.Parse(value.ToString().Replace(".", "").Replace("-", "")) * Volumen / 1000);
                }
                else
                {
                    response.Add("Response", false);
                }
            }

            catch (RfcLogonException le)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }
        public List<ObraDTO> GetSAPPrecioMat(long Material,string Centro,string CondPago)
        {
            List<ObraDTO> response = new List<ObraDTO>();
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "A868");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "KNUMH");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "MATNR EQ '" + CompleteCodes(Material.ToString(), 18) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KSCHL EQ 'YCP1' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "VKORG EQ '7460' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "WERKS EQ '"+ Centro +"' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "ZTERM EQ '" + CondPago + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATBI GT '" + (DateTime.Now.Year + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATAB LE '" + (DateTime.Now.Year + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "'");

                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                if (DATA.ToList().Count > 0)
                {
                    for (int i = 0; i < DATA.ToList().Count; i++)
                    {
                        var result = DATA.ToList().ElementAt(i)["WA"];
                        var value = result.GetValue();
                        response.Add(new ObraDTO() { Contrato = value.ToString() });
                    }
                }
                else
                {
                    response.Add(new ObraDTO() { Contrato = "False" });
                }
            }

            catch (RfcLogonException le)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = le.Message } };
            }
            catch (Exception ex)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = ex.Message } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }

        public List<ObraDTO> GetSAPPrecioMat2(long Material, string Centro, string CondPago)
        {
            List<ObraDTO> response = new List<ObraDTO>();
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "A707");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "KNUMH");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "MATNR EQ '" + CompleteCodes(Material.ToString(), 18) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KSCHL EQ 'YCP1' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "VKORG EQ '7460' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "WERKS EQ '" + Centro + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "ZTERM EQ '" + CondPago + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATBI GT '" + (DateTime.Now.Year + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "DATAB LE '" + (DateTime.Now.Year + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())) + "'");

                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                if (DATA.ToList().Count > 0)
                {
                    for (int i = 0; i < DATA.ToList().Count; i++)
                    {
                        var result = DATA.ToList().ElementAt(i)["WA"];
                        var value = result.GetValue();
                        response.Add(new ObraDTO() { Contrato = value.ToString() });
                    }
                }
                else
                {
                    response.Add(new ObraDTO() { Contrato = "False" });
                }
            }

            catch (RfcLogonException le)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = le.Message } };
            }
            catch (Exception ex)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = ex.Message } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }
        public List<ObraDTO> GetSAPPrice(List<string> DocComercial)
        {
            List<ObraDTO> response = new List<ObraDTO>();
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "KONP");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "KBETR");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                foreach (var p in DocComercial)
                {
                    OPTIONS.Append();
                    OPTIONS.SetValue("TEXT", "KNUMH EQ '" + p + "' OR");
                }
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KNUMH EQ '"+DocComercial.Last()+"'");

                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                if (DATA.ToList().Count > 0)
                {
                    for (int i = 0; i < DATA.ToList().Count; i++)
                    {
                        var result = DATA.ToList().ElementAt(i)["WA"];
                        var value = result.GetValue();
                        response.Add(new ObraDTO() { Contrato = value.ToString() });
                    }
                }
                else
                {
                    response.Add(new ObraDTO() { Contrato = "False" });
                }
            }

            catch (RfcLogonException le)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = le.Message } };
            }
            catch (Exception ex)
            {

                return new List<ObraDTO>() { new ObraDTO() { Contrato = ex.Message } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }
     
        public Dictionary<String, Object> GetSAPIVA(Int64 ObraId)
        {
            Dictionary<String, Object> response = null;
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "KNVI");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "TAXKD");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KUNNR EQ '" + CompleteCodes(ObraId.ToString(), 10) + "'");
                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                response = new Dictionary<string, object>();
                if (DATA.ToList().Count > 0)
                {
                    var result = DATA.ToList().First()["WA"];
                    var value = result.GetValue();
                    response.Add("Response", true);
                    response.Add("SAPIVA", value.ToString());
                }
                else
                {
                    response.Add("Response", false);
                }
            }

            catch (RfcLogonException le)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }



        public DataTable  GetCargoLogisticoSAP (Int64 ObraId)
        {
            //var TD_TZONT = P.ReadTableDW("TZONT", '~', "LAND1,ZONE1,VTEXT", "LAND1 IN ('CO')", rfcDest, DefineDT.TZONT_DataBasic(), string.Empty, string.Empty);
            Dictionary<String, Object> response = null;
            DataTable DT = new DataTable();
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "KNVV");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "ZZFUELSURC");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "ZZDAMX33");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "KUNNR EQ '" + CompleteCodes(ObraId.ToString(), 10) + "' AND SPART EQ '03'");
                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                //DataTable DT = new DataTable();
                DT.Columns.Add("CargoCombustible");
                DT.Columns.Add("ZonaSuburbana");
                IRfcTable dataTable = qry.GetTable("DATA");
                string[] columns;
                foreach (var dataRow in dataTable)
                {
                    string data = (string)dataRow.GetValue("WA");
                    columns = data.Split('|');
                    DT.LoadDataRow(columns.ToArray(), true);
                }
                return DT;
            }

            catch (RfcLogonException le)
            {

                return DT;
            }
            catch (Exception ex)
            {

                return DT;
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return DT;

        }

        public Dictionary<String, Object> GetSAPIVAMat(Int64 MaterialId)
        {
            Dictionary<String, Object> response = null;
            try
            {
                var config = System.Configuration.ConfigurationManager.GetSection(ConfigurationName.Trim()) as NameValueCollection;
                var resultSeparator = config["ResultSeparator"].ToCharArray()[0];
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
                IRfcFunction qry = rfcRep.CreateFunction("RFC_READ_TABLE");
                qry.SetValue("QUERY_TABLE", "MLAN");
                qry.SetValue("DELIMITER", resultSeparator);
                IRfcTable FIELDS = qry.GetTable("FIELDS");
                FIELDS.Append();
                FIELDS.SetValue("FIELDNAME", "TAXM1");
                IRfcTable OPTIONS = qry.GetTable("OPTIONS");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "MATNR EQ '" + CompleteCodes(MaterialId.ToString(), 18) + "' AND");
                OPTIONS.Append();
                OPTIONS.SetValue("TEXT", "ALAND EQ 'CO'");
                IRfcTable DATA = qry.GetTable("DATA");
                qry.Invoke(rfcDest);
                response = new Dictionary<string, object>();
                if (DATA.ToList().Count > 0)
                {
                    var result = DATA.ToList().First()["WA"];
                    var value = result.GetValue();
                    response.Add("Response", true);
                    response.Add("SAPIVAMat", value.ToString());
                }
                else
                {
                    response.Add("Response", false);
                }
            }

            catch (RfcLogonException le)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {

                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return response;
        }

        public void Credentials(String user, String password)
        {
            this.SAPConfig.Credentials(user, password);
        }

        public Dictionary<String, Object> TestConnection()
        {
            try
            {
                RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
                var rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
                RfcRepository rfcRep = null;
                rfcRep = rfcDest.Repository;
            }
            catch (RfcLogonException le)
            {
                return new Dictionary<string, object>() { { "Response", false }, { "Message", le.Message } };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>() { { "Response", false }, { "Message", ex.Message + " : " + ex.StackTrace } };
            }
            finally
            {
                RfcDestinationManager.UnregisterDestinationConfiguration(this.SAPConfig);
            }
            return new Dictionary<string, object>() { { "Response", true }, { "Message", "Success" } };
        }

        private static String CompleteCodes(String Code, Int32 maxLenght)
        {
            if (Code != null)
            {
                var s = "";
                var length = (maxLenght - Code.Length) >= 0 ? (maxLenght - Code.Length) : 0;
                for (var i = 0; i < length; i++)
                {
                    s += "0";
                }
                Code = s + Code;
            }
            else
            {
                Code = "";
            }
            return Code;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="line"></param>
        public void Log(String context, String message, String folder, String name)
        {
            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(folder, name + String.Format("{0:yyyyMMdd}", DateTime.Today))))
                {
                    wLog(context, message, w);
                    w.Close();
                }
            }
            catch (Exception )
            {
                #region Error
                //if (Dts.TaskResult != (int)ScriptResults.Failure)
                //{
                // String Error = DateTime.Now.ToString() + "|" + "SAPFTP_LCF" + "|" + "Load_SAPDB_Clientes" + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + e.Message.ToString() + "|" + e.StackTrace.ToString();
                // Dts.Variables["Error"].Value = (Dts.Variables["Error"].Value.ToString() == String.Empty ? Error : Dts.Variables["Error"].Value.ToString() + ";" + Error);
                // Dts.TaskResult = (int)ScriptResults.Failure;
                //}
                throw;
                #endregion
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        /// 















        public DataTable ReadTableDW(string table, char delimiter, string fields, string filters, RfcDestination rfcDest, DataTable DT, string filters2, string filters3)
        {
            string[] field_names = fields.Split(",".ToCharArray());
            RfcDestinationManager.RegisterDestinationConfiguration(this.SAPConfig);
            // RfcDestination rfcDest = RfcDestinationManager.GetDestination(this.ConfigurationName);
            // RfcDestination destination = RfcDestinationManager.GetDestination(dest);
            IRfcFunction readTable = rfcDest.Repository.CreateFunction("BBP_RFC_READ_TABLE");
            // we want to query table KNA1
            readTable.SetValue("QUERY_TABLE", table);
            // fields will be separated by semicolon
            readTable.SetValue("DELIMITER", delimiter);
            // Parameter table FIELDS contains the columns you want to receive
            // here we query 2 fields, KUNNR and NAME1
            IRfcTable fieldsTable = readTable.GetTable("FIELDS");
            if (field_names.Length > 0)
            {
                fieldsTable.Append(field_names.Length);
                int i = 0;
                foreach (string n in field_names)
                {
                    fieldsTable.CurrentIndex = i++;
                    fieldsTable.SetValue(0, n);
                }
            }
            // the table OPTIONS contains the WHERE condition(s) of your query
            // here a single condition, KUNNR is to be 0012345600
            // several conditions have to be concatenated in ABAP syntax, for instance with AND or OR
            IRfcTable optsTable = readTable.GetTable("OPTIONS");
            optsTable.Append();
            optsTable.SetValue("TEXT", filters);
            if (filters2 != string.Empty)
            {
                optsTable.Append();
                optsTable.SetValue("TEXT", filters2);
            }
            if (filters3 != string.Empty)
            {
                optsTable.Append();
                optsTable.SetValue("TEXT", filters3);
            }

            readTable.Invoke(rfcDest);
            rfcDest = null;

            IRfcTable dataTable = readTable.GetTable("DATA");
            string[] columns;
            foreach (var dataRow in dataTable)
            {
                string data = (string)dataRow.GetValue("WA");
                columns = data.Split(delimiter);
                DT.LoadDataRow(columns.ToArray(), true);
            }

            return DT;
        }






        public void wLog(String CtxtLog, String logMessage, TextWriter w)
        {
            try
            {
                w.Write("\r\nContext Message: ");
                w.WriteLine(" {0}", CtxtLog);
                w.Write("\r\nLog Entry: ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                w.WriteLine(" :");
                w.WriteLine(" :{0}", logMessage);
                w.WriteLine("-------------------------------");
                // Update the underlying file.
                w.Flush();
            }
            catch (Exception )
            {
                #region Error
                //if (Dts.TaskResult != (int)ScriptResults.Failure)
                //{
                // String Error = DateTime.Now.ToString() + "|" + "SAPFTP_LCF" + "|" + "Load_SAPDB_Clientes" + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + e.Message.ToString() + "|" + e.StackTrace.ToString();
                // Dts.Variables["Error"].Value = (Dts.Variables["Error"].Value.ToString() == String.Empty ? Error : Dts.Variables["Error"].Value.ToString() + ";" + Error);
                // Dts.TaskResult = (int)ScriptResults.Failure;
                //}
                throw;
                #endregion

            }
        }

    }
}

