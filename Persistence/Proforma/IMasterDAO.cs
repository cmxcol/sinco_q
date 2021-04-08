using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.Proforma;
using DTO_Adapter.SQL;

namespace Persistence.Proforma
{
    public interface IMasterDAO
    {
        MasterDTO GetMaestrosByObra(long Obra);
        List<MatbytypeDTO> GetMaterialesPorTipo(string Tipo);
    }
}
