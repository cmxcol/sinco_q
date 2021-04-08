using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.StaRg;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DAO.Generic;

namespace Persistence.DAO.StaRg
{
    public interface IStaRgDAO:IGenericDAO<catStaRg,Int32>
    {
        IEnumerable<IStaRgDTO> LoadAll();
    }
}
