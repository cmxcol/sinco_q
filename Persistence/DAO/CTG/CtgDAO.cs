using DTO_Adapter.CTG;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using Persistence.Utilidades;
using Persistence.DAO.CTG;
using Persistence.DBConn;
using DTO_Adapter.Proforma;

namespace Persistence.DAO
{
    public class CtgDAO : ICtgDao
    {

        
        public CtgDAO()
        {
           
        }


        public IEnumerable<CtgDTO> GetCtg(Int32 idCtg, Int32 idPais, Int32 idSupBCtg = 0, int isActive = -99)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdSupBCtg", idSupBCtg));
            param.Add(new SqlParameter("@IdCtg", idCtg));
            param.Add(new SqlParameter("@IdPais", idPais));
            param.Add(new SqlParameter("@IsActive", isActive));
            IRdmsConnection cnn = new SqlRdmsConnection<CtgDTO>(UtilSh.strCnn, "ctg.SPS_TbCtg", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            //return (result.Count() > 0 ? (CtgDTO)result.ToList() : null);
            var res = (from r in result
                       select (CtgDTO)r).ToList();
            return (res.Count() > 0 ? res : null);
        }


        public IEnumerable<proformaDatos> GetDtosPrfm(int Tcom, String codCliente, String codObra, String codComercial, String cemexID, String valNeto, String fecha, String nomProdAdd, String ivaProdAdd)//johanProforma
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@TCOM", Tcom));
            param.Add(new SqlParameter("@codCliente", codCliente));
            param.Add(new SqlParameter("@codObra", codObra));
            param.Add(new SqlParameter("@codComercial", codComercial));
            param.Add(new SqlParameter("@cemexID", cemexID));
            param.Add(new SqlParameter("@valNeto", valNeto));
            param.Add(new SqlParameter("@fecha", fecha));
            param.Add(new SqlParameter("@nomProdAdd", nomProdAdd));
            param.Add(new SqlParameter("@ivaProdAdd", ivaProdAdd));
            IRdmsConnection cnn = new SqlRdmsConnection<proformaDatos>(UtilSh.strCnn, "dbo.SP_Prfm_GetSetDatos", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            //return (result.Count() > 0 ? (CtgDTO)result.ToList() : null);
            var res = (from r in result
                       select (proformaDatos)r).ToList();

            return (res.Count() > 0 ? res : null);
        }

    }
}
