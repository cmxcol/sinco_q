using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Components.Logs;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Services.Logs
{
    public partial class ServLog :IServLogs
    {
        private ICmpLog _cmpLog=null;

        public ServLog()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            _cmpLog = container.Resolve<ICmpLog>();

        }
        public void SaveCLog(string CemexId, Int64 Obra, int Sector, string NombreObra, Int64 RazonSocial, string NombreSocial, string CondObra, string Comercial, Int64 Cupo, Int64 EstadoCta, Int64 Cartera, Int64 CompSap, Int64 SaldoTotal,string Msj)
        {
            _cmpLog.SaveCLog(CemexId, Obra, Sector, NombreObra, RazonSocial, NombreSocial, CondObra, Comercial, Cupo, EstadoCta, Cartera, CompSap, SaldoTotal,Msj);
        }
        public void SavePLog(string CemexId, int CodCliente, string NCliente, int CodObra, string Segmentacion, string Tipo, string Logistica, Decimal MaxDto)
        {
            _cmpLog.SavePLog(CemexId,CodCliente, NCliente, CodObra, Segmentacion, Tipo, Logistica, MaxDto);
        }

        public void SaveProgLog(string CemexId, string SAPID, string CodObra , string Mat, string Vol, string Centro, string DateOrd, bool Ajuste, string Ad1, string Ad2, string Ad3, string Bomba, string Saldo, string Costo )
        {
           _cmpLog.SaveProgLog(CemexId,SAPID,CodObra,Mat,Vol,Centro,DateOrd,Ajuste,Ad1,Ad2,Ad3,Bomba,Saldo,Costo);
        }
    }
}
