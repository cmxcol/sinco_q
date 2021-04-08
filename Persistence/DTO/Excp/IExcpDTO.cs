using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.Excp.TExcp;
using Persistence.DTO.Pais;
using Persistence.DTO.Rol;

namespace Persistence.DTO.Excp
{
    public interface IExcpDTO
    {
        Int32 IdEx { get; set; }
        Int64 IdCliente { get; set; }
        ITExcpDTO TEx { get; set; }
        String DtVig { get; set; }
        IPaisDTO Pais { get; set; }
        IRolDTO Rol { get; set; }
        String MsgEx { get; set; }
        Int64 IdEmp { get; set; }
    }
}
