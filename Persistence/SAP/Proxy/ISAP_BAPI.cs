using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.SAP;
using DTO_Adapter.SQL;
using System.Data;

namespace Persistence.SAP.Proxy
{
    public interface ISAP_BAPI
    {
        String ConfigurationName { get; set; }

        Dictionary<String, Object> Execute_VA01(VA01_DTO income);
        Dictionary<String, Object> GetSAPPrecio(Int64 ObraId, Int64 MaterialId, int Volumen);
        Dictionary<String, Object> GetSAPPrecioUnitario(Int64 ResponseId, Int64 MaterialId, int Volumen);
        List<ObraDTO> GetSAPPrecioMat(long Material,string Centro,string CondPago);
        List<ObraDTO> GetSAPPrecioMat2(long Material, string Centro, string CondPago);
        List<ObraDTO> GetSAPPrice(List<string> DocComercial);
        Dictionary<String, Object> GetSAPIVA(Int64 ObraId);
        Dictionary<String, Object> GetSAPIVAMat(Int64 MaterialId);
        void Credentials(String user, String password);
        Dictionary<String, Object> TestConnection();
        DataTable GetCargoLogisticoSAP(Int64 ObraId);
    }
}
