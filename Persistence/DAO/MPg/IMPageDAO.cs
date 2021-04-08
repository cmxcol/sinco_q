using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.MPg;
using Persistence.EntityDataModel;

namespace Persistence.DAO.MPg
{
    public interface IMPageDAO : IGenericDAO<catMPage, Int32>
    {
        IMPageDTO LoadAuthMPage(String cemexId, Int32 idPais);
    }
}
