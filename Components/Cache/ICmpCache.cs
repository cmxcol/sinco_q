using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Cache;
using Components.PTO.MPg;
using Components.PTO.Pg;
using Persistence.DTO.MPg;
using Persistence.DTO.Pg;

namespace Components.Cache
{
    public interface ICmpCache
    {
        IObjCache CreateObjCache(IEnumerable<IPagePTO> pages, IMPagePTO mPage);
        IObjCache CreateObjCache(IEnumerable<IPageDTO> pages, IMPageDTO mPage);
    }
}
