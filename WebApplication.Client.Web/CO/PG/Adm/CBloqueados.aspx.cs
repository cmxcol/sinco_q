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

namespace WebApplication.Client.Web.CO.PG.Adm
{

    /// <summary>
    ///     Año:2019
    ///     Modulo para la administración de los clientes bloqueados de excepciones
    /// </summary>
    /// 
    public partial class CBloqueados : BPage
    {
        #region Eventos



        protected void Page_Load(object sender, EventArgs e)
        {
          
            lblEInsUp.Visible = false;
            if (IsPostBack != true)
            {
                ServCBloqueados serv = new ServCBloqueados();
                Session["dtCBloqueados"] = serv.readClients(((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
                if (((DataTable)Session["dtCBloqueados"]).Rows.Count == 0)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= ((DataTable)Session["dtCBloqueados"]).Columns.Count - 1; i++)
                    {
                        dt.Columns.Add(((DataTable)Session["dtCBloqueados"]).Columns[i].ColumnName);
                    }
                    DataRow rw = dt.NewRow();
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        rw[i] = "##";
                    }
                    dt.Rows.Add(rw);
                    Session["dtCBloqueados"] = dt;
                    gvCliBlo.DataSource = dt;
                    gvCliBlo.DataBind();
                }
                else
                {
                    gvCliBlo.DataSource = Session["dtCBloqueados"];
                    gvCliBlo.DataBind();

                }
                gvCliBlo.Visible = true;
            }
        }
        protected void gvCliBlo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCliBlo.EditIndex = -1;
            gvCliBlo.PageIndex = e.NewPageIndex;
            gvCliBlo.DataSource = Session["dtCBloqueados"];
            gvCliBlo.DataBind();
        }
        protected void gvCliBlo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Nuevo":
                    {
                        #region Nuevo

                        Session["editOp"] = false;

                        MpUp_MA.Hide();

                        gvCliBlo.EditIndex = -1;
                        Session["dtDoc"] = null;
                        DataTable dt = ((DataTable)Session["dtCBloqueados"]).Copy();
                        Session["RANew"] = Convert.ToInt32(e.CommandArgument.ToString());
                        if (dt.Rows[0][0].ToString() == "##" & gvCliBlo.Rows.Count == 1)
                        {
                            gvCliBlo.EditIndex = 0;
                            gvCliBlo.DataSource = dt;
                            gvCliBlo.DataBind();

                        }
                        else
                        {
                            DataRow rw = dt.NewRow();
                            for (int i = 0; i <= dt.Columns.Count - 1; i++)
                            {
                                rw[i] = DBNull.Value;
                            }
                            dt.Rows.Add(rw);
                            gvCliBlo.DataSource = dt;
                            gvCliBlo.DataBind();
                            gvCliBlo.PageIndex = gvCliBlo.PageCount;
                            gvCliBlo.DataBind();
                            gvCliBlo.EditIndex = gvCliBlo.Rows.Count - 1;
                            gvCliBlo.DataBind();


                        }
                        IServCBloqueados serv = new ServCBloqueados();

                        var dtTEx = serv.getTypeExceptions();
                        LdDdlGv(gvCliBlo, dtTEx, "ddlTExCli | IdTEx | NTEx", 1);

                        #endregion
                    }
                    break;
                case "Buscar":
                    {
                        #region Buscar


                        Session["dtDoc"] = null;
                        DataTable dt2 = ((DataTable)Session["dtCBloqueados"]).Copy();
                        if (dt2.Rows[0][0].ToString() == "##" & gvCliBlo.Rows.Count == 1) return;
                        gvCliBlo.EditIndex = -1;



                        gvCliBlo.DataSource = Session["dtCBloqueados"];
                        gvCliBlo.DataBind();
                        gv_Alertas.DataSource = new DataTable();
                        gv_Alertas.DataBind();

                        txtIdBusq.Text = String.Empty;
                        txtMSGBusq.Text = String.Empty;

                        lblId.Text = "Id:";
                        lblTxtCod.Text = "Codigo:";


                        txtIdBusq_TextBoxWatermarkExtender.WatermarkText = "Id";
                        txtMSGBusq_TextBoxWatermarkExtender.WatermarkText = "Cliente";

                        gv_Alertas.Visible = true;


                        pnlMpBA.Visible = true;
                        MpUp_MA.Show();


                        #endregion
                    }
                    break;
                //case "Editar":
                //    {
                //        #region Editar
                //        Session["dtDoc"] = null;
                //        DataTable dt = ((DataTable)Session["dtCBloqueados"]).Copy();

