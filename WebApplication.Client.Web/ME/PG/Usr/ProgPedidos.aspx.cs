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
using Services.BCfg;
using Services.Instance;
using Services.Logs;
using Services.Simulador;
using DTO_Adapter.SAP;




//using WSDL_SO_MRP;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace WebApplication.Client.Web.ME.PG.Usr
{
    public partial class ProgPedidos : BPage
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
                d.MT_CustomerStatementReq.I_VKORG = "7544";
                d.MT_CustomerStatementReq.I_VTWEG = "00";
                d.MT_CustomerStatementReq.I_SPART = (txtSector.Text.Trim() == "" ? "0" : (txtSector.Text.Trim().Length == 1 & int.Parse(txtSector.Text) < 10 ? "0" + txtSector.Text.Trim() : txtSector.Text.Trim()));
                d.MT_CustomerStatementReq.I_FCURR = "COP";
                d.MT_CustomerStatementReq.I_TCURR = "COP";
                cli.ClientCredentials.UserName.UserName = "WSPOSINCOGBL";
                cli.ClientCredentials.UserName.Password = "C3m3X2020";
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
                    SimServ Master = new SimServ();
                    if (x.MT_CustomerStatementResp.E_BITOC.Trim() != "" & txtCodObra.Text.Trim() != "")
                        CAlert("1;|2;" + x.MT_CustomerStatementResp.E_BITOC.Trim() + "|3;" + txtCodObra.Text.Trim());
                    dtSeg = (DataTable)ECmSp("SPSIUSEGCLI|@TCOM;4|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC) + "|@IDSCANAL;9000032821", cnnSAPDB, 1);
                    Session["CliEx"] = ECmSp("SPSIT_CE|@IDCON;11|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC) + "|@IDOBRA;" + Int64.Parse(txtCodObra.Text), cnnCOS, 1);
                    dtExAll = (DataTable)ECmSp("SPSIT_CE|@IDCON;12|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC) + "|@IDOBRA;" + Int64.Parse(txtCodObra.Text) + "|@IdPais;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, cnnCOS, 1);
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
                    row[3] = Master.GetZTERM(long.Parse(txtCodObra.Text), Int32.Parse(txtSector.Text)<10?txtSector.Text.Split('0')[1]:txtSector.Text);//x.MT_CustomerStatementResp.E_ZTERM;
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



                    Session["dtBl"] = dtBl;

                    gvBlCli.DataSource = dtBl;
                    gvBlCli.DataBind();

                    Session["dtExAll"] = dtExAll;

                    gv_ExCli.DataSource = dtExAll;
                    gv_ExCli.DataBind();

                    dt.Rows.Add(row);
                    gv_DataCli.DataSource = dt;
                    gv_DataCli.DataBind();
                    gv_DataCli.Visible = true;

                    //Estado de cuenta

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

                    var rpctIlc = ServiceI<BCfgServ>.Instance.getPctILC(((IUsr)Session["Usr"]).usr.Pais.IdPais);
                    Double pctIlc = 0;
                    if (rpctIlc != null)
                    {
                        pctIlc = (rpctIlc.Value == String.Empty ? 0 : DNum(rpctIlc.Value));
                        pctIlc = (pctIlc >= 1 ? pctIlc / 100 : pctIlc) + 1;
                    }
                    else
                    {
                        pctIlc = 1;
                    }

                    row[0] = DNum(x.MT_CustomerStatementResp.E_LIMCR) * 100 * pctIlc;
                    row[1] = DNum(x.MT_CustomerStatementResp.E_SLDAF);
                    row[2] = DNum(x.MT_CustomerStatementResp.E_CARTT);
                    //row[3] = (x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ',').Contains('-') ? (double.Parse(x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ',').Replace('-', ' ')) * -1) : double.Parse(x.MT_CustomerStatementResp.E_IPFAC.Replace('.', ','))) * 100;
                    row[3] = DNum(x.MT_CustomerStatementResp.E_IPFAC) * 100;
                    //row[4] = CmpGinCliente(int.Parse(dt.Rows[0]["CODCLI"].ToString()));
                    row[4] = 0;

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

                    //Ocultar Columna Comprometido en GINCO
                    gv_EC_Cli.HeaderRow.Cells[4].Visible = false;
                    gv_EC_Cli.Rows[0].Cells[4].Visible = false;

                    //////////////////

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
                        try
                        {
                            var res = (from ex in exE
                                       where (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? DateTime.Parse(ex["dtVig"].ToString()) >= DateTime.Today : true)
                                       select new { Excepción = ex["NTEx"] as string, FechaVigencia = (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? String.Format("{0:yyyy-MM-dd}", ex["dtVig"]) : ex["dtVig"].ToString()), MsgEx = ex["MsgEx"] as string }).ToList();
                            gv_ExClientes.DataSource = res;
                            gv_ExClientes.DataBind();
                            gv_ExClientes.Visible = true;
                            lblExMsg.Text = "Puede proceder con la programación en RMS!!!";
                            lblEC.Visible = true;
                            //lblECT.Visible = true;
                            lblExMsg.CssClass = "labelInfo";
                            lblExMsg.Visible = true;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        if (double.Parse(row[5].ToString()) < 0)
                        {
                         
                            lblSN.Visible = true;
                            lblANP.Text = "NO EFECTUAR LA PROGRAMACIÓN SI NO SE REGISTRA UN AJUSTE!!";
                            lblANP.Visible = true;
                        }
                        else
                        {
                            lblSN.Visible = false;
                            //lblAC.Visible = false;
                  
                            //btnCal.Visible = true;
                            btnNCal.Visible = true;
                        }

                        var CliEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;10|@IDCLIENTE;" + Int64.Parse(x.MT_CustomerStatementResp.E_BITOC), cnnCOS, 1);
                        var ce = CliEx.Rows.Count > 0 ? CliEx.AsEnumerable() : null;
                        if (ce != null)
                        {
                            lblEC.Text = "Excepciones Activas.";
                            var res = (from ex in ce
                                       where (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? DateTime.Parse(ex["dtVig"].ToString()) >= DateTime.Today : true)
                                       select new { Excepción = ex["NTEx"] as string, FechaVigencia = (DateTime.TryParse(ex["dtVig"].ToString(), out date) ? String.Format("{0:yyyy-MM-dd}", ex["dtVig"]) : ex["dtVig"].ToString()), MsgEx = ex["MsgEx"] as string }).ToList();
                            gv_ExClientes.DataSource = res;
                            gv_ExClientes.DataBind();
                            gv_ExClientes.Visible = true;
                            lblExMsg.Visible = false;
                            lblEC.Visible = true;
                        }
                    }

                    ServLog RegLog = new ServLog();
                    RegLog.SaveCLog(((IUsr)Session["Usr"]).usr.CemexId, Int64.Parse(txtCodObra.Text.ToString()), Int32.Parse(txtSector.Text.ToString()), x.MT_CustomerStatementResp.E_CNAME, Int64.Parse(x.MT_CustomerStatementResp.E_BITOC), x.MT_CustomerStatementResp.E_BITON, Master.GetZTERM(long.Parse(txtCodObra.Text), Int32.Parse(txtSector.Text) < 10 ? txtSector.Text.Split('0')[1] : txtSector.Text)/*x.MT_CustomerStatementResp.E_ZTERM*/, dtVen.Rows.Count > 0 ? (dtVen.Rows[0]["NVen"]).ToString() : "", (Int64)(DNum(x.MT_CustomerStatementResp.E_LIMCR) * 100 * pctIlc), (Int64)DNum(x.MT_CustomerStatementResp.E_SLDAF), (Int64)DNum(x.MT_CustomerStatementResp.E_CARTT), (Int64)(DNum(x.MT_CustomerStatementResp.E_IPFAC) * 100), Int64.Parse(row[5].ToString()), dtExAll.Rows.Count > 0 ? (dtExAll.Rows[0].ItemArray[0]).ToString() : "");
                }
                else
                {
                    //pnlAditivos.Visible = false;
                    //pnlAB.Visible = false;
                    gv_DataCli.Visible = false;
                    gv_EC_Cli.Visible = false;
                    lblErrorCod.Visible = true;
                }


            }

            catch (Exception e) 
            {　

                throw ;
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
        //public double CmpGinCliente(int codigo)
        //{
        //    try
        //    {
        //        DataTable dtPg = new DataTable();
        //        dtPg.Columns.Add("NomParam");
        //        dtPg.Columns.Add("ValParam");
        //        DataRow row = dtPg.NewRow();
        //        row[0] = "@IDCOM";
        //        row[1] = 1;
        //        dtPg.Rows.Add(row);
        //        row = dtPg.NewRow();
        //        row[0] = "@IDCLIENTE";
        //        row[1] = codigo;
        //        dtPg.Rows.Add(row);
        //        DataTable dt = new DataTable();
        //        Double cmpGin = 0;
        //        String strCon = "SPSTOC";
        //        dt = ConSp(strCon, 1, dtPg, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString());
        //        //String strCon = "SELECT OBRA,PEDIDO,NOKIT,PLANTA,VOLUMEN,UNIDAD FROM TAB_GINCO_PEDIDOS WHERE CLIENTE = " + codigo;
        //        //dt = ConSp(strCon, 0, new DataTable(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString());
        //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            cmpGin = cmpGin + DNum(((SOrder(dt.Rows[i], (dt.Rows[i]["IdSector"].ToString().Trim() == "" ? "0" : (dt.Rows[i]["IdSector"].ToString().Trim().Length == 1 & int.Parse(dt.Rows[i]["IdSector"].ToString()) < 10 ? "0" + dt.Rows[i]["IdSector"].ToString().Trim() : dt.Rows[i]["IdSector"].ToString().Trim()))).MessageOutList == null ? SOrder(dt.Rows[i], (dt.Rows[i]["IdSector"].ToString().Trim() == "" ? "0" : (dt.Rows[i]["IdSector"].ToString().Trim().Length == 1 & int.Parse(dt.Rows[i]["IdSector"].ToString()) < 10 ? "0" + dt.Rows[i]["IdSector"].ToString().Trim() : dt.Rows[i]["IdSector"].ToString().Trim()))).ItemOutList[0].SubTot / 100 : 0)).ToString());
        //        }
        //        return cmpGin;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public void GvStyle(int gv, DataTable dt)
        {
            if (gv == 1)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //dt.Rows[i][5] = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][5].ToString()));
                    //dt.Rows[i][6] = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][6].ToString()));
                    //gv_SOrder.Rows[i].Cells[5].Text = String.Format("{0:$ #,#}", Double.Parse(dt.Rows[i][5].ToString()));
                    gv_SOrder.Rows[i].Cells[3].Text = gv_SOrder.Rows[i].Cells[3].Text!="&nbsp;"?String.Format("{0:$ #,#}", double.Parse(gv_SOrder.Rows[i].Cells[3].Text)):"0";
                    gv_SOrder.Rows[i].Cells[4].Text = gv_SOrder.Rows[i].Cells[4].Text!= "&nbsp;" ? String.Format("{0:$ #,#}", double.Parse(gv_SOrder.Rows[i].Cells[4].Text)):"0";
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
        /// 
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
            catch (Exception  e)
            {
                //String Error = DateTime.Now.ToString() + "|" + "SAPFTPLOAD" + "|" + "Load_SAPDB_Pedidos_SMS" + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + e.Message.ToString() + "|" + e.StackTrace.ToString();
                //Dts.Variables["Error"].Value = (Dts.Variables["Error"].Value.ToString() == String.Empty ? Error : Dts.Variables["Error"].Value.ToString() + ";" + Error);
                //Dts.TaskResult = (int)ScriptResults.Failure;
                throw;
            }

        }

        public DT_ResponseSimulateOrderTaking SOrder(DataRow row, String idSector, DateTime dtEnt)
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
            DT_SH.Purch_Ord = "1";
            DT_SH.Order_Dte = String.Format("{0:yyyyMMdd}", dtEnt);
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
            DT_SICRM.Req_Dte = String.Format("{0:yyyyMMdd}", dtEnt);
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
          

                var lI = new List<List<String>>()
                           {
                               new List<String>() {"##","##"},
                           };

                var qry = (from l in lI
                           select new { txt = l[0], Valor = l[1] }).ToList();

                Session["VolPed"] = lI;
                //Session["DVPed"] = new List<List<String>>();


            }
        }

        #region client Status Account 
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

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
            Response.Redirect("~/ME/PG/Usr/ProgPedidos.aspx");
        }




        #region Return Init Page

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
            Response.Redirect("~/ME/PG/Usr/ProgPedidos.aspx");
        }


        #endregion


        

        #endregion




        protected void gv_ExCli_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ExCli.PageIndex = e.NewPageIndex;
            gv_ExCli.DataSource = Session["dtExAll"];
            gv_ExCli.DataBind();
        }

        protected void gvAlSh_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlSh.PageIndex = e.NewPageIndex;
            gvAlSh.DataSource = Session["dtA"];
            gvAlSh.DataBind();
        }

        protected void gvBlCli_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBlCli.PageIndex = e.NewPageIndex;
            gvBlCli.DataSource = Session["dtBl"];
            gvBlCli.DataBind();
        }

        protected void gv_Condition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Condition.PageIndex = e.NewPageIndex;
            gv_Condition.DataSource = Session["Condiciones"];
            gv_Condition.DataBind();
        }

        protected void gv_SOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_SOrder.PageIndex = e.NewPageIndex;
            gv_SOrder.DataSource = Session["SOrder"];
            gv_SOrder.DataBind();
        }

        

        
    }
}
