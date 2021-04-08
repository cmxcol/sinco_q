using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Ex;
using Persistence.DAO.Excp.TExcp;
using Persistence.DTO.Excp.TExcp;

namespace Components.Excp
{
    public interface ICmpEx
    {
        IExcpPTO ValIExcp(IExcpPTO eX);
        IEnumerable<IEnumerable<IExcpPTO>> ValIcExcp(IEnumerable<IExcpPTO> eX);
        IEnumerable<IEnumerable<IExcpPTO>> InsExcpArch(IEnumerable<IExcpPTO> eX, String usr, Int32 idRol, Int32 idPais);
    }
}
