using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;
using Persistence.DBConn;
using Persistence.SAP.Proxy;
using Persistence.SQL;
using Persistence.Utilidades;

namespace Persistence.DAO.Sim
{


    public class SimDAO : ISimDAO

    {
        SAPCredentialsDTO credentials = new SAPCredentialsDTO();
        private readonly object locker = new Object();
        private String CondMaterial;

        public String PrecioT(Int64 Obra, Int64 Material, float Volumen, string Centro, string CondPago, SAPCredentialsDTO credentials)
        {


            SQLDAO sql = new SQLDAO();
            IEnumerable<ValDTO> Corridor = sql.ConsultarCondiciones(Obra);


            ISAP_BAPI Base2 = new BAPI_Access { ConfigurationName = "SAPConnectionConfig" };
            Base2.Credentials(credentials.UserName, credentials.Password);
            var baseprecio2 = sql.GetObraPriceList(Obra.ToString(), 3) != "2" ? Base2.GetSAPPrecioMat(Material, Centro, CondPago) : Base2.GetSAPPrecioMat2(Material, Centro, CondPago);
            ISAP_BAPI Base3 = new BAPI_Access { ConfigurationName = "SAPConnectionConfig" };
            Base3.Credentials(credentials.UserName, credentials.Password);
            List<string> Params = new List<string>();
            if (baseprecio2.First().Contrato != "False")
            {
                Params.Add(baseprecio2.First().Contrato);
                var baseprecio3 = Base3.GetSAPPrice(Params);
                var FinalPrice = new double();
                var aux = long.Parse(baseprecio3.ElementAt(0).Contrato.Split('.')[0]) / 10 * Volumen;
                if (Corridor.Count() > 0)
                {
                    FinalPrice = aux - (aux * (long.Parse(Corridor.First().Max)) / 100);

                }

                CondMaterial = "Price Corridor";
                return FinalPrice.ToString();

            }
            else
            {
                return "Nulo";
            }

        }
        public String ValSegmentacion(String SegCli, String SegReg,Boolean planta)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IDOBRA", "14251"));
            param.Add(new SqlParameter("@SEGCLI", SegCli));
            param.Add(new SqlParameter("@SEGREG", SegReg));
            param.Add(new SqlParameter("@PLANTA", planta));
            param.Add(new SqlParameter("@TCOM", 3));
            IRdmsConnection cnn = new SqlRdmsConnection<object>(UtilSh.strCnn2, "[MASTERS].[GetSegRegional_Cliente]", param);
            var result = cnn.Execute(true);
            return ((object[])result.ToList().First()).First().ToString();
         
        }
        public String IVA(Int64 Obra, SAPCredentialsDTO credentials)
        {
            ISAP_BAPI serviceIVA = new BAPI_Access { ConfigurationName = "SAPConnectionConfig" };
            serviceIVA.Credentials(credentials.UserName, credentials.Password);
            var IVA = serviceIVA.GetSAPIVA(Obra);
            Boolean pB;
            Int64 i64;
            var response = IVA.ContainsKey("Response") ? (Boolean.TryParse(IVA["Response"].ToString(), out pB) ? Boolean.Parse(IVA["Response"].ToString()) : false) : false;
            var SAPIVA = IVA.ContainsKey("SAPIVA") ? (Int64.TryParse(IVA["SAPIVA"].ToString(), out i64) ? Int64.Parse(IVA["SAPIVA"].ToString()) : 0) : 0;
            return SAPIVA.ToString();
        }


        public String IVAMat(Int64 Material, SAPCredentialsDTO credentials)
        {
            ISAP_BAPI serviceIVA = new BAPI_Access { ConfigurationName = "SAPConnectionConfig" };
            serviceIVA.Credentials(credentials.UserName, credentials.Password);
            var IVA = serviceIVA.GetSAPIVAMat(Material);
            Boolean pB;
            Int64 i64;
            var response = IVA.ContainsKey("Response") ? (Boolean.TryParse(IVA["Response"].ToString(), out pB) ? Boolean.Parse(IVA["Response"].ToString()) : false) : false;
            var SAPIVA = IVA.ContainsKey("SAPIVAMat") ? (Int64.TryParse(IVA["SAPIVAMat"].ToString(), out i64) ? Int64.Parse(IVA["SAPIVAMat"].ToString()) : 0) : 0;
            return SAPIVA.ToString();

        }

        public CondicionLogisticaDTO CondicionLogistica(Int64 IdObra, SAPCredentialsDTO credentials)
        {
            ISAP_BAPI s = new BAPI_Access { ConfigurationName = "SAPConnectionConfig" };
            s.Credentials(credentials.UserName, credentials.Password);
            var Res = s.GetCargoLogisticoSAP(IdObra);
            //Boolean pB;
            //Int64 i64;
            //var response = IVA.ContainsKey("Response") ? (Boolean.TryParse(IVA["Response"].ToString(), out pB) ? Boolean.Parse(IVA["Response"].ToString()) : false) : false;
            //var SAPIVA = IVA.ContainsKey("SAPCondicionLogistica") ? (Int64.TryParse(IVA["SAPCondicionLogistica"].ToString(), out i64) ? Int64.Parse(IVA["SAPCondicionLogistica"].ToString()) : 0) : 0;

            CondicionLogisticaDTO dto = new CondicionLogisticaDTO();
            dto.CargoCombustible = Res.Rows[0]["CargoCombustible"].ToString();
            dto.ZonaSubUrbana = Res.Rows[0]["ZonaSuburbana"].ToString();
            return dto;
        }

        public string GetCondMat()
        {
            return CondMaterial;
        }

        //public IEnumerable<ValDTO> ValObra(long IdObra)
        //{
        //    var param = new List<SqlParameter>();
        //    param.Add(new SqlParameter("@IdObra", long.Parse(IdObra.ToString())));
        //    IRdmsConnection cnn = new SqlRdmsConnection<ValDTO>(UtilSh.strCnn2, "MASTERS.GetSegmentacionCli", param);
        //    var result = cnn.Execute(true, CommandType.StoredProcedure);
        //    var res = (from r in result
        //               select (ValDTO)r).ToList();
        //    return res;
        //}

        public IEnumerable<ValDTO> ValObra(long IdObra,Boolean planta)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdObra", IdObra.ToString()));
            if (planta)
            {
                param.Add(new SqlParameter("@planta",true));
            }
            else
            {
                param.Add(new SqlParameter("@planta", false));
            }
            
            IRdmsConnection cnn = new SqlRdmsConnection<ValDTO>(UtilSh.strCnn2, "MASTERS.GetSegmentacionCli", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            var res = (from r in result
                       select (ValDTO)r).ToList();
            return res;
        }

    }
}
