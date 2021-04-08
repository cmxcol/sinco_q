using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.MPg;

namespace Components.MPg
{
    public interface ICmpMPg
    {
        IMPagePTO AuthMPage(String cemexId, Int32 idPais);
    }
}
