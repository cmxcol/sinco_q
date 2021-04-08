using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.Rep.Usr
{
    public interface ICmpRepUsr
    {
        IEnumerable<Object> RepUsrs(Int32 tCom, Int32 idPais, Int32 idRol);
    }
}
