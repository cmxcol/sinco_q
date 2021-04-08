using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Cache;
using Components.Pg;
using Components.MPg;
using Components.PTO.Cache;
using Components.PTO.Pg;
using Components.PTO.MPg;

namespace Services.Cache
{
    public class CacheServ:ICacheServ
    {
        private ICmpCache _cmpCache = null;
        private ICmpPg _cmpPg = null;
        private ICmpMPg _cmpMPg = null;

        public CacheServ ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            _cmpCache = container.Resolve<ICmpCache>();
            _cmpPg = container.Resolve<ICmpPg>();
            _cmpMPg = container.Resolve<ICmpMPg>();
        }
        public IObjCache DataCache(String cemexId, int idPais)
        {
            return _cmpCache.CreateObjCache(_cmpPg.AuthPages(cemexId,idPais),_cmpMPg.AuthMPage(cemexId,idPais));
        }
    }
}
