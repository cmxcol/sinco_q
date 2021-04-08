using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.CBloqueados.Cli
{
    public interface ICliPTO
    {
        String IdCliente { get; set; }
        String Deudor { get; set; }
        String Excepcion { get; set; }


    }
}
