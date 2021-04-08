using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.BCfg;
using DTO_Adapter.DBCnn;

namespace Components.BCfg
{
    public class CmpBCfg
    {
        private BusinessConfigDao _bCfg;
        private CargosCliDao _cCli;

        public CmpBCfg()
        {
            _bCfg = new BusinessConfigDao();
            _cCli = new CargosCliDao();
        }
        #region Tipos de Bomba

        public IEnumerable<BConfigDto> getPumpM(Int32 idPais)
        {
            return _bCfg.GetBConfig(idPais, "TPUMP", true);
        } 
        #endregion

        #region Sobre Cargos
        public BConfigDto getSC_CargaMinima(Int32 idPais)
        {
            return _bCfg.GetBConfig("SCCM", idPais, "SCC", true);
        }
        public BConfigDto getSC_BombeoMinimo_E(Int32 idPais)
        {
            return _bCfg.GetBConfig("SCBME", idPais, "SCC", true);
        }
        public BConfigDto getSC_BombeoMinimo_A(Int32 idPais)
        {
            return _bCfg.GetBConfig("SCMAB", idPais, "SCC", true);
        } 
        public CargosCliDto getSC_Obr(String obra, Int32 idOrg, Int32 idSector)
        {
            return _cCli.GetCargos(obra, idOrg, idSector);
        }

        public IEnumerable<BConfigDto> getSC_All(Int32 idPais)
        {
            return _bCfg.GetBConfig(idPais, "SCC", true);
        }

        #endregion

        #region Reglas Sobre Cargo
        public BConfigDto getR_SC_CargaMinima(String idBCfg,Int32 idPais)
        {
            return _bCfg.GetBConfig(idBCfg, idPais, "SCCM", true);
        }

        public BConfigDto getR_SC_BombeoMinimo_E(String idBCfg,Int32 idPais)
        {
            return _bCfg.GetBConfig(idBCfg, idPais, "SCBME", true);
        }

        public BConfigDto getR_SC_BombeoMinimo_A(String idBCfg,Int32 idPais)
        {
            return _bCfg.GetBConfig(idBCfg, idPais, "SCMAB", true);
        } 
        #endregion

        #region Volumen Maximo Obra

        public VolMaxDto getVM_Obr(String obra, Int32 idOrg, Int32 idSector)
        {
            return _cCli.GetVolMax(obra, idOrg, idSector);
        }
        
        #endregion

        #region % de Incremento del Volumen (Ajuste)

        public BConfigDto getPctVAjs(Int32 idPais)
        {
            return _bCfg.GetBConfig("AVCP", idPais, "CCSR", true);
        }

        #endregion

        #region % de Incremento Limite de Credito

        public BConfigDto getPctILC(Int32 idPais)
        {
            return _bCfg.GetBConfig("ILC", idPais, "CCart", true);
        }

        #endregion

    }
}
