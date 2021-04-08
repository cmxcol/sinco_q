using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Excp.ExMsg
{
    public interface IExMsgDTO:IExMsgTDTO
    {
        Int32 IdExMsg { get; set; }
        String IdTEx { get; set; }
        Int32 IdStaRg { get; set; }
    }
}
