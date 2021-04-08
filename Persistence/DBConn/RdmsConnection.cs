using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Data.Odbc;
//using DTO_Adapter.DBCnn;


namespace Persistence.DBConn
{

    public interface IRdmsConnection
    {
        IEnumerable<Object> Execute(bool hasRet, CommandType cmdType = CommandType.StoredProcedure);
    }

    public abstract class RdmsConnection<TRq, TRp> : IRdmsConnection
        where TRq : IDataParameter, IDbDataParameter
        where TRp : new()
    {
        public String ConnectionString { get; set; }
        public String StrCmd { get; set; }
        public IEnumerable<TRq> Parameter { get; set; }

        protected RdmsConnection()
        {
        }

        protected RdmsConnection(String connectionString, String strCmd, IEnumerable<TRq> parameter)
        {
            this.ConnectionString = connectionString;
            this.StrCmd = strCmd;
            this.Parameter = parameter;
        }

        protected RdmsConnection(String connectionString, String strCmd)
        {
            this.ConnectionString = connectionString;
            this.StrCmd = strCmd;
        }

        public abstract IEnumerable<Object> Execute(bool hasRet, CommandType cmdType = CommandType.StoredProcedure);
        public abstract IEnumerable<TRp> ExecuteCommand(bool hasRet, CommandType cmdType, Boolean hasReturnParameters = false);
    }

    public interface ISqlRdmsConnection<T> : IRdmsConnection
    {
        IEnumerable<T> ExecuteCommand(bool hasRet, CommandType cmdType = CommandType.StoredProcedure, Boolean hasReturnParameters = false);
    }

    public class SqlRdmsConnection<T> : RdmsConnection<SqlParameter, T>, ISqlRdmsConnection<T>
        where T : new()
    {

        public SqlRdmsConnection()
            : base()
        {

        }

        public SqlRdmsConnection(String connectionString, String strCmd, IEnumerable<SqlParameter> parameter)
            : base(connectionString, strCmd, parameter)
        {
        }

        public SqlRdmsConnection(String connectionString, String strCmd)
            : base(connectionString, strCmd)
        {
        }

        public override IEnumerable<Object> Execute(bool hasRet, CommandType cmdType = CommandType.StoredProcedure)
        {
            var lRet = new List<Object>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand { CommandType = cmdType, CommandText = StrCmd, CommandTimeout = 0, Connection = connection })
                {
                    if (Parameter != null)
                    {
                        if (Parameter.ToList().Count > 0)
                        {
                            foreach (var item in Parameter)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
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
                                            Object ins = (property.PropertyType == typeof(String) ? String.Empty : (property.PropertyType == typeof(byte[]) ? new byte[10] : Activator.CreateInstance(property.PropertyType)));
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

        public override IEnumerable<T> ExecuteCommand(bool hasRet, CommandType cmdType, Boolean hasReturnParameters = false)
        {
            var lRet = new List<T>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand { CommandType = cmdType, CommandText = StrCmd, CommandTimeout = 0, Connection = connection })
                {
                    if (Parameter != null)
                    {
                        if (Parameter.ToList().Count > 0)
                        {
                            foreach (var item in Parameter)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }
                    }
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    if (hasRet)
                    {
                        using (var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                var instance = new T();
                                for (var i = 0; i < dr.FieldCount; i++)
                                {
                                    var properties = typeof(T).GetProperties();
                                    foreach (var property in properties)
                                    {
                                        if (property.Name.ToLower() != dr.GetName(i).ToLower()) continue;
                                        Object ins = (property.PropertyType == typeof(String) ? String.Empty : (property.PropertyType == typeof(byte[]) ? new byte[10] : Activator.CreateInstance(property.PropertyType)));
                                        property.SetValue(instance, (dr.IsDBNull(i) ? ins : dr.GetValue(i)), null);
                                        break;
                                    }
                                }
                                lRet.Add(instance);
                            }
                        }
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        if (hasReturnParameters)
                        {
                            foreach (var p in cmd.Parameters)
                            {
                                lRet.Add((T)p);
                            }
                        }
                    }
                }
            }
            return lRet;
        }


        public static IEnumerable<SqlParameter> GParameters(T parametersDto)
        {
            var lm = new List<SqlParameter>();
            var properties = typeof(T).GetProperties();
            if (properties.Count() > 0 & parametersDto != null)
            {
                lm.AddRange(
                    properties.Select((p) =>
                    {
                        return new SqlParameter(
                                                (p.Name.Contains('@') ? p.Name : "@" + p.Name),
                                                p.GetValue(parametersDto, null)
                                               );
                    }
                                     )
                          );
            }
            else
            {
                lm = null;
            }
            return lm;
        }

    }
}
