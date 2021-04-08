using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.Excp.ExMsg;

namespace Persistence.DAO.Excp.ExMsg
{
    public interface IExMsgDAO
    {
        IEnumerable<IExMsgTDTO> TExMsg(String idTEx, int idStaRg);
    }
}
