using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.Pais;
using Persistence.DTO.Rol;
using Persistence.DTO.StaRg;

namespace Persistence.DTO.Usr
{
    public interface IUsrDTO
    {
        String CemexId { get; set; }
        String NUsr { get; set; }
        String EMail { get; set; }
        IRolDTO Rol { get; set; }
        IPaisDTO Pais { get; set; }
        IStaRgDTO StaRg { get; set; }
    }
}
