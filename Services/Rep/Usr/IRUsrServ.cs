using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Rep.Usr
{
    public interface IRUsrServ
    {
        IEnumerable<Object> RepUsrs(Int32 tCom, Int32 idPais, Int32 idRol);
    }
}
