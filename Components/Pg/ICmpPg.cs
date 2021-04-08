using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pg;


namespace Components.Pg
{
    public interface ICmpPg
    {
        IEnumerable<IPagePTO> AuthPages(String cemexId, Int32 idPais);
        IEnumerable<IPagePTO> PtPage(int idTPage);
    }
}
