using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Persistence.DBConn;
using Persistence.Utilidades;
using DTO_Adapter.SQL;

namespace Persistence.SQL
{
    public class SQLDAO : ISQLDAO
    {
        
       
        public IEnumerable<ValDTO> ConsultarCondiciones(long Obra)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IDOBRA", Obra.ToString()));
            IRdmsConnection cnn = new SqlRdmsConnection<ValDTO>(UtilSh.strCnn2, "MASTERS.GetSegmentacionCli", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (ValDTO)r).ToList();
            return res;
        }

        public String GetZTERM(long Obra,string Sector)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdObra", Obra.ToString()));
            param.Add(new SqlParameter("@Sector", Sector));
            IRdmsConnection cnn = new SqlRdmsConnection<ZtermDTO>(UtilSh.strCnn2, "MASTERS.GetZtermCli", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (ZtermDTO)r).ToList();
            return res.First().CondPago;
        }
        public string GetCentro(long Obra, string Sector)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdObra", Obra.ToString()));
            param.Add(new SqlParameter("@Sector", int.Parse(Sector)<10?"0"+ Sector :Sector));
            IRdmsConnection cnn = new SqlRdmsConnection<CentroDTO>(UtilSh.strCnn2, "MASTERS.GetCentroCli", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (CentroDTO)r).ToList();
            return res.First().Centro;
        }
        public MasterMatDTO GetMatUM(string Familia, string Norma, string Res, string TamGra, string TipGra, string Edad, string Asen, string Bomb, string TipCem, string Var)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Familia", Familia));
            param.Add(new SqlParameter("@Norma", Norma));
            param.Add(new SqlParameter("@Res", Res));
            param.Add(new SqlParameter("@TamGra", TamGra));
            param.Add(new SqlParameter("@TipGra", TipGra));
            param.Add(new SqlParameter("@Edad", Edad));
            param.Add(new SqlParameter("@Asen", Asen));
            param.Add(new SqlParameter("@Bomb", Bomb));
            param.Add(new SqlParameter("@TipCem", TipCem));
            param.Add(new SqlParameter("@Var", Var));
            IRdmsConnection cnn = new SqlRdmsConnection<MasterMatDTO>(UtilSh.strCnn2, "MASTERS.GetMatUM", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (MasterMatDTO)r).ToList();
            return res.First();
        }

        public string GetObraPriceList(string IdCliente,int IdSector)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdCliente", IdCliente));
            param.Add(new SqlParameter("@IdSector", IdSector));
            IRdmsConnection cnn = new SqlRdmsConnection<MasterMatDTO>(UtilSh.strCnn2, "MASTERS.GetVolObra", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (string)r).ToList();
            return res.First();
        }
    }
}
