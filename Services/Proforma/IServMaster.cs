using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.Proforma;
using DTO_Adapter.SQL;

namespace Services.Proforma
{
    public interface IServMaster
    {
        MasterDTO GetMaestrosByObra(long Obra);
        List<MatbytypeDTO> GetMaterialesPorTipo(string Tipo);

    }
}
