using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pais;

namespace Services.Pais
{
    public interface IPaisServ
    {
        IEnumerable<IPaisPTO> LoadActive();
    }
}
