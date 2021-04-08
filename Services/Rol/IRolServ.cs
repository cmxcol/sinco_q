using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Rol;

namespace Services.Rol
{
    public interface IRolServ
    {
        IEnumerable<IRolPTO> LoadAll();
        IEnumerable<IRolPTO> LoadAsig(Int32 idRol);
    }
}
