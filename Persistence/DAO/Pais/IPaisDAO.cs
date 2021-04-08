using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Pais;
using Persistence.EntityDataModel;

namespace Persistence.DAO.Pais
{
    public interface IPaisDAO : IGenericDAO<catPais, Int32>
    {
        IEnumerable<IPaisDTO> LoadByState(int IdStaRg);
        new IPaisDTO Load(int id);
    }
}
