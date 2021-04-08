using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Ex;
using Components.PTO.Ex.ExMsg;

namespace Services.Excp
{
    public interface IExS
    {
        IEnumerable<IEnumerable<IExcpPTO>> InsExArch(IEnumerable<IExcpPTO> ex, String usr, Int32 idRol, Int32 idPais);
        IEnumerable<IExMsgTPTO> ExMsg(String idTEx, int idStaRg);
    }
}
