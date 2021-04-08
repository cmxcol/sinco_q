using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Components.PTO.Cache;
using Infrastructure.Security.Usuario;

namespace WebApplication.Client.Web.App_Code
{
    public class BMPage : MasterPage
    {
        public BMPage()
        {
            this.Init += new EventHandler(BMPage_Init);
        }

        void BMPage_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usr"] != null)
                {
                    var objCache = (IObjCache)HttpContext.Current.Cache.Get(((Usr)Session["Usr"]).usr.CemexId);
                    if (Master.FindControl("lblUsr") != null)
                    {
                        ((Label)Master.FindControl("lblUsr")).Text = ((Usr)Session["Usr"]).usr.NUsr.Trim(); 
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}