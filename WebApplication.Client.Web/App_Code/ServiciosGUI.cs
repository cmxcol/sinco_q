using DTO_Adapter.CTG;
using Services.CTG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DTO_Adapter.Proforma;

namespace WebApplication.Client.Web.App_Code
{
    public static class ServiciosGUI
    {
        private static ServCTG sctg = new ServCTG();


        public static IEnumerable<CtgDTO> GetTipoAjuste(int idPais, int idCtg = 0)
        {
            return sctg.GetTipoAjuste(idPais, idCtg);
        }

        public static IEnumerable<proformaDatos> GetDtosPrfm(int Tcom, String codCliente, String codObra, String codComercial, String cemexID, String valNeto, String fecha, String nomProdAdd, String ivaProdAdd)//johanProforma
        {
            return sctg.GetDtosPrfm(Tcom,  codCliente,  codObra,  codComercial,  cemexID,  valNeto,  fecha,  nomProdAdd,  ivaProdAdd);
        }
               
    }

    
}
