using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Ex.ExMsg;

namespace Components.Excp.ExMsg
{
    public interface ICmpExMsg
    {
        IEnumerable<IExMsgTPTO> TExMsg(String idTEx, int idStaRg);
    }
}
