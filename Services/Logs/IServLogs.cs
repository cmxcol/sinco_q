using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logs
{
    public interface IServLogs
    {
        void SaveCLog(string CemexId, Int64 Obra, int Sector, string NombreObra, Int64 RazonSocial, string NombreSocial, string CondObra, string Comercial, Int64 Cupo, Int64 EstadoCta, Int64 Cartera, Int64 CompSap, Int64 SaldoTotal,string Msj);
        void SavePLog(string CemexId,int CodCliente, string NCliente, int CodObra, string Segmentacion,string Tipo, string Logistica, Decimal MaxDto);
    }
}
