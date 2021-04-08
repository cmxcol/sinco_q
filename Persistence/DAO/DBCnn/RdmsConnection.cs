using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Data.Odbc;
using DTO_Adapter.DBCnn;


namespace Persistence.DAO.DBcnn
{
    public enum App
    {
        SINCO_COLOMBIA
    } ;

    public interface IRdmsConnection<TRp>
    {
        IEnumerable<Object> Execute(bool hasRet, CommandType cmdType);
    }

    public interface IRdmsConnection
    {
        IEnumerable<Object> Execute(bool hasRet, CommandType cmdType = CommandType.StoredProcedure);
    }


    public interface ISqlRdmsConnection<T> : IRdmsConnection
    {
        IEnumerable<T> ExecuteCommand(bool hasRet, CommandType cmdType = CommandType.StoredProcedure, Boolean hasReturnParameters = false);
    }



    public abstract class RdmsConnection<TCnn, TRq, TRp> : IRdmsConnection<TRp>
        where TCnn : IDbConnection, IDisposable, new()
        where TRq : IDataParameter, IDbDataParameter
        where TRp : new()
    {
        public TCnn Connection { get; set; }
        public String StrCmd { get; set; }
        public IEnumerable<TRq> Parameter { get; set; }

        protected RdmsConnection()
        {
        }

        protected RdmsConnection(TCnn connection, String strCmd, IEnumerable<TRq> parameter)
        {
            this.Connection = connection;
            this.StrCmd = strCmd;
            this.Parameter = parameter;
        }

        protected RdmsConnection(TCnn connection, String strCmd)
        {
            this.Connection = connection;
            this.StrCmd = strCmd;
        }

        public abstract IEnumerable<Object> Execute(bool hasRet, CommandType cmdType);

    }

    public class SqlRdmsConnection<T> : RdmsConnection<SqlConnection, SqlParameter, T> where T : new()
    {

        public SqlRdmsConnection()
            : base()
        {

        }

        public SqlRdmsConnection(SqlConnection connection, String strCmd, IEnumerable<SqlParameter> parameter)
            : base(connection, strCmd, parameter)
        {
        }
        
        public SqlRdmsConnection(SqlConnection connection, String strCmd)
            : base(connection, strCmd)
        {
        }

        public override IEnumerable<Object> Execute(bool hasRet, CommandType cmdType)
        {
            var lRet = new List<Object>();
            using (Connection)
            {
                using (var cmd = new SqlCommand { CommandType = cmdType, CommandText = StrCmd, CommandTimeout = 0, Connection = Connection })
                {
                    if (Parameter != null)
                    {
                        if (Parameter.ToList().Count > 0)
                        {
                            foreach (var item in Parameter)
                            {
                                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                            }
                        }
                    }
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    if (hasRet)
                    {
                        using (var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            Boolean Match = false;
                            while (dr.Read())
                            {
                                Object[] result = null;
                                var instance = new T();
                                for (var i = 0; i < dr.FieldCount; i++)
                                {
                                    var properties = typeof(T).GetProperties();

                                    if (properties.Count() > 0)
                                    {
                                        foreach (var property in properties)
                                        {
                                            if (property.Name.ToLower() != dr.GetName(i).ToLower()) continue;
                                            Object ins = (property.PropertyType == typeof(String) ? String.Empty : Activator.CreateInstance(property.PropertyType));
                                            property.SetValue(instance, (dr.IsDBNull(i) ? ins : dr.GetValue(i)), null);
                                            if (!Match)
                                            {
                                                Match = true; 
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        result = new object[dr.FieldCount];
                                        dr.GetValues(result);
                                        break;
                                    }
                                }
                                if (!Match)
                                {
                                    lRet.Add(result);
                                }
                                else
                                {
                                    lRet.Add(instance);
                                }

                            }
                        }
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return lRet;
        }
    }

}
