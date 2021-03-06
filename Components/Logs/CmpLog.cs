using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Logs;

namespace Components.Logs
{
    public partial class CmpLog :ICmpLog
    {
        private ILogsDAO cmpLog = null;

        public CmpLog()
        {
            cmpLog = new LogsDAO();
        }
        public void SaveCLog(string CemexId, Int64 Obra, int Sector, string NombreObra, Int64 RazonSocial, string NombreSocial, string CondObra, string Comercial, Int64 Cupo, Int64 EstadoCta, Int64 Cartera, Int64 CompSap, Int64 SaldoTotal, string Msj)
        {
            cmpLog.SaveCLog(CemexId, Obra, Sector, NombreObra, RazonSocial, NombreSocial, CondObra, Comercial, Cupo, EstadoCta, Cartera, CompSap, SaldoTotal,Msj);
        }
        public void SavePLog(string CemexId,int CodCliente, string NCliente, int CodObra, string Segmentacion, string Tipo, string Logistica, Decimal MaxDto)
        {
            cmpLog.SavePLog(CemexId,CodCliente,NCliente,CodObra,Segmentacion, Tipo,Logistica,MaxDto);
        }
        public void SaveProgLog(string CemexId, string SAPID, string CodObra, string Mat, string Vol, string Centro, string DateOrd, bool Ajuste, string Ad1, string Ad2, string Ad3, string Bomba, string Saldo, string Costo)
        {
            cmpLog.SaveProgLog(CemexId, SAPID, CodObra, Mat, Vol, Centro, DateOrd, Ajuste, Ad1, Ad2, Ad3, Bomba, Saldo, Costo);
        }
    }
}
