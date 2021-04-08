using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using Infrastructure.Security.Domain;
using Infrastructure.Security.Rol;
using Infrastructure.Security.Pais;
using Infrastructure.Cache;
using Services.Usr;
using Services.Cache;
using Services.Pg;
using Components.PTO.Usr;
using Components.PTO.Cache;
using Components.PTO.Pg;
using System.Web.Security;

namespace Infrastructure.Security.Usuario
{
    public class Usr : IUsr
    {
        public IUsrPTO usr { get; set; }
        public String PassWord { get; set; }

        public Usr() { }

        public Usr(IUsrPTO usr, String passWord)
        {
            this.usr = usr;
            PassWord = passWord;
        }
        public Usr(String cemexId, String passWord, int idPais)
        {
            usr = getUserData(cemexId.ToLower(), idPais);
            PassWord = passWord;
        }
        public bool IsActive()
        {
            return (usr != null ? usr.StaRg.IdStaRg == 1 : false);
        }
        public bool IsAuth()
        {
            //return (usr != null ? true : false);
            return (usr != null ? AuthUsrDomain(ConfigurationManager.AppSettings["DomainName"], ConfigurationManager.AppSettings["Path_LDAP_Directory_Server"]) : false);            
        }
        private bool AuthUsrDomain(String domainName, String Path_LDAP)
        {
            IDomainServer domainServer = new DomainServer(domainName, Path_LDAP);
            return domainServer.IsAuthenticated(usr.CemexId, PassWord);
        }
        public IUsrPTO getUserData(String cemexId, int idPais)
        {
            return UsrServI.Instance.UsrData(cemexId, idPais);
        }
        /// <summary>
        /// Obtiene las paginas que un usuario tiene acceso por pais
        /// </summary>
        /// <param name="cemexId"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public static IObjCache GetDataCache(String cemexId, Int32 idPais)
        {
            return CacheServI.Instance.DataCache(cemexId, idPais);
        }
        /// <summary>
        /// Obtiene un nuevo objeto con la información que se asignara a la cache
        /// </summary>
        /// <returns></returns>
        public IObjCache GetDataCache()
        {
            return CacheServI.Instance.DataCache(usr.CemexId, usr.Pais.IdPais);
        }
        public String DefaultPage()
        {
            return UsrServI.Instance.DefaultPage(usr.Rol.IdRol, usr.Pais.IdPais);
        }
        /// <summary>
        /// Determina si una pagina esta habilitada a un usuario
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="cemexId"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public static bool IsPageEnabled(String pName, String cemexId, int idPais)
        {
            CheckCache(cemexId, idPais);
            bool result = false;
            try
            {
                IObjCache objCache  = (IObjCache)System.Web.HttpContext.Current.Cache.Get(cemexId);                
                foreach (var page in objCache.Pages)
                {
                    if (page.Url == pName | ("/" + page.AppName + page.Url) == pName)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception )
            {
                //log.Error(ex);
            }
            return result;
        }
        /// <summary>
        /// Verifica la información en Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="idPais"></param>
        private static void CheckCache(String key, int idPais)
        {
            try
            {
                if (System.Web.HttpContext.Current.Cache.Get(key) == null)
                    UsrCache.AddPagesToCache(key, GetDataCache(key, idPais), System.Web.HttpContext.Current);
            }
            catch (Exception )
            {
                //log.Error(ex);
            }
        }
        public static String LoginPage()
        {
            var HostName = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName;
            IPagePTO lpg = PgServI.Instance.LoginPage();
            return lpg == null ? String.Empty : (FormsAuthentication.RequireSSL ? "https://" : "http://") + HostName + "/" + lpg.AppName + lpg.Url;         
        }
        public static IPagePTO LPage()
        {
            return PgServI.Instance.LoginPage();
        }
        public static String RUrl(IPagePTO pg)
        {
            var HostName = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName;
            return pg == null ? String.Empty : (FormsAuthentication.RequireSSL ? "https://" : "http://") + HostName + "/" + pg.AppName + pg.Url;
        }
    }
}
