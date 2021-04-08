using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Persistence.DBConn;
using DTO_Adapter.Log;
using Persistence.Utilidades;

namespace Persistence.DAO.Logs
{
    public partial class LogsDAO : ILogsDAO
    {
        public void SaveCLog(string CemexId, Int64 Obra, int Sector, string NombreObra, Int64 RazonSocial, string NombreSocial, string CondObra, string Comercial, Int64 Cupo, Int64 EstadoCta, Int64 Cartera, Int64 CompSap, Int64 SaldoTotal, string Msj)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CemexId", CemexId));
            param.Add(new SqlParameter("@CodObra", Obra));
            param.Add(new SqlParameter("@Sector", Sector));
            param.Add(new SqlParameter("@NombreObra", NombreObra));
            param.Add(new SqlParameter("@RazonSocial", RazonSocial));
            param.Add(new SqlParameter("@NombreSocial", NombreSocial));
            param.Add(new SqlParameter("@CondObra", CondObra));
            param.Add(new SqlParameter("@Comercial", Comercial));
            param.Add(new SqlParameter("@Cupo", Cupo));
            param.Add(new SqlParameter("@EstadoCta", EstadoCta));
            param.Add(new SqlParameter("@Cartera", Cartera));
            param.Add(new SqlParameter("@CompSAP", CompSap));
            param.Add(new SqlParameter("@SaldoTotal", SaldoTotal));
            param.Add(new SqlParameter("@Msj", Msj));
            IRdmsConnection cnn = new SqlRdmsConnection<LogDTO>(UtilSh.strCnn, "dbo.ConsultasLog", param);
            cnn.Execute(true, CommandType.StoredProcedure);
        }
        public void SavePLog(string CemexId, int CodCliente, string NCliente, int CodObra, string Segmentacion,  string Tipo, string Logistica, Decimal MaxDto)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CemexId", CemexId));
            param.Add(new SqlParameter("@CodCliente", CodCliente));
            param.Add(new SqlParameter("@NCliente", NCliente));
            param.Add(new SqlParameter("@CodObra", CodObra));
            param.Add(new SqlParameter("@Segmentacion", Segmentacion));
            param.Add(new SqlParameter("@Tipo", Tipo));
            param.Add(new SqlParameter("@Logistica", Logistica));
            param.Add(new SqlParameter("@MaxDto", MaxDto));
            IRdmsConnection cnn = new SqlRdmsConnection<LogDTO>(UtilSh.strCnn, "dbo.DescuentosLog", param);
            cnn.Execute(true, CommandType.StoredProcedure);
        }


        public void SaveProgLog(string CemexId, string SAPID, string CodObra, string Mat, string Vol, string Centro, string DateOrd, bool Ajuste, string Ad1, string Ad2, string Ad3, string Bomba, string Saldo, string Costo)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CemexId", CemexId));
            param.Add(new SqlParameter("@SAPID", SAPID));
            param.Add(new SqlParameter("@CodObra",CodObra));
            param.Add(new SqlParameter("@Mat", Mat));
            param.Add(new SqlParameter("@Vol", Vol));
            param.Add(new SqlParameter("@Centro", Centro));
            param.Add(new SqlParameter("@DateOrd", DateOrd));
            param.Add(new SqlParameter("@Ajuste", Ajuste));
            param.Add(new SqlParameter("@Ad1", Ad1));
            param.Add(new SqlParameter("@Ad2", Ad2));
            param.Add(new SqlParameter("@Ad3", Ad3));
            param.Add(new SqlParameter("@Bomba", Bomba));
            param.Add(new SqlParameter("@Saldo",Saldo));
            param.Add(new SqlParameter("@Costo", Costo));
            IRdmsConnection cnn = new SqlRdmsConnection<LogDTO>(UtilSh.strCnn, "dbo.ProgLog", param);
            cnn.Execute(true, CommandType.StoredProcedure);
        }
    }
}
