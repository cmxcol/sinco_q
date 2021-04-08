using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure.Security.Usuario;
using WebApplication.Client.Web.App_Code;

namespace WebApplication.Client.Web.CO.PG.Usr
{
    public partial class Reportes : BPage
    {
        #region Atributos
        private String cnnCOS = System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString();
        private String cnnSAPDB = System.Configuration.ConfigurationManager.ConnectionStrings["cnnSAPDB"].ToString();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ddlRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_Rep1.Visible = false;
            gv_Rep2.Visible = false;
            gv_RepSINCO.Visible = false;
            gv_RepGINCO.Visible = false;
            lblESin.Visible = false;
            lblPSin.Visible = false;
            lblPGin.Visible = false;
            lblEGinIn.Visible = false;
            //pnlDREnv.Enabled = true;
            txtIdFac.Text = String.Empty;
            txtDIniRFE.Text = String.Empty;
            txtDFinRFE.Text = String.Empty;
            switch (ddlRep.SelectedIndex)
            {
                case 0:
                    rbPed.Checked = false;
                    rbFecha.Checked = false;
                    rbPed.Visible = false;
                    rbFecha.Visible = false;
                    pnlRFac.Visible = false;
                    pnlDREnv.Visible = false;
                    btnConsultar.Visible = false;
                    break;
                case 1:
                    rbPed.Checked = false;
                    rbFecha.Checked = false;
                    rbPed.Visible = true;
                    rbFecha.Visible = true;
                    pnlRFac.Visible = false;
                    pnlDREnv.Visible = false;
                    btnConsultar.Visible = false;
                    break;
                case 2:
                    rbPed.Checked = false;
                    rbFecha.Checked = false;
                    rbPed.Visible = true;
                    rbFecha.Visible = true;
                    pnlRFac.Visible = false;
                    pnlDREnv.Visible = false;
                    btnConsultar.Visible = false;
                    break;
                case 3:
                    rbPed.Checked = false;
                    rbFecha.Checked = false;
                    rbPed.Visible = true;
                    rbFecha.Visible = true;
                    pnlRFac.Visible = false;
                    pnlDREnv.Visible = false;
                    btnConsultar.Visible = false;
                    break;
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            gv_Rep1.Visible = false;
            gv_Rep2.Visible = false;
            gv_RepSINCO.Visible = false;
            gv_RepGINCO.Visible = false;
            switch (ddlRep.SelectedIndex)
            {
                case 1:
                    ExeRepProg();
                    break;
                case 2:
                    ExeRepA();
                    break;
                case 3:
                    ExeRepScvsGin();
                    break;
            }
            txtIdFac.Text = String.Empty;
        }
        public void ExeRepA()
        {
            try
            {
                lblError.Visible = false;
                DateTime dCom;
                if (rbFecha.Checked)
                {
                    if ((DateTime.TryParse(txtDIniRFE.Text, out dCom) & DateTime.TryParse(txtDFinRFE.Text, out dCom) ? (DateTime.Parse(txtDIniRFE.Text) > DateTime.Parse(txtDFinRFE.Text) ? false : true) : false))
                    {
                        Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;2|@TCOM;1|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        //var a = FachadaServicios.Instance.cRepFM(int.Parse(ddlMensj.SelectedValue), int.Parse(ddlEstados.SelectedValue), DateTime.Parse(txtDIni.Text), DateTime.Parse(txtDFin.Text));
                        if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                        {
                            gv_Rep1.DataSource = Session["dataR1"];
                            gv_Rep1.DataBind();
                            gv_Rep1.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "No existen Datos";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Fechas Incorrectas";
                        lblError.Visible = true;
                    }
                }
                if (rbPed.Checked)
                {
                    Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;2|@TCOM;2|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    //var a = FachadaServicios.Instance.cRepFM(int.Parse(ddlMensj.SelectedValue), int.Parse(ddlEstados.SelectedValue), DateTime.Parse(txtDIni.Text), DateTime.Parse(txtDFin.Text));
                    if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                    {
                        gv_Rep1.DataSource = Session["dataR1"];
                        gv_Rep1.DataBind();
                        gv_Rep1.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "No existen Datos";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                lblError.Visible = true;
            }
        }
        public void ExeRepProg()
        {
            try
            {
                lblError.Visible = false;
                DateTime dCom;
                if (rbFecha.Checked)
                {
                    if ((DateTime.TryParse(txtDIniRFE.Text, out dCom) & DateTime.TryParse(txtDFinRFE.Text, out dCom) ? (DateTime.Parse(txtDIniRFE.Text) > DateTime.Parse(txtDFinRFE.Text) ? false : true) : false))
                    {
                        Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;1|@TCOM;1|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        Session["dataR2"] = (DataTable)ECmSp("SPS_Rep|@TREP;5|@TCOM;1|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        //var a = FachadaServicios.Instance.cRepFM(int.Parse(ddlMensj.SelectedValue), int.Parse(ddlEstados.SelectedValue), DateTime.Parse(txtDIni.Text), DateTime.Parse(txtDFin.Text));                        
                        if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                        {
                            gv_Rep1.DataSource = Session["dataR1"];
                            gv_Rep1.DataBind();
                            gv_Rep1.Visible = true;

                            gv_Rep2.DataSource = Session["dataR2"];
                            gv_Rep2.DataBind();
                            gv_Rep2.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "No existen Datos";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Fechas Incorrectas";
                        lblError.Visible = true;
                    }
                }
                if (rbPed.Checked)
                {
                    Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;1|@TCOM;2|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    Session["dataR2"] = (DataTable)ECmSp("SPS_Rep|@TREP;5|@TCOM;2|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    //var a = FachadaServicios.Instance.cRepFM(int.Parse(ddlMensj.SelectedValue), int.Parse(ddlEstados.SelectedValue), DateTime.Parse(txtDIni.Text), DateTime.Parse(txtDFin.Text));
                    if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                    {
                        gv_Rep1.DataSource = Session["dataR1"];
                        gv_Rep1.DataBind();
                        gv_Rep1.Visible = true;

                        gv_Rep2.DataSource = Session["dataR2"];
                        gv_Rep2.DataBind();
                        gv_Rep2.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "No existen Datos";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                lblError.Visible = true;
            }
        }
        public void ExeRepScvsGin()
        {
            try
            {
                lblError.Visible = false;
                lblESin.Visible = false;
                lblEGinIn.Visible = false;
                lblPGin.Visible = false;
                lblPSin.Visible = false;
                DateTime dCom;
                if (rbFecha.Checked)
                {
                    if ((DateTime.TryParse(txtDIniRFE.Text, out dCom) & DateTime.TryParse(txtDFinRFE.Text, out dCom) ? (DateTime.Parse(txtDIniRFE.Text) > DateTime.Parse(txtDFinRFE.Text) ? false : true) : false))
                    {
                        Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;3|@TCOM;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        Session["ProgSINCO"] = (DataTable)ECmSp("SPS_Rep|@TREP;3|@TCOM;2|@IDPAIS;"+ ((IUsr)Session["Usr"]).usr.Pais.IdPais +"|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        Session["ProgGINCO"] = (DataTable)ECmSp("SPS_Rep|@TREP;3|@TCOM;3|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@FINI;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDIniRFE.Text)) + "|@FFIN;" + String.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDFinRFE.Text)), cnnCOS, 1);
                        //var a = FachadaServicios.Instance.cRepFM(int.Parse(ddlMensj.SelectedValue), int.Parse(ddlEstados.SelectedValue), DateTime.Parse(txtDIni.Text), DateTime.Parse(txtDFin.Text));
                        if (((DataTable)Session["dataR1"]).Rows.Count > 0 | ((DataTable)Session["ProgSINCO"]).Rows.Count > 0 | ((DataTable)Session["ProgGINCO"]).Rows.Count > 0)
                        {
                            if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                            {
                                lblESin.Visible = true;
                                gv_Rep1.DataSource = Session["dataR1"];
                                gv_Rep1.DataBind();
                                gv_Rep1.Visible = true;
                            }

                            if (((DataTable)Session["ProgSINCO"]).Rows.Count > 0 | ((DataTable)Session["ProgGINCO"]).Rows.Count > 0)
                            {
                                lblEGinIn.Visible = true;
                                lblPSin.Visible = true;
                                gv_RepSINCO.DataSource = Session["ProgSINCO"];
                                gv_RepSINCO.DataBind();
                                gv_RepSINCO.Visible = true;

                                lblPGin.Visible = true;
                                gv_RepGINCO.DataSource = Session["ProgGINCO"];
                                gv_RepGINCO.DataBind();
                                gv_RepGINCO.Visible = true;
                            }
                        }
                        else
                        {
                            lblError.Text = "No existen Datos";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Fechas Incorrectas";
                        lblError.Visible = true;
                    }
                }
                if (rbPed.Checked)
                {
                    Session["dataR1"] = (DataTable)ECmSp("SPS_Rep|@TREP;4|@TCOM;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    Session["ProgSINCO"] = (DataTable)ECmSp("SPS_Rep|@TREP;4|@TCOM;2|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    Session["ProgGINCO"] = (DataTable)ECmSp("SPS_Rep|@TREP;4|@TCOM;3|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDPEDIDO;" + txtIdFac.Text, cnnCOS, 1);
                    if (((DataTable)Session["dataR1"]).Rows.Count > 0 | ((DataTable)Session["ProgSINCO"]).Rows.Count > 0 | ((DataTable)Session["ProgGINCO"]).Rows.Count > 0)
                    {
                        if (((DataTable)Session["dataR1"]).Rows.Count > 0)
                        {
                            lblESin.Visible = true;
                            gv_Rep1.DataSource = Session["dataR1"];
                            gv_Rep1.DataBind();
                            gv_Rep1.Visible = true;
                        }

                        if (((DataTable)Session["ProgSINCO"]).Rows.Count > 0 | ((DataTable)Session["ProgGINCO"]).Rows.Count > 0)
                        {
                            lblEGinIn.Visible = true;
                            lblPSin.Visible = true;
                            gv_RepSINCO.DataSource = Session["ProgSINCO"];
                            gv_RepSINCO.DataBind();
                            gv_RepSINCO.Visible = true;

                            lblPGin.Visible = true;
                            gv_RepGINCO.DataSource = Session["ProgGINCO"];
                            gv_RepGINCO.DataBind();
                            gv_RepGINCO.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "No existen Datos";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                lblError.Visible = true;
            }
        }

        protected void rbFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFecha.Checked)
            {
                rbPed.Checked = false;
                pnlRFac.Visible = false;
                pnlDREnv.Visible = true;
                btnConsultar.Visible = true;
            }
        }
        protected void rbPed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPed.Checked)
            {
                rbFecha.Checked = false;
                pnlRFac.Visible = true;
                pnlDREnv.Visible = false;
                btnConsultar.Visible = true;
            }
        }
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


        protected void gv_Rep1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Rep1.EditIndex = -1;
            gv_Rep1.PageIndex = e.NewPageIndex;
            gv_Rep1.DataSource = Session["dataR1"];
            gv_Rep1.DataBind();
        }

        protected void gv_Rep2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Rep2.EditIndex = -1;
            gv_Rep2.PageIndex = e.NewPageIndex;
            gv_Rep2.DataSource = Session["dataR2"];
            gv_Rep2.DataBind();
        }

        protected void gv_RepSINCO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_RepSINCO.EditIndex = -1;
            gv_RepSINCO.PageIndex = e.NewPageIndex;
            gv_RepSINCO.DataSource = Session["ProgSINCO"];
            gv_RepSINCO.DataBind();
        }

        protected void gv_RepGINCO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_RepGINCO.EditIndex = -1;
            gv_RepGINCO.PageIndex = e.NewPageIndex;
            gv_RepGINCO.DataSource = Session["ProgGINCO"];
            gv_RepGINCO.DataBind();
        }
    }
}