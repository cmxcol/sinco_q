using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pais;
using Components.PTO.Rol;
using Components.PTO.StaRg;

namespace Components.PTO.Usr
{
    public class UsrPTO : IUsrPTO
    {
        public String CemexId { get; set; }
        public String NUsr { get; set; }
        public String EMail { get; set; }
        public IRolPTO Rol { get; set; }
        public IPaisPTO Pais { get; set; }
        public IStaRgPTO StaRg { get; set; }

        public UsrPTO()
        { }

        public UsrPTO(String cemexId, String nUsr, String eMail, IRolPTO rol, IPaisPTO pais, IStaRgPTO staRg)
        {
            this.CemexId = cemexId;
            this.NUsr = nUsr;
            this.EMail = eMail;
            this.Rol = rol;
            this.Pais = pais;
            this.StaRg = staRg;
        }
    }
}
