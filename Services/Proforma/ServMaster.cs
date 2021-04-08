using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Proforma;
using DTO_Adapter.Proforma;
using DTO_Adapter.SQL;

namespace Services.Proforma
{
    public class ServMaster:IServMaster
    {
        private ICmpMaster cmpMaster = null;
        public ServMaster()
        {
            cmpMaster= new CmpMaster();
        }

        public MasterDTO GetMaestrosByObra(long Obra)
        {
            return cmpMaster.GetMaestrosByObra(Obra);
        }

        public List<MatbytypeDTO> GetMaterialesPorTipo(string Tipo)
        {
            return cmpMaster.GetMaterialesPorTipo(Tipo);
        }
    }
}
