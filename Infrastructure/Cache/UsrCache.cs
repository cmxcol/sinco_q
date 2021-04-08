using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Components.PTO.Cache;
using System.Web.Caching;

namespace Infrastructure.Cache
{
    public class UsrCache
    {
        public UsrCache(){}

        /// <summary>
        /// Agrega los Paginas de un Perfil al Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objCache">Objeto que se ingresara en la cache</param>
        /// <param name="context">El HTTPContext que se esta ejecutando</param>
        public static void AddPagesToCache(int key,IObjCache objCache , System.Web.HttpContext context)
        {
            AddPaginas(key.ToString(), objCache, context);
        }

        /// <summary>
        /// Agrega los Paginas de un Perfil al Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dsPaginas">Paginas</param>
        /// <param name="context">El HTTPContext que se esta ejecutando</param>
        public static void AddPagesToCache(String key, IObjCache objCache, System.Web.HttpContext context)
        {
            AddPaginas(key, objCache, context);
        }


        /// <summary>
        /// Agrega los Paginas de un Perfil al Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dsPaginas">Paginas</param>
        /// <param name="context">El HTTPContext que se esta ejecutando</param>
        private static void AddPaginas(String key, IObjCache objCache, System.Web.HttpContext context)
        {
            try
            {                
                if (context.Cache.Get(key) != null)
                    context.Cache.Remove(key); //remuevo si lo encuentra para reemplazarlo

                context.Cache.Add(key,
                    objCache, null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    System.Web.Caching.CacheItemPriority.High, null); //agrego al cache
            }
            catch (Exception )
            {
                //log.Error(ex);
            }
        }

        /// <summary>
        /// Limpia el Cache
        /// </summary>
        public static void InvalidarCache(System.Web.HttpContext context)
        {
            try
            {
                System.Collections.IDictionaryEnumerator num = context.Cache.GetEnumerator();
                while (num.MoveNext())
                    context.Cache.Remove(num.Key.ToString());
            }
            catch (Exception )
            {
                //log.Error(ex);
            }

        }

        public static bool IsInCache(string key, System.Web.HttpContext context)
        {
            bool result = false;
            try
            {
                if (context.Cache.Get(key) != null)
                    result = true;
            }
            catch (Exception )
            {
                //log.Error(ex);
            }

            return result;
        }
    }
}
