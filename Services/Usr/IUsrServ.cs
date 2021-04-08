using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Usr;

namespace Services.Usr
{
    public interface IUsrServ
    {
        IUsrPTO UsrData(String cemexId, Int32 idPais);
        IUsrPTO LoadUsrAsig(String cemexId, Int32 idPais, Int32 idRol);
        int InsertUsr(IUsrPTO usr);
        int UpdateUsr(IUsrPTO usr);
        String DefaultPage(Int32 idRol, Int32 idPais);
    }
}
