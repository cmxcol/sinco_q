using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTO_Adapter.Proforma;
using DTO_Adapter.SQL;
using Persistence.DBConn;
using Persistence.Utilidades;

namespace Persistence.Proforma
{
    public class MasterDAO : IMasterDAO
    {
        public MasterDTO GetMaestrosByObra(long Obra)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Obra", Obra));
            IRdmsConnection cnn = new SqlRdmsConnection<MasterDTO>(UtilSh.strCnn2, "MASTERS.GetMaestrosByObra", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (MasterDTO)r).ToList();
            if (res.Count>0)//johanProforma validacion por si no encuentra la obra
            {
                return res.First();
            }else
            {
                return null;
            }
            
        }

        public List<MatbytypeDTO> GetMaterialesPorTipo(string Tipo)
        {

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Tipo", Tipo));
            IRdmsConnection cnn = new SqlRdmsConnection<MatbytypeDTO>(UtilSh.strCnn, "dbo.GetMatsByType", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            List < MatbytypeDTO > res = new List< MatbytypeDTO > ();
            foreach (MatbytypeDTO r in result)
            {
                res.Add(r);
            }

            return res;
        }
    }
}
