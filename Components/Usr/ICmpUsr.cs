using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Usr;
using Persistence.DTO.Usr;
using Persistence.EntityDataModel;

namespace Components.Usr
{
    public interface ICmpUsr
    {
        IUsrPTO UsrData(String cemexId, int idPais);
        IUsrPTO LoadUsrAsig(String cemexId, Int32 idPais, Int32 idRol);
        int InsertUsr(IUsrPTO usr);
        int UpdateUsr(IUsrPTO usr);
        Boolean Exists(String cemexId);
        Boolean IsActive(String cemexId, Int32 idPais);
        String DefaultPage(Int32 idRol, Int32 idPais);
    }
}
