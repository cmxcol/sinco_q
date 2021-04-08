using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.Excp.TExcp;
using Persistence.DTO.Pais;
using Persistence.DTO.Rol;

namespace Persistence.DTO.Excp
{
    public class ExcpDTO : IExcpDTO
    {
        public Int32 IdEx { get; set; }
        public Int64 IdCliente { get; set; }
        public ITExcpDTO TEx { get; set; }
        public String DtVig { get; set; }
        public IPaisDTO Pais { get; set; }
        public IRolDTO Rol { get; set; }
        public String MsgEx { get; set; }
        public Int64 IdEmp { get; set; }

        public ExcpDTO()
        {
        }

        public ExcpDTO(Int32 idEx, Int64 idCliente, ITExcpDTO tEx, String dtVig, IPaisDTO pais, IRolDTO rol, String msgEx, Int64 idEmp)
        {
            IdEx = idEx;
            IdCliente = idCliente;
            TEx = tEx;
            DtVig = dtVig;
            Pais = pais;
            Rol = rol;
            MsgEx = msgEx;
            IdEmp = idEmp;
        }
    }
}
