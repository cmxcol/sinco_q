using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using Components.PTO.Cache;
using Components.PTO.Pg;
using Infrastructure.Security.Principal;
using Infrastructure.Security.Usuario;
using Infrastructure.Cache;

namespace Infrastructure.HttpModule
{
    public class CustomAuthenticationModule : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomAuthenticationModule()
        { }

        /// <summary>
        /// Inicializa el HTTPModule y asigna los EventHandlers a cada Evento
        /// Esta es la parte donde se define a que eventos va a atender el HttpModule
        /// </summary>
        /// <param name="oHttpApp"></param>
        public void Init(HttpApplication oHttpApp)
        {
            // Se Registran los Manejadores de Evento que nos interesa
            oHttpApp.AuthorizeRequest += new EventHandler(this.AuthorizeRequest);
            oHttpApp.AuthenticateRequest += new EventHandler(this.AuthenticateRequest);
            oHttpApp.Error += this.OnError;
            oHttpApp.PostAcquireRequestState += new EventHandler(Application_PostAcquireRequestState);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        { }

        /// <summary>
        /// Administra la autorización por Request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorizeRequest(object sender, EventArgs e)
        {
            var lt = new List<String>() { "/Account/", "/Images/", "/Scripts/", ".axd", ".ashx",".css" , ".asmx"};
            if (((IObjCache)System.Web.HttpContext.Current.Cache.Get("Lpg")) == null)
            {
                List<IPagePTO> pg = new List<IPagePTO>();
                pg.Add(Usr.LPage());
                UsrCache.AddPagesToCache("Lpg", new ObjCache(pg, null), HttpContext.Current);
            }
            if (lt.Any(r => HttpContext.Current.Request.Path.ToLower().Contains(r.ToLower())))
            {
                return;
            }
            //if (HttpContext.Current.Request.Path.ToLower().Contains("/account/"))
            //{
            //    return;
            //}
            if (HttpContext.Current.User != null)
            {
                //Si el usuario esta Autenticado
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User is FPrincipal)
                    {
                        FPrincipal principal = (FPrincipal)HttpContext.Current.User;
                        if (!principal.IsPageEnabled(HttpContext.Current.Request.Path))
                        {
                            HttpContext.Current.Server.Transfer("~/Account/AuthE.aspx");
                        }
                    }
                }
            }

        }

        void Application_PostAcquireRequestState(object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            if (System.Web.HttpContext.Current.Session != null)
            {

                if (System.Web.HttpContext.Current.Session["IdSolicitud"] != null)
                {
                    var IdSolicitud = System.Web.HttpContext.Current.Session["IdSolicitud"];
                    if (System.Web.HttpContext.Current.Session["Usr"] != null)
                    {
                        FPrincipal principal = (FPrincipal)HttpContext.Current.User;
                        var IdUsr = ((Usr)HttpContext.Current.Session["Usr"]).usr.CemexId;

                    }
                }


            }
        }

        /// <summary>
        /// Autentica en Cada Request
        /// </summary>
        /// <param name="sender">HttpApplication</param>
        /// <param name="e"></param>
        private void AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                //Si el usuario esta Autenticado
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        //Traigo el Rol que esta guardado en una Cookie encriptada
                        FormsIdentity _identity = (FormsIdentity)HttpContext.Current.User.Identity;
                        String cookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
                        String userData = System.Web.HttpContext.Current.Request.Cookies[cookieName].Value;
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(userData);

                        if (((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Expiration != ticket.Expiration)
                        {
                            var authTicket = new FormsAuthenticationTicket(2, ticket.Name, ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.IssueDate, ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Expiration, false, ticket.UserData, FormsAuthentication.FormsCookiePath);
                            String crypTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, crypTicket);
                            HttpContext.Current.Response.Cookies.Add(authCookie);
                        }

                        int IdPais = 0;
                        if (userData.Length > 0)
                            IdPais = int.Parse(ticket.UserData);

                        //Se crea la clase y se asigna al CurrenUser del Contexto			
                        HttpContext.Current.User = new FPrincipal(_identity, IdPais);
                    }
                }
            }
        }//AuthenticateRequest

        public void OnError(object obj, EventArgs args)
        {
            // At this point we have information about the error
/*
            HttpContext ctx = HttpContext.Current;
            HttpResponse response = ctx.Response;
            HttpRequest request = ctx.Request;

            Exception exception = ctx.Server.GetLastError();

            response.Write("Your request could not processed. " +
                           "Please press the back button on" +
                           " your browser and try again.<br/>");
            response.Write("If the problem persists, please " +
                           "contact technical support<p/>");
            response.Write("Information below is for " +
                           "technical support:<p/>");

            string errorInfo = "<p/>URL: " + ctx.Request.Url.ToString();
            errorInfo += "<p/>Stacktrace:---<br/>" +
                (exception.InnerException != null ? exception.InnerException.StackTrace.ToString() : "");
            errorInfo += "<p/>Error Message:<br/>" + (exception.InnerException != null ? exception.InnerException.Message : "");

            //Write out the query string 
            response.Write("Querystring:<p/>");

            for (int i = 0; i < request.QueryString.Count; i++)
            {
                response.Write("<br/>" +
                     request.QueryString.Keys[i].ToString() + " :--" +
                     request.QueryString[i].ToString() + "--<br/>");// + nvc.
            }

            //Write out the form collection
            response.Write("<p>---------------" +
                           "----------<p/>Form:<p/>");

            for (int i = 0; i < request.Form.Count; i++)
            {
                response.Write("<br/>" +
                         request.Form.Keys[i].ToString() +
                         " :--" + request.Form[i].ToString() +
                         "--<br/>");// + nvc.
            }

            response.Write("<p>-----------------" +
                           "--------<p/>ErrorInfo:<p/>");

            response.Write(errorInfo);

            // --------------------------------------------------
            // To let the page finish running we clear the error
            // --------------------------------------------------

            ctx.Server.ClearError();*/
        }

    }//class
}//namespace
