using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Components.PTO.Cache;
using Infrastructure.Security.Usuario;

namespace WebApplication.Client.Web.App_Code
{
    public class BPage : Page
    {
        public BPage()
        {
            this.PreInit += new EventHandler(BasePage_PreInit);
        }
        void BasePage_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usr"] != null)
                {
                    var objCache = (IObjCache)HttpContext.Current.Cache.Get(((Usr)Session["Usr"]).usr.CemexId);
                    MasterPageFile = "~" + objCache.MPage.Url;
                    if (Master.FindControl("lblUsr") != null)
                    {
                        ((Label)Master.FindControl("lblUsr")).Text = ((Usr)Session["Usr"]).usr.NUsr.Trim();
                    }
                }
            }
            catch (Exception )
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}