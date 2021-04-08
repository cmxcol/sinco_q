using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;

namespace Components.Sim
{
    public interface ICmpSim
    {
        String PrecioT(Int64 Obra,Int64 Material,float Volumen,string centro,string condpago, SAPCredentialsDTO credentials);
        String IVA(Int64 Obra,  SAPCredentialsDTO credentials);
        String ValSegmentacion(String Segcli, String SegReg, Boolean planta);
        String IVAMat(Int64 Material, SAPCredentialsDTO credentials);
        String GetCondMat();
        IEnumerable<ValDTO> ValObra(long Obra,Boolean planta);
        String GetZTERM(long Obra,string Sector);
        string GetCentro(long Obra, string Sector);
        MasterMatDTO GetMatUM(string Familia, string Norma, string Res, string TamGra, string TipGra, string Edad, string Asen, string Bomb, string TipCem, string Var);
        CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials);

    }
}
