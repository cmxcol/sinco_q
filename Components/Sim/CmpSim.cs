using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Sim;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;
using Persistence.SQL;

namespace Components.Sim
{
    public class CmpSim :ICmpSim
    {

        private ISimDAO cmpSim = null;
        private ISQLDAO cmpSQL = null;
        public CmpSim()
        {
            cmpSim = new SimDAO();
            cmpSQL = new SQLDAO();
        }
        public String PrecioT(Int64 Obra, Int64 Material, float Volumen,string centro,string condpago, SAPCredentialsDTO credentials)
        {
            return cmpSim.PrecioT(Obra, Material, Volumen,centro,condpago,credentials);
        }
        public String IVA(Int64 Obra, SAPCredentialsDTO credentials)
        {
            return cmpSim.IVA(Obra, credentials);
        }
        public String ValSegmentacion(String Segcli, String SegReg, Boolean planta)
        {
            return cmpSim.ValSegmentacion(Segcli, SegReg, planta);
        }
        public String IVAMat(Int64 Material, SAPCredentialsDTO credentials)
        {
            return cmpSim.IVAMat(Material, credentials);
        }

        public CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials)
        {
            return cmpSim.CondicionLogistica(IdObra, credentials);
        }
        public String GetCondMat()
        {
            return cmpSim.GetCondMat();
        }
        public IEnumerable<ValDTO> ValObra(long Obra,Boolean planta)
        {
            return cmpSim.ValObra(Obra,planta);
        }

        public string GetCentro(long Obra, string Sector)
        {
            return cmpSQL.GetCentro(Obra, Sector);
        }
        public String GetZTERM(long Obra,string Sector)
        {
            return cmpSQL.GetZTERM(Obra,Sector);
        }
        public MasterMatDTO GetMatUM(string Familia, string Norma, string Res, string TamGra, string TipGra, string Edad, string Asen, string Bomb, string TipCem, string Var)
        {
            return cmpSQL.GetMatUM( Familia,  Norma,  Res,  TamGra,  TipGra,  Edad,  Asen,  Bomb,  TipCem,  Var);
        }
    }
}
