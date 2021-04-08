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

namespace WebApplication.Client.Web.ME.PG.Adm
{
    public partial class CVen : BPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session["dtVen"] = ECmSp("SPSIU_VEN|@TCOM;5|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
            if (((DataTable)Session["dtVen"]).Rows.Count == 0)
            {
                //DataTable dt = ((DataTable)Session["dtVen"]).Clone();
                DataTable dt = new DataTable();
                for (int i = 0; i <= ((DataTable)Session["dtVen"]).Columns.Count - 1; i++)
                {
                    dt.Columns.Add(((DataTable)Session["dtVen"]).Columns[i].ColumnName);
                }
                DataRow rw = dt.NewRow();
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    rw[i] = "##";
                }
                dt.Rows.Add(rw);
                Session["dtVen"] = dt;
                gv_Alertas.DataSource = dt;
                gv_Alertas.DataBind();
            }
            else
            {
                gv_Alertas.DataSource = Session["dtVen"];
                gv_Alertas.DataBind();
            }
            gv_Alertas.Visible = true;
        }
        protected void gv_Alertas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Alertas.EditIndex = -1;
            gv_Alertas.DataSource = Session["dtVen"];
            gv_Alertas.DataBind();
        }
        protected void gv_Alertas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_Alertas.EditIndex = e.NewEditIndex;
            gv_Alertas.DataSource = Session["dtVen"];
            gv_Alertas.DataBind();
        }
        protected void gv_Alertas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int res = (int)ECmSp("alt.SPSIU_AL|@TCOM;3|@TTAB;1|@IDMSGA;|@MSGTEXT;", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            //if (res == 1)
            //{
            //    Session["dtVen"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
            //}
            gv_Alertas.EditIndex = -1;
            gv_Alertas.DataSource = Session["dtVen"];
            gv_Alertas.DataBind();
        }
        protected void gv_Alertas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Alertas.EditIndex = -1;
            gv_Alertas.PageIndex = e.NewPageIndex;
            gv_Alertas.DataSource = Session["dtVen"];
            gv_Alertas.DataBind();
        }
        protected void gv_Alertas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo
                        //DataTable dt = ((DataTable)Session["dtVen"]).Copy();
                        //Session["bNew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        //if (dt.Rows[0][0].ToString() == "##" & gv_Alertas.Rows.Count == 1)
                        //{
                        //    gv_Alertas.EditIndex = 0;
                        //    gv_Alertas.DataSource = dt;
                        //    gv_Alertas.DataBind();
                        //}
                        //else
                        //{
                        //    DataRow rw = dt.NewRow();
                        //    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        //    {
                        //        rw[i] = DBNull.Value;
                        //    }
                        //    dt.Rows.Add(rw);
                        //    gv_Alertas.DataSource = dt;
                        //    gv_Alertas.DataBind();
                        //    gv_Alertas.PageIndex = gv_Alertas.PageCount;
                        //    gv_Alertas.DataBind();
                        //    gv_Alertas.EditIndex = gv_Alertas.Rows.Count - 1;
                        //    gv_Alertas.DataBind();
                        //}
                        #endregion
                    }
                    break;
                case "Editar":
                    {
                        #region Editar
                        DataTable dt = ((DataTable)Session["dtVen"]).Copy();
                        if (dt.Rows[0][0].ToString() != "##")
                        {
                            Session["bNew"] = 0;
                            gv_Alertas.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gv_Alertas.DataSource = Session["dtVen"];
                            gv_Alertas.DataBind();
                        }
                        #endregion
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar
                        var strTel = ((TextBox)gv_Alertas.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtTel")).Text.Trim();
                        switch (((int)Session["bNew"]))
                        {
                            case 0:
                                {
                                    var IdVen = gv_Alertas.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text;
                                    if (strTel != "" & strTel != "##")
                                    {
                                        Int64 res = 0;
                                        if (IdVen != "##" & strTel != "" & Int64.TryParse(IdVen, out res))
                                        {
                                            lblEBusq.Visible = false;
                                            res = (int)ECmSp("SPSIU_VEN|@TCOM;4|@IDVEN;" + IdVen.Trim() + "|@TEL;" + strTel.Trim() + "", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                                            if (res == 1)
                                            {
                                                Session["dtVen"] = ECmSp("SPSIU_VEN|@TCOM;5|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                            }
                                        }
                                        else
                                        {
                                            lblEBusq.Text = "Datos Incorrectos";
                                            lblEBusq.Visible = true;

                                        }
                                        gv_Alertas.EditIndex = -1;
                                        gv_Alertas.DataSource = Session["dtVen"];
                                        gv_Alertas.DataBind();
                                        Session["bNew"] = 0;
                                    }
                                    else
                                    {
                                        lblEBusq.Text = "Ingrese el Numero de Celular";
                                        lblEBusq.Visible = true;
                                    }
                                }
                                break;
                            case 1:
                                //if (strTel != "" & strTel != "##")
                                //{
                                //    ECmSp("alt.SPSIU_AL|@TCOM;2|@TTAB;1|@MSGTEXT;" + strTel + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 0);
                                //    Session["dtVen"] = ECmSp("alt.SPSIU_AL|@TCOM;4|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDROL;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                //    gv_Alertas.EditIndex = -1;
                                //    gv_Alertas.DataSource = Session["dtVen"];
                                //    gv_Alertas.DataBind();
                                //    Session["bNew"] = 0;
                                //    lblEBusq.Visible = false;
                                //}
                                //else
                                //{
                                //    lblEBusq.Text = "Ingrese el Numero de Celular";
                                //    lblEBusq.Visible = true;
                                //}
                                break;
                        }
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        lblEBusq.Visible = false;
                        Session["bNew"] = 0;
                        gv_Alertas.EditIndex = -1;
                        gv_Alertas.DataSource = Session["dtVen"];
                        gv_Alertas.DataBind();
                        #endregion
                    }
                    break;
            }
        }
        protected void btnBusq_Click(object sender, EventArgs e)
        {
            gv_Alertas.EditIndex = -1;
            gv_Alertas.DataSource = Session["dtVen"];
            gv_Alertas.DataBind();
            if (txtNVenBusq.Text != "" | txtIdBusq.Text != "")
            {
                lblEBusq.Visible = false;
                bool pPage = false;
                if (txtIdBusq.Text != "")
                {
                    for (int i = 0; i <= gv_Alertas.PageCount - 1; i++)
                    {
                        gv_Alertas.DataSource = Session["dtVen"];
                        gv_Alertas.PageIndex = i;
                        gv_Alertas.DataBind();
                        for (int j = 0; j <= gv_Alertas.Rows.Count - 1; j++)
                        {
                            if (gv_Alertas.Rows[j].Cells[0].Text.Trim() == txtIdBusq.Text.Trim())
                            {
                                pPage = true;
                                gv_Alertas.DataSource = Session["dtVen"];
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
                        gv_Alertas.DataSource = Session["dtVen"];
                        gv_Alertas.PageIndex = 0;
                        gv_Alertas.DataBind();
                    }
                    //gv_Alertas.DataSource = Session["dtVen"];
                    //gv_Alertas.PageIndex = pPage;
                    //gv_Alertas.DataBind();
                }
                else if (txtNVenBusq.Text != "")
                {
                    Session["dtABusq"] = ECmSp("SPSIU_VEN|@TCOM;6|@NVEN;" + txtNVenBusq.Text.Trim() + "|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                    if (((DataTable)Session["dtABusq"]).Rows.Count > 0)
                    {
                        gv_ABusq.DataSource = Session["dtABusq"];
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
        protected void gv_ABusq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ABusq.PageIndex = e.NewPageIndex;
            gv_ABusq.DataSource = Session["dtABusq"];
            gv_ABusq.DataBind();
            pnlMPB.Visible = true;
            MpUp_AB.Show();
        }
        protected void btnCanMP_Click(object sender, EventArgs e)
        {
            pnlMPB.Visible = false;
            MpUp_AB.Hide();
        }
        protected void lbtnId_Click(object sender, EventArgs e)
        {
            var id = ((LinkButton)sender).Text;
            var pPage = false;
            for (var i = 0; i <= gv_Alertas.PageCount - 1; i++)
            {
                gv_Alertas.DataSource = Session["dtVen"];
                gv_Alertas.PageIndex = i;
                gv_Alertas.DataBind();
                for (int j = 0; j <= gv_Alertas.Rows.Count - 1; j++)
                {
                    if (gv_Alertas.Rows[j].Cells[0].Text.Trim() != id) continue;
                    pPage = true;
                    gv_Alertas.DataSource = Session["dtVen"];
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

        #endregion

        #region Metodos

        /// <summary>
        /// EJECUTA UN PROCEDIMIENTO ALMACENADO EN BASE A LA CONEXION y PARAMETROS DADOS
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

        #endregion
    }
}