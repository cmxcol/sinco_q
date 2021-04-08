using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Usr;
using Persistence.EntityDataModel;

namespace Persistence.DAO.Usr
{
    public interface IUsrDAO : IGenericDAO<catUsr, String>
    {
        IUsrDTO LoadUsrR(String cemexId, int idPais);
        Int32 isRolAsig(Int32 idRol, Int32 idRolA);
        int InsertUsr(IUsrDTO usr);
        int UpdateUsr(IUsrDTO usr);
        String DefaultPage(Int32 idRol, Int32 idPais);
        Boolean IsActive(String cemexId, Int32 idPais);
    }
}
