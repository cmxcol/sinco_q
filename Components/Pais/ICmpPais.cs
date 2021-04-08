using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pais;

namespace Components.Pais
{
    public interface ICmpPais
    {
        IEnumerable<IPaisPTO> LoadActive();
        Boolean Exists(int idPais);
        Boolean IsActive(int idPais);
    }
}
