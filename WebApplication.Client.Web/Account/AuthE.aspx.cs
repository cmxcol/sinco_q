using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Components.PTO.Cache;
using Infrastructure.Security.Usuario;

namespace WebApplication.Client.Web.Account
{
    public partial class AuthE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var lpg = ((IObjCache) System.Web.HttpContext.Current.Cache.Get("Lpg"));
            var LoginP = "";
            if (lpg == null)
            {
                LoginP = Usr.LoginPage();
            }
            else
            {
                LoginP = lpg.Pages.Count() > 0 ? Usr.RUrl(lpg.Pages.First()) : Usr.LoginPage();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript'>");
            sb.Append("var x=alert('Acceso Denegado - Su Usuario no cuenta con los permisos Requeridos.');");
            sb.Append("var r = confirm('Aceptar: Login - Cancelar: Pagina Anterior');");
            sb.Append("if (r == true){window.location = '" + LoginP + "';}else{window.history.back(-1);}");
            sb.Append("</script>");
            if (!ClientScript.IsStartupScriptRegistered("JSScript"))
            {
                ClientScript.RegisterStartupScript(base.GetType(), "JSScript", sb.ToString());
            }
        }
    }
}