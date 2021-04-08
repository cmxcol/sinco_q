using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;

namespace Persistence.DAO.Sim
{
    public interface ISimDAO
    {
        String PrecioT(Int64 Obra, Int64 Material, float Volumen,string Centro,string CondPago, SAPCredentialsDTO credentials);
        String IVA(Int64 Obra, SAPCredentialsDTO credentials);
        String ValSegmentacion(String Segcli, String SegReg, Boolean planta);
        String IVAMat(Int64 Material, SAPCredentialsDTO credentials);

        String GetCondMat();

        IEnumerable<ValDTO> ValObra(long Obra,Boolean planta);
        CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials);
    }
}
