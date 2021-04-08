using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.StaRg;
using Persistence.DTO.Rol;
using Persistence.DTO.Pais;

namespace Persistence.DTO.Usr
{
    public class UsrDTO:IUsrDTO
    {
        public String CemexId { get; set; }
        public String NUsr { get; set; }
        public String EMail { get; set; }
        public IRolDTO Rol { get; set; }
        public IPaisDTO Pais { get; set; }
        public IStaRgDTO StaRg { get; set; }

        public UsrDTO ()
        {}

        public UsrDTO(String cemexId, String nUsr, String eMail, IRolDTO rol, IPaisDTO pais,IStaRgDTO staRg)
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
