using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pais;
using Components.PTO.Rol;
using Components.PTO.StaRg;

namespace Components.PTO.Usr
{
    public interface IUsrPTO
    {
         String CemexId { get; set; }
         String NUsr { get; set; }
         String EMail { get; set; }
         IRolPTO Rol { get; set; }
         IPaisPTO Pais { get; set; }
         IStaRgPTO StaRg { get; set; }
    }
}
