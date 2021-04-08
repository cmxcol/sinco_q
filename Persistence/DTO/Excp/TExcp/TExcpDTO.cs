using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Excp.TExcp
{
    public class TExcpDTO:ITExcpDTO
    {
        public String IdTEx { get; set; }
        public String NTEx { get; set; }
        
        public TExcpDTO()
        {
        }
        public TExcpDTO(String idTEx, String nTEx)
        {
            IdTEx = idTEx;
            NTEx = nTEx;
        }
    }
}
