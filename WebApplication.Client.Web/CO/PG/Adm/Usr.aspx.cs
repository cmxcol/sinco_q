using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Components.PTO.Pais;
using Components.PTO.Rol;
using Components.PTO.StaRg;
using WebApplication.Client.Web.App_Code;
using Services.StaRg;
using Services.Rol;
using Services.Usr;
using Services.Rep.Usr;
using Infrastructure.Security.Usuario;
using Components.PTO.Usr;

namespace WebApplication.Client.Web.CO.PG.Adm
{
    public partial class Usr : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ddlRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gv_Usr.Visible = false;
                if (int.Parse(ddlRep.SelectedValue) != 0)
                {
                    ddlRep.Enabled = false;
                    ddlRol.Items.Clear();
                    var rl = new ListItem("Seleccione", "0");
                    ddlRol.Items.Add(rl);
                    foreach (var r in RolServI.Instance.LoadAsig(((IUsr)Session["Usr"]).usr.Rol.IdRol))
                    {
                        ddlRol.Items.Add(new ListItem(r.NRol, r.IdRol.ToString()));
                    }
                    ddlRol.DataBind();
                    ddlStaRs.Items.Clear();
                    var sl = new ListItem("Seleccione", "0");
                    ddlStaRs.Items.Add(sl);
                    foreach (var s in StaRgServI.Instance.LoadAll())
                    {
                        ddlStaRs.Items.Add(new ListItem(s.NStaRg, s.IdStaRg.ToString()));
                    }
                    ddlStaRs.DataBind();

                    gv_Usr.DataSource = RUsrServI.Instance.RepUsrs(1, ((IUsr)Session["Usr"]).usr.Pais.IdPais, ((IUsr)Session["Usr"]).usr.Rol.IdRol);
                    gv_Usr.DataBind();
                    gv_Usr.Visible = true;
                    pnlRegAct.Visible = true;
                    switch (int.Parse(ddlRep.SelectedValue))
                    {
                        case 1:
                            break;
                        case 2:
                            txtCemexID.Enabled = true;
                            txtNom.Enabled = false;
                            txtEmail.Enabled = false;
                            ddlRol.Enabled = false;
                            ddlStaRs.Enabled = false;
                            btnGuardar.Enabled = false;
                            break;

                    }
                }
            }
            catch (Exception)
            {
                //Response.Redirect("~/Error/Error.aspx");
                throw;
            }
        }

        protected void gv_Usr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Usr.EditIndex = -1;
            gv_Usr.PageIndex = e.NewPageIndex;
            gv_Usr.DataSource = RUsrServI.Instance.RepUsrs(1, ((IUsr)Session["Usr"]).usr.Pais.IdPais, ((IUsr)Session["Usr"]).usr.Rol.IdRol);
            gv_Usr.DataBind();
        }

        protected void txtCemexID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlRep.SelectedValue) == 0) return;
                if (txtCemexID.Text == string.Empty) return;
                lblError.Visible = false;
                switch (int.Parse(ddlRep.SelectedValue))
                {
                    case 1:
                        txtCemexID.Enabled = true;
                        txtNom.Enabled = true;
                        txtEmail.Enabled = true;
                        ddlRol.Enabled = true;
                        ddlStaRs.Enabled = true;
                        btnGuardar.Enabled = true;
                        break;
                    case 2:
                        var usr = UsrServI.Instance.LoadUsrAsig(txtCemexID.Text.ToLower(), ((IUsr)Session["Usr"]).usr.Pais.IdPais,((IUsr)Session["Usr"]).usr.Rol.IdRol);                        
                        if (usr != null)
                        {
                            if (usr.Rol != null)
                            {
                                txtNom.Enabled = true;
                                txtEmail.Enabled = true;
                                ddlRol.Enabled = true;
                                ddlStaRs.Enabled = true;
                                txtCemexID.Enabled = false;
                                btnGuardar.Enabled = true;

                                txtNom.Text = usr.NUsr;
                                txtEmail.Text = usr.EMail;
                                ddlRol.SelectedValue = usr.Rol.IdRol.ToString();
                                ddlStaRs.SelectedIndex = usr.StaRg.IdStaRg;
                            }
                            else
                            {
                                btnGuardar.Enabled = false;
                                txtNom.Enabled = false;
                                txtEmail.Enabled = false;
                                ddlRol.Enabled = false;
                                ddlStaRs.Enabled = false;
                                txtCemexID.Enabled = true;

                                lblError.Text = "El Usuario no se encuentra en el sistema";
                                lblError.Visible = true;
                            }
                        }
                        else
                        {
                            btnGuardar.Enabled = false;
                            txtNom.Enabled = false;
                            txtEmail.Enabled = false;
                            ddlRol.Enabled = false;
                            ddlStaRs.Enabled = false;
                            txtCemexID.Enabled = true;

                            lblError.Text = "El Usuario no se encuentra en el sistema";
                            lblError.Visible = true;
                        }
                        break;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Se genero un error, Contacte al Administrador";
                lblError.Visible = true;
                throw;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Page.IsValid)
                {
                    if (int.Parse(ddlRol.SelectedValue) != 0 & int.Parse(ddlStaRs.SelectedValue) != 0)
                    {
                        lblError.Visible = false;
                        IUsrPTO usr = new UsrPTO(txtCemexID.Text.ToLower().Trim(), txtNom.Text, txtEmail.Text,
                                                 new RolPTO(int.Parse(ddlRol.SelectedValue), ddlRol.SelectedItem.ToString()),
                                                 ((IUsr)Session["Usr"]).usr.Pais,
                                                 new StaRgPTO(int.Parse(ddlStaRs.SelectedValue), ddlStaRs.SelectedItem.ToString()));
                        switch (int.Parse(ddlRep.SelectedValue))
                        {
                            case 1:
                                switch (UsrServI.Instance.InsertUsr(usr))
                                {
                                    case 0:
                                        lblError.Text = "El Usuario ya se encuentra en el sistema";
                                        lblError.Visible = true;
                                        break;
                                    case 1:
                                        btnGuardar.Enabled = false;
                                        txtNom.Enabled = false;
                                        txtEmail.Enabled = false;
                                        ddlRol.Enabled = false;
                                        ddlStaRs.Enabled = false;
                                        txtCemexID.Enabled = false;
                                        lblResp.Text = "Datos Guardados";
                                        lblResp.Visible = true;
                                        gv_Usr.DataSource = RUsrServI.Instance.RepUsrs(1, ((IUsr)Session["Usr"]).usr.Pais.IdPais, ((IUsr)Session["Usr"]).usr.Rol.IdRol);
                                        gv_Usr.DataBind();
                                        break;
                                    case 2:
                                        lblError.Text = "Se genero un error en la creación del usuario.";
                                        lblError.Visible = true;
                                        break;
                                }
                                break;
                            case 2:
                                switch (UsrServI.Instance.UpdateUsr(usr))
                                {
                                    case 0:
                                        lblError.Text = "El Usuario a actualizar no existe";
                                        lblError.Visible = true;
                                        break;
                                    case 1:
                                        btnGuardar.Enabled = false;
                                        txtNom.Enabled = false;
                                        txtEmail.Enabled = false;
                                        ddlRol.Enabled = false;
                                        ddlStaRs.Enabled = false;
                                        txtCemexID.Enabled = false;
                                        lblResp.Text = "Datos Guardados";
                                        lblResp.Visible = true;
                                        gv_Usr.DataSource = RUsrServI.Instance.RepUsrs(1, ((IUsr)Session["Usr"]).usr.Pais.IdPais, ((IUsr)Session["Usr"]).usr.Rol.IdRol);
                                        gv_Usr.DataBind();
                                        break;
                                    case 2:
                                        lblError.Text = "Se genero un error en la actualización";
                                        lblError.Visible = true;
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        lblError.Text = "Datos Insuficientes";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception )
            {
                lblError.Text = "Se genero un error, Contacte al Administrador";
                lblError.Visible = true;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCemexID.Enabled = true;
            Response.Redirect(HttpContext.Current.Request.Path);
        }
    }
}