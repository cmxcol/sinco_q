using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Rol
{
    public class RolDTO:IRolDTO
    {
        public int IdRol { get; set; }
        public String NRol { get; set; }

        public RolDTO ()
        {}

        public RolDTO(int idRol,String nRol)
        {
            this.IdRol = idRol;
            this.NRol = nRol;
        }

    }
}
