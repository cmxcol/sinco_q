using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Cache;
using Components.PTO.Usr;
using Infrastructure.Security.Pais;
using Infrastructure.Security.Rol;

namespace Infrastructure.Security.Usuario
{
    public interface IUsr
    {
        IUsrPTO usr { get; set; }
        String PassWord { get; set; }
        bool IsActive();
        bool IsAuth();
        IUsrPTO getUserData(String cemexId, int idPais);
        IObjCache GetDataCache();
        String DefaultPage();
    }
}
