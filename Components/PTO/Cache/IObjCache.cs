using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.MPg;
using Components.PTO.Pg;

namespace Components.PTO.Cache
{
    public interface IObjCache
    {
        IEnumerable<IPagePTO> Pages { get; set; }
        IMPagePTO MPage { get; set; }
    }
}
