using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.Rol
{
    public class RolPTO:IRolPTO
    {
        public int IdRol { get; set; }
        public String NRol { get; set; }

        public RolPTO ()
        {}

        public RolPTO(int idRol, String nRol)
        {
            this.IdRol = idRol;
            this.NRol = nRol;
        }
    }
}