                //        DropDownList ddl = ((DropDownList)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlTExCli"));

                //        ServCBloqueados serv = new ServCBloqueados();
                        
                //        string IdCli = gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                //        string tEx = "";
                //        Session["editOp"] = true;

                //        MpUp_MA.Hide();

                //        if (dt.Rows[0][0].ToString() != "##")
                //        {
                //            Session["RANew"] = 0;
                //            gvCliBlo.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                //            gvCliBlo.DataSource = Session["dtCBloqueados"];
                //            gvCliBlo.DataBind();

                //        }
                //         ((TextBox)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtIdCli")).Enabled = false;
                //        var countDocs = dt.Rows[Convert.ToInt32(e.CommandArgument.ToString())]["CountDoc"].ToString();

                //        var dtTEx = serv.getTypeExceptions();
                       
                //        var itemSelected = serv.findClient(IdCli.ToString(),"8", ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                //        foreach (DataRow row in itemSelected.Rows)
                //        {
                //            tEx = row["IdTEx"].ToString();
                         
                //        }

                //        LdDdlGv(gvCliBlo, dtTEx, "ddlTExCli | IdTEx | NTEx", 2,null,tEx);

                       

                //        #endregion
                //    }
                //    break;
                case "Guardar":
                    {
                        #region Guardar



                        var Id = gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        var IdCli = ((TextBox)gvCliBlo.Rows[Convert.ToInt32(gvCliBlo.EditIndex)].FindControl("txtIdCli")).Text;
                        var deudor = ((TextBox)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtDeudor")).Text.Trim();

                        ServCBloqueados serv = new ServCBloqueados();

                        switch (((int)Session["RANew"]))
                        {
                            case 0:
                                {
                                    //Actualizar Cli
                                    
                                    DropDownList ddl = ((DropDownList)gvCliBlo.Rows[Convert.ToInt32(gvCliBlo.EditIndex)].FindControl("ddlTExCli"));
                                    string selectedTE = Session["selectedEx"].ToString();
                                    string res = serv.updateClient(Id, IdCli, deudor, ((IUsr)Session["Usr"]).usr.CemexId, ddl.SelectedValue, selectedTE, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                                    if (res.CompareTo("0") == 0)
                                    {
                                        lblEInsUp.ForeColor = Color.Red;
                                        lblEInsUp.Text = "No se ha podido realizar la modificación";
                                        lblEInsUp.Visible = true;
                                    }
                                    else
                                    {
                                        Response.Redirect(Request.RawUrl);
                                    }
                                                                        
                                }

                                break;
                            case 1:
                                {
                                    //Nuevo Cli

                                    IdCli = ((TextBox)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtIdCli")).Text.Trim();

                                    DropDownList ddl = ((DropDownList)gvCliBlo.Rows[Convert.ToInt32(gvCliBlo.EditIndex)].FindControl("ddlTExCli"));
                                    string selected = ddl.SelectedValue;
                                    string res = "";
                                    if (selected.CompareTo("0") == 0 || IdCli.CompareTo("") == 0)
                                    {
                                        res = "-1";
                                    }
                                    else {
                                        res = serv.insertClient(IdCli, deudor, ((IUsr)Session["Usr"]).usr.CemexId, ddl.SelectedValue, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString(),"1");
                                        if (res != "0")
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
                                                            cmd.CommandText = "auth_ccs.SPI_ExFBLO";
                                                            cmd.CommandTimeout = 0;
                                                            cmd.CommandType = CommandType.StoredProcedure;
                                                            cmd.Parameters.AddWithValue("@TCOM", 1);
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
                                                    Session["dtCBloqueados"] = serv.readClients(((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
                                                    
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



                                    if (res.CompareTo("0") == 0)
                                    {
                                        lblEInsUp.ForeColor = Color.Red;
                                        lblEInsUp.Text = "El cliente ya se encuentra registrado con ese tipo de Excepción";
                                        lblEInsUp.Visible = true;
                                    }
                                    else { 
                                        if (res.CompareTo("-1") == 0) {
                                            lblEInsUp.ForeColor = Color.Red;
                                            lblEInsUp.Text = "Datos Incorrectos";
                                            lblEInsUp.Visible = true;
                                        }
                                        else
                                        {
                                            Response.Redirect(Request.RawUrl);
                                        }
                                    }
                                }


                                break;
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
                case "DocAdj":
                        {
                            if (((LinkButton)gvCliBlo.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbtnDocAdjV")).Text == "##") return;
                            if (gvCliBlo.EditIndex == -1)
                            {
                                //gvCEmp.Visible = false;
                                gvCDoc.Visible = true;
                                int idEx = int.Parse(gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim());
                                var docs = (DataTable)ECmSp("auth_ccs.SPI_ExFBLO|@TCOM;2|@IdEx;" + idEx, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                                if (docs.Rows.Count == 0) return;
                                //Session["dtDocOld"] = docs;
                                gvCDoc.DataSource = docs;
                                gvCDoc.DataBind();
                                Session["idExDocA"] = idEx;
                                pnlMpCEmpDoc.Visible = true;
                                MpUp_CEmpDoc.Show();
                            }
                            
                        }
                        break;
                       
                case "Borrar":
                    {
                        #region Borrar
                        if (gvCliBlo.EditIndex != -1) return;
                        int ent;
                        var IdCli = gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();

                        ServCBloqueados serv = new ServCBloqueados();
                        string res = serv.deleteClient(IdCli, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                        if (res.CompareTo("0") == 0)
                        {
                            lblEInsUp.ForeColor = Color.Red;
                            lblEInsUp.Text = "No se pudo eliminar el Cliente";
                            lblEInsUp.Visible = true;
                        }
                        else {
                            Response.Redirect(Request.RawUrl);
                        }


                        #endregion
                    }
                    break;
                case "Cancelar":
                    {
                        #region Cancelar


                        MpUp_MA.Hide();

                        Session["dtDoc"] = null;
                        lblEInsUp.Visible = false;
                        Session["RANew"] = 0;
                        gvCliBlo.EditIndex = -1;
                        gvCliBlo.DataSource = Session["dtCBloqueados"];
                        gvCliBlo.DataBind();
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
            for (var i = 0; i <= gvCliBlo.PageCount - 1; i++)
            {
                //Session["dtExCli"])
                gvCliBlo.DataSource = Session["dtExCli"];
                gvCliBlo.PageIndex = i;
                gvCliBlo.DataBind();
                for (int j = 0; j <= gvCliBlo.Rows.Count - 1; j++)
                {
                    //if (((LinkButton)gv_Alertas.Rows[j].FindControl("lbtnIdMSGA")).Text.Trim() != id) continue;
                    if (gvCliBlo.Rows[j].Cells[0].Text.Trim() != id) continue;
                    pPage = true;
                    gvCliBlo.DataSource = Session["dtExCli"];
                    gvCliBlo.PageIndex = i;
                    gvCliBlo.DataBind();
                    gvCliBlo.Rows[j].BackColor = Color.LightYellow;
                    break;
                }
                if (pPage)
                {
                    break;
                }
            }
        }
        protected void lbtnId_Click(object sender, EventArgs e)
        {
            
            pnlMpBA.Visible = false;
            MpUp_MA.Hide();

            var id = ((LinkButton)sender).Text;
            var pPage = false;
            for (var i = 0; i <= gvCliBlo.PageCount - 1; i++)
            {
               
                gvCliBlo.DataSource = Session["dtCBloqueados"];
                gvCliBlo.PageIndex = i;
                gvCliBlo.DataBind();
                for (int j = 0; j <= gvCliBlo.Rows.Count - 1; j++)
                {
                   
                    if (gvCliBlo.Rows[j].Cells[0].Text.Trim() != id) continue;
                    pPage = true;
                    gvCliBlo.DataSource = Session["dtCBloqueados"];
                    gvCliBlo.PageIndex = i;
                    gvCliBlo.DataBind();
                    gvCliBlo.Rows[j].BackColor = Color.LightYellow;
                    break;
                }
                if (pPage)
                {
                    break;
                }
            }
        }
        protected void btnCCon_Click(object sender, EventArgs e)
        {
            ServCBloqueados serv = new ServCBloqueados();
            Session["dtCBloqueados"] = serv.readClients(((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
            if (((DataTable)Session["dtCBloqueados"]).Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                for (int i = 0; i <= ((DataTable)Session["dtCBloqueados"]).Columns.Count - 1; i++)
                {
                    dt.Columns.Add(((DataTable)Session["dtCBloqueados"]).Columns[i].ColumnName);
                }
                DataRow rw = dt.NewRow();
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    rw[i] = "##";
                }
                dt.Rows.Add(rw);
                Session["dtCBloqueados"] = dt;
                gvCliBlo.DataSource = dt;
                gvCliBlo.DataBind();
            }
            else
            {
                gvCliBlo.DataSource = Session["dtCBloqueados"];
                gvCliBlo.DataBind();

            }
            gvCliBlo.Visible = true;
            pnlMpCEmpDoc.Visible = false;
            MpUp_CEmpDoc.Hide();
        }

        
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

        protected void btnBusq_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMpBA.Visible = true;
                MpUp_MA.Show();

                if (txtMSGBusq.Text != "" | txtIdBusq.Text != "")
                {
                    lblEBusq.Visible = false;
                    ServCBloqueados serv = new ServCBloqueados();

                    gv_Alertas.Visible = true;


                    if (txtIdBusq.Text != "")
                    {
                        Session["dtAlBusq"] = serv.findClient(txtIdBusq.Text, "7", ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
                        gv_Alertas.DataSource = Session["dtAlBusq"];
                        gv_Alertas.PageIndex = 0;
                        gv_Alertas.DataBind();
                    }
                    else if (txtMSGBusq.Text != "")
                    {
                        Session["dtAlBusq"] = serv.findClient(txtMSGBusq.Text, "6", ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
                        gv_Alertas.DataSource = Session["dtAlBusq"];
                        gv_Alertas.PageIndex = 0;
                        gv_Alertas.DataBind();
                    }


                }
                else
                {
                    lblEBusq.Text = "Ingrese los datos solicitados";
                    lblEBusq.Visible = true;
                }
            }
            catch (Exception exc)
            {

                throw;
            }
        }


        protected void btnCanlBusq_Click(object sender, EventArgs e)
        {
            pnlMpBA.Visible = false;
            MpUp_MA.Hide();
        }
        protected void ddlCome_SelectedIndexChanged(Object sender, EventArgs e)
        {

        }



        #endregion

        #region Metodos


        /// <summary>
        /// PERMITE INGRESAR DATOS EN UNA LISTA DESPLEGABLE QUE SE ENCUENTRA EN UN GRIDVIEW
        /// </summary>
        /// <param name="gV">GridView que contiene el DropDownList</param>
        /// <param name="dt">DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="strDataVt">Cadena que contiene el Nombre,DataValueField,DataTextField del DropDownList </param>
        /// <param name="TLoad">Indica si la carga se va a realizar para un nuevo registro o la modificacion de uno existente</param>
        /// <param name="dtDdl"></param>
        /// <param name="strPos"></param>
        public void LdDdlGv(GridView gV, DataTable dtDdl, String strDataVt, int TLoad, DataTable dt = null, String value = null)
        {
            DropDownList ddl = ((DropDownList)gV.Rows[Convert.ToInt32(gV.EditIndex)].FindControl(strDataVt.Split('|')[0].Trim()));

            switch (TLoad)
            {
                

                case 1:
                    {
                        #region Nuevo

                        ddl.DataSource = dtDdl;
                        ddl.DataValueField = strDataVt.Split('|')[1].Trim();
                        ddl.DataTextField = strDataVt.Split('|')[2].Trim();
                        ddl.DataBind();
                        ddl.Items.Insert(0, new ListItem("Seleccione...", "0"));
                        ddl.Items[0].Selected = true;
                                                                     

                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region Edición
                        if ( value != null)
                        {
                            ddl.DataSource = dtDdl;
                            ddl.DataValueField = strDataVt.Split('|')[1].Trim();
                            ddl.DataTextField = strDataVt.Split('|')[2].Trim();
                            ddl.DataBind();
                            ddl.SelectedValue = value;
                            Session["selectedEx"] = value;
                        }
                        #endregion
                    }
                    break;
            }
        }


        #endregion





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
        protected void btnBackAdj_Click(object sender, EventArgs e)
        {
            pnlMpAdj.Visible = false;
            MpUp_Adj.Hide();
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


        protected void btnCMasiva_Click(object sender, EventArgs e)
        {
            Response.Redirect("CBloMasivo.aspx");
        }

        protected void ddlTEx_SelectedIndexChanged(Object sender, EventArgs e) {

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
                        var Data = ((DataTable)ECmSp("auth_ccs.SPI_ExFBLO|@TCOM;3|@IdDocument;" + IdDoc, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1)).AsEnumerable();

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
                        var r = (int)ECmSp("auth_ccs.SPI_ExFBLO|@TCOM;4|@IdDocument;" + idDocument + ";" + ((IUsr)Session["Usr"]).usr.Rol.IdRol, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
                        if (r == 1)
                        {
                            var docs = (DataTable)ECmSp("auth_ccs.SPI_ExFBLO|@TCOM;2|@IdEx;" + Session["idExDocA"].ToString().Trim(), System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
                            gvCDoc.DataSource = docs;
                            gvCDoc.DataBind();
                            if (docs.Rows.Count > 0)
                            {
                                pnlMpCEmpDoc.Visible = true;
                                MpUp_CEmpDoc.Show();
                            }
                            else
                            {
                                ServCBloqueados serv = new ServCBloqueados();
                                Session["dtCBloqueados"] = serv.readClients(((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());
                                if (((DataTable)Session["dtCBloqueados"]).Rows.Count == 0)
                                {
                                    DataTable dt = new DataTable();
                                    for (int i = 0; i <= ((DataTable)Session["dtCBloqueados"]).Columns.Count - 1; i++)
                                    {
                                        dt.Columns.Add(((DataTable)Session["dtCBloqueados"]).Columns[i].ColumnName);
                                    }
                                    DataRow rw = dt.NewRow();
                                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                                    {
                                        rw[i] = "##";
                                    }
                                    dt.Rows.Add(rw);
                                    Session["dtCBloqueados"] = dt;
                                    gvCliBlo.DataSource = dt;
                                    gvCliBlo.DataBind();
                                }
                                else
                                {
                                    gvCliBlo.DataSource = Session["dtCBloqueados"];
                                    gvCliBlo.DataBind();

                                }

                            }
                        }
                        
                        #endregion
                    }
                    break;
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


    }
}