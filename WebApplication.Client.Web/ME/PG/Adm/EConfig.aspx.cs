using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure.Security.Usuario;
using WebApplication.Client.Web.App_Code;
using Services.Excp;
using Services.CBloqueados;

namespace WebApplication.Client.Web.ME.PG.Adm
{
    public partial class EConfig : BPage
    {
        #region Eventos



        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["editOp"] = false;
            lblEInsUp.Visible = false;
            if (IsPostBack != true)
            {

                Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                if (((DataTable)Session["dtExCli"]).Rows.Count == 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= ((DataTable)Session["dtExCli"]).Columns.Count - 1; i++)
                    {
                        dt.Columns.Add(((DataTable)Session["dtExCli"]).Columns[i].ColumnName);
                    }
                    DataRow rw = dt.NewRow();
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        rw[i] = "##";
                    }
                    dt.Rows.Add(rw);
                    Session["dtExCli"] = dt;
                    gvExCli.DataSource = dt;
                    gvExCli.DataBind();
                }
                else
                {
                    gvExCli.DataSource = Session["dtExCli"];
                    gvExCli.DataBind();
                }
                gvExCli.Visible = true;

                if (((IUsr)Session["Usr"]).usr.Rol.IdRol != 7 ) {
                    btnCliBlo.Visible = false;
                }
                    
            }
        }
        protected void gvExCli_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExCli.EditIndex = -1;
            gvExCli.PageIndex = e.NewPageIndex;
            gvExCli.DataSource = Session["dtExCli"];
            gvExCli.DataBind();
        }
        protected void gvExCli_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo

                        Session["editOp"] = false;
                        pnlMotivo.Visible = false;
                        MpUp_MA.Hide();

                        gvExCli.EditIndex = -1;
                        Session["dtDoc"] = null;
                        DataTable dt = ((DataTable)Session["dtExCli"]).Copy();
                        Session["RANew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (dt.Rows[0][0].ToString() == "##" & gvExCli.Rows.Count == 1)
                        {
                            gvExCli.EditIndex = 0;
                            gvExCli.DataSource = dt;
                            gvExCli.DataBind();
                            var dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtTEx, "ddlTEx|IdTEx|NTEx", 1);
                            ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";

                            dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtTEx, "ddlCome|ComercialID|NComercial", 1);
                            ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";


                            //----
                            var dtMotivos = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;5", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtMotivos, "dropMotivo|IdMotivo|Descripcion", 1);
                            //----
                        }
                        else
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = DBNull.Value;
                            }
                            dt.Rows.Add(rw);
                            gvExCli.DataSource = dt;
                            gvExCli.DataBind();
                            gvExCli.PageIndex = gvExCli.PageCount;
                            gvExCli.DataBind();
                            gvExCli.EditIndex = gvExCli.Rows.Count - 1;
                            gvExCli.DataBind();

                            var dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtTEx, "ddlTEx|IdTEx|NTEx", 1);
                            ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";

                            dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtTEx, "ddlCome|ComercialID|NComercial", 1);
                            ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";

                            //----
                            var dtMotivos = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;5", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtMotivos, "dropMotivo|IdMotivo|Descripcion", 1);
                            //----
                        }
                        #endregion
                    }
                    break;
                case "Buscar":
                    {
                        #region Buscar

                        pnlMotivo.Visible = false;
                        MpUp_MA.Hide();

                        Session["dtDoc"] = null;
                        DataTable dt2 = ((DataTable)Session["dtExCli"]).Copy();
                        if (dt2.Rows[0][0].ToString() == "##" & gvExCli.Rows.Count == 1) return;
                        gvExCli.EditIndex = -1;
                        gvExCli.DataSource = Session["dtExCli"];
                        gvExCli.DataBind();
                        gv_Alertas.DataSource = new DataTable();
                        gv_Alertas.DataBind();

                        txtIdBusq.Text = String.Empty;
                        txtMSGBusq.Text = String.Empty;

                        lblId.Text = "Id:";
                        lblTxtCod.Text = "Codigo:";
                        lblCargo.Visible = false;
                        ddlCargo.Visible = false;

                        txtIdBusq_TextBoxWatermarkExtender.WatermarkText = "Excepción";
                        txtMSGBusq_TextBoxWatermarkExtender.WatermarkText = "Cliente";

                        gv_Alertas.Visible = true;
                        gvEmpA.Visible = false;

                        pnlMpBA.Visible = true;
                        MpUp_MA.Show();

                        Session["TCon"] = "ConEx";

                        #endregion
                    }
                    break;
                case "Editar":
                    {
                        #region Editar
                        Session["dtDoc"] = null;
                        DataTable dt = ((DataTable)Session["dtExCli"]).Copy();

                        Session["editOp"] = true;
                        pnlMotivo.Visible = false;
                        MpUp_MA.Hide();

                        if (dt.Rows[0][0].ToString() != "##")
                        {
                            Session["RANew"] = 0;
                            gvExCli.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gvExCli.DataSource = Session["dtExCli"];
                            gvExCli.DataBind();





                            var dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtRaId = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;2|@IdEx;" + gvExCli.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text + "", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            var dtMotivos = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;5", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

                            LdDdlGv(gvExCli, dtTEx, "ddlTEx|IdTEx|NTEx", 2, dtRaId, "0|2");

                            dtTEx = (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;4", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            LdDdlGv(gvExCli, dtTEx, "ddlCome|ComercialID|NComercial", 2, dtRaId, "0|2");

                            //----
                            LdDdlGv(gvExCli, dtMotivos, "dropMotivo|IdMotivo|Descripcion", 2, dtRaId, "0|2");
                            //----


                            string idEx = gvExCli.Rows[gvExCli.EditIndex].Cells[0].Text;


                            var query = "SPSIT_CE|@IDCON;13|@IdEx;" + idEx;
                            var dtMot = (DataTable)ECmSp(query, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

                            string mot = "";
                            string tipoExp = "";

                            foreach (DataRow row in dtMot.Rows)
                            {
                                mot = row["DescMotivo"].ToString();
                                tipoExp = row["IdTEx"].ToString();
                            }

                            if (mot.CompareTo("") != 0)
                            {


                                ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).Items.Insert(0, new ListItem(mot, "-1"));
                                ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).ClearSelection();
                                ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).Items.FindByText(mot).Selected = true;
                            }


                            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).ClearSelection();
                            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).Items.FindByText(tipoExp).Selected = true;
                            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).Enabled = false;

                            if (((DropDownList)gvExCli.Rows[Int32.Parse(e.CommandArgument.ToString())].FindControl("ddlTEx")).SelectedValue == "PMT")
                            {
                                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Text = "-";
                                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Enabled = false;
                            }
                            else
                            {
                                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Enabled = true;
                            }
                            var countDocs = dt.Rows[Convert.ToInt32(e.CommandArgument.ToString())]["CountDoc"].ToString();
                            if (((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text == "" | ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text == null)
                            {
                                ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";
                            }
                        }
                        #endregion
                    }
                    break;
                case "Guardar":
                    {
                        #region Guardar

                        pnlMotivo.Visible = false;
                        MpUp_MA.Hide();

                        var IdAl = gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        var IdTEx = ((DropDownList)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlTEx")).SelectedValue;
                        var IdCli = ((TextBox)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtIdCli")).Text.Trim();
                        var dtVg = ((TextBox)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtdtVg")).Text.Trim();
                        var MsgEx = ((TextBox)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtMsgEx")).Text.Trim();
                        var IdEmp = ((LinkButton)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("lbtnIdEmpAuthC")).Text.Trim();
                        var IdComcial = ((DropDownList)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlCome")).SelectedValue;
                        var Motivo = ((DropDownList)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("dropMotivo")).SelectedValue;
                        var descMotivo = "";

                        if (Motivo.CompareTo("-1") == 0)
                        {
                            Motivo = "6";
                        }

                        if (Motivo.CompareTo("6") == 0)
                        {
                            descMotivo = ((DropDownList)gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("dropMotivo")).SelectedItem.Text;
                            if (descMotivo.CompareTo("Otro:") == 0)
                            {
                                descMotivo = "";
                            }
                        }

                        switch (((int)Session["RANew"]))
                        {
                            case 0:
                                {
                                    //Actualizar Ex
                                    DateTime date;
                                    int res;
                                    Int64 rCli;
                                    if (IdAl != "##" & int.TryParse(IdAl, out res) & Int64.TryParse(IdCli.Trim(), out rCli) & ((IdTEx != "0" & IdTEx != "PMT" & DateTime.TryParse(dtVg, out date)) | (IdTEx == "PMT") ? true : false) & (IdTEx == "PMT" ? true : (IdEmp != String.Empty & IdEmp != "Buscar" ? true : false)))
                                    {
                                        Boolean dateV = false;

                                        if ((IdTEx == "PMT" ? true : DateTime.Parse(dtVg) >= DateTime.Today) || (((Boolean)Session["editOp"]) == true))
                                        {
                                            dateV = true;
                                        }



                                        if (dateV)
                                        {
                                            lblEInsUp.Visible = false;
                                            IdEmp = (IdEmp != String.Empty & IdEmp != "Buscar" ? IdEmp : "0");
                                            res = (int)ECmSp("SPU_Ex|@IdEx;" + IdAl.Trim() +
                                            "|@IdCliente;" + IdCli.Trim() +
                                            "|@IdTEx;" + IdTEx.Trim() +
                                            "|@dtVig;" + (IdTEx.Trim() != "PMT" ? String.Format("{0:yyyy-MM-dd}", DateTime.Parse(dtVg.Trim())) : "") +
                                            "|@IdPais;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais +
                                            "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol +
                                            "|@IdUsr;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                            "|@MsgEx;" + MsgEx.ToString().Trim() +
                                            "|@IdEmp;" + IdEmp +
                                            "|@IdComcial;" + IdComcial +
                                            "|@Motivo;" + Motivo +
                                            "|@DescMotivo;" + descMotivo,
                                            System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                                            if (res == 1)
                                            {
                                                var lNDoc = (List<List<Object>>)Session["dtDoc"];

                                                if (lNDoc != null)
                                                {
                                                    var lF = (from li in lNDoc
                                                              where li[0].ToString() != "##"
                                                              select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3] as String, FData = li[4] as Byte[], Path = li[5] as String }).ToList();
                                                    if (lF.Count > 0)
                                                    {
                                                        foreach (var l in lF)
                                                        {
                                                            using (var cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString()))
                                                            {
                                                                SqlCommand cmd = new SqlCommand();
                                                                cmd.CommandText = "auth_ccs.SPI_ExF";
                                                                cmd.CommandTimeout = 0;
                                                                cmd.CommandType = CommandType.StoredProcedure;
                                                                cmd.Parameters.AddWithValue("@FData", l.FData);
                                                                cmd.Parameters.AddWithValue("@Nombre", l.Nombre);
                                                                cmd.Parameters.AddWithValue("@FileExt", l.ContentType);
                                                                cmd.Parameters.AddWithValue("@IdEx", IdAl);
                                                                cmd.Connection = cnn;
                                                                if (cnn.State == ConnectionState.Closed)
                                                                    cnn.Open();
                                                                cmd.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                }
                                                Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

                                                var list = new List<List<Object>>
                                                               {
                                                                   new List<Object>()
                                                                       {"##", "##", "##", "##", new object(), "##"}
                                                               };



                                                Session["dtDoc"] = (from ld in list
                                                                    select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();

                                                //Response.Redirect(Request.RawUrl);
                                                //Response.Write(Request.RawUrl.ToString());
                                                lblEInsUp.ForeColor = Color.Green;
                                                lblEInsUp.Text =
                                                    "Refresque la página para ver reflejados los cambios";
                                                lblEInsUp.Visible = true;

                                                Response.Redirect(Request.RawUrl);
                                            }
                                            else if (res == 2)
                                            {
                                                lblEInsUp.ForeColor = Color.Red;
                                                lblEInsUp.Text = "La Excepción pertenece al Rol " +
                                                                 ((Label)
                                                                  gvExCli.Rows[Int32.Parse(e.CommandArgument.ToString())
                                                                      ].FindControl("lblNRol")).Text +
                                                                 ", No se aplicaron los cambios";
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res == 3)
                                            {
                                                lblEInsUp.ForeColor = Color.Red;
                                                lblEInsUp.Text =
                                                    "La excepción se encuentra configurada para un pais diferente, No se aplicaron los cambios";
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res == 5)
                                            {
                                                lblEInsUp.ForeColor = Color.Green;
                                                lblEInsUp.Text =
                                                    "Refresque la página para ver reflejados los cambios";
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res == 0)
                                            {
                                                lblEInsUp.ForeColor = Color.Red;
                                                lblEInsUp.Text =
                                                    "El cliente ya se encuentra configurado con el tipo Excepción seleccionada.";
                                                lblEInsUp.Visible = true;
                                            }
                                            gvExCli.EditIndex = -1;
                                            gvExCli.DataSource = Session["dtExCli"];
                                            gvExCli.DataBind();
                                            Session["RANew"] = 0;
                                        }
                                        else
                                        {
                                            lblEInsUp.ForeColor = Color.Red;
                                            lblEInsUp.Text = "La Fecha de Vigencia debe ser Mayor o Igual a la Actual";
                                            lblEInsUp.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        lblEInsUp.ForeColor = Color.Red;
                                        lblEInsUp.Text = "Datos Incorrectos";
                                        lblEInsUp.Visible = true;
                                    }
                                    //  Response.Redirect(Request.RawUrl);
                                }

                                break;
                            case 1:
                                {
                                    //Nueva Ex
                                    DateTime date;
                                    int res;
                                    Int64 rcli;
                                    if (Int64.TryParse(IdCli.Trim(), out rcli) & ((IdTEx != "0" & IdTEx != "PMT" & DateTime.TryParse(dtVg, out date)) | (IdTEx == "PMT") ? true : false) & (IdTEx == "PMT" ? true : (IdEmp != String.Empty & IdEmp != "Buscar" ? true : false)))
                                    {
                                        if ((IdTEx == "PMT" ? true : DateTime.Parse(dtVg) >= DateTime.Today))
                                        {
                                            lblEInsUp.Visible = false;
                                            IdEmp = (IdEmp != String.Empty & IdEmp != "Buscar" ? IdEmp : "0");
                                            res = (int)ECmSp(
                                                    "SPI_Ex|@IdCliente;" + IdCli.Trim() +
                                                    "|@IdTEx;" + IdTEx.Trim() +
                                                    "|@dtVig;" + (IdTEx.Trim() != "PMT" ? String.Format("{0:yyyy-MM-dd}", DateTime.Parse(dtVg.Trim())) : "") +
                                                    "|@IdPais;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais +
                                                    "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol +
                                                    "|@IdUsr;" + ((IUsr)Session["Usr"]).usr.CemexId +
                                                    "|@MsgEx;" + MsgEx.ToString().Trim() +
                                                    "|@IdEmp;" + IdEmp +
                                                    "|@IdComcial;" + IdComcial +
                                                    "|@Motivo;" + Motivo +
                                                    "|@DescMotivo;" + descMotivo,

                                                    System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                                            if (res > 100)
                                            {
                                                Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                                var lex = ((DataTable)Session["dtExCli"]).AsEnumerable();
                                                var lTEx = ((DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1)).AsEnumerable();
                                                var lr = (from ex in lex
                                                          join lt in lTEx on ex[2].ToString() equals lt[1].ToString()
                                                          where
                                                              ex[1].ToString() == IdCli.Trim() &
                                                              lt[0].ToString() == IdTEx.Trim()
                                                          select new { IdEx = ex[0] }).ToList();
                                                int idEx = 0;
                                                if (lr.Count > 0)
                                                {
                                                    idEx = int.Parse(lr.First().IdEx.ToString());
                                                }
                                                if (idEx != 0)
                                                {
                                                    var lNDoc = (List<List<Object>>)Session["dtDoc"];
                                                    if (lNDoc != null)
                                                    {
                                                        var lF = (from li in lNDoc
                                                                  where li[0].ToString() != "##"
                                                                  select new
                                                                  {
                                                                      IdDocument = li[0] as String,
                                                                      Nombre = li[1] as String,
                                                                      FileExt = li[2] as String,
                                                                      ContentType = li[3] as String,
                                                                      FData = li[4] as Byte[],
                                                                      Path = li[5] as String
                                                                  }).ToList();
                                                        if (lF.Count > 0)
                                                        {
                                                            //inserta el documento
                                                            foreach (var l in lF)
                                                            {
                                                                using (
                                                                    var cnn =
                                                                        new SqlConnection(
                                                                            System.Configuration.ConfigurationManager.
                                                                                ConnectionStrings["cnnCOS"].ToString()))
                                                                {
                                                                    SqlCommand cmd = new SqlCommand();
                                                                    cmd.CommandText = "auth_ccs.SPI_ExF";
                                                                    cmd.CommandTimeout = 0;
                                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                                    cmd.Parameters.AddWithValue("@FData", l.FData);
                                                                    cmd.Parameters.AddWithValue("@Nombre", l.Nombre);
                                                                    cmd.Parameters.AddWithValue("@FileExt", l.ContentType);
                                                                    //cmd.Parameters.AddWithValue("@IdEx", idEx);
                                                                    cmd.Parameters.AddWithValue("@IdEx", res);
                                                                    cmd.Connection = cnn;
                                                                    if (cnn.State == ConnectionState.Closed)
                                                                        cnn.Open();
                                                                    cmd.ExecuteNonQuery();
                                                                }
                                                            }
                                                            Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                                        }
                                                    }
                                                }
                                                var list = new List<List<Object>>();
                                                list.Add(new List<Object>() { "##", "##", "##", "##", new object(), "##" });
                                                var lFin = (from li in list
                                                            select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3], FData = li[4] as Byte[], Path = li[5] as String }).ToList();

                                                Session["dtDoc"] = (from ld in list
                                                                    select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();

                                                Response.Redirect(Request.RawUrl);
                                            }
                                            else if (res == 0)
                                            {
                                                lblEInsUp.ForeColor = Color.Red;
                                                lblEInsUp.Text = "El cliente ya tiene una Excepción configurada con una fecha mayor a " + dtVg.Trim();
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res == 7)
                                            {
                                                lblEInsUp.ForeColor = Color.Red;
                                                lblEInsUp.Text = "La fecha de la Excepción debe ser mayor a la del dia de hoy";
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res < 0)
                                            {
                                                lblEInsUp.ForeColor = Color.Black;
                                                lblEInsUp.Font.Size = 10;
                                                lblEInsUp.Text = "Autorización no creada, ya que el cliente solo tiene " + (res * -1) + " dias de vigencia para autorizar ";
                                                lblEInsUp.Visible = true;
                                            }
                                            else if (res == 4)
                                            {
                                                lblEInsUp.ForeColor = Color.Black;
                                                lblEInsUp.Font.Size = 10;
                                                lblEInsUp.Text = "No se puede generar autorización debido a que ha superado el número máximo de autorizaciones en 3 meses para este cliente";
                                                lblEInsUp.Visible = true;
                                            }
                                            gvExCli.EditIndex = -1;
                                            gvExCli.DataSource = Session["dtExCli"];
                                            gvExCli.DataBind();
                                            Session["RANew"] = 0;
                                        }
                                        else
                                        {
                                            lblEInsUp.ForeColor = Color.Red;
                                            lblEInsUp.Text = "La Fecha de Vigencia debe ser Mayor a la Actual";
                                            lblEInsUp.Visible = true;
                                        }
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
                case "EmpAuthC":
                    {
                        #region Autorizante

                        if (((LinkButton)gvExCli.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbtnIdEmpAuthC")).Text == "##") return;
                        if (gvExCli.EditIndex == -1)
                        {
                            var emp = (DataTable)ECmSp("hr.SPS_EmpCNC|@IdEmp;" + ((LinkButton)gvExCli.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbtnIdEmpAuthC")).Text, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            if (emp.Rows.Count == 0) return;
                            gvCEmp.DataSource = emp;
                            gvCEmp.DataBind();
                            gvCEmp.Visible = true;
                            gvCDoc.Visible = false;

                            pnlMpCEmpDoc.Visible = true;
                            MpUp_CEmpDoc.Show();
                        }
                        else
                        {
                            gv_Alertas.Visible = false;
                            gvEmpA.Visible = true;

                            txtIdBusq.Text = String.Empty;
                            txtMSGBusq.Text = String.Empty;

                            lblId.Text = "Id:";
                            lblTxtCod.Text = "Nombre:";
                            lblCargo.Visible = true;
                            ddlCargo.Visible = true;

                            txtIdBusq_TextBoxWatermarkExtender.WatermarkText = "Empleado";
                            txtMSGBusq_TextBoxWatermarkExtender.WatermarkText = "Empleado";
                            Session["TCon"] = "AsigA";

                            if (ddlCargo.Items.Count == 0)
                            {
                                var lCargos = (DataTable)ECmSp("hr.SPS_CargEmp", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                ddlCargo.DataSource = lCargos;
                                ddlCargo.DataValueField = "IdCargo";
                                ddlCargo.DataTextField = "NCargo";
                                ddlCargo.DataBind();
                                ddlCargo.Items.Insert(0, new ListItem("Seleccione...", "0"));
                                ddlCargo.Items[0].Selected = true;
                            }
                        }


                        #endregion
                    }
                    break;



                case "AddDoc":
                    {
                        #region Documentos

                        if (Session["dtDoc"] == null)
                        {
                            var l = new List<List<Object>>();
                            l.Add(new List<Object>() { "##", "##", "##", "##", new object(), "##" });
                            var lF = (from li in l
                                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3], FData = li[4] as Byte[], Path = li[5] as String }).ToList();

                            var lres = (from ld in l
                                        select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();
                            Session["dtDoc"] = lres;
                            gvNDoc.DataSource = lF;
                            gvNDoc.DataBind();
                        }
                        else
                        {
                            var l = (List<List<Object>>)Session["dtDoc"];


                            var lF = (from li in l
                                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3], FData = li[4] as Byte[], Path = li[5] as String }).ToList();

                            gvNDoc.DataSource = lF;
                            gvNDoc.DataBind();
                        }

                        gvNDocOld.Visible = false;
                        gvNDoc.Visible = true;

                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        #endregion
                    }
                    break;
                //case "ModDoc":
                //    {
                //        #region Documentos
                //        if (gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim() == "##") return;
                //        var IdEx = gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                //        DataTable dtNew = (DataTable)ECmSp("auth_ccs.SPS_ExF|@IdEx;" + IdEx, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                //        if (dtNew.Rows.Count == 0) return;
                //        Session["dtDocOld"] = dtNew;
                //        gvNDocOld.DataSource = Session["dtDocOld"];
                //        gvNDocOld.DataBind();
                //        gvNDocOld.Visible = true;
                //        gvNDoc.Visible = false;
                //        pnlMpAdj.Visible = true;
                //        MpUp_Adj.Show();
                //        #endregion
                //    }
                //    break;
                case "DocAdj":
                    {
                        #region Documentos Consulta
                        if (((LinkButton)gvExCli.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbtnDocAdjV")).Text == "##") return;
                        if (gvExCli.EditIndex == -1)
                        {
                            gvCEmp.Visible = false;
                            gvCDoc.Visible = true;
                            int idEx = int.Parse(gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim());
                            var docs = (DataTable)ECmSp("auth_ccs.SPS_ExF|@IdEx;" + idEx, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            if (docs.Rows.Count == 0) return;
                            //Session["dtDocOld"] = docs;
                            gvCDoc.DataSource = docs;
                            gvCDoc.DataBind();
                            Session["idExDocA"] = idEx;
                            pnlMpCEmpDoc.Visible = true;
                            MpUp_CEmpDoc.Show();
                        }

                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        if (gvExCli.EditIndex != -1) return;
                        int ent;
                        var IdEx = gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        if (IdEx.Trim() == "##" & !int.TryParse(IdEx, out ent)) return;
                        lblEInsUp.Visible = false;
                        var r = (int)ECmSp("SPD_Ex|@IdEx;" + IdEx + "|@IdPais;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol + "|@IdUsr;" + ((IUsr)Session["Usr"]).usr.CemexId, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                        if (r == 1)
                        {
                            Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            //gvExCli.DataSource = Session["dtExCli"];
                            //gvExCli.DataBind();
                            if (((DataTable)Session["dtExCli"]).Rows.Count == 0)
                            {
                                DataTable dt = new DataTable();
                                for (int i = 0; i <= ((DataTable)Session["dtExCli"]).Columns.Count - 1; i++)
                                {
                                    dt.Columns.Add(((DataTable)Session["dtExCli"]).Columns[i].ColumnName);
                                }
                                DataRow rw = dt.NewRow();
                                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                                {
                                    rw[i] = "##";
                                }
                                dt.Rows.Add(rw);
                                Session["dtExCli"] = dt;
                                gvExCli.DataSource = dt;
                                gvExCli.DataBind();
                            }
                            else
                            {
                                gvExCli.DataSource = Session["dtExCli"];
                                gvExCli.DataBind();
                            }
                            gvExCli.Visible = true;
                        }
                        else if (r == 2)
                        {
                            lblEInsUp.Text = "La Excepción pertenece al Rol " + ((Label)gvExCli.Rows[Int32.Parse(e.CommandArgument.ToString())].FindControl("lblNRol")).Text + ",  No fue posible aplicar los cambios.";
                            lblEInsUp.Visible = true;
                        }
                        else if (r == 3)
                        {
                            lblEInsUp.Text = "La excepción se encuentra configurada para un pais diferente, No fue posible aplicar los cambios.";
                            lblEInsUp.Visible = true;
                        }
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar

                        pnlMotivo.Visible = false;
                        MpUp_MA.Hide();

                        Session["dtDoc"] = null;
                        lblEInsUp.Visible = false;
                        Session["RANew"] = 0;
                        gvExCli.EditIndex = -1;
                        gvExCli.DataSource = Session["dtExCli"];
                        gvExCli.DataBind();
                        #endregion
                    }
                    break;
            }
        }
        protected void gv_Alertas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Alertas.PageIndex = e.NewPageIndex;
            gv_Alertas.DataSource = Session["dtAlBusq"];
            gv_Alertas.DataBind();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }
        protected void lbtnIdEx_Click(object sender, EventArgs e)
        {
            //((LinkButton)gvRelAlert.Rows[gvRelAlert.EditIndex].FindControl("lbtnIdMsgRel")).Text = ((LinkButton)sender).Text;
            pnlMpBA.Visible = false;
            MpUp_MA.Hide();

            var id = ((LinkButton)sender).Text;
            var pPage = false;
            for (var i = 0; i <= gvExCli.PageCount - 1; i++)
            {
                //Session["dtExCli"])
                gvExCli.DataSource = Session["dtExCli"];
                gvExCli.PageIndex = i;
                gvExCli.DataBind();
                for (int j = 0; j <= gvExCli.Rows.Count - 1; j++)
                {
                    //if (((LinkButton)gv_Alertas.Rows[j].FindControl("lbtnIdMSGA")).Text.Trim() != id) continue;
                    if (gvExCli.Rows[j].Cells[0].Text.Trim() != id) continue;
                    pPage = true;
                    gvExCli.DataSource = Session["dtExCli"];
                    gvExCli.PageIndex = i;
                    gvExCli.DataBind();
                    gvExCli.Rows[j].BackColor = Color.LightYellow;
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
            try
            {
                pnlMpBA.Visible = true;
                MpUp_MA.Show();

                if (txtMSGBusq.Text != "" | txtIdBusq.Text != "" | ddlCargo.SelectedValue != "0")
                {
                    lblEBusq.Visible = false;

                    if (Session["TCon"].ToString() == "ConEx")
                    {
                        gv_Alertas.Visible = true;
                        gvEmpA.Visible = false;
                        if (txtIdBusq.Text != "")
                        {
                            Session["dtAlBusq"] = ECmSp("SPSIT_CE|@IDCON;8|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdEx;" + txtIdBusq.Text.Trim() + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gv_Alertas.DataSource = Session["dtAlBusq"];
                            gv_Alertas.PageIndex = 0;
                            gv_Alertas.DataBind();
                        }
                        else if (txtMSGBusq.Text != "")
                        {
                            Session["dtAlBusq"] = ECmSp("SPSIT_CE|@IDCON;8|@TTAB;2|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IDCLIENTE;" + txtMSGBusq.Text.Trim() + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gv_Alertas.DataSource = Session["dtAlBusq"];
                            gv_Alertas.PageIndex = 0;
                            gv_Alertas.DataBind();
                        }
                    }
                    else if (Session["TCon"].ToString() == "AsigA")
                    {
                        gv_Alertas.Visible = false;
                        gvEmpA.Visible = true;
                        if (txtIdBusq.Text != "")
                        {
                            Session["dtAlBusq"] = ECmSp("hr.SPS_EmpCNC|@IdEmp;" + txtIdBusq.Text.Trim(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvEmpA.DataSource = Session["dtAlBusq"];
                            gvEmpA.PageIndex = 0;
                            gvEmpA.DataBind();
                        }
                        else if (txtMSGBusq.Text != "" & ddlCargo.SelectedValue != "0")
                        {
                            Session["dtAlBusq"] = ECmSp("hr.SPS_EmpCNC|@NEmp;" + txtMSGBusq.Text.Trim() + "|@IdCargo;" + ddlCargo.SelectedValue, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvEmpA.DataSource = Session["dtAlBusq"];
                            gvEmpA.PageIndex = 0;
                            gvEmpA.DataBind();
                        }
                        else if (txtMSGBusq.Text != "")
                        {
                            Session["dtAlBusq"] = ECmSp("hr.SPS_EmpCNC|@NEmp;" + txtMSGBusq.Text.Trim(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvEmpA.DataSource = Session["dtAlBusq"];
                            gvEmpA.PageIndex = 0;
                            gvEmpA.DataBind();
                        }
                        else if (ddlCargo.SelectedValue != "0")
                        {
                            Session["dtAlBusq"] = ECmSp("hr.SPS_EmpCNC|@IdCargo;" + ddlCargo.SelectedValue, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvEmpA.DataSource = Session["dtAlBusq"];
                            gvEmpA.PageIndex = 0;
                            gvEmpA.DataBind();
                        }
                    }
                }
                else
                {
                    lblEBusq.Text = "Ingrese los datos solicitados";
                    lblEBusq.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnCanlBusq_Click(object sender, EventArgs e)
        {
            Boolean edit = (Boolean) Session["editOp"];
            if (!edit) {
                ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = "Buscar";
            }
            ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtIdCli")).Enabled = true;

            pnlMpBA.Visible = false;
            MpUp_MA.Hide();
        }
        protected void ddlCome_SelectedIndexChanged(Object sender, EventArgs e)
        {

        }


        protected void btnMotivo_Click(object sender, EventArgs e)
        {

            ListItem l = new ListItem("text", "value", true); l.Selected = true;


            string value = txtNMotivo.Text;
            string selected = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).SelectedValue;

            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).Items.Insert(0, new ListItem(value, "6"));


            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).ClearSelection();

            ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).Items.FindByText(value).Selected = true;


            txtNMotivo.Text = "";
            pnlMotivo.Visible = false;

        }


        protected void ddlTEx_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue == "PMT")
            {
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Text = "-";
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Enabled = false;
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtMsgEx")).Text = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).SelectedItem.Text;
            }
            else if (((DropDownList)sender).SelectedItem.Value == "0")
            {
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Text = "";
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Enabled = true;
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtMsgEx")).Text = "";
            }
            else
            {
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Text = "";
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtdtVg")).Enabled = true;
                ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtMsgEx")).Text = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).SelectedItem.Text;
            }
            IExS sEx = new ExS();
            var res = sEx.ExMsg(((DropDownList)sender).SelectedValue, 1);

            if (((DropDownList)sender).SelectedValue.CompareTo("0") != 0)
            {
                IServCBloqueados serv = new ServCBloqueados();
                string idCliente = ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtIdCli")).Text; 
                gv_MsgTEx.DataSource = res;
                gv_MsgTEx.DataBind();
                gv_MsgTEx.Visible = true;

                if (serv.existClient(idCliente, ((DropDownList)sender).SelectedItem.Value.ToString(), ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString()).CompareTo("1") == 0) {
                    lblEInsUp.ForeColor = Color.DarkRed;
                    lblEInsUp.Text = "Al cliente '" + idCliente + "' no se le pueden aplicar excepciones de tipo " + ((DropDownList)sender).SelectedItem.Text;
                    lblEInsUp.Visible = true;
                    gv_MsgTEx.Visible = false;
                    ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtIdCli")).Text = "";
                    ((DropDownList)sender).ClearSelection();
                    ((DropDownList)sender).SelectedValue = "0";
                }
            }
            else
            {
                gv_MsgTEx.Visible = false;
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
                        adp.SelectCommand.CommandText = aParam[0].ToString();
                        adp.SelectCommand.CommandTimeout = 0;
                        adp.SelectCommand.Connection = cnn;
                        if (aParam.Length > 1)
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
                        if (tCom == 0)
                        {
                            cnn.Open();
                            adp.SelectCommand.ExecuteNonQuery();
                            cnn.Close();
                        }
                        if (tCom == 1)
                        {
                            cnn.Open();
                            adp.Fill(dt);
                            cnn.Close();
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
            catch (Exception e)
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
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).Items.Insert(0, new ListItem("Seleccione...", "0"));
                        ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).Items[0].Selected = true;

                        if (strDataVt.CompareTo("dropMotivo|IdMotivo|Descripcion") == 0)
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).Items.FindByValue("0").Enabled = false;



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
                            ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0])).SelectedValue = dt.Rows[int.Parse(strPos.Split('|')[0])][7].ToString();
                        }
                        #endregion
                    }
                    break;
            }
        }


        #endregion




        protected void lbtnIdEmpAuthC_Click(object sender, EventArgs e)
        {
            if (gvExCli.Rows.Count == 1 & ((LinkButton)gvExCli.Rows[0].FindControl("lbtnIdEmpAuthC")).Text == "##") return;
            if (gvExCli.EditIndex == -1)
            {
                lblEBusq.Visible = false;
                pnlMpCEmpDoc.Visible = true;
                MpUp_CEmpDoc.Show();
            }
            else
            {
                lblEBusq.Visible = false;
                pnlMpBA.Visible = true;
                MpUp_MA.Show();
            }
        }





        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }

        protected void lbtnId_Click(object sender, EventArgs e)
        {

            string idCliente = ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtIdCli")).Text.Trim();
            string idEmp = ((LinkButton)sender).Text;
            ((TextBox)gvExCli.Rows[gvExCli.EditIndex].FindControl("txtIdCli")).Enabled = false;

            if (idCliente.CompareTo("") != 0)
            {
                IServCBloqueados serv = new ServCBloqueados();
                //string res = serv.validateLock("50117857", "9000038120");
                string res = serv.validateLock(idCliente, idEmp, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                if (res.CompareTo("-") == 0)
                {
                    ((LinkButton)gvExCli.Rows[gvExCli.EditIndex].FindControl("lbtnIdEmpAuthC")).Text = ((LinkButton)sender).Text;
                    pnlMpBA.Visible = false;
                    MpUp_MA.Hide();
                }
                else
                {
                    pnlMpBA.Visible = true;
                    MpUp_MA.Show();
                    lblEBusq.Text = "El cliente solo puede ser autorizado por '" + res + "'";
                    lblEBusq.Visible = true;
                }

            }else {
                pnlMpBA.Visible = true;
                MpUp_MA.Show();
                lblEBusq.Text = "Ingrese primero un código de cliente";
                lblEBusq.Visible = true;
            }

            




        }

        protected void btnCCon_Click(object sender, EventArgs e)
        {
            Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
            gvExCli.EditIndex = -1;
            gvExCli.DataSource = Session["dtExCli"];
            gvExCli.DataBind();
            pnlMpCEmpDoc.Visible = false;
            MpUp_CEmpDoc.Hide();
        }

        //protected void lbtnDocAdjV_Click(object sender, EventArgs e)
        //{
        //    pnlMpCEmpDoc.Visible = true;
        //    MpUp_CEmpDoc.Show();
        //}

        protected void gvNDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo


                        var l = (List<List<Object>>)Session["dtDoc"];

                        Session["DocNew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (l[0][0].ToString() == "##" & gvNDoc.Rows.Count == 1)
                        {
                            var ls = new List<List<Object>>();

                            ls.Add(new List<Object>() { "##", "##", "##", "##", new object(), "##" });
                            var lF = (from li in ls
                                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3] as String, FData = li[4] as Byte[], Path = li[5] as String }).ToList();
                            gvNDoc.EditIndex = 0;
                            gvNDoc.DataSource = lF;
                            gvNDoc.DataBind();
                        }
                        else
                        {
                            l.Add(new List<Object>() { null, null, null, null, null, null });

                            var lgv = (from i in l
                                       select new { IdDocument = i[0] as String, Nombre = i[1] as String, FileExt = i[2] as String, ContentType = i[3] as String, FData = i[4] as Byte[], Path = i[5] as String }).ToList();
                            gvNDoc.DataSource = lgv;
                            gvNDoc.DataBind();
                            gvNDoc.PageIndex = gvNDoc.PageCount;
                            gvNDoc.DataBind();
                            gvNDoc.EditIndex = gvNDoc.Rows.Count - 1;
                            gvNDoc.DataBind();
                        }

                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        return;
                        #endregion
                    }
                case "Guardar":
                    {
                        #region Guardar
                        var txt = "Doc " + (Convert.ToInt32(e.CommandArgument.ToString()) + 1 + (gvNDoc.PageSize * gvNDoc.PageIndex)).ToString();
                        var fExt = ((Label)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("lblFExt")).Text.Trim();
                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        if (((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).HasFile == false) return;
                        ((List<List<Object>>)Session["dtDoc"]).RemoveAt((Convert.ToInt32(e.CommandArgument.ToString()) + (gvNDoc.PageSize * gvNDoc.PageIndex)));
                        List<List<Object>> lData = (List<List<Object>>)Session["dtDoc"];
                        List<Object> l = new List<object>();
                        l.Add(txt);
                        l.Add(Path.GetFileName(((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).PostedFile.FileName));
                        l.Add(Path.GetExtension(((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).PostedFile.FileName));
                        l.Add(((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).PostedFile.ContentType);
                        l.Add(ReadFully(((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).PostedFile.InputStream));
                        l.Add(((FileUpload)gvNDoc.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("fp_DocExt")).PostedFile.FileName);
                        lData.Add(l);
                        var lres = (from ld in lData
                                    select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();

                        var lgv = (from ld in lData
                                   select new { IdDocument = ld[0] as String, Nombre = ld[1] as String, FileExt = ld[2] as String, ContentType = ld[3] as String, Path = ld[5] as String }).ToList();

                        Session["dtDoc"] = lres;

                        var eI = gvNDoc.EditIndex;
                        gvNDoc.DataSource = lgv;
                        gvNDoc.DataBind();
                        gvNDoc.PageIndex = gvNDoc.PageCount;
                        gvNDoc.DataBind();
                        gvNDoc.EditIndex = -1;
                        gvNDoc.DataBind();

                        #endregion
                    }
                    break;
                case "DesDoc":
                    {
                        #region Descarga
                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        gvNDoc.EditIndex = -1;
                        var list = (List<List<Object>>)Session["dtDoc"];
                        if (list[int.Parse(e.CommandArgument.ToString())][0].ToString().Trim() == "##" | list[int.Parse(e.CommandArgument.ToString())][0].ToString().Trim() == "" | list[int.Parse(e.CommandArgument.ToString())][0] == null) return;

                        var pos = (Convert.ToInt32(e.CommandArgument.ToString()) + (gvNDoc.PageSize * gvNDoc.PageIndex));

                        var lr = (from ld in list
                                  select
                                      new
                                      {
                                          IdDocument = ld[0] as String,
                                          Nombre = ld[1] as String,
                                          FileExt = ld[2] as String,
                                          ContentType = ld[3] as String,
                                          FData = ld[4] as Byte[],
                                          Path = ld[5] as String
                                      }).ToList();

                        Response.Clear();
                        Response.BinaryWrite(lr[pos].FData);
                        Response.AddHeader("Content-Disposition", "attachment; filename = " + lr[pos].Nombre);
                        Response.ContentType = lr[pos].ContentType;
                        Response.End();

                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        var list = (List<List<Object>>)Session["dtDoc"];
                        if (list[int.Parse(e.CommandArgument.ToString())][0].ToString().Trim() == "##") return;
                        list.RemoveAt((Convert.ToInt32(e.CommandArgument.ToString()) + (gvNDoc.PageSize * gvNDoc.PageIndex)));
                        if (list.Count == 0)
                        {
                            var ls = new List<List<Object>>();
                            ls.Add(new List<Object>() { "##", "##", "##", "##", new object(), "##" });
                            var lF = (from li in ls
                                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3] as String, FData = li[4] as Byte[], Path = li[5] as String }).ToList();
                            var lres = (from ld in ls
                                        select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();
                            Session["dtDoc"] = lres;

                            gvNDoc.DataSource = lF;
                            gvNDoc.DataBind();
                            gvNDoc.EditIndex = -1;
                            gvNDoc.DataBind();
                        }
                        else
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                list[i][0] = "Doc " + (i + 1).ToString();
                            }

                            var lF = (from li in list
                                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3] as String, FData = li[4] as Byte[], Path = li[5] as String }).ToList();
                            var lres = (from ld in list
                                        select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();
                            Session["dtDoc"] = lres;
                            gvNDoc.DataSource = lF;
                            gvNDoc.DataBind();
                            gvNDoc.EditIndex = -1;
                            gvNDoc.DataBind();
                        }
                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar
                        List<List<Object>> lData = (List<List<Object>>)Session["dtDoc"];
                        var lgv = (from ld in lData
                                   where ld[0] != null
                                   select new { IdDocument = ld[0] as String, Nombre = ld[1] as String, FileExt = ld[2] as String, ContentType = ld[3] as String, Path = ld[5] as String }).ToList();
                        var lres = (from ld in lData
                                    where ld[0] != null
                                    select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();
                        Session["dtDoc"] = lres;
                        Session["DocNew"] = 0;
                        gvNDoc.EditIndex = -1;
                        gvNDoc.DataSource = lgv;
                        gvNDoc.DataBind();

                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();
                        #endregion
                    }
                    break;
            }
            for (int i = 0; i < ((List<List<Object>>)Session["dtDoc"]).Count; i++)
            {
                if (((List<List<Object>>)Session["dtDoc"])[i][0] == null) continue;
                if (((List<List<Object>>)Session["dtDoc"])[i][0].ToString() == "")
                {
                    ((List<List<Object>>)Session["dtDoc"]).RemoveAt(i);
                }
            }
        }

        protected void btnBackAdj_Click(object sender, EventArgs e)
        {
            pnlMpAdj.Visible = false;
            MpUp_Adj.Hide();
        }

        //protected void lbtnModDocAdj_Click(object sender, EventArgs e)
        //{
        //    pnlMpAdj.Visible = true;
        //    MpUp_Adj.Show();
        //}

        protected void gvNDocOld_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DesDocO":
                    {
                        #region Descarga

                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        //int ent;
                        //var IdEx = gvExCli.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        //if (IdEx.Trim() == "##" & !int.TryParse(IdEx, out ent)) return;
                        //lblEInsUp.Visible = false;
                        //var r = (int)ECmSp("auth_ccs.SPD_ExF|@IdEx;" + IdEx + "|@IdUsr;" + ((IUsr)Session["Usr"]).usr.CemexId, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                        //if (r == 1)
                        //{
                        //    Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                        //    //gvExCli.DataSource = Session["dtExCli"];
                        //    //gvExCli.DataBind();
                        //    if (((DataTable)Session["dtExCli"]).Rows.Count == 0)
                        //    {
                        //        DataTable dt = new DataTable();
                        //        for (int i = 0; i <= ((DataTable)Session["dtExCli"]).Columns.Count - 1; i++)
                        //        {
                        //            dt.Columns.Add(((DataTable)Session["dtExCli"]).Columns[i].ColumnName);
                        //        }
                        //        DataRow rw = dt.NewRow();
                        //        for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        //        {
                        //            rw[i] = "##";
                        //        }
                        //        dt.Rows.Add(rw);
                        //        Session["dtExCli"] = dt;
                        //        gvExCli.DataSource = dt;
                        //        gvExCli.DataBind();
                        //    }
                        //    else
                        //    {
                        //        gvExCli.DataSource = Session["dtExCli"];
                        //        gvExCli.DataBind();
                        //    }
                        //    gvExCli.Visible = true;
                        //}
                        //gvNDoc.EditIndex = -1;
                        //pnlMpAdj.Visible = true;
                        //MpUp_Adj.Show();
                        #endregion
                    }
                    break;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected void gvCDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblEDoc.Visible = false;
            switch (e.CommandName)
            {
                case "dlDocCon":
                    {
                        #region Descarga
                        pnlMpAdj.Visible = true;
                        MpUp_Adj.Show();

                        var IdDoc = int.Parse(((Label)gvCDoc.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblIdDoc")).Text.Trim());
                        var NomDoc = ((Label)gvCDoc.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblNDoc")).Text.Trim();
                        var Cont = ((Label)gvCDoc.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblFExt")).Text.Trim();
                        var Data = ((DataTable)ECmSp("auth_ccs.SPS_FData|@IdDocument;" + IdDoc, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1)).AsEnumerable();

                        var lFd = (from fd in Data
                                   select new { FData = fd[0] as byte[] }).ToList();

                        if (lFd.Count > 0)
                        {
                            Response.Clear();
                            Response.BinaryWrite(lFd.First().FData);
                            Response.AddHeader("Content-Disposition", "attachment; filename = " + NomDoc.ToString());
                            Response.ContentType = Cont.ToString();
                            Response.End();
                        }
                        #endregion
                    }
                    break;
                case "Borrar":
                    {
                        #region Borrar
                        int ent;
                        var idDocument = ((Label)gvCDoc.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblIdDoc")).Text.Trim();
                        if (idDocument == "##" & !int.TryParse(idDocument, out ent)) return;
                        var r = (int)ECmSp("auth_ccs.SPD_ExF|@IdDocument;" + idDocument + "|@IdUsr;" + ((IUsr)Session["Usr"]).usr.CemexId + "|@IdPais;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                        if (r == 1)
                        {
                            var docs = (DataTable)ECmSp("auth_ccs.SPS_ExF|@IdEx;" + Session["idExDocA"].ToString().Trim(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvCDoc.DataSource = docs;
                            gvCDoc.DataBind();
                            if (docs.Rows.Count > 0)
                            {
                                pnlMpCEmpDoc.Visible = true;
                                MpUp_CEmpDoc.Show();
                            }
                            else
                            {
                                Session["dtExCli"] = ECmSp("SPSIT_CE|@IDCON;7|@TTAB;1|@IDPAIS;" + ((IUsr)Session["Usr"]).usr.Pais.IdPais + "|@IdRol;" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                gvExCli.EditIndex = -1;
                                gvExCli.DataSource = Session["dtExCli"];
                                gvExCli.DataBind();
                            }
                        }
                        else if (r == 2)
                        {
                            var ex = ((DataTable)Session["dtExCli"]).AsEnumerable();
                            var rol = (from l in ex
                                       where l["IdEx"].ToString() == Session["idExDocA"].ToString().Trim()
                                       select l["NRol"]).ToList();
                            if (rol.Count > 0)
                            {
                                lblEDoc.Text = "El Documento pertenece al Rol " + rol.First() + ", No se aplicaron los cambios";
                                lblEDoc.Visible = true;
                                pnlMpCEmpDoc.Visible = true;
                                MpUp_CEmpDoc.Show();
                            }
                        }
                        else if (r == 3)
                        {
                            lblEDoc.Text = "El Documento esta vinculado a una excepción configurada en un pais diferente, No se aplicaron los cambios";
                            lblEDoc.Visible = true;
                            pnlMpCEmpDoc.Visible = true;
                            MpUp_CEmpDoc.Show();
                        }
                        #endregion
                    }
                    break;
            }
        }

        protected void gvEmpA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpA.PageIndex = e.NewPageIndex;
            gvEmpA.DataSource = Session["dtAlBusq"];
            gvEmpA.DataBind();
            pnlMpBA.Visible = true;
            MpUp_MA.Show();
        }



        public void dropMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).SelectedValue;

            if (selected.CompareTo("6") == 0)
            {
                pnlMotivo.Visible = true;
                MpUp_MA.Show();
            }
            else
            {
                pnlMotivo.Visible = false;
                MpUp_MA.Hide();
            }

        }

        public void dropMotivo_SelectedIndexChangedEdit()
        {
            string selected = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("dropMotivo")).SelectedValue;

            if (selected.CompareTo("6") == 0)
            {
                pnlMotivo.Visible = true;
                MpUp_MA.Show();
            }
            else
            {
                pnlMotivo.Visible = false;
                MpUp_MA.Hide();
            }

        }

        protected void gvCDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var docs = (DataTable)ECmSp("auth_ccs.SPS_ExF|@IdEx;" + Session["idExDocA"].ToString(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
            if (docs.Rows.Count == 0) return;
            gvCDoc.PageIndex = e.NewPageIndex;
            gvCDoc.DataSource = docs;
            gvCDoc.DataBind();
            pnlMpCEmpDoc.Visible = true;
            MpUp_CEmpDoc.Show();
        }

        protected void gvNDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var l = (List<List<Object>>)Session["dtDoc"];
            var lF = (from li in l
                      where li[0] != null
                      select new { IdDocument = li[0] as String, Nombre = li[1] as String, FileExt = li[2] as String, ContentType = li[3], FData = li[4] as Byte[], Path = li[5] as String }).ToList();

            var lres = (from list in l
                        where list[0] != null
                        select list).ToList();

            //var lres = (from ld in l
            //            where ld[0] != null
            //            select new List<Object> { ld[0] as String, ld[1] as String, ld[2] as String, ld[3] as String, ld[4] as Byte[], ld[5] as String }).ToList();

            Session["dtDoc"] = lres;
            gvNDoc.EditIndex = -1;
            gvNDoc.PageIndex = e.NewPageIndex;
            gvNDoc.DataSource = lF;
            gvNDoc.DataBind();
            pnlMpAdj.Visible = true;
            MpUp_Adj.Show();
        }

        protected void gvCDoc_PreRender(object sender, EventArgs e)
        {
            if (sender is GridView)
            {
                GridView MyButton = (GridView)sender;
                ScriptManager NewScriptManager = ScriptManager.GetCurrent(this.Page);
                NewScriptManager.RegisterPostBackControl(MyButton);
            }
        }

        protected void gvNDocOld_PreRender(object sender, EventArgs e)
        {
            if (sender is GridView)
            {
                GridView MyButton = (GridView)sender;
                ScriptManager NewScriptManager = ScriptManager.GetCurrent(this.Page);
                NewScriptManager.RegisterPostBackControl(MyButton);
            }
        }

        protected void gvNDoc_PreRender(object sender, EventArgs e)
        {
            if (sender is GridView)
            {
                GridView MyButton = (GridView)sender;
                ScriptManager NewScriptManager = ScriptManager.GetCurrent(this.Page);
                NewScriptManager.RegisterPostBackControl(MyButton);
            }
        }

        
         protected void txtIdCli_TextChanged(object sender, EventArgs e)
        {
            IServCBloqueados serv = new ServCBloqueados();

            string cliente = ((TextBox)sender).Text;
            string tEx = ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).SelectedItem.Value;

            if (tEx.CompareTo("0") != 0) {

                string exist = serv.existClient(cliente, tEx, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                if (exist.CompareTo("1") == 0)
                {
                    ((TextBox)sender).Text = "";
                    lblEInsUp.ForeColor = Color.DarkRed;
                    lblEInsUp.Text = "Al cliente '" + cliente + "' no se le pueden aplicar excepciones";
                    lblEInsUp.Visible = true;

                    gv_MsgTEx.Visible = false;
                    ((TextBox)sender).Text = "";
                    ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).ClearSelection();
                    ((DropDownList)gvExCli.Rows[gvExCli.EditIndex].FindControl("ddlTEx")).SelectedValue = "0";
                }
                else
                {
                    lblEInsUp.Text = "";
                    lblEInsUp.Visible = false;
                }

            }           
                       
        }

        protected void btnCliBlo_Click(object sender, EventArgs e) {
            Response.Redirect("CBloqueados.aspx");
        }

    }
}