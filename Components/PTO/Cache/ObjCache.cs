using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pg;
using Components.PTO.MPg;

namespace Components.PTO.Cache
{
    public class ObjCache:IObjCache
    {
        public IEnumerable<IPagePTO> Pages { get; set; }
        public IMPagePTO MPage { get; set; }

        public ObjCache()
        {
        }

        public ObjCache(IEnumerable<IPagePTO> pages, IMPagePTO mPage)
        {
            this.Pages = pages;
            this.MPage = mPage;
        }
    }
}
