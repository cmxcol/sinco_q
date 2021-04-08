using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Infrastructure.Security.Usuario;
using WebApplication.Client.Web.App_Code;
using WebApplication.Client.Web.WSDL_CC_MRP;
using WebApplication.Client.Web.WSDL_SO_MRP;


namespace WebApplication.Client.Web.CO.PG.Usr
{
    public partial class MOrd : BPage
    {
        #region Atributos
        private String cnnCOS = System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString();
        private String cnnSAPDB = System.Configuration.ConfigurationManager.ConnectionStrings["cnnSAPDB"].ToString();
        #endregion

        #region Metodos

        /// <summary>
        /// OBTIENE LOS DATOS DEL CLIENTE
        /// </summary>
        public void DCliente()
        {
            try
            {
                lblErrorCod.Visible = false;
                SI_OS_CustomerStatementClient cli = new SI_OS_CustomerStatementClient("HTTP_Port");
                requestCustomerStatementRequest d = new requestCustomerStatementRequest();
                requestCustomerStatementResponse x = new requestCustomerStatementResponse();
                d.MT_CustomerStatementReq = new DT_CustomerStatementReq();
                d.MT_CustomerStatementReq.I_KUNNR = txtCodObra.Text.Trim();
                d.MT_CustomerStatementReq.I_VKORG = "7460";
                d.MT_CustomerStatementReq.I_VTWEG = "00";
                d.MT_CustomerStatementReq.I_SPART = (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim()));
                d.MT_CustomerStatementReq.I_FCURR = "COP";
                d.MT_CustomerStatementReq.I_TCURR = "COP";
                cli.ClientCredentials.UserName.UserName = "WSPIUSER";
                cli.ClientCredentials.UserName.Password = "Cemex2011";
                SI_OS_CustomerStatement inter2 = cli;
                x.MT_CustomerStatementResp = inter2.requestCustomerStatement(d).MT_CustomerStatementResp;
                if (x.MT_CustomerStatementResp.E_EXCEPTION == string.Empty | x.MT_CustomerStatementResp.E_EXCEPTION == null)
                {
                    txtCodObra.Enabled = false;
                    btnValidar.Enabled = false;
                    txtSector.Enabled = false;
                    DataTable dt = new DataTable();
                    DataTable dtEC = new DataTable();
                    DataTable dtSeg = new DataTable();
                    DataTable dtBl = new DataTable();
                    DataTable dtVen = new DataTable();
                    DataTable dtVenL = new DataTable();
                    DataTable dtExAll = new DataTable();
                    DataRow row;
                    if (x.MT_CustomerStatementResp.E_BITOC.Trim() != "" & txtCodObra.Text.Trim() != "")
                        CAlert("1;|2;" + x.MT_CustomerStatementResp.E_BITOC.Trim() + "|3;" + txtCodObra.Text.Trim());
                    dtSeg = (DataTable)ECmSp("SPSIUSEGCLI|@TCOM;4|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC) + "|@IDSCANAL;9000032821", cnnSAPDB, 1);
                    Session["CliEx"] = ECmSp("SPSIT_CE|@IDCON;1|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC), cnnCOS, 1);
                    dtExAll = (DataTable)ECmSp("SPSIT_CE|@IDCON;5|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC), cnnCOS, 1);
                    dtVen = (DataTable)ECmSp("SPS_R_OV|@TCOM;1|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDOBRA;" + txtCodObra.Text.Trim(), cnnSAPDB, 1);
                    if (dtVen.Rows.Count > 0)
                        dtVenL = (DataTable)ECmSp("SPSIU_VEN|@TCOM;1|@IDVEN;" + dtVen.Rows[0]["IdCodVen"].ToString(), cnnCOS, 1);
                    dtBl = (DataTable)ECmSp("SPSIT_BC|@TCOM;4|@IDCLIENTE;" + int.Parse(x.MT_CustomerStatementResp.E_BITOC).ToString() + "|@IDOBRA;" + txtCodObra.Text.Trim(), cnnCOS, 1);
                    dt.Columns.Add("NOMOBRA");
                    dt.Columns.Add("CODCLI");
                    dt.Columns.Add("NOMCLI");
                    dt.Columns.Add("FPAGO");
                    if (dtSeg.Rows.Count > 0) { dt.Columns.Add("NSEG"); }
                    if (dtVen.Rows.Count > 0) { dt.Columns.Add("VEN"); }
                    if (dtVenL.Rows.Count > 0) { dt.Columns.Add("TELVEN"); }
                    row = dt.NewRow();
                    row[0] = x.MT_CustomerStatementResp.E_CNAME;
                    row[1] = x.MT_CustomerStatementResp.E_BITOC;
                    row[2] = x.MT_CustomerStatementResp.E_BITON;
                    row[3] = x.MT_CustomerStatementResp.E_ZTERM;
                    if (dtSeg.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtSeg.Rows.Count - 1; i++)
                        {
                            if (Int64.Parse(dtSeg.Rows[i][0].ToString()) == 9000032821)
                            {
                                Session["IdSCanal"] = Int64.Parse(dtSeg.Rows[i][0].ToString());
                                row[4] = dtSeg.Rows[i][1];
                                break;
                            }
                            if (Int64.Parse(dtSeg.Rows[i][0].ToString()) == 9000032822)
                            {
                                Session["IdSCanal"] = Int64.Parse(dtSeg.Rows[i][0].ToString());
                                row[4] = dtSeg.Rows[i][1];
                                break;
                            }
                            if (i == dtSeg.Rows.Count - 1)
                            {
                                Session["IdSCanal"] = Int64.Parse(dtSeg.Rows[i][0].ToString());
                                row[4] = dtSeg.Rows[i][1];
                            }
                        }
                        gv_DataCli.Columns[4].Visible = true;
                    }
                    if (dtVen.Rows.Count > 0)
                    {
                        if (dtSeg.Rows.Count > 0)
                        {
                            row[5] = dtVen.Rows[0]["NVen"];
                            gv_DataCli.Columns[5].Visible = true;
                            if (dtVenL.Rows.Count > 0)
                            {
                                row[6] = dtVenL.Rows[0]["Tel"];
                                gv_DataCli.Columns[6].Visible = true;
                            }
                        }
                        else
                        {
                            row[4] = dtVen.Rows[0]["NVen"];
                            gv_DataCli.Columns[5].Visible = true;
                            if (dtVenL.Rows.Count > 0)
                            {
                                row[5] = dtVenL.Rows[0]["Tel"];
                                gv_DataCli.Columns[6].Visible = true;
                            }
                        }
                    }

                    gvBlCli.DataSource = dtBl;
                    gvBlCli.DataBind();

                    gv_ExCli.DataSource = dtExAll;
                    gv_ExCli.DataBind();

                    dt.Rows.Add(row);
                    gv_DataCli.DataSource = dt;
                    gv_DataCli.DataBind();
                    gv_DataCli.Visible = true;


                    Boolean val = false;
                    DateTime date;
                    var exE = ((DataTable)Session["CliEx"]).AsEnumerable();
                    if (exE.Count() > 0)
                    {
                        foreach (var ex in exE)
                        {
                            if (ex["IdTEx"].ToString().Trim() == "PMT")
                            {
                                val = true;
                                break;
                            }
                            if (DateTime.TryParse(ex["dtVig"].ToString(), out date))
                            {
                                if ((DateTime.Parse(ex["dtVig"].ToString()) >= DateTime.Today))
                                {
                                    val = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (val)
                    {
                        lblEC.Text = "Excepciones Activas.";
                        //lblECT.Text = "Fecha de vigencia: " + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(((DataTable)Session["CliEx"]).Rows[0][2].ToString())) + ".";                        
                        try
                        {
                            var res = (from ex in exE
                                       where (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? DateTime.Parse(ex["dtVig"].ToString()) >= DateTime.Today : true)
                                       select new { Excepción = ex["NTEx"] as string, FechaVigencia = (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? String.Format("{0:yyyy-MM-dd}", ex["dtVig"]) : ex["dtVig"].ToString()), MsgEx = ex["MsgEx"] as string }).ToList();

                            //DateTime.Parse(ex["dtVig"].ToString()) >= DateTime.Today
                            gv_ExClientes.DataSource = res;
                            gv_ExClientes.DataBind();
                            gv_ExClientes.Visible = true;
                            lblAPG.Text = "Puede proceder con la programación en GINCO!!!";
                            lblEC.Visible = true;
                            lblECT.Visible = true;
                            lblAPG.CssClass = "labelInfo";
                            lblAPG.Visible = true;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        btnNCal.Visible = false;
                        lblAPG.Visible = false;
                        lblEC.Visible = false;
                        lblECT.Visible = false;
                        dtEC.Columns.Add("CUPO");
                        dtEC.Columns.Add("SFAVOR");
                        dtEC.Columns.Add("CTOTAL");
                        dtEC.Columns.Add("CMPSAP");
                        dtEC.Columns.Add("CMPGINCO");
                        dtEC.Columns.Add("TSCUPO");
                        row = dtEC.NewRow();

                        row[0] = DNum(x.MT_CustomerStatementResp.E_LIMCR) * 100 * 1.10;
                        row[1] = DNum(x.MT_CustomerStatementResp.E_SLDAF);
                        row[2] = DNum(x.MT_CustomerStatementResp.E_CARTT);
                        //row[3] = (x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ',').Contains('-') ? (double.Parse(x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ',').Replace('-', ' ')) * -1) : double.Parse(x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ','))) * 100;
                        row[3] = DNum(x.MT_CustomerStatementResp.E_IPFAC) * 100;
                        row[4] = CmpGinCliente(int.Parse(dt.Rows[0]["CODCLI"].ToString()));

                        row[5] = double.Parse(row[0].ToString()) + (double.Parse(row[1].ToString()) * (-1));
                        row[5] = double.Parse(row[5].ToString()) - double.Parse(row[2].ToString());
                        row[5] = double.Parse(row[5].ToString()) - double.Parse(row[3].ToString());
                        row[5] = double.Parse(row[5].ToString()) - double.Parse(row[4].ToString());

                        dtEC.Rows.Add(row);

                        Session["EstadoCuenta"] = dtEC;
                        gv_EC_Cli.DataSource = dtEC;
                        gv_EC_Cli.DataBind();
                        gv_EC_Cli.Visible = true;
                        GvStyle(2, dtEC);

                        ///////////////////////////////////////////////////
                        if (double.Parse(row[5].ToString()) < 0)
                        {
                            btnAC.Visible = true;
                            lblSN.Visible = true;
                            lblANP.Text = "NO EFECTUAR LA PROGRAMACIÓN SI NO SE REGISTRA UN AJUSTE!!";
                            lblANP.Visible = true;
                        }
                        else
                        {
                            lblSN.Visible = false;
                            //lblAC.Visible = false;
                            pnlAC.Visible = false;
                            if (int.Parse(txtSector.Text) == 3)
                            {
                                DataTable dtAdit = new DataTable();
                                dtAdit = CrgDdL();
                                ddlAD1.DataSource = dtAdit;
                                ddlAD1.DataTextField = "DESCRIPCION";
                                ddlAD1.DataValueField = "IDPRODUCTO";
                                ddlAD1.DataBind();
                                ddlAD1.Items.Insert(0, new ListItem("Seleccione...", "0"));
                                ddlAD1.Items[0].Selected = true;

                                ddlAD2.DataSource = dtAdit;
                                ddlAD2.DataTextField = "DESCRIPCION";
                                ddlAD2.DataValueField = "IDPRODUCTO";
                                ddlAD2.DataBind();
                                ddlAD2.Items.Insert(0, new ListItem("Seleccione...", "0"));
                                ddlAD2.Items[0].Selected = true;

                                ddlAD3.DataSource = dtAdit;
                                ddlAD3.DataTextField = "DESCRIPCION";
                                ddlAD3.DataValueField = "IDPRODUCTO";
                                ddlAD3.DataBind();
                                ddlAD3.Items.Insert(0, new ListItem("Seleccione...", "0"));
                                ddlAD3.Items[0].Selected = true;

                                pnlDatos.Visible = true;
                                pnlAditivos.Visible = true;
                                pnlAB.Visible = true;

                                btnCal.Visible = true;
                                //btnNCal.Visible = true;
                            }
                            else
                            {
                                //pnlDatos.Visible = true;
                                lblPed_OG.Text = "Valor de los Pedidos";
                                pnlPedidos.Visible = true;
                            }
                            //btnCal.Visible = true;
                            btnNCal.Visible = true;
                        }
                    }

                }
                else
                {
                    pnlDatos.Visible = false;
                    pnlAditivos.Visible = false;
                    pnlAB.Visible = false;
                    gv_DataCli.Visible = false;
                    gv_EC_Cli.Visible = false;
                    lblErrorCod.Visible = true;
                }
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// CONSULTA LOS ADITIVOS
        /// </summary>
        public DataTable CrgDdL()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NomParam");
            dt.Columns.Add("ValParam");
            DataRow row = dt.NewRow();
            row[0] = "@IDCOM";
            row[1] = 5;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row[0] = "@IDUM";
            row[1] = "EA";
            dt.Rows.Add(row);
            return ConSp("cat.SPSIUDM", 1, dt, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString());
        }
        /// <summary>
        /// REALIZA EL CALCULO DEL ESTADO DE CUENTA REAL DE UN CLIENTE
        /// </summary>
        public double CmpGinCliente(int codigo)
        {
            try
            {
                DataTable dtPg = new DataTable();
                dtPg.Columns.Add("NomParam");
                dtPg.Columns.Add("ValParam");
                DataRow row = dtPg.NewRow();
                row[0] = "@IDCOM";
                row[1] = 1;
                dtPg.Rows.Add(row);
                row = dtPg.NewRow();
                row[0] = "@IDCLIENTE";
                row[1] = codigo;
                dtPg.Rows.Add(row);
                DataTable dt = new DataTable();
                Double cmpGin = 0;
                String strCon = "SPSTOC";
                dt = ConSp(strCon, 1, dtPg, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString());
                //String strCon = "SELECT OBRA,PEDIDO,NOKIT,PLANTA,VOLUMEN,UNIDAD FROM TAB_GINCO_PEDIDOS WHERE CLIENTE = " + codigo;
                //dt = ConSp(strCon, 0, new DataTable(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString());
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    cmpGin = cmpGin + DNum(((SOrder(dt.Rows[i], (dt.Rows[i]["IdSector"].ToString().Trim() == "" ? "0" : (dt.Rows[i]["IdSector"].ToString().Trim().Length == 1 & int.Parse(dt.Rows[i]["IdSector"].ToString()) < 10 ? "0" + dt.Rows[i]["IdSector"].ToString().Trim() : dt.Rows[i]["IdSector"].ToString().Trim()))).MessageOutList == null ? SOrder(dt.Rows[i], (dt.Rows[i]["IdSector"].ToString().Trim() == "" ? "0" : (dt.Rows[i]["IdSector"].ToString().Trim().Length == 1 & int.Parse(dt.Rows[i]["IdSector"].ToString()) < 10 ? "0" + dt.Rows[i]["IdSector"].ToString().Trim() : dt.Rows[i]["IdSector"].ToString().Trim()))).ItemOutList[0].SubTot / 100 : 0)).ToString());
                }
                return cmpGin;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GvStyle(int gv, DataTable dt)
        {
            if (gv == 1)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //dt.Rows[i][5] = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][5].ToString()));
                    //dt.Rows[i][6] = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][6].ToString()));
                    //gv_SOrder.Rows[i].Cells[5].Text = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][5].ToString()));
                    gv_SOrder.Rows[i].Cells[3].Text = String.Format("{0:$ #,#}", double.Parse(gv_SOrder.Rows[i].Cells[3].Text));
                    gv_SOrder.Rows[i].Cells[4].Text = String.Format("{0:$ #,#}", double.Parse(gv_SOrder.Rows[i].Cells[4].Text));
                }
            }
            if (gv == 2)
            {
                gv_EC_Cli.Rows[0].Cells[0].ForeColor = (double.Parse(gv_EC_Cli.Rows[0].Cells[0].Text) < 0 ? Color.Red : Color.Green);
                gv_EC_Cli.Rows[0].Cells[1].ForeColor = (double.Parse(gv_EC_Cli.Rows[0].Cells[1].Text) < 0 ? Color.Green : Color.Red);
                gv_EC_Cli.Rows[0].Cells[2].ForeColor = Color.Red;
                gv_EC_Cli.Rows[0].Cells[3].ForeColor = Color.Red;
                gv_EC_Cli.Rows[0].Cells[4].ForeColor = Color.Red;
                gv_EC_Cli.Rows[0].Cells[5].ForeColor = (double.Parse(gv_EC_Cli.Rows[0].Cells[5].Text) < 0 ? Color.Red : Color.Green);
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    if (double.Parse(gv_EC_Cli.Rows[0].Cells[i].Text) == 0)
                    {
                        gv_EC_Cli.Rows[0].Cells[i].Text = "-";
                    }
                    else
                    {
                        gv_EC_Cli.Rows[0].Cells[i].Text = String.Format("{0:$ #,#}", double.Parse(gv_EC_Cli.Rows[0].Cells[i].Text));
                    }
                }
            }
        }
        public DataTable ConSp(String strComm, int comT, DataTable dtParam, String strCnn)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(strCnn))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = new SqlCommand();
                    adp.SelectCommand.CommandText = strComm;
                    adp.SelectCommand.CommandTimeout = 0;
                    adp.SelectCommand.Connection = cnn;
                    if (comT == 1)
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= dtParam.Rows.Count - 1; i++)
                        {
                            adp.SelectCommand.Parameters.AddWithValue(dtParam.Rows[i][0].ToString(), dtParam.Rows[i][1]);
                        }
                    }
                    cnn.Open();
                    adp.SelectCommand.ExecuteNonQuery();
                    cnn.Close();
                    adp.Fill(dt);
                }
            }
            return dt;
        }
        public void InUpSp(String strComm, int comT, DataTable dtParam, String strCnn)
        {
            using (SqlConnection cnn = new SqlConnection(strCnn))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = new SqlCommand();
                    adp.SelectCommand.CommandText = strComm;
                    adp.SelectCommand.CommandTimeout = 0;
                    adp.SelectCommand.Connection = cnn;
                    if (comT == 1)
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= dtParam.Rows.Count - 1; i++)
                        {
                            adp.SelectCommand.Parameters.AddWithValue(dtParam.Rows[i][0].ToString(), dtParam.Rows[i][1]);
                        }
                    }
                    cnn.Open();
                    adp.SelectCommand.ExecuteNonQuery();
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// EJECUTA UN PROCEDIMIENTO ALMACENADO EN BASE A LA CONEXION y PARAMETROS DADOS
        /// </summary>
        /// <param name="strParam"></param>
        /// <param name="strCon"></param>
        /// <returns></returns>
        public Object ECmSp(String strParam, String strCon, int tCom)
        {
            try
            {
                DataTable dt = new DataTable();
                String[] aParam = strParam.Split('|');
                int resEx = 0;
                int vInt;
                Int64 vInt64;
                using (SqlConnection cnn = new SqlConnection(strCon))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {

                        adp.SelectCommand = new SqlCommand();
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //adp.SelectCommand.CommandText = strParam.Split('|')[0].ToString();
                        adp.SelectCommand.CommandText = aParam[0].ToString();
                        adp.SelectCommand.CommandTimeout = 0;
                        adp.SelectCommand.Connection = cnn;
                        if (aParam.Length > 2)
                        {
                            for (int i = 0; i <= aParam.Length - 1; i++)
                            {
                                if (i != 0)
                                {
                                    if (int.TryParse(aParam[i].Split(';')[1].ToString(), out vInt))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), int.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else if (Int64.TryParse(aParam[i].Split(';')[1].ToString(), out vInt64))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), Int64.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), aParam[i].Split(';')[1].ToString().Trim());
                                    }
                                }
                            }
                        }
                        if (tCom != 2)
                        {
                            cnn.Open();
                            adp.SelectCommand.ExecuteNonQuery();
                            cnn.Close();
                        }
                        if (tCom == 1)
                        {
                            adp.Fill(dt);
                        }
                        else if (tCom == 2)
                        {
                            cnn.Open();
                            resEx = (int)adp.SelectCommand.ExecuteScalar();
                            cnn.Close();
                        }
                    }
                }
                if (tCom == 1)
                {
                    return dt;
                }
                else if (tCom == 2)
                {
                    return resEx;
                }
                else
                {
                    return new Object();
                }
            }
            catch (Exception )
            {
                //String Error = DateTime.Now.ToString() + "|" + "SAPFTPLOAD" + "|" + "Load_SAPDB_Pedidos_SMS" + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + e.Message.ToString() + "|" + e.StackTrace.ToString();
                //Dts.Variables["Error"].Value = (Dts.Variables["Error"].Value.ToString() == String.Empty ? Error : Dts.Variables["Error"].Value.ToString() + ";" + Error);
                //Dts.TaskResult = (int)ScriptResults.Failure;
                throw;
            }

        }
        public DT_ResponseSimulateOrderTaking SOrder(DataRow row, String idSector)
        {
            WSDL_SO_MRP.DT_SimulateHeader DT_SH = new WSDL_SO_MRP.DT_SimulateHeader();
            WSDL_SO_MRP.DT_SimulateItemCRM DT_SICRM = new WSDL_SO_MRP.DT_SimulateItemCRM();
            WSDL_SO_MRP.DT_SimulatePartner DT_SP = new WSDL_SO_MRP.DT_SimulatePartner();
            WSDL_SO_MRP.SI_IS_SimulateOrderTakingRequest SI_IS_SOTRQ = new WSDL_SO_MRP.SI_IS_SimulateOrderTakingRequest();
            WSDL_SO_MRP.SI_IS_SimulateOrderTakingResponse SI_IS_SOTRP = new WSDL_SO_MRP.SI_IS_SimulateOrderTakingResponse();
            WSDL_SO_MRP.DT_RequestSimulateOrderTakingCRM DT_RQSOTCRM = new WSDL_SO_MRP.DT_RequestSimulateOrderTakingCRM();
            WSDL_SO_MRP.DT_ResponseSimulateOrderTaking DT_RPSOT = new WSDL_SO_MRP.DT_ResponseSimulateOrderTaking();
            WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRMClient SI_EOTCRMCLI = new WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRMClient("ZB_ORDTAKINGCRM");
            WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRM SI_ISEOTCRM_I;

            // Header
            DT_SH.Doc_Type = "ZTA";
            DT_SH.Sales_Org = "7460";
            DT_SH.Channel = "00";
            DT_SH.Division = idSector;
            DT_SH.Ship_Cond = "01";
            DT_SH.Country = "CO";
            DT_SH.Currency = "COP";
            //DT_SH.Purch_Ord = "8000000523";
            DT_SH.Purch_Ord = "";
            DT_SH.Order_Dte = String.Format("{0:yyyyMMdd}", DateTime.Now);
            //DT_SH.Order_Dte = "20120111";
            DT_SH.Inbo_Outb = "";
            DT_SH.Agent = "";
            DT_SH.Cond_Man = "";
            DT_SH.Cap_Min = "";
            DT_SH.Cap_Max = "";

            //Item
            DT_SICRM.Item = "10";
            DT_SICRM.Material = row[2].ToString().Trim();
            DT_SICRM.Dlv_Grp = "";
            DT_SICRM.Tar_Qty = "";
            DT_SICRM.Tar_QU = "";
            DT_SICRM.Req_Qty = row[4].ToString().Replace(',', '.');
            DT_SICRM.Req_Dte = "";
            DT_SICRM.Req_Time = "";
            DT_SICRM.Item_typ = "";
            //DT_SICRM.Paym_Trm = "ZCON";
            DT_SICRM.Paym_Trm = "";
            DT_SICRM.Plant = row[3].ToString().Trim().ToUpper();

            //Partners
            DT_SP.Part_Role = "SH";
            DT_SP.Part_Number = row[0].ToString().Trim();

            //Input
            DT_RQSOTCRM.Header = DT_SH;
            DT_RQSOTCRM.Item = new DT_SimulateItemCRM[1];
            DT_RQSOTCRM.Item[0] = DT_SICRM;
            DT_RQSOTCRM.Partners = new DT_SimulatePartner[1];
            DT_RQSOTCRM.Partners[0] = DT_SP;

            //Request
            SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM = DT_RQSOTCRM;

            //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Header = new DT_SimulateHeader();
            //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Item = new DT_SimulateItemCRM[0];
            //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Partners = new DT_SimulatePartner[0];

            SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Header = DT_SH;
            SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Item[0] = DT_SICRM;
            SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Partners[0] = DT_SP;

            SI_EOTCRMCLI.ClientCredentials.UserName.UserName = "ITOPERCOL";
            SI_EOTCRMCLI.ClientCredentials.UserName.Password = "cemex2011";

            //SI_EOTCRMCLI.ClientCredentials.UserName.UserName = "E0RASANDOV";
            //SI_EOTCRMCLI.ClientCredentials.UserName.Password = "ramiro11";

            SI_ISEOTCRM_I = SI_EOTCRMCLI;

            DT_RPSOT = SI_EOTCRMCLI.SI_IS_SimulateOrderTaking(DT_RQSOTCRM);

            //DT_RPSOT.MessageOutList = new DT_SimulateMessa[1];

            if (DT_RPSOT.MessageOutList != null)
            {
                return DT_RPSOT;
            }
            else
            {
                //return Double.Parse((DT_RPSOT.ItemOutList[0].SubTot/100).ToString());
                return DT_RPSOT;
            }
        }
        public void CAlert(String strP)
        {
            DataTable dtF = new DataTable();
            DataTable dt = new DataTable();
            DataSet dtS = new DataSet();
            for (int i = 0; i < strP.Split('|').Length; i++)
            {
                if (strP.Split('|')[i].Split(';')[1] == "")
                {
                    dt = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;6|@TTAB;5|@IDSTARG;1|@IDTCRI;" + strP.Split('|')[i].Split(';')[0] + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, cnnCOS, 1);
                    if (dt.Rows.Count > 0)
                    {
                        dtS.Tables.Add(dt);
                    }
                }
                else
                {
                    dt = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;6|@TTAB;5|@IDSTARG;1|@IDTCRI;" + strP.Split('|')[i].Split(';')[0] + "|@IDCRI;" + strP.Split('|')[i].Split(';')[1] + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, cnnCOS, 1);
                    if (dt.Rows.Count > 0)
                    {
                        dtS.Tables.Add(dt);
                    }
                }
            }
            if (dtS.Tables.Count > 0)
            {
                dtF = Union(dtS);

                Session["dtA"] = dtF;

                gvAlSh.DataSource = dtF;
                gvAlSh.DataBind();
            }
        }
        public static DataTable Union(DataSet dtS)
        {
            DataTable dtU = new DataTable("Union");
            DataColumn[] aCol = new DataColumn[dtS.Tables[0].Columns.Count];
            for (int i = 0; i < dtS.Tables[0].Columns.Count; i++)
            {
                aCol[i] = new DataColumn(dtS.Tables[0].Columns[i].ColumnName, dtS.Tables[0].Columns[i].DataType);
            }
            dtU.Columns.AddRange(aCol);
            dtU.BeginLoadData();
            for (int i = 0; i < dtS.Tables.Count; i++)
            {
                foreach (DataRow row in dtS.Tables[i].Rows)
                {
                    dtU.LoadDataRow(row.ItemArray, true);
                }
            }
            dtU.EndLoadData();
            return dtU;
        }
        public Int64 Num(String val)
        {
            var a = int.Parse(val.Contains('.') ? val.Remove(val.IndexOf('.')) : (val.Contains(',') ? val.Remove(val.IndexOf(',')) : val));
            a = (val.Contains('-') ? a * -1 : a);
            return a;
        }
        public Double DNum(String val)
        {
            var a = (val.Contains('-') ? val.Replace('-', ' ').Trim() : val);
            Double b;
            if (val.Contains('.'))
            {
                b = Double.Parse((Int64)(Double.Parse(a)) < (Int64)(Double.Parse(a.Replace('.', ','))) ? a : a.Replace('.', ','));
            }
            else if (val.Contains(','))
            {
                b = Double.Parse((Int64)(Double.Parse(a)) < (Int64)(Double.Parse(a.Replace(',', '.'))) ? a : a.Replace(',', '.'));
            }
            else
            {
                b = Double.Parse(a);
            }
            b = (val.Contains('-') ? b * -1 : b);
            return b;
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("txt");
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("Valor");
                DataRow rw = dt.NewRow();
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    rw[i] = "##";
                }
                dt.Rows.Add(rw);
                Session["DVPedIni"] = dt;
                Session["DVPed"] = dt;
                gv_Ped.DataSource = dt;
                gv_Ped.DataBind();
                gv_ValPed.DataSource = dt;
                gv_ValPed.DataBind();
            }
        }
        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Numbers");
                if (Page.IsValid)
                {
                    //txtCodObra.Enabled = false;
                    DCliente();
                }
            }
            catch (Exception )
            {
                throw;
            }
        }
        protected void btnNCal_Click(object sender, EventArgs e)
        {
            Session["DVPed"] = null;
            Session["DVPedIni"] = null;
            Session["tReg"] = null;
            Session["tReg_OG"] = null;
            Session["SumPed_OG"] = null;
            Session["RANew"] = null;
            Session["New"] = null;
            Session["EstadoCuenta"] = null;
            Session["SmPed"] = null;
            Response.Redirect("~/CO/PG/Usr/ProgPedidos.aspx");
        }
        protected void btnCal_Click(object sender, EventArgs e)
        {
            Page.Validate("Numbers");
            if (Page.IsValid & txtCodMat.Text != String.Empty & txtVol.Text != String.Empty)
            {
                DataTable dtdatos = new DataTable();
                dtdatos.Columns.Add("CodObra");
                dtdatos.Columns.Add("IdPedido");
                dtdatos.Columns.Add("IdMaterial");
                dtdatos.Columns.Add("IdCentro");
                dtdatos.Columns.Add("Cantidad");
                dtdatos.Columns.Add("PBase");
                dtdatos.Columns.Add("PNeto");
                Double ValSO = 0;
                DataRow row = dtdatos.NewRow();
                DataRow rowPB = dtdatos.NewRow();
                if (int.Parse(txtSector.Text) == 3)
                {
                    //"OBRA,PEDIDO,NOKIT,PLANTA,VOLUMEN ";
                    String strIdProduct = txtCodMat.Text + (chbBom.Checked == true ? "|" + "60002577" : "");
                    strIdProduct = strIdProduct + (int.Parse(ddlAD1.SelectedValue) != 0 ? "|" + ddlAD1.SelectedValue : "");
                    strIdProduct = strIdProduct + (int.Parse(ddlAD2.SelectedValue) != 0 ? "|" + ddlAD2.SelectedValue : "");
                    strIdProduct = strIdProduct + (int.Parse(ddlAD3.SelectedValue) != 0 ? "|" + ddlAD3.SelectedValue : "");
                    String[] arrayIdProduct = strIdProduct.Split('|');
                    DT_ResponseSimulateOrderTaking DT_RSOT = new DT_ResponseSimulateOrderTaking();
                    for (int i = 0; i <= arrayIdProduct.Length - 1; i++)
                    {
                        row[0] = txtCodObra.Text;
                        row[1] = "1";
                        row[2] = arrayIdProduct[i];
                        row[3] = txtCentro.Text.ToUpper();
                        //row[4] = (chbAj.Checked == true ? Double.Parse(txtVol.Text.Replace('.', ',')) * 1.10 : Double.Parse(txtVol.Text.Replace('.', ',')));
                        row[4] = (chbAj.Checked ? DNum(txtVol.Text) * 1.10 : DNum(txtVol.Text));

                        DT_RSOT = SOrder(row, (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim())));
                        if (DT_RSOT.MessageOutList == null)
                        {
                            //row[6] = Double.Parse((DT_RSOT.ItemOutList[0].SubTot / 100).ToString());
                            row[6] = DNum((DT_RSOT.ItemOutList[0].SubTot / 100).ToString());
                            rowPB[0] = txtCodObra.Text;
                            rowPB[1] = "1";
                            rowPB[2] = arrayIdProduct[i];
                            rowPB[3] = txtCentro.Text.ToUpper();
                            rowPB[4] = 1;
                            //row[5] = Double.Parse((SOrder(rowPB).ItemOutList[0].NetVal / 100).ToString());
                            row[5] = DNum((SOrder(rowPB, (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim()))).ItemOutList[0].NetVal / 100).ToString());
                            dtdatos.Rows.Add(row);
                            row = dtdatos.NewRow();
                            rowPB = dtdatos.NewRow();

                            if (txtCodMat.Text == DNum(DT_RSOT.ItemOutList[0].Material).ToString())
                            {
                                DT_ResponseSimulateOrderTaking DT_RSOT_U = new DT_ResponseSimulateOrderTaking();

                                DataRow rw = dtdatos.NewRow();
                                rw[0] = txtCodObra.Text;
                                rw[1] = "1";
                                rw[2] = txtCodMat.Text;
                                rw[3] = txtCentro.Text.ToUpper();
                                rw[4] = "1";
                                DT_RSOT_U = SOrder(rw, (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim())));
                                if (DT_RSOT_U.MessageOutList == null)
                                {
                                    var condition = DT_RSOT_U.ItemOutList.ToList()[0].Conditions.ToList();
                                    List<String> Cond = new List<String>() { "ZCK4", "ZCA1", "ZCK3", "ZCA3", "ZCST" };

                                    var res = (from c in condition
                                               where Cond.Contains(c.Cond_Code.Trim())
                                               select new { Codigo = c.Cond_Code, Nombre = c.Cond_Name, Valor = String.Format("{0:$ #,#}", Decimal.Round(Decimal.Parse((DNum(c.Value_Cond) / 100).ToString()), 0)) }).ToList();

                                    if (res.Count() > 0)
                                    {
                                        gv_Condition.DataSource = res;
                                        gv_Condition.DataBind();
                                        gv_Condition.Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (DT_RSOT.MessageOutList == null)
                    {
                        gv_SOrder.DataSource = dtdatos;
                        gv_SOrder.DataBind();
                        gv_SOrder.Visible = true;
                        GvStyle(1, dtdatos);
                        //for (int i = 0; i <= dtdatos.Rows.Count - 1; i++) { ValSO = ValSO + Double.Parse((DT_RSOT.ItemOutList[0].SubTot / 100).ToString());}
                        //for (int i = 0; i <= dtdatos.Rows.Count - 1; i++) { ValSO = ValSO + Double.Parse(((DataTable)Session["SOrder"]).Rows[0][6].ToString()); }
                        for (int i = 0; i <= dtdatos.Rows.Count - 1; i++) { ValSO = ValSO + Double.Parse((dtdatos.Rows[i][6].ToString())); }
                        lblMsgVSO.Text = "El TOTAL del pedido a programar es:" + String.Format("{0:$ #,#}", ValSO);
                        //lblVSO.Text = String.Format("{0:$ #,#}", ValSO);
                        lblMsgSF.Text = "SALDO =" + String.Format("{0:$ #,#}", Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - ValSO);
                        //lblSF.Text = String.Format("{0:$ #,#}", Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][4].ToString()) - ValSO);

                        Session["SmPed"] = dtdatos.Copy();
                        ((DataTable)Session["SmPed"]).Columns.Add("ValTPed");
                        for (var i = 0; i < ((DataTable)Session["SmPed"]).Rows.Count; i++)
                        {
                            ((DataTable)Session["SmPed"]).Rows[i]["Cantidad"] = DNum(txtVol.Text);
                            ((DataTable)Session["SmPed"]).Rows[i]["ValTPed"] = ValSO;

                        }

                        if ((Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - ValSO) >= 0)
                        {
                            btnASC.Visible = false;
                            lblAP.Visible = false;
                            lblMsgSF.CssClass = "labelInfo";
                            lblAPG.Text = "Puede proceder con la programación en GINCO";
                            lblAPG.CssClass = "labelInfo";
                            btnRegP.Visible = true;
                        }
                        else
                        {
                            btnRegP.Visible = false;
                            lblMsgSF.CssClass = "labelError";
                            lblAPG.Text = "El valor del pedido es superior al saldo disponible.";
                            lblAPG.CssClass = "labelError";
                            lblAP.Text = "NO EFECTUAR LA PROGRAMACIÓN SI NO SE REGISTRA UN AJUSTE!!";
                            lblAP.CssClass = "labelError";
                            lblAP.Visible = true;
                            btnASC.Visible = true;
                        }
                        lbl_E_SO.Visible = false;
                        lblMsgVSO.Visible = true;
                        lblMsgSF.Visible = true;
                        lblAPG.Visible = true;
                    }
                    else
                    {
                        lbl_E_SO.Text = DT_RSOT.MessageOutList[0].Message;
                        lbl_E_SO.Visible = true;
                        lblMsgVSO.Visible = false;
                        lblMsgSF.Visible = false;
                        lblAPG.Visible = false;
                        gv_SOrder.Visible = false;
                    }
                }
                else
                {
                    row[0] = txtCodObra.Text;
                    row[1] = "1";
                    row[2] = txtCodMat.Text;
                    row[3] = txtCentro.Text.ToUpper();
                    row[4] = txtVol.Text;
                    DT_ResponseSimulateOrderTaking DT_RSOT = new DT_ResponseSimulateOrderTaking();
                    DT_RSOT = SOrder(row, (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim())));
                    if (DT_RSOT.MessageOutList == null)
                    {
                        //row[6] = String.Format("{0:$ #,#}", Double.Parse((DT_RSOT.ItemOutList[0].SubTot / 100).ToString()));
                        row[6] = String.Format("{0:$ #,#}", DNum((DT_RSOT.ItemOutList[0].SubTot / 100).ToString()));
                        rowPB[0] = txtCodObra.Text;
                        rowPB[1] = "1";
                        rowPB[2] = txtCodMat.Text;
                        rowPB[3] = txtCentro.Text.ToUpper();
                        rowPB[4] = 1;
                        //row[5] = String.Format("{0:$ #,#}", Double.Parse((SOrder(rowPB).ItemOutList[0].NetVal / 100).ToString()));
                        row[5] = String.Format("{0:$ #,#}", DNum((SOrder(rowPB, (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim()))).ItemOutList[0].NetVal / 100).ToString()));
                        dtdatos.Rows.Add(row);
                        gv_SOrder.DataSource = dtdatos;
                        gv_SOrder.DataBind();
                        gv_SOrder.Visible = true;
                        lblMsgVSO.Text = "El TOTAL del pedido a programar es:" + String.Format("{0:$ #,#}", Double.Parse(dtdatos.Rows[0][6].ToString()));
                        //lblVSO.Text = String.Format("{0:$ #,#}",Double.Parse(dtdatos.Rows[0][6].ToString()));
                        lblMsgSF.Text = "SALDO =" + String.Format("{0:$ #,#}", Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - Double.Parse(dtdatos.Rows[0][6].ToString()));
                        //lblSF.Text = String.Format("{0:$ #,#}", Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][4].ToString()) - Double.Parse(dtdatos.Rows[0][6].ToString()));
                        lblAPG.Text = "Puede proceder con la programación en GINCO!!";
                        lblMsgVSO.Visible = true;
                        //lblVSO.Visible = true;
                        lblMsgSF.Visible = true;
                        //lblSF.Visible = true;
                        lblAPG.Visible = true;
                        btnRegP.Visible = true;
                    }
                    else
                    {
                        lbl_E_SO.Text = DT_RSOT.MessageOutList[0].Message;
                        lbl_E_SO.Visible = true;
                    }
                }
                pnlDatos.Enabled = false;
                pnlAditivos.Enabled = false;
                pnlAB.Enabled = false;
                btnCal.Enabled = false;
                btnMod.Visible = true;
            }
            else
            {
                lbl_E_SO.Text = "Datos Insuficientes";
                lbl_E_SO.Visible = true;
            }
        }
        protected void btnGuardaAC_Click(object sender, EventArgs e)
        {
            try
            {
                switch (int.Parse(Session["tReg"].ToString()))
                {
                    case 1:
                        {
                            #region Ajuste

                            if (ddlVen.Items[0].Selected == false & txtIdPedido.Text != String.Empty &
                                txtDTComp.Text != String.Empty)
                            {
                                lblErrorDC.Visible = false;

                                var sPr = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                          "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text);
                                var r = (int)ECmSp(sPr, cnnCOS, 2);

                                var sP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                         "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text);
                                var ret = (int)ECmSp(sP, cnnCOS, 2);

                                if (ret != 1 & r != 1)
                                {
                                    var strP = "SPIPC|@TCOM;1|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                               "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                               "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) + "|@CUPO;" +
                                               ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                               "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] + "|@CART;" +
                                               ((DataTable)Session["EstadoCuenta"]).Rows[0][2] + "|@CMPSAP;" +
                                               ((DataTable)Session["EstadoCuenta"]).Rows[0][3] + "|@CMPGINCO;" +
                                               ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                               "|@IDVEN;" + Int64.Parse(ddlVen.SelectedValue) + "|@IDPEDIDO;" +
                                               Int64.Parse(txtIdPedido.Text) + "|@DTCOMP;" +
                                               String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDTComp.Text)) +
                                               "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId + "|@DCOMMIT;" +
                                               String.Format("{0:yyyy-MM-dd}", DateTime.Today) + "|@TCOMMIT;" +
                                               String.Format("{0:HH:mm:ss}", DateTime.Now);
                                    ECmSp(strP, cnnCOS, 0);
                                    lblGP.Text = "Datos Guardados";
                                    lblGP.Visible = true;
                                    btnGuardaAC.Visible = false;
                                    pnlAC.Enabled = false;
                                }
                                else
                                {
                                    lblErrorDC.Text = "Numero de pedido ya se encuentra registrado";
                                    lblErrorDC.Visible = true;
                                }
                            }
                            else
                            {
                                lblErrorDC.Text = "Datos Incorrectos";
                                lblErrorDC.Visible = true;
                            }

                            #endregion
                        }
                        break;
                    case 2:
                        {
                            #region Prog
                            int vIns;
                            lblErrorDC.Visible = false;
                            if (txtIdPedido.Text != String.Empty)
                            {
                                var sp = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text);
                                var ret = (int)ECmSp(sp, cnnCOS, 2);
                                if (ret != 1)
                                {
                                    var strP = "SPSIU_PROG|@TCOM;2|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                               "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text) +
                                               "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                               "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) +
                                               "|@VALPED;" + ((DataTable)Session["SmPed"]).Rows[0]["ValTPed"] +
                                               "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                               "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                               "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                               "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                               "|@CMPGINCO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                               "|@IDCONP;" + gv_DataCli.Rows[0].Cells[3].Text +
                                               "|@AJUSTE;" + (chbAj.Checked ? 1 : 0) +
                                               "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                               "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                               "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                    vIns = (int)ECmSp(strP, cnnCOS, 2);
                                    if (vIns == 1)
                                    {
                                        for (var i = 0; i < ((DataTable)Session["SmPed"]).Rows.Count; i++)
                                        {
                                            strP = "SPSIU_PROG|@TCOM;3|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                                   "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text) +
                                                   "|@IDMATERIAL;" + ((DataTable)Session["SmPed"]).Rows[i]["IdMaterial"] +
                                                   "|@VOLUMEN;" + ((DataTable)Session["SmPed"]).Rows[i]["Cantidad"].ToString().Replace(',', '.') +
                                                   "|@IDCENTRO;" + ((DataTable)Session["SmPed"]).Rows[i]["IdCentro"].ToString().Trim().ToUpper() +
                                                   "|@VALU;" + ((DataTable)Session["SmPed"]).Rows[i]["PBase"] +
                                                   "|@VALT;" + ((DataTable)Session["SmPed"]).Rows[i]["PNeto"] +
                                                   "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                                   "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                            ECmSp(strP, cnnCOS, 2);
                                        }
                                        lblGP.Text = "Datos Guardados";
                                        lblGP.Visible = true;
                                        btnGuardaAC.Visible = false;
                                        pnlAC.Enabled = false;
                                    }
                                    else
                                    {
                                        lblErrorDC.Text = "Numero de pedido ya se encuentra registrado";
                                        lblErrorDC.Visible = true;
                                    }
                                }
                                else
                                {
                                    lblErrorDC.Text = "Numero de pedido ya se encuentra registrado";
                                    lblErrorDC.Visible = true;
                                }
                            }
                            else
                            {
                                lblErrorDC.Text = "Datos Incorrectos";
                                lblErrorDC.Visible = true;
                            }
                            #endregion
                        }
                        break;
                    case 3:
                        {
                            #region Ajuste-Prog
                            if (ddlVen.Items[0].Selected == false & txtIdPedido.Text != String.Empty & txtDTComp.Text != String.Empty)
                            {
                                lblErrorDC.Visible = false;
                                var sp = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text);
                                var ret = (int)ECmSp(sp, cnnCOS, 2);
                                if (ret != 1)
                                {
                                    var strPed = "SPSIU_PROG|@TCOM;2|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                               "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text) +
                                               "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                               "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) +
                                               "|@VALPED;" + ((DataTable)Session["SmPed"]).Rows[0]["ValTPed"] +
                                               "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                               "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                               "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                               "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                               "|@CMPGINCO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                               "|@IDCONP;" + gv_DataCli.Rows[0].Cells[3].Text +
                                               "|@AJUSTE;" + (chbAj.Checked ? 1 : 0) +
                                               "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                               "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                               "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                    var vInsp = (int)ECmSp(strPed, cnnCOS, 2);
                                    if (vInsp == 1)
                                    {


                                        var strP = "SPIPC|@TCOM;1|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) + "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) + "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                                   "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] + "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] + "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] + "|@CMPGINCO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                                   "|@IDVEN;" + Int64.Parse(ddlVen.SelectedValue) + "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text) + "|@DTCOMP;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDTComp.Text)) +
                                                   "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId + "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) + "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                        ECmSp(strP, cnnCOS, 0);

                                        for (var i = 0; i < ((DataTable)Session["SmPed"]).Rows.Count; i++)
                                        {
                                            strPed = "SPSIU_PROG|@TCOM;3|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                                   "|@IDPEDIDO;" + Int64.Parse(txtIdPedido.Text) +
                                                   "|@IDMATERIAL;" + ((DataTable)Session["SmPed"]).Rows[i]["IdMaterial"] +
                                                   "|@VOLUMEN;" + ((DataTable)Session["SmPed"]).Rows[i]["Cantidad"].ToString().Replace(',', '.') +
                                                   "|@IDCENTRO;" + ((DataTable)Session["SmPed"]).Rows[i]["IdCentro"].ToString().Trim().ToUpper() +
                                                   "|@VALU;" + ((DataTable)Session["SmPed"]).Rows[i]["PBase"] +
                                                   "|@VALT;" + ((DataTable)Session["SmPed"]).Rows[i]["PNeto"] +
                                                   "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                                   "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                            ECmSp(strPed, cnnCOS, 2);
                                        }
                                        lblGP.Text = "Datos Guardados";
                                        lblGP.Visible = true;
                                        btnGuardaAC.Visible = false;
                                        pnlAC.Enabled = false;

                                    }
                                    else
                                    {
                                        lblErrorDC.Text = "Numero de pedido ya se encuentra registrado";
                                        lblErrorDC.Visible = true;
                                    }
                                }
                                else
                                {
                                    lblErrorDC.Text = "Numero de pedido ya se encuentra registrado";
                                    lblErrorDC.Visible = true;
                                }
                            }
                            else
                            {
                                lblErrorDC.Text = "Datos Incorrectos";
                                lblErrorDC.Visible = true;
                            }
                            #endregion
                        }

                        break;
                }
                MpUp_AC.Show();
            }
            catch (Exception ex)
            {
                lblErrorDC.Text = "Error: " + ex.ToString();
                lblErrorDC.Visible = true;
                //throw;
            }
        }
        protected void btnNAC_Click(object sender, EventArgs e)
        {
            Session["DVPed"] = null;
            Session["DVPedIni"] = null;
            Session["tReg"] = null;
            Session["tReg_OG"] = null;
            Session["SumPed_OG"] = null;
            Session["RANew"] = null;
            Session["New"] = null;
            Session["EstadoCuenta"] = null;
            Session["SmPed"] = null;
            Response.Redirect("~/CO/PG/Usr/ProgPedidos.aspx");

        }
        protected void btnAC_Click(object sender, EventArgs e)
        {
            try
            {
                //btnAC.Enabled = false;
                DataTable dtVen = new DataTable();
                DataTable dtVenL = new DataTable();
                dtVen = (DataTable)ECmSp("SPS_R_OV|@TCOM;1|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDOBRA;" + txtCodObra.Text.Trim(), cnnSAPDB, 1);
                if (dtVen.Rows.Count == 0 & int.Parse(txtSector.Text) == 3)
                {
                    dtVen = (DataTable)ECmSp("SPIUVEN|@IDCOM;5|@IDORG;7460|@IDTIPOGC;ZC50", cnnSAPDB, 1);
                    ddlVen.DataSource = dtVen;
                    ddlVen.DataTextField = "NVEN";
                    ddlVen.DataValueField = "CODVEN";
                    ddlVen.DataBind();
                    ddlVen.Items.Insert(0, "Seleccione...");
                    ddlVen.Items[0].Selected = true;
                }
                else if (int.Parse(txtSector.Text) == 3)
                {
                    ddlVen.DataSource = dtVen;
                    ddlVen.DataTextField = "NVen";
                    ddlVen.DataValueField = "IdCodVen";
                    ddlVen.DataBind();
                    ddlVen.Items.Insert(0, "Seleccione...");
                    ddlVen.Items[1].Selected = true;
                    ddlVen.Enabled = false;
                }
                else if (dtVen.Rows.Count == 0 & int.Parse(txtSector.Text) != 3)
                {
                    dtVen = (DataTable)ECmSp("SPIUVEN|@IDCOM;5|@IDORG;7460|@IDTIPOGC;ZC50", cnnSAPDB, 1);
                    ddlVenA_OG.DataSource = dtVen;
                    ddlVenA_OG.DataTextField = "NVEN";
                    ddlVenA_OG.DataValueField = "CODVEN";
                    ddlVenA_OG.DataBind();
                    ddlVenA_OG.Items.Insert(0, "Seleccione...");
                    ddlVenA_OG.Items[0].Selected = true;
                }
                else if (int.Parse(txtSector.Text) != 3)
                {
                    ddlVenA_OG.DataSource = dtVen;
                    ddlVenA_OG.DataTextField = "NVen";
                    ddlVenA_OG.DataValueField = "IdCodVen";
                    ddlVenA_OG.DataBind();
                    ddlVenA_OG.Items.Insert(0, "Seleccione...");
                    ddlVenA_OG.Items[1].Selected = true;
                    ddlVenA_OG.Enabled = false;
                }
                if (gv_SOrder.Rows.Count > 0 & int.Parse(txtSector.Text) == 3)
                {
                    Session["tReg"] = 3;
                }
                else if (int.Parse(txtSector.Text) == 3)
                {
                    Session["tReg"] = 1;
                }
                else if ((Session["SumPed_OG"] == null ? 0 : Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - DNum(Session["SumPed_OG"].ToString())) < 0 & int.Parse(txtSector.Text) != 3)
                {
                    Session["tReg_OG"] = 6;
                    lblAC_OG.Text = "Contacte al Comercial y Ingrese los codigos de Pedido";
                    gv_GPed_OG.DataSource = Session["DVPedIni"];
                    gv_GPed_OG.DataBind();
                    gv_Ped.Visible = false;
                    gv_GPed_OG.Visible = true;
                }
                else if (int.Parse(txtSector.Text) != 3)
                {
                    Session["tReg_OG"] = 5;
                    lblAC_OG.Text = "Contacte al Comercial";
                    gv_Ped.DataSource = Session["DVPed"];
                    gv_Ped.DataBind();
                    gv_Ped.Visible = true;
                    gv_GPed_OG.Visible = false;
                }

                if (int.Parse(txtSector.Text) == 3)
                {
                    lblAC.Text = "Contacte al Comercial";
                    pnlMpUp.Visible = true;
                    pnlAC.Visible = true;
                    MpUp_AC.Show();
                    lblVen.Visible = true;
                    lblDTComp.Visible = true;
                    ddlVen.Visible = true;
                    txtDTComp.Visible = true;
                }
                else if (int.Parse(txtSector.Text) != 3)
                {
                    pnlMP_OG.Visible = true;
                    pnlA_OG.Visible = true;
                    MPUP_OG.Show();
                    lblVenA_OG.Visible = true;
                    lbldt_OG.Visible = true;
                    ddlVenA_OG.Visible = true;
                    txtdt_OG.Visible = true;
                    //gv_GPed_OG.DataSource = Session["DVPedIni"];
                    //gv_GPed_OG.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnNVCli_Click(object sender, EventArgs e)
        {
            Session["DVPed"] = null;
            Session["DVPedIni"] = null;
            Session["tReg"] = null;
            Session["tReg_OG"] = null;
            Session["SumPed_OG"] = null;
            Session["RANew"] = null;
            Session["New"] = null;
            Session["EstadoCuenta"] = null;
            Session["SmPed"] = null;
            Response.Redirect("~/CO/PG/Usr/ProgPedidos.aspx");
        }
        protected void btnMod_Click(object sender, EventArgs e)
        {
            btnRegP.Visible = false;
            pnlDatos.Enabled = true;
            pnlAditivos.Enabled = true;
            pnlAB.Enabled = true;
            btnCal.Enabled = true;
            btnMod.Visible = false;
            btnASC.Visible = false;
        }
        protected void btnRegP_Click(object sender, EventArgs e)
        {
            btnAC.Enabled = false;
            lblAC.Text = "Ingrese el numero de Pedido";
            Session["tReg"] = 2;
            lblVen.Visible = false;
            lblDTComp.Visible = false;
            ddlVen.Visible = false;
            txtDTComp.Visible = false;
            pnlMpUp.Visible = true;
            pnlAC.Visible = true;
            MpUp_AC.Show();
        }

        #endregion

        protected void btnRegP_OG_Click(object sender, EventArgs e)
        {
            try
            {
                btnAC.Enabled = false;
                lblAC_OG.Text = "Ingrese los codigos de Pedido";
                Session["tReg_OG"] = 4;
                gv_GPed_OG.DataSource = Session["DVPedIni"];
                gv_GPed_OG.DataBind();
                lblVenA_OG.Visible = false;
                lbldt_OG.Visible = false;
                ddlVenA_OG.Visible = false;
                txtdt_OG.Visible = false;
                pnlA_OG.Visible = true;
                pnlMP_OG.Visible = true;
                MPUP_OG.Show();

                gv_Ped.Visible = false;
                gv_GPed_OG.Visible = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnCalPed_OG_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_E_OG.Visible = false;
                gv_ValPed.DataSource = Session["DVPedIni"];
                gv_ValPed.EditIndex = -1;
                gv_ValPed.DataBind();
                DataTable dt = (DataTable)Session["DVPedIni"];
                //if (txtPed1.Text != "" | txtPed2.Text != "" | txtPed3.Text != "" | txtPed4.Text != "" | txtPed5.Text != "")
                if (dt.Rows[0]["txt"].ToString().Trim() != "##" & dt.Rows[0]["txt"].ToString().Trim() != String.Empty)
                {
                    Int64 res = 0;
                    //res += (txtPed1.Text == "" ? 0 : Int64.Parse(txtPed1.Text));
                    //res += (txtPed2.Text == "" ? 0 : Int64.Parse(txtPed2.Text));
                    //res += (txtPed3.Text == "" ? 0 : Int64.Parse(txtPed3.Text));
                    //res += (txtPed4.Text == "" ? 0 : Int64.Parse(txtPed4.Text));
                    //res += (txtPed5.Text == "" ? 0 : Int64.Parse(txtPed5.Text));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        res += Int64.Parse(dt.Rows[i]["Valor"].ToString());
                    }

                    Session["SumPed_OG"] = res;

                    lblMsgVSO.Text = "El TOTAL del pedido a programar es:" + String.Format("{0:$ #,#}", DNum(Session["SumPed_OG"].ToString()));
                    lblMsgSF.Text = "SALDO =" + String.Format("{0:$ #,#}", Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - DNum(Session["SumPed_OG"].ToString()));

                    if ((Double.Parse(((DataTable)Session["EstadoCuenta"]).Rows[0][5].ToString()) - DNum(Session["SumPed_OG"].ToString())) >= 0)
                    {
                        btnASC.Visible = false;
                        lblAP.Visible = false;
                        lblMsgSF.CssClass = "labelInfo";
                        lblAPG.Text = "Puede proceder con la programación en GINCO";
                        lblAPG.CssClass = "labelInfo";
                        btnRegP_OG.Visible = true;
                        pnlPedidos.Enabled = false;
                        pnlPedReg.Visible = true;
                    }
                    else
                    {
                        btnRegP_OG.Visible = false;
                        lblMsgSF.CssClass = "labelError";
                        lblAPG.Text = "El valor del pedido es superior al saldo disponible.";
                        lblAPG.CssClass = "labelError";
                        lblAP.Text = "NO EFECTUAR LA PROGRAMACIÓN SI NO SE REGISTRA UN AJUSTE!!";
                        lblAP.CssClass = "labelError";
                        lblAP.Visible = true;
                        pnlPedidos.Enabled = false;
                        pnlPedReg.Visible = true;
                        //if ((Session["IdSCanal"] == null ? true : (Int64.Parse(Session["IdSCanal"].ToString()) != 9000032821 ? true : false)))
                        //{
                        //    btnASC.Visible = true;
                        //}
                        btnASC.Visible = true;
                    }
                    lbl_E_SO.Visible = false;
                    lblMsgVSO.Visible = true;
                    lblMsgSF.Visible = true;
                    lblAPG.Visible = true;
                }
                else
                {
                    lbl_E_OG.Text = "Datos Incorrectos";
                    lbl_E_OG.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnMod_OG_Click(object sender, EventArgs e)
        {
            try
            {
                pnlPedidos.Enabled = true;
                pnlPedReg.Visible = false;
                btnASC.Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void gv_ValPed_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo
                        DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                        Session["RANew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (dt.Rows[0]["txt"].ToString() == "##" & gv_ValPed.Rows.Count == 1)
                        {
                            gv_ValPed.EditIndex = 0;
                            gv_ValPed.DataSource = dt;
                            gv_ValPed.DataBind();
                        }
                        else
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = DBNull.Value;
                            }
                            dt.Rows.Add(rw);
                            gv_ValPed.DataSource = dt;
                            gv_ValPed.DataBind();
                            gv_ValPed.PageIndex = gv_ValPed.PageCount;
                            gv_ValPed.DataBind();
                            gv_ValPed.EditIndex = gv_ValPed.Rows.Count - 1;
                            gv_ValPed.DataBind();
                            //Session["DVPedIni"] = dt;
                        }
                        #endregion
                    }
                    break;
                case "Editar":
                    {
                        #region Editar
                        DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                        if (dt.Rows[0]["txt"].ToString() != "##")
                        {
                            Session["RANew"] = 0;
                            gv_ValPed.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gv_ValPed.DataSource = Session["DVPedIni"];
                            gv_ValPed.DataBind();
                        }
                        #endregion
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar
                        var txt = "Pedido " + (Convert.ToInt32(e.CommandArgument.ToString()) + 1 + +(gv_ValPed.PageSize * gv_ValPed.PageIndex)).ToString();
                        var valPed = ((TextBox)gv_ValPed.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtValPed")).Text.Trim();
                        switch (((int)Session["RANew"]))
                        {
                            case 0:
                                {
                                    DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                                    if (dt.Rows[0]["txt"].ToString() == "##" | valPed == "") return;
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if ((Convert.ToInt32(e.CommandArgument.ToString()) + (gv_ValPed.PageSize * gv_ValPed.PageIndex)) == i)
                                        {
                                            dt.Rows[i]["Valor"] = valPed;
                                            break;
                                        }
                                    }
                                    gv_ValPed.DataSource = dt;
                                    gv_ValPed.DataBind();
                                    gv_ValPed.PageIndex = gv_ValPed.PageCount;
                                    gv_ValPed.DataBind();
                                    gv_ValPed.EditIndex = -1;
                                    gv_ValPed.DataBind();
                                    Session["DVPedIni"] = dt;
                                    Session["RANew"] = 0;
                                }
                                break;
                            case 1:
                                {
                                    if (valPed == "##" | valPed == "") return;
                                    DataTable dt = (((DataTable)Session["DVPedIni"]).Rows[0]["txt"].ToString() == "##" ? ((DataTable)Session["DVPedIni"]).Clone() : ((DataTable)Session["DVPedIni"]).Copy());
                                    DataRow rw = dt.NewRow();
                                    rw["txt"] = txt;
                                    rw["Valor"] = valPed;
                                    dt.Rows.Add(rw);
                                    gv_ValPed.DataSource = dt;
                                    gv_ValPed.DataBind();
                                    gv_ValPed.PageIndex = gv_ValPed.PageCount;
                                    gv_ValPed.DataBind();
                                    gv_ValPed.EditIndex = -1;
                                    gv_ValPed.DataBind();
                                    Session["DVPedIni"] = dt;
                                    Session["RANew"] = 0;
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                        if (dt.Rows[0]["txt"].ToString().Trim() == "##") return;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(e.CommandArgument.ToString()) + (gv_ValPed.PageSize * gv_ValPed.PageIndex)) == i)
                            {
                                dt.Rows[i].Delete();
                                dt.AcceptChanges();
                                break;
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["txt"] = "Pedido " + (i + 1).ToString();
                        }
                        if (dt.Rows.Count == 0)
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = "##";
                            }
                            dt.Rows.Add(rw);
                        }
                        gv_ValPed.DataSource = dt;
                        gv_ValPed.DataBind();
                        gv_ValPed.EditIndex = -1;
                        gv_ValPed.DataBind();
                        Session["DVPedIni"] = dt;
                        Session["RANew"] = 0;

                        gv_ValPed.EditIndex = -1;
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        //lblEInsUp.Visible = false;
                        Session["RANew"] = 0;
                        gv_ValPed.EditIndex = -1;
                        gv_ValPed.DataSource = Session["DVPedIni"];
                        gv_ValPed.DataBind();
                        #endregion
                    }
                    break;
            }
        }

        protected void gv_ValPed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ValPed.EditIndex = -1;
            gv_ValPed.PageIndex = e.NewPageIndex;
            gv_ValPed.DataSource = Session["DVPedIni"];
            gv_ValPed.DataBind();
        }

        protected void gv_GPed_OG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    {
                        #region Editar
                        DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                        if (dt.Rows[0]["txt"].ToString() != "##")
                        {
                            Session["RANew"] = 0;
                            gv_GPed_OG.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gv_GPed_OG.DataSource = Session["DVPedIni"];
                            gv_GPed_OG.DataBind();
                        }
                        #endregion
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar
                        //var txt = "Pedido " + (Convert.ToInt32(e.CommandArgument.ToString()) + 1 + (gv_GPed_OG.PageSize * gv_GPed_OG.PageIndex)).ToString();
                        var Ped = ((TextBox)gv_GPed_OG.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtIdPed")).Text.Trim();
                        lblError_OG.Visible = false;
                        switch (((int)Session["RANew"]))
                        {
                            case 0:
                                {
                                    DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                                    var found = false;
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped)
                                        if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped & (Convert.ToInt32(e.CommandArgument.ToString()) + (gv_GPed_OG.PageSize * gv_GPed_OG.PageIndex)) != i)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (found == false)
                                    {

                                        var sp = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped.ToString());
                                        var ret = (int)ECmSp(sp, cnnCOS, 2);

                                        var strP = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped.ToString());
                                        var vIns = (int)ECmSp(strP, cnnCOS, 2);

                                        if (vIns != 1 & ret != 1)
                                        {
                                            if (dt.Rows[0]["txt"].ToString() == "##" | Ped == "") return;
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                if ((Convert.ToInt32(e.CommandArgument.ToString()) + (gv_GPed_OG.PageSize * gv_ValPed.PageIndex)) == i)
                                                {
                                                    dt.Rows[i]["IdPedido"] = Ped;
                                                    break;
                                                }
                                            }

                                            gv_GPed_OG.DataSource = dt;
                                            gv_GPed_OG.DataBind();
                                            gv_GPed_OG.PageIndex = gv_ValPed.PageCount;
                                            gv_GPed_OG.DataBind();
                                            gv_GPed_OG.EditIndex = -1;
                                            gv_GPed_OG.DataBind();
                                            Session["DVPedIni"] = dt;
                                            Session["RANew"] = 0;
                                        }
                                        else
                                        {
                                            lblError_OG.Text = "Codigo Invalido, ya se encuentra registrado";
                                            lblError_OG.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        lblError_OG.Text = "Uno o mas Codigos de Pedido son Iguales";
                                        lblError_OG.Visible = true;
                                    }
                                    dt.Dispose();
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        //lblEInsUp.Visible = false;
                        Session["RANew"] = 0;
                        gv_GPed_OG.EditIndex = -1;
                        gv_GPed_OG.DataSource = Session["DVPedIni"];
                        gv_GPed_OG.DataBind();
                        #endregion
                    }
                    break;
            }
            MPUP_OG.Show();
        }

        protected void gv_GPed_OG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_GPed_OG.EditIndex = -1;
            gv_GPed_OG.PageIndex = e.NewPageIndex;
            gv_GPed_OG.DataSource = Session["DVPedIni"];
            gv_GPed_OG.DataBind();
        }

        protected void btnGA_OG_Click(object sender, EventArgs e)
        {
            lblError_OG.Visible = false;
            if (int.Parse(Session["tReg_OG"].ToString()) == 5)
            {
                Int64 i64;
                var isError = false;
                DataTable dts = ((DataTable)Session["DVPed"]);
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    if (Int64.TryParse(dts.Rows[i]["IdPedido"].ToString(), out i64) == false)
                    {
                        lblError_OG.Text = "Datos Incorrectos";
                        lblError_OG.Visible = true;
                        isError = true;
                        break;
                    }
                }

                if (isError == false)
                {
                    //DataTable dts = ((DataTable)Session["DVPedIni"]);
                    DateTime iDt;
                    if (ddlVenA_OG.Items[0].Selected == false & dts.Rows.Count > 0 & DateTime.TryParse(txtdt_OG.Text, out iDt))
                    {
                        for (int i = 0; i < dts.Rows.Count; i++)
                        {


                            var sPr = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                            var r = (int)ECmSp(sPr, cnnCOS, 2);

                            var sP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                            var ret = (int)ECmSp(sP, cnnCOS, 2);

                            if (ret != 1 & r != 1)
                            {
                                var strP = "SPIPC|@TCOM;1|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                           "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                           "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) +
                                           "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                           "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                           "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                           "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                           "|@CMPGINCO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                           "|@IDVEN;" + Int64.Parse(ddlVenA_OG.SelectedValue) +
                                           "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString()) +
                                           "|@DTCOMP;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtdt_OG.Text)) +
                                           "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                           "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                           "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                ECmSp(strP, cnnCOS, 0);

                                if ((i + 1) == dts.Rows.Count)
                                {
                                    lblGP_OG.Text = "Datos Guardados";
                                    lblGP_OG.Visible = true;
                                    pnlA_OG.Enabled = false;
                                }
                            }
                            else
                            {
                                lblError_OG.Text = "Numero de pedido ya se encuentra registrado";
                                lblError_OG.Visible = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        lblError_OG.Text = "Datos Incorrectos";
                        lblError_OG.Visible = true;
                    }
                }
            }
            else
            {
                DataTable dt = ((DataTable)Session["DVPedIni"]).Copy();
                Int64 i64;
                var isError = false;
                var Exist = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Int64.TryParse(dt.Rows[i]["IdPedido"].ToString(), out i64) == false)
                    {
                        lblError_OG.Text = "Datos Incorrectos";
                        lblError_OG.Visible = true;
                        isError = true;
                        break;
                    }
                }
                if (isError == false)
                {
                    switch (int.Parse(Session["tReg_OG"].ToString()))
                    {
                        case 4:
                            {
                                DataTable dts = ((DataTable)Session["DVPedIni"]);
                                if (dts.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dts.Rows.Count; i++)
                                    {

                                        var sPr = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                                        var r = (int)ECmSp(sPr, cnnCOS, 2);

                                        var sP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                                        var ret = (int)ECmSp(sP, cnnCOS, 2);

                                        if (ret == 0 & r == 0) continue;
                                        Exist = true;
                                        break;

                                        //var strP = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                        //           "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());

                                        //var vIns = (int)ECmSp(strP, cnnCOS, 2);

                                        //if (vIns == 0) continue;
                                        //Exist = true;
                                        //break;
                                    }
                                    if (Exist == false)
                                    {
                                        for (int i = 0; i < dts.Rows.Count; i++)
                                        {
                                            var strP = "SPSIU_PROG|@TCOM;2|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) +
                                            "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString()) +
                                            "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                            "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) +
                                            "|@VALPED;" + dts.Rows[i]["Valor"].ToString() +
                                            "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                            "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                            "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                            "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                            "|@CMPGINCO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                            "|@IDCONP;" + gv_DataCli.Rows[0].Cells[3].Text +
                                            "|@AJUSTE;" + "3" +
                                            "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                            "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                            "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                            int vIns_OG = (int)ECmSp(strP, cnnCOS, 2);
                                            if (vIns_OG == 1 & (i + 1) == dts.Rows.Count)
                                            {
                                                lblGP_OG.Text = "Datos Guardados";
                                                lblGP_OG.Visible = true;
                                                //btnGA_OG.Visible = false;
                                                pnlA_OG.Enabled = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblError_OG.Text = "Numero de pedido ya se encuentra registrado";
                                        lblError_OG.Visible = true;
                                    }
                                }
                            }
                            break;
                        case 6:
                            {
                                DataTable dts = ((DataTable)Session["DVPedIni"]);
                                DateTime iDt;
                                if (ddlVenA_OG.Items[0].Selected == false & dts.Rows.Count > 0 &
                                    DateTime.TryParse(txtdt_OG.Text, out iDt))
                                {
                                    for (int i = 0; i < dts.Rows.Count; i++)
                                    {

                                        var sPr = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                                        var r = (int)ECmSp(sPr, cnnCOS, 2);

                                        var sP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());
                                        var ret = (int)ECmSp(sP, cnnCOS, 2);

                                        if (ret == 0 & r == 0) continue;
                                        Exist = true;
                                        break;

                                        //var strP = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" +
                                        //           int.Parse(txtSector.Text) +
                                        //           "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString());

                                        //var vIns = (int)ECmSp(strP, cnnCOS, 2);

                                        //if (vIns == 0) continue;
                                        //Exist = true;
                                        //break;
                                    }

                                    if (Exist == false)
                                    {
                                        for (int i = 0; i < dts.Rows.Count; i++)
                                        {
                                            var strPed = "SPSIU_PROG|@TCOM;2|@IDORG;7460|@IDSECTOR;" +
                                                         int.Parse(txtSector.Text) +
                                                         "|@IDPEDIDO;" + Int64.Parse(dts.Rows[i]["IdPedido"].ToString()) +
                                                         "|@IDCLIENTE;" + Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) +
                                                         "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) +
                                                         "|@VALPED;" + dts.Rows[i]["Valor"].ToString() +
                                                         "|@CUPO;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                                         "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                                         "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                                         "|@CMPSAP;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                                         "|@CMPGINCO;" +
                                                         ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                                         "|@IDCONP;" + gv_DataCli.Rows[0].Cells[3].Text +
                                                         "|@AJUSTE;" + "3" +
                                                         "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                                         "|@DCOMMIT;" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                                         "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                            var vInsp = (int)ECmSp(strPed, cnnCOS, 2);

                                            if (vInsp == 1)
                                            {
                                                var strP = "SPIPC|@TCOM;1|@IDORG;7460|@IDSECTOR;" +
                                                           int.Parse(txtSector.Text) + "|@IDCLIENTE;" +
                                                           Int64.Parse(gv_DataCli.Rows[0].Cells[1].Text) + "|@IDOBRA;" +
                                                           Int64.Parse(txtCodObra.Text) + "|@CUPO;" +
                                                           ((DataTable)Session["EstadoCuenta"]).Rows[0][0] +
                                                           "|@SALA;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][1] +
                                                           "|@CART;" + ((DataTable)Session["EstadoCuenta"]).Rows[0][2] +
                                                           "|@CMPSAP;" +
                                                           ((DataTable)Session["EstadoCuenta"]).Rows[0][3] +
                                                           "|@CMPGINCO;" +
                                                           ((DataTable)Session["EstadoCuenta"]).Rows[0][4] +
                                                           "|@IDVEN;" + Int64.Parse(ddlVenA_OG.SelectedValue) +
                                                           "|@IDPEDIDO;" +
                                                           Int64.Parse(dts.Rows[i]["IdPedido"].ToString()) + "|@DTCOMP;" +
                                                           String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtdt_OG.Text)) +
                                                           "|@CEMEXID;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                                           "|@DCOMMIT;" +
                                                           String.Format("{0:yyyy-MM-dd}", DateTime.Today) +
                                                           "|@TCOMMIT;" + String.Format("{0:HH:mm:ss}", DateTime.Now);

                                                ECmSp(strP, cnnCOS, 0);

                                                if (vInsp == 1 & (i + 1) == dts.Rows.Count)
                                                {
                                                    lblGP_OG.Text = "Datos Guardados";
                                                    lblGP_OG.Visible = true;
                                                    //btnGA_OG.Visible = false;
                                                    pnlA_OG.Enabled = false;
                                                }
                                            }
                                            else
                                            {
                                                lblError_OG.Text = "Numero de pedido ya se encuentra registrado";
                                                lblError_OG.Visible = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblError_OG.Text = "Numero de pedido ya se encuentra registrado";
                                        lblError_OG.Visible = true;
                                    }
                                }
                                else
                                {
                                    lblError_OG.Text = "Datos Incorrectos";
                                    lblError_OG.Visible = true;
                                }
                            }
                            break;
                    }
                }
            }
            MPUP_OG.Show();
        }

        protected void gv_Ped_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MPUP_OG.Show();
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo
                        DataTable dt = ((DataTable)Session["DVPed"]).Copy();
                        Session["New"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (dt.Rows[0]["txt"].ToString() == "##" & gv_Ped.Rows.Count == 1)
                        {
                            gv_Ped.EditIndex = 0;
                            gv_Ped.DataSource = dt;
                            gv_Ped.DataBind();
                        }
                        else
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = DBNull.Value;
                            }
                            dt.Rows.Add(rw);
                            gv_Ped.DataSource = dt;
                            gv_Ped.DataBind();
                            gv_Ped.PageIndex = gv_Ped.PageCount;
                            gv_Ped.DataBind();
                            gv_Ped.EditIndex = gv_Ped.Rows.Count - 1;
                            gv_Ped.DataBind();
                            //Session["DVPedIni"] = dt;
                        }
                        dt.Dispose();
                        #endregion
                    }
                    break;
                case "Editar":
                    {
                        #region Editar
                        DataTable dt = ((DataTable)Session["DVPed"]).Copy();
                        if (dt.Rows[0]["txt"].ToString() != "##")
                        {
                            Session["New"] = 0;
                            gv_Ped.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gv_Ped.DataSource = Session["DVPed"];
                            gv_Ped.DataBind();
                        }
                        #endregion
                        dt.Dispose();
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar
                        var txt = "Pedido " + (Convert.ToInt32(e.CommandArgument.ToString()) + 1 + +(gv_Ped.PageSize * gv_Ped.PageIndex)).ToString();
                        var Ped = ((TextBox)gv_Ped.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtPed")).Text.Trim();
                        lblError_OG.Visible = false;
                        switch (((int)Session["New"]))
                        {
                            case 0:
                                {
                                    DataTable dt = ((DataTable)Session["DVPed"]).Copy();
                                    var found = false;
                                    if (dt.Rows[0]["txt"].ToString() == "##" | Ped == "") return;
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped)
                                        if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped & (Convert.ToInt32(e.CommandArgument.ToString()) + (gv_GPed_OG.PageSize * gv_GPed_OG.PageIndex)) != i)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (found == false)
                                    {

                                        var sP = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped.ToString());
                                        var r = (int)ECmSp(sP, cnnCOS, 2);

                                        var strP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped);
                                        var vIns = (int)ECmSp(strP, cnnCOS, 2);

                                        if (vIns != 1 & r != 1)
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                if ((Convert.ToInt32(e.CommandArgument.ToString()) + (gv_Ped.PageSize * gv_Ped.PageIndex)) == i)
                                                {
                                                    dt.Rows[i]["IdPedido"] = Ped;
                                                    break;
                                                }
                                            }
                                            gv_Ped.DataSource = dt;
                                            gv_Ped.DataBind();
                                            gv_Ped.PageIndex = gv_Ped.PageCount;
                                            gv_Ped.DataBind();
                                            gv_Ped.EditIndex = -1;
                                            gv_Ped.DataBind();
                                            Session["DVPed"] = dt;
                                            Session["New"] = 0;
                                        }
                                        else
                                        {
                                            lblError_OG.Text = "Codigo Invalido, ya se encuentra registrado";
                                            lblError_OG.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        lblError_OG.Text = "Uno o mas Codigos de Pedido son Iguales";
                                        lblError_OG.Visible = true;
                                    }
                                    dt.Dispose();
                                }
                                break;
                            case 1:
                                {
                                    if (Ped == "##" | Ped == "") return;
                                    DataTable dt = (((DataTable)Session["DVPed"]).Rows[0]["txt"].ToString() == "##" ? ((DataTable)Session["DVPed"]).Clone() : ((DataTable)Session["DVPed"]).Copy());
                                    var found = false;
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped)
                                        if (dt.Rows[i]["IdPedido"].ToString().Trim() == Ped)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (found == false)
                                    {

                                        var sP = "SPSIU_PROG|@TCOM;9|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped);
                                        var r = (int)ECmSp(sP, cnnCOS, 2);

                                        var strP = "SPExstProgVen|@IDORG;7460|@IDSECTOR;" + int.Parse(txtSector.Text) + "|@IDPEDIDO;" + Int64.Parse(Ped);
                                        var vIns = (int)ECmSp(strP, cnnCOS, 2);

                                        if (vIns != 1 & r != 1)
                                        {
                                            DataRow rw = dt.NewRow();
                                            rw["txt"] = txt;
                                            rw["IdPedido"] = Ped;
                                            dt.Rows.Add(rw);
                                            gv_Ped.DataSource = dt;
                                            gv_Ped.DataBind();
                                            gv_Ped.PageIndex = gv_Ped.PageCount;
                                            gv_Ped.DataBind();
                                            gv_Ped.EditIndex = -1;
                                            gv_Ped.DataBind();
                                            Session["DVPed"] = dt;
                                            Session["New"] = 0;

                                        }
                                        else
                                        {
                                            lblError_OG.Text = "Codigo Invalido, ya se encuentra registrado";
                                            lblError_OG.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        lblError_OG.Text = "Uno o mas Codigos de Pedido son Iguales";
                                        lblError_OG.Visible = true;
                                    }
                                    dt.Dispose();
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        DataTable dt = ((DataTable)Session["DVPed"]).Copy();
                        if (dt.Rows[0]["txt"].ToString().Trim() == "##") return;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(e.CommandArgument.ToString()) + (gv_Ped.PageSize * gv_Ped.PageIndex)) == i)
                            {
                                dt.Rows[i].Delete();
                                dt.AcceptChanges();
                                break;
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["txt"] = "Pedido " + (i + 1).ToString();
                        }
                        if (dt.Rows.Count == 0)
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = "##";
                            }
                            dt.Rows.Add(rw);
                        }
                        gv_Ped.DataSource = dt;
                        gv_Ped.DataBind();
                        gv_Ped.EditIndex = -1;
                        gv_Ped.DataBind();
                        Session["DVPed"] = dt;
                        Session["New"] = 0;

                        gv_Ped.EditIndex = -1;
                        dt.Dispose();
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        //lblEInsUp.Visible = false;
                        Session["New"] = 0;
                        gv_Ped.EditIndex = -1;
                        gv_Ped.DataSource = Session["DVPed"];
                        gv_Ped.DataBind();
                        #endregion
                    }
                    break;
            }
            //MPUP_OG.Show();
        }

        protected void gv_Ped_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Ped.EditIndex = -1;
            gv_Ped.PageIndex = e.NewPageIndex;
            gv_Ped.DataSource = Session["DVPed"];
            gv_Ped.DataBind();
        }

    }
}