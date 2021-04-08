using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Rol;
using Persistence.EntityDataModel;

namespace Persistence.DAO.Rol
{
    public interface IRolDAO : IGenericDAO<catRol, Int32>
    {
        IEnumerable<IRolDTO> LoadAll();
        IEnumerable<IRolDTO> LoadAsig(Int32 idRol);
        new IRolDTO Load(int id);
    }
}
