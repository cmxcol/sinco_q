using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.Ex
{
    public class ExcpPTO:IExcpPTO
    {

        public String IdCliente { get; set; }
        public String TEx { get; set; }
        public String DtVig { get; set; }
        public String MsgEx { get; set; }
        public String EMsg { get; set; }
        public Int64 IdEmp { get; set; }

        public ExcpPTO ()
        {
            EMsg = null;
        }

        public ExcpPTO(String idCliente, String tEx, String dtVig, String msgEx, String eMsg = null, Int64 idEmp = 0)
        {
            IdCliente = idCliente;
            TEx = tEx;
            DtVig = dtVig;
            MsgEx = msgEx;
            EMsg = eMsg;
            IdEmp = idEmp;
        }
    }
}
