using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Cache;

namespace Services.Cache
{
    public interface ICacheServ
    {
        IObjCache DataCache(String cemexId, int idPais);
    }
}
