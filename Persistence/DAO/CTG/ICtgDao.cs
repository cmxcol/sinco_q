using DTO_Adapter.CTG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DAO.CTG
{
    public interface ICtgDao
    {
        IEnumerable<CtgDTO> GetCtg(Int32 idCtg, Int32 idPais, Int32 idSupBCtg = 0, int isActive = -99);
    }
}
