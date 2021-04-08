using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.Proforma;
using DTO_Adapter.SQL;
using Persistence.Proforma;

namespace Components.Proforma
{
    public class CmpMaster:ICmpMaster
    {
        private IMasterDAO _cmpMaster= null;
        public CmpMaster()
        {
            _cmpMaster = new MasterDAO();
        }

        public MasterDTO GetMaestrosByObra(long Obra)
        {
            return _cmpMaster.GetMaestrosByObra(Obra);
        }
        public List<MatbytypeDTO> GetMaterialesPorTipo(string Tipo)
        {
            return _cmpMaster.GetMaterialesPorTipo(Tipo);
        }

    }
}
