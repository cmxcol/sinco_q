using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Excp.TExcp;
using Persistence.EntityDataModel;


namespace Persistence.DAO.Excp.TExcp
{
    public interface ITExcpDAO : IGenericDAO<catTEx, String>
    {
        IEnumerable<ITExcpDTO> LoadAll();
        new ITExcpDTO Load(String id);
    }
}