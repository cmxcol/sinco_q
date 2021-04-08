using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure.Security.Usuario;
using WebApplication.Client.Web.App_Code;

namespace WebApplication.Client.Web.CO.PG.Adm
{
    public partial class AsigA : BPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                Session["dtRelAl"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;2|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                if (((DataTable)Session["dtRelAl"]).Rows.Count == 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= ((DataTable)Session["dtRelAl"]).Columns.Count - 1; i++)
                    {
                        dt.Columns.Add(((DataTable)Session["dtRelAl"]).Columns[i].ColumnName);
                    }
                    DataRow rw = dt.NewRow();
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        rw[i] = "##";
                    }
                    dt.Rows.Add(rw);
                    Session["dtRelAl"] = dt;
                    gvRelAlert.DataSource = dt;
                    gvRelAlert.DataBind();
                }
                else
                {
                    gvRelAlert.DataSource = Session["dtRelAl"];
                    gvRelAlert.DataBind();
                }
                gvRelAlert.Visible = true;
            }
        }
        protected void gvRelAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRelAlert.EditIndex = -1;
            gvRelAlert.PageIndex = e.NewPageIndex;
            gvRelAlert.DataSource = Session["dtRelAl"];
            gvRelAlert.DataBind();
        }
        protected void gvRelAlert_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo
                        DataTable dt = ((DataTable)Session["dtRelAl"]).Copy();
                        Session["RANew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (dt.Rows[0][0].ToString() == "##" & gvRelAlert.Rows.Count == 1)
                        {
                            gvRelAlert.EditIndex = 0;
                            gvRelAlert.DataSource = dt;
                            gvRelAlert.DataBind();
                            var dtTCri = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtSta = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvRelAlert, dtTCri, "ddlTAlert|IDTCRI|TCTXT", 1);
                            LdDdlGv(gvRelAlert, dtSta, "ddlSta|IDSTARG|STATXT", 1);
                            ((LinkButton)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("lbtnIdMsgRel")).Text = "Buscar";
                        }
                        else
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = DBNull.Value;
                            }
                            dt.Rows.Add(rw);
                            gvRelAlert.DataSource = dt;
                            gvRelAlert.DataBind();
                            gvRelAlert.PageIndex = gvRelAlert.PageCount;
                            gvRelAlert.DataBind();
                            gvRelAlert.EditIndex = gvRelAlert.Rows.Count - 1;
                            gvRelAlert.DataBind();
                            var dtTCri = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtSta = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvRelAlert, dtTCri, "ddlTAlert|IDTCRI|TCTXT", 1);
                            LdDdlGv(gvRelAlert, dtSta, "ddlSta|IDSTARG|STATXT", 1);
                            ((LinkButton)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("lbtnIdMsgRel")).Text = "Buscar";
                        }
                        #endregion
                    }
                    break;
                case "Editar":
                    {
                        #region Editar
                        DataTable dt = ((DataTable)Session["dtRelAl"]).Copy();
                        if (dt.Rows[0][0].ToString() != "##")
                        {
                            Session["RANew"] = 0;
                            gvRelAlert.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gvRelAlert.DataSource = Session["dtRelAl"];
                            gvRelAlert.DataBind();
                            var dtTCri = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtSta = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtRaId = (DataTable)ECmSp("alt.SPSIU_AL|@TCOM;6|@TTAB;2|@IDALERT;" + gvRelAlert.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text + "", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvRelAlert, dtTCri, "ddlTAlert|IDTCRI|TCTXT", 2, dtRaId, "0|1");
                            LdDdlGv(gvRelAlert, dtSta, "ddlSta|IDSTARG|STATXT", 2, dtRaId, "0|4");
                            if (int.Parse(((DropDownList)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("ddlTAlert")).SelectedValue) == 1)
                            {
                                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Text = "0";
                                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Enabled = false;
                            }
                            else
                            {
                                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Enabled = true;
                            }
                        }
                        #endregion
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar
                        var IdAl = gvRelAlert.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        var IdTCri = ((DropDownList)gvRelAlert.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlTAlert")).SelectedValue;
                        var IdMsg = ((LinkButton)gvRelAlert.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("lbtnIdMsgRel")).Text.Trim();
                        var IdCri = ((TextBox)gvRelAlert.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtCri")).Text.Trim();
                        var IdStaA = ((DropDownList)gvRelAlert.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlSta")).SelectedValue;
                        switch (((int)Session["RANew"]))
                        {
                            case 0:
                                {
                                    var res = 0;
                                    if (IdAl != "##" & int.TryParse(IdAl, out res) & int.TryParse(IdTCri, out res) & int.TryParse(IdMsg, out res) & IdCri != "" & IdCri != "##" & int.TryParse(IdStaA, out res))
                                    {
                                        lblEInsUp.Visible = false;
                                        res = (int)ECmSp("alt.SPSIU_AL|@TCOM;3|@TTAB;2|@IDALERT;" + IdAl.Trim() + "|@IDTCRI;" + IdTCri.Trim() + "|@IDMSGA;" + IdMsg.Trim() + "|@IDCRI;" + IdCri.Trim() + "|@IDSTARG;" + IdStaA.Trim() + "", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                                        if (res == 1)
                                        {
                                            Session["dtRelAl"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;2|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                        }
                                        gvRelAlert.EditIndex = -1;
                                        gvRelAlert.DataSource = Session["dtRelAl"];
                                        gvRelAlert.DataBind();
                                        Session["RANew"] = 0;
                                    }
                                    else
                                    {
                                        lblEInsUp.Text = "Datos Incorrectos";
                                        lblEInsUp.Visible = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    var res = 0;
                                    if (int.TryParse(IdTCri, out res) & int.TryParse(IdMsg, out res) & IdCri != "" & IdCri != "##" & int.TryParse(IdStaA, out res))
                                    {
                                        ECmSp("alt.SPSIU_AL|@TCOM;2|@TTAB;2|@IDTCRI;" + IdTCri.Trim() + "|@IDMSGA;" + IdMsg.Trim() + "|@IDCRI;" + IdCri.Trim() + "|@IDSTARG;" + IdStaA.Trim() + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 0);
                                        Session["dtRelAl"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;2|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                        gvRelAlert.EditIndex = -1;
                                        gvRelAlert.DataSource = Session["dtRelAl"];
                                        gvRelAlert.DataBind();
                                        Session["RANew"] = 0;
                                        lblEInsUp.Visible = false;
                                    }
                                    else
                                    {
                                        lblEInsUp.Text = "Datos Incorrectos";
                                        lblEInsUp.Visible = true;
                                    }
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        lblEInsUp.Visible = false;
                        Session["RANew"] = 0;
                        gvRelAlert.EditIndex = -1;
                        gvRelAlert.DataSource = Session["dtRelAl"];
                        gvRelAlert.DataBind();
                        #endregion
                    }
                    break;
            }
        }
        protected void lbtnIdMsgRel_Click(object sender, EventArgs e)
        {
            if (gvRelAlert.EditIndex == -1) return;
            if (((LinkButton)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("lbtnIdMsgRel")).Text == "##") return;
            Session["dtAlBusq"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
            gv_Alertas.PageIndex = 0;
            gv_Alertas.DataSource = Session["dtAlBusq"];
            gv_Alertas.DataBind();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }
        protected void gv_Alertas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Alertas.PageIndex = e.NewPageIndex;
            gv_Alertas.DataSource = Session["dtAlBusq"];
            gv_Alertas.DataBind();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }
        protected void gv_ABusq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ABusq.PageIndex = e.NewPageIndex;
            gv_ABusq.DataSource = Session["dtABCon"];
            gv_ABusq.DataBind();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
            pnlMPB.Visible = true;
            MpUp_AB.Show();
        }
        protected void gv_Alertas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void lbtnIdMSGA_Click(object sender, EventArgs e)
        {
            ((LinkButton)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("lbtnIdMsgRel")).Text = ((LinkButton)sender).Text;
            pnlMpBA.Visible = false;
            MpUp_MA.Hide();
        }
        protected void lbtnId_Click(object sender, EventArgs e)
        {
            pnlMPB.Visible = false;
            MpUp_AB.Hide();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
            var id = ((LinkButton)sender).Text;
            var pPage = false;
            for (var i = 0; i <= gv_Alertas.PageCount - 1; i++)
            {
                gv_Alertas.DataSource = Session["dtAlBusq"];
                gv_Alertas.PageIndex = i;
                gv_Alertas.DataBind();
                for (int j = 0; j <= gv_Alertas.Rows.Count - 1; j++)
                {
                    if (((LinkButton)gv_Alertas.Rows[j].FindControl("lbtnIdMSGA")).Text.Trim() != id) continue;
                    pPage = true;
                    gv_Alertas.DataSource = Session["dtAlBusq"];
                    gv_Alertas.PageIndex = i;
                    gv_Alertas.DataBind();
                    gv_Alertas.Rows[j].BackColor = Color.LightYellow;
                    break;
                }
                if (pPage)
                {
                    break;
                }
            }

        }
        protected void btnBusq_Click(object sender, EventArgs e)
        {
            pnlMpBA.Visible = true;
            MpUp_MA.Show();

            gv_Alertas.EditIndex = -1;
            gv_Alertas.DataSource = Session["dtAlBusq"];
            gv_Alertas.DataBind();
            if (txtMSGBusq.Text != "" | txtIdBusq.Text != "")
            {
                lblEBusq.Visible = false;
                bool pPage = false;
                if (txtIdBusq.Text != "")
                {
                    for (int i = 0; i <= gv_Alertas.PageCount - 1; i++)
                    {
                        gv_Alertas.DataSource = Session["dtAlBusq"];
                        gv_Alertas.PageIndex = i;
                        gv_Alertas.DataBind();
                        for (int j = 0; j <= gv_Alertas.Rows.Count - 1; j++)
                        {
                            if (((LinkButton)gv_Alertas.Rows[j].FindControl("lbtnIdMSGA")).Text.Trim() == txtIdBusq.Text.Trim())
                            {
                                pPage = true;
                                gv_Alertas.DataSource = Session["dtAlBusq"];
                                gv_Alertas.PageIndex = i;
                                gv_Alertas.DataBind();
                                gv_Alertas.Rows[j].BackColor = Color.LightYellow;
                                break;
                            }
                        }
                        if (pPage)
                        {
                            break;
                        }
                    }
                    if (pPage == false)
                    {
                        gv_Alertas.DataSource = Session["dtAlBusq"];
                        gv_Alertas.PageIndex = 0;
                        gv_Alertas.DataBind();
                    }
                }
                else if (txtMSGBusq.Text != "")
                {
                    Session["dtABCon"] = ECmSp("alt.SPSIU_AL|@TCOM;5|@TTAB;1|@MSGTEXT;" + txtMSGBusq.Text.Trim() + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                    if (((DataTable)Session["dtABCon"]).Rows.Count > 0)
                    {
                        gv_ABusq.DataSource = Session["dtABCon"];
                        gv_ABusq.PageIndex = 0;
                        gv_ABusq.DataBind();
                        pnlMPB.Visible = true;
                        MpUp_AB.Show();
                    }
                }
            }
            else
            {
                lblEBusq.Text = "Ingrese el Id o el texto de la Alerta!!";
                lblEBusq.Visible = true;
            }
        }
        protected void btnCanlBusq_Click(object sender, EventArgs e)
        {
            pnlMpBA.Visible = false;
            MpUp_MA.Hide();
        }
        protected void btnCanMP_Click(object sender, EventArgs e)
        {
            pnlMPB.Visible = false;
            MpUp_AB.Hide();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }
        protected void ddlTAlert_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(((DropDownList)sender).SelectedValue) == 1)
            {
                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Text = "0";
                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Enabled = false;
            }
            else
            {
                ((TextBox)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("txtCri")).Enabled = true;
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// PERMITE EJECUTAR PROCEDIMIENTOS ALMACENADOS
        /// </summary>
        /// <param name="strParam"></param>
        /// <param name="strCon"></param>
        /// <param name="tCom"></param>
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
        /// <summary>
        /// PERMITE INGRESAR DATOS EN UNA LISTA DESPLEGABLE QUE SE ENCUENTRA EN UN GRIDVIEW
        /// </summary>
        /// <param name="gV">GridView que contiene el DropDownList</param>
        /// <param name="dt">DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="strDataVt">Cadena que contiene el Nombre,DataValueField,DataTextField del DropDownList </param>
        /// <param name="TLoad">Indica si la carga se va a realizar para un nuevo registro o la modificacion de uno existente</param>
        /// <param name="dtDdl"></param>
        /// <param name="strPos"></param>
        public void LdDdlGv(GridView gV, DataTable dtDdl, String strDataVt, int TLoad, DataTable dt = null, String strPos = null)
        {
            switch (TLoad)
            {
                case 1:
                    {
                        #region Nuevo
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataSource = dtDdl;
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataValueField = strDataVt.Split('|')[1];
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataTextField = strDataVt.Split('|')[2];
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataBind();
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).Items.Insert(0, "Seleccione...");
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).Items[0].Selected = true;
                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region Edición
                        if (dt != null & strPos != null)
                        {
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataSource = dtDdl;
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataValueField = strDataVt.Split('|')[1];
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataTextField = strDataVt.Split('|')[2];
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).DataBind();
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).SelectedValue = dt.Rows[int.Parse(strPos.Split('|')[0])][int.Parse(strPos.Split('|')[1])].ToString();
                        }
                        #endregion
                    }
                    break;
            }
        }

        #endregion
    }
}