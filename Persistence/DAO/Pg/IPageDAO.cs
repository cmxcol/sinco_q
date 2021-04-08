using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Pg;
using Persistence.EntityDataModel;

namespace Persistence.DAO.Pg
{
    public interface IPageDAO : IGenericDAO<catPage, Int32>
    {
        IEnumerable<IPageDTO> LoadAuthPages(String cemexId, Int32 idPais);
        IEnumerable<IPageDTO> PageByTPage(int idTPage);
    }
}
