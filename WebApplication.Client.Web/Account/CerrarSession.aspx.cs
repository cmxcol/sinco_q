using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Client.Web.App_Code;

namespace WebApplication.Client.Web.Account
{
    public partial class CerrarSession :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session["Usr"] = null;
            FormsAuthentication.SignOut();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            //Cache.Remove("Lpg");
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}