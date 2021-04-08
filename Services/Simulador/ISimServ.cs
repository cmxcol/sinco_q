using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;

namespace Services.Simulador
{
    public interface ISimServ
    {
        String PrecioT(Int64 Obra,Int64 Material,float Volumen,string centro,string condpago, SAPCredentialsDTO credentials);
        String IVA(Int64 Obra, SAPCredentialsDTO credentials);
        String IVAMat(Int64 Material, SAPCredentialsDTO credentials);
        String GetCondMat();
        String ValSegmentacion(String Segrcli, String SegReg, Boolean planta);
        IEnumerable<ValDTO> ValObra(long Obra,Boolean planta);
        String GetZTERM(long Obra,string Sector);
        CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials);
    }
}
