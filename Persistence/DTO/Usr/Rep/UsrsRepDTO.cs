using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.Usr.Rep
{
    public class UsrsRepDTO:IUsrsRepDTO
    {
        public String CemexId { get; set; }
        public String NUsr { get; set; }
        public String EMail { get; set; }
        public String Rol { get; set; }
        public String StaRg { get; set; }

        public UsrsRepDTO()
        {
        }

        public UsrsRepDTO(String cemexId, String nUsr, String eMail, String rol, String staRg)
        {
            this.CemexId = cemexId;
            this.NUsr = nUsr;
            this.EMail = eMail;
            this.Rol = rol;
            this.StaRg = staRg;
        }
    }
}
