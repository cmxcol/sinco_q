using DTO_Adapter.CTG;
using DTO_Adapter.Proforma;
using Persistence.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.CTG
{
    public class CmpCTG : ICmpCTG
    {
        private CtgDAO ctg = null;
        public CmpCTG()
        {
            ctg = new CtgDAO();
        }
        public IEnumerable<CtgDTO> getCtg(Int32 idCtg, Int32 idPais, Int32 idSupBCtg, Int32 isActive)
        {
            return ctg.GetCtg(idCtg, idPais, idSupBCtg, isActive);
        }

        public IEnumerable<proformaDatos> GetDtosPrfm(int Tcom, String codCliente, String codObra, String codComercial, String cemexID, String valNeto, String fecha, String nomProdAdd, String ivaProdAdd)//johanProforma
        {
            return ctg.GetDtosPrfm(Tcom, codCliente, codObra, codComercial, cemexID, valNeto, fecha,  nomProdAdd,  ivaProdAdd);
        }     

    }


}
