using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.CBloqueados.Cli
{
    public class VigenciaDTO : IVigenciaDTO
    {
        public String IdCliente { get; set; }
        public String Vigencia { get; set; }


        public VigenciaDTO()
        {
           
        }

        public VigenciaDTO(String idCliente, String vigencia)
        {
            IdCliente = idCliente;
            Vigencia = vigencia;
        }

        
    }
}
