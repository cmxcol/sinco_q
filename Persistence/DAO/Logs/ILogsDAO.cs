using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DAO.Logs
{
    public interface ILogsDAO
    {
       void SaveCLog(string CemexId,Int64 Obra,int Sector,string NombreObra, Int64 RazonSocial,string NombreSocial, string CondObra,string Comercial, Int64 Cupo, Int64 EstadoCta,Int64 Cartera, Int64 CompSap, Int64 SaldoTotal, string Msj);
        void SavePLog(string CemexId,int CodCliente, string NCliente, int CodObra, string Segmentacion, string Tipo, string Logistica, Decimal MaxDto);
        void SaveProgLog(string CemexId, string SAPID, string CodObra, string Mat, string Vol, string Centro, string DateOrd, bool Ajuste, string Ad1, string Ad2, string Ad3, string Bomba, string Saldo, string Costo);
    }
}
