using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Cache;
using Components.PTO.Pg;
using Components.PTO.MPg;
using Persistence.DTO.Pg;
using Persistence.DTO.MPg;

namespace Components.Cache
{
    public class CmpCache:ICmpCache
    {
        public CmpCache()
        {
        }
        public IObjCache CreateObjCache(IEnumerable<IPagePTO> pages,IMPagePTO mPage)
        {
            return new ObjCache(pages,mPage);
        }
        public IObjCache CreateObjCache(IEnumerable<IPageDTO> pages, IMPageDTO mPage)
        {
            return new ObjCache(null, null);
        }
}
}
