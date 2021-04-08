using Components.CTG;
using DTO_Adapter.CTG;
using DTO_Adapter.Proforma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.CTG
{
    public class ServCTG
    {
        private CmpCTG cmpCtg = null;

        public ServCTG()
        {
            cmpCtg = new CmpCTG();
        }

        public IEnumerable<CtgDTO> GetTipoAjuste(int idPais, int idCtg = 0)
        {
            return cmpCtg.getCtg(idCtg, idPais, 320, 1);
        }

        public IEnumerable<proformaDatos> GetDtosPrfm(int Tcom, String codCliente, String codObra, String codComercial, String cemexID, String valNeto, String fecha, String nomProdAdd, String ivaProdAdd)//johanProforma
        {
            return cmpCtg.GetDtosPrfm(Tcom, codCliente, codObra, codComercial, cemexID, valNeto, fecha,  nomProdAdd,  ivaProdAdd);
        }
       
    }
}
