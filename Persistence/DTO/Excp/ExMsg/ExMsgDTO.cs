using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Excp.ExMsg
{
    public class ExMsgDTO : IExMsgDTO
    {
        public Int32 IdExMsg { get; set; }
        public String Msg { get; set; }
        public String IdTEx { get; set; }
        public Int32 IdStaRg { get; set; }

        public ExMsgDTO()
        {

        }
    }
}
