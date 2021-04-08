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
                case "Editar":
                    {
                        #region Editar
                        Session["dtDoc"] = null;
                        DataTable dt = ((DataTable)Session["dtCBloqueados"]).Copy();

                        DropDownList ddl = ((DropDownList)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("ddlTExCli"));

                        ServCBloqueados serv = new ServCBloqueados();
                        
                        string IdCli = gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text.Trim();
                        string tEx = "";
                        Session["editOp"] = true;

                        MpUp_MA.Hide();

                        if (dt.Rows[0][0].ToString() != "##")
                        {
                            Session["RANew"] = 0;
                            gvCliBlo.EditIndex = Convert.ToInt32(e.CommandArgument.ToString());
                            gvCliBlo.DataSource = Session["dtCBloqueados"];
                            gvCliBlo.DataBind();

                        }
                         ((TextBox)gvCliBlo.Rows[Convert.ToInt32(e.CommandArgument.ToString())].FindControl("txtIdCli")).Enabled = false;
                        

                        var dtTEx = serv.getTypeExceptions();
                       
                        var itemSelected = serv.findClient(IdCli.ToString(),"8", ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString());

                        foreach (DataRow row in itemSelected.Rows)
                        {
                            tEx = row["IdTEx"].ToString();
                         
                        }

                        LdDdlGv(gvCliBlo, dtTEx, "ddlTExCli | IdTEx | NTEx", 2,null,tEx);

                       

                        #endregion
                    }
                    break;
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
                                        res = serv.insertClient(IdCli, deudor, ((IUsr)Session["Usr"]).usr.CemexId, ddl.SelectedValue, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString(),"4");
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

        protected void gvCDoc_PreRender(object sender, EventArgs e)
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


    }
}