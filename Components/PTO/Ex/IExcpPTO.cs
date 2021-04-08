using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.Ex
{
    public interface IExcpPTO
    {
        String IdCliente { get; set; }
        String TEx { get; set; }
        String DtVig { get; set; }
        String MsgEx { get; set; }
        String EMsg { get; set; }
        Int64 IdEmp { get; set; }
    }
}
