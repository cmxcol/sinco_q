using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Usr.Rep
{
    public interface IUsrsRepDTO
    {
        String CemexId { get; set; }
        String NUsr { get; set; }
        String EMail { get; set; }
        String Rol { get; set; }
        String StaRg { get; set; }
    }
}
