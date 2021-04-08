using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.BCfg;
using DTO_Adapter.DBCnn;

namespace Services.BCfg
{
    public class BCfgServ
    {
        private CmpBCfg _cmpBcfg;

        public BCfgServ()
        {
            _cmpBcfg = new CmpBCfg();
        }

        #region Tipos de Bomba

        public IEnumerable<BConfigDto> getPumpM(Int32 idPais)
        {
            return _cmpBcfg.getPumpM(idPais);
        }

        #endregion

        #region Sobre Cargos
        public BConfigDto getSC_CargaMinima(Int32 idPais)
        {
            return _cmpBcfg.getSC_CargaMinima(idPais);
        }
        public BConfigDto getSC_BombeoMinimo_E(Int32 idPais)
        {
            return _cmpBcfg.getSC_BombeoMinimo_E(idPais);
        }
        public BConfigDto getSC_BombeoMinimo_A(Int32 idPais)
        {
            return _cmpBcfg.getSC_BombeoMinimo_A(idPais);
        }
        public CargosCliDto getSC_Obr(String obra, Int32 idOrg, Int32 idSector)
        {
            return _cmpBcfg.getSC_Obr(obra, idOrg, idSector);
        }

        public IEnumerable<BConfigDto> getSC_All(Int32 idPais)
        {
            return _cmpBcfg.getSC_All(idPais);
        }

        #endregion

        #region Reglas Sobre Cargo
        public BConfigDto getR_SC_CargaMinima(String idBCfg, Int32 idPais)
        {
            return _cmpBcfg.getR_SC_CargaMinima(idBCfg, idPais);
        }

        public BConfigDto getR_SC_BombeoMinimo_E(String idBCfg, Int32 idPais)
        {
            return _cmpBcfg.getR_SC_BombeoMinimo_E(idBCfg, idPais);
        }

        public BConfigDto getR_SC_BombeoMinimo_A(String idBCfg, Int32 idPais)
        {
            return _cmpBcfg.getR_SC_BombeoMinimo_A(idBCfg, idPais);
        }
        #endregion

        #region Volumen Maximo Obra

        public VolMaxDto getVM_Obr(String obra, Int32 idOrg, Int32 idSector)
        {
            return _cmpBcfg.getVM_Obr(obra, idOrg, idSector);
        }

        #endregion

        #region % de Incremento del Volumen (Ajuste)

        public BConfigDto getPctVAjs(Int32 idPais)
        {
            return _cmpBcfg.getPctVAjs(idPais);
        }

        #endregion

        #region % de Incremento Limite de Credito

        public BConfigDto getPctILC(Int32 idPais)
        {
            return _cmpBcfg.getPctILC(idPais);
        }

        #endregion


    }
}
