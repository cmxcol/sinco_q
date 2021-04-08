using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Rol;

namespace Components.Rol
{
    public interface ICmpRol
    {
        IEnumerable<IRolPTO> LoadAll();
        IEnumerable<IRolPTO> LoadAsig(Int32 idRol);
    }
}
