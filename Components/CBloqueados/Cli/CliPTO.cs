using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.CBloqueados.Cli
{
    public class CliPTO : ICliPTO
    {

        public String IdCliente { get; set; }
        public String Deudor { get; set; }
        public String Excepcion { get; set; }


        public CliPTO()
        {
           
        }

        public CliPTO(String idCliente, String deudor, String exp)
        {
            IdCliente = idCliente;
            Deudor = deudor;
            Excepcion  = exp;
        }
    }
}
