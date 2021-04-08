using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Components.PTO.Cache;
using Infrastructure.Security.Usuario;
using Infrastructure.Cache;
using Services.Pais;
using System.Web.Security;

namespace WebApplication.Client.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Usr"] = null;
            FormsAuthentication.SignOut();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            //cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            Cache.Remove("Lpg");
            string myHost = System.Net.Dns.GetHostName();
            for (var i = 0; i < System.Net.Dns.GetHostEntry(myHost).AddressList.Count(); i++)
            {
                var myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[i].ToString();
                if (!HttpContext.Current.Request.Url.Host.ToLower().Contains(myIP)) continue;
                Response.Redirect(Usr.LoginPage());
                break;
            }
            if (HttpContext.Current.Request.Url.Host == myHost)
            {
                Response.Redirect(Usr.LoginPage());
            }
            if (IsPostBack) return;
           
            foreach (var p in PaisServI.Instance.LoadActive())
            {
                ddlPa.Items.Add(new ListItem(p.NPais, p.IdPais.ToString()));
            }
            ddlPa.DataBind();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

                if (Page.IsValid)
                {
                    IUsr Usr = new Usr(txtUsuario.Text.ToLower(), txtPass.Text, Int32.Parse(ddlPa.SelectedValue));
                    if (Usr.usr != null & Usr.IsAuth())
                    {
                        if (Usr.IsActive())
                        {
                            if (Usr.usr.Rol != null)
                            {
                                UsrCache.AddPagesToCache(Usr.usr.CemexId, Usr.GetDataCache(), HttpContext.Current);

                                var authTicket = new FormsAuthenticationTicket(2, Usr.usr.CemexId, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, Usr.usr.Pais.IdPais.ToString(), FormsAuthentication.FormsCookiePath);
                                String crypTicket = FormsAuthentication.Encrypt(authTicket);
                                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, crypTicket);
                                //authCookie.Expires = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
                                Response.Cookies.Add(authCookie);                                                                
                                Session["Usr"] = Usr; 
                                Response.Redirect("~" + Usr.DefaultPage());
                            }
                            else
                            {
                                lblError.Text = "El usuario no tiene asignado un Rol valido";
                                lblError.Visible = true;
                            } 
                        }
                        else
                        {
                            lblError.Text = "Usuario Inactivo";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Usuario o Contraseña Incorrecto";
                        lblError.Visible = true;
                    }
                
            }
        }
    }
}
