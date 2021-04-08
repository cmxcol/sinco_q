using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DTO_Adapter.CBloqueados;
using Persistence.DBConn;
using Persistence.Utilidades;

namespace Persistence.DAO.Proforma
{
    public class CBloqueadosDAO : ICBloqueadosDAO
    {
        public string insertClient(string idCliente, string deudor, string user, string NIdTEx, string idPais, string tipo)
        {

            String res = (string)ECmSpBlo("dbo.bloExCliente|@TCOM;" + 1 + "|@IdCliente;" + idCliente.Trim() + "|@Deudor;" + deudor.Trim() + "|@User;" + user + "|@NIdTEx;" + NIdTEx + "|@IdPais;" + idPais +"|@Tipo;" + tipo,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();
        }


        public string insertValidity(string idCliente, string vigencia, string idPais)
        {
            int res = (int)ECmSp("dbo.vigenciaCliNal|@TCOM;" + 1 + "|@IdCliente;" + idCliente.Trim() + "|@Vigencia;" + vigencia.Trim() + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();
        }


        public DataTable readClients(string idPais)
        {
            var clientes = (DataTable)ECmSp("dbo.bloExCliente|@TCOM;2|@IdPais;" + idPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

            return clientes;
        }

        public DataTable readValidities(string idPais)
        {
            var vigencias = (DataTable)ECmSp("dbo.vigenciaCliNal|@TCOM;2|@IdPais;" + idPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

            return vigencias;
        }


        public string updateClient(string id, string idCliente, string deudor, string user, string NIdTEx, string IdTEx, string idPais)
        {

            int res = (int)ECmSp("dbo.bloExCliente|@TCOM;" + 3 + "|@Id;" + id.Trim() + "|@IdCliente;" + idCliente.Trim() + "|@Deudor;" + deudor.Trim() + "|@User;" + user + "|@NIdTEx;" + NIdTEx + "|@IdTEx;" + IdTEx + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();
        }

        public string updateValidyty(string id, string idCliente, string vigencia, string idPais)
        {
            int res = (int)ECmSp("dbo.vigenciaCliNal|@TCOM;" + 3 + "|@Id;" + id.Trim() + "|@IdCliente;" + idCliente.Trim() + "|@Vigencia;" + vigencia.Trim() + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();
        }


        public string deleteClient(string idCliente, string idPais)
        {

            try
            {
                var res = ECmSp("dbo.bloExCliente|@TCOM;" + 4 + "|@IdCliente;" + idCliente.Trim() + "|@IdPais;" + idPais,
                       System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);

                return res.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }



        }

        public string deleteValidity(string idCliente, string idPais)
        {

            int res = (int)ECmSp("dbo.vigenciaCliNal|@TCOM;" + 4 + "|@Id;" + idCliente.Trim() + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();

        }

        public string existClient(string idCliente, string IdTEx, string idPais)
        {

            int res = (int)ECmSp("dbo.bloExCliente|@TCOM;" + 5 + "|@IdCliente;" + idCliente.Trim() + "|@IdTEx;" + IdTEx + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
            return res.ToString();

        }

        public string validateLock(string idCliente, string IdEmp, string idPais)
        {

            string res = (string)ECmSp("dbo.AdminBloSegmento|@TCOM;" + 6 + "|@IdCliente;" + idCliente.Trim() + "|@IdEmp;" + IdEmp + "|@IdPais;" + idPais,
                        System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 3);
            return res.ToString();

        }

        public DataTable findClient(string idCliente, string TCOM, string idPais)
        {
            var clientes = (DataTable)ECmSp("dbo.bloExCliente|@TCOM;" + TCOM + "|@IdCliente;" + idCliente + "|@IdPais;" + idPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

            return clientes;
        }

        public DataTable findValidity(string idCliente, string TCOM, string idPais)
        {
            var clientes = (DataTable)ECmSp("dbo.vigenciaCliNal|@TCOM;" + TCOM + "|@IdCliente;" + idCliente + "|@IdPais;" + idPais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);

            return clientes;
        }

        public DataTable getTypeExceptions()
        {
            return (DataTable)ECmSp("SPSIT_CE|@IDCON;7|@TTAB;3", System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 1);
        }

        public void truncateClients(string idpais)
        {
            ECmSp("dbo.bloExCliente|@TCOM;" + 9 + "|@IdPais;" + idpais, System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString(), 2);
        }

        public Object ECmSp(String strParam, String strCon, int tCom)
        {
            try
            {
                DataTable dt = new DataTable();
                String[] aParam = strParam.Split('|');
                int resEx = 0;
                int vInt;
                Int64 vInt64;
                using (SqlConnection cnn = new SqlConnection(strCon))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {

                        adp.SelectCommand = new SqlCommand();
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.CommandText = aParam[0].ToString();
                        adp.SelectCommand.CommandTimeout = 0;
                        adp.SelectCommand.Connection = cnn;
                        if (aParam.Length > 1)
                        {
                            for (int i = 0; i <= aParam.Length - 1; i++)
                            {
                                if (i != 0)
                                {
                                    if (int.TryParse(aParam[i].Split(';')[1].ToString(), out vInt))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), int.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else if (Int64.TryParse(aParam[i].Split(';')[1].ToString(), out vInt64))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), Int64.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), aParam[i].Split(';')[1].ToString().Trim());
                                    }
                                }
                            }
                        }
                        if (tCom == 0)
                        {
                            cnn.Open();
                            adp.SelectCommand.ExecuteNonQuery();
                            cnn.Close();
                        }
                        if (tCom == 1)
                        {
                            cnn.Open();
                            adp.Fill(dt);
                            cnn.Close();
                        }
                        else if (tCom == 2)
                        {
                            cnn.Open();
                            resEx = (int)adp.SelectCommand.ExecuteScalar();
                            cnn.Close();
                        }
                        else if (tCom == 3)
                        {
                            cnn.Open();
                            string res = (string)adp.SelectCommand.ExecuteScalar();
                            cnn.Close();
                            return res;
                        }
                    }
                }
                if (tCom == 1)
                {
                    return dt;
                }
                else if (tCom == 2)
                {
                    return resEx;
                }
                else
                {
                    return new Object();
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public String ECmSpBlo(String strParam, String strCon, int tCom)
        {
            try
            {
                DataTable dt = new DataTable();
                String[] aParam = strParam.Split('|');
                string resEx = "0";
                int vInt;
                Int64 vInt64;
                using (SqlConnection cnn = new SqlConnection(strCon))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {

                        adp.SelectCommand = new SqlCommand();
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.CommandText = aParam[0].ToString();
                        adp.SelectCommand.CommandTimeout = 0;
                        adp.SelectCommand.Connection = cnn;
                        if (aParam.Length > 1)
                        {
                            for (int i = 0; i <= aParam.Length - 1; i++)
                            {
                                if (i != 0)
                                {
                                    if (int.TryParse(aParam[i].Split(';')[1].ToString(), out vInt))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), int.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else if (Int64.TryParse(aParam[i].Split(';')[1].ToString(), out vInt64))
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), Int64.Parse(aParam[i].Split(';')[1]));
                                    }
                                    else
                                    {
                                        adp.SelectCommand.Parameters.AddWithValue(aParam[i].Split(';')[0].ToString().Trim(), aParam[i].Split(';')[1].ToString().Trim());
                                    }
                                }
                            }
                        }
                        if (tCom == 0)
                        {
                            cnn.Open();
                            adp.SelectCommand.ExecuteNonQuery();
                            cnn.Close();
                        }
                        if (tCom == 1)
                        {
                            cnn.Open();
                            adp.Fill(dt);
                            cnn.Close();
                        }
                        else if (tCom == 2)
                        {
                            cnn.Open();
                            resEx = (string)adp.SelectCommand.ExecuteScalar();
                            cnn.Close();
                        }
                        else if (tCom == 3)
                        {
                            cnn.Open();
                            string res = (string)adp.SelectCommand.ExecuteScalar();
                            cnn.Close();
                            return res;
                        }
                    }
                }
                if (tCom == 2)
                {
                    return resEx;
                }
                else
                {
                    return "111";
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
