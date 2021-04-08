using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTO_Adapter.Proforma;
using Persistence.DBConn;
using Persistence.Utilidades;

namespace Persistence.DAO.Proforma
{
    public class ProformaDAO
    {
        public int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, long Total)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Tipo", Tipo));
            param.Add(new SqlParameter("@Obra", Obra));
            param.Add(new SqlParameter("@Cliente", Cliente));
            param.Add(new SqlParameter("@Comercial", Comercial));
            param.Add(new SqlParameter("@Sector", Sector));
            param.Add(new SqlParameter("@Total", Total));
            IRdmsConnection cnn = new SqlRdmsConnection<ProformDTO>(UtilSh.strCnn, "Proforma.SetProform", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (ProformDTO)r).ToList();
            return res.First().Id;
        }
        public void SaveMats(int Num, string Mat, double Vol, string Deno)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Num", Num));
            param.Add(new SqlParameter("@Mat", Mat));
            param.Add(new SqlParameter("@Vol", Vol));
            param.Add(new SqlParameter("@Deno", Deno));
            IRdmsConnection cnn = new SqlRdmsConnection<ProfMatDTO>(UtilSh.strCnn, "Proforma.SetMatProform", param);
            cnn.Execute(true, CommandType.StoredProcedure);
  

        }
        public void SaveSolici(int Num, string Name, string Cargo, string Tel, string Mail)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Num", Num));
            param.Add(new SqlParameter("@Name", Name));
            param.Add(new SqlParameter("@Cargo", Cargo));
            param.Add(new SqlParameter("@Tel", Tel));
            param.Add(new SqlParameter("@Mail", Mail));
            IRdmsConnection cnn = new SqlRdmsConnection<ProfSolDTO>(UtilSh.strCnn, "Proforma.SetSoliProform", param);
            cnn.Execute(true, CommandType.StoredProcedure);
 
        }
    }
}
