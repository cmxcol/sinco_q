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
    public class BusinessConfigDao
    {

        public BusinessConfigDao()
        {

        }

        public BConfigDto GetBConfig(String idBCfg, Int32 idPais, String idTConfig, bool isActive)
        {
            String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString();
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdTConfig", idTConfig));
            param.Add(new SqlParameter("@IdBCfg", idBCfg));
            param.Add(new SqlParameter("@IdPais", idPais));
            param.Add(new SqlParameter("@IsActive", isActive));
            IRdmsConnection<BConfigDto> cnn = new SqlRdmsConnection<BConfigDto>(new SqlConnection(strCnn), "bcfg.SPS_BsConfig", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);

            return (result.Count() > 0 ? (BConfigDto)result.First() : null);
        }

        public IEnumerable<BConfigDto> GetBConfig(Int32 idPais, String idTConfig, bool isActive)
        {
            String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString();
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdTConfig", idTConfig));
            param.Add(new SqlParameter("@IdPais", idPais));
            param.Add(new SqlParameter("@IsActive", isActive));
            IRdmsConnection<BConfigDto> cnn = new SqlRdmsConnection<BConfigDto>(new SqlConnection(strCnn), "bcfg.SPS_BsConfig", param);
            var result = cnn.Execute(true, CommandType.StoredProcedure);

            var res = (from r in result
                      select (BConfigDto) r).ToList();

            return (result.Count() > 0 ? res : null);
        }

    }
}
