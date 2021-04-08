using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTO_Adapter.DBCnn;
using Persistence.DAO.DBcnn;

namespace Persistence.DAO.BCfg
{
    public class CargosCliDao
    {
        public CargosCliDao()
        {

        }
        public CargosCliDto GetCargos(String idObra, Int32 idOrg, Int32 idSector)
        {
            String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["cnnSAPDB"].ToString();
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdObra", idObra));
            param.Add(new SqlParameter("@IdOrg", idOrg));
            param.Add(new SqlParameter("@IdSector", idSector));
            param.Add(new SqlParameter("@Type", "SCARGO"));
            IRdmsConnection<CargosCliDto> cnn = new SqlRdmsConnection<CargosCliDto>(new SqlConnection(strCnn), "rep.SPS_ObrSC", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            return (result.Count() > 0 ? (CargosCliDto)result.First() : null);
        }

        public VolMaxDto GetVolMax(String idObra, Int32 idOrg, Int32 idSector)
        {
            String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["cnnSAPDB"].ToString();
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdObra", idObra));
            param.Add(new SqlParameter("@IdOrg", idOrg));
            param.Add(new SqlParameter("@IdSector", idSector));
            param.Add(new SqlParameter("@Type", "VOLMAX"));
            IRdmsConnection<VolMaxDto> cnn = new SqlRdmsConnection<VolMaxDto>(new SqlConnection(strCnn), "rep.SPS_ObrSC", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);
            return (result.Count() > 0 ? (VolMaxDto)result.First() : null);
        }
    }
}
