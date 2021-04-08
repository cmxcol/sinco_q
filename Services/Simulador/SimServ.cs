using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Components.Sim;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;

namespace Services.Simulador
{
    public class SimServ :ISimServ
    {

        private ICmpSim _cmpSim = null;

        public SimServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            _cmpSim = container.Resolve<ICmpSim>();
        }
        public String PrecioT(Int64 Obra, Int64 Material,float Volumen,string centro,string condpago, SAPCredentialsDTO credentials)
        {
           return  _cmpSim.PrecioT(Obra,Material,Volumen,centro,condpago,credentials);
        }
        public String IVA(Int64 Obra, SAPCredentialsDTO credentials)
        {
            return _cmpSim.IVA(Obra, credentials);
        }
        public String ValSegmentacion(String Segrcli, String SegReg, Boolean planta)
        {
            return _cmpSim.ValSegmentacion(Segrcli, SegReg, planta);
        }

        public CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials)
        {
            return _cmpSim.CondicionLogistica(IdObra, credentials);
        }


        public String IVAMat(Int64 Material, SAPCredentialsDTO credentials)
        {
            return _cmpSim.IVAMat(Material, credentials);
        }
        public String GetCondMat()
        {
            return _cmpSim.GetCondMat();
        }
        public string GetCentro(long Obra, string Sector)
        {
            return _cmpSim.GetCentro(Obra, Sector);
        }
        public IEnumerable<ValDTO> ValObra(long Obra, Boolean Planta)
        {
            return _cmpSim.ValObra(Obra, Planta);
        }
        public String GetZTERM(long Obra,string Sector)
        {
            return _cmpSim.GetZTERM(Obra,Sector);
        } 
        public MasterMatDTO GetMatUM (string Familia , string Norma, string Res, string TamGra, string TipGra, string Edad, string Asen, string Bomb, string TipCem, string Var )
        {
            return _cmpSim.GetMatUM( Familia,  Norma,  Res,  TamGra,  TipGra,  Edad,  Asen,  Bomb,  TipCem,  Var);
        }
    }
}
