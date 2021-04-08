using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Excp;
using Persistence.DTO.Excp.TExcp;
using Persistence.DTO.Pais;
using Persistence.DAO.Pais;
using Persistence.DTO.Rol;
using Persistence.DAO.Rol;
using Persistence.DAO.Excp.TExcp;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using SHUtil;


namespace Persistence.DAO.Excp
{
    public class ExCliDAO : GenericDAO<tbExCli, Int32>, IExCliDAO
    {
        public ExCliDAO()
            : base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public String InsExFni(IExcpDTO ex, String usr)
        {
            try
            {
                String ret = null;
                DateTime dt;
                var response = ObjCtxSCAdmIns.Instance.SCAdmEntity().Fn_I_Ex(
                                                                    idCliente: ex.IdCliente,
                                                                    idTEx: ex.TEx.IdTEx,
                                                                    idPais: ex.Pais.IdPais,
                                                                    idRol: ex.Rol.IdRol,
                                                                    dtVig: (DateTime.TryParse(ex.DtVig, out dt) ? ex.DtVig : null),
                                                                    idUsr: usr,
                                                                    msgEx: ex.MsgEx,
                                                                    idEmp: ex.IdEmp);

                var lres = response.ToList();

                if (lres.Count > 0)
                {
                    switch (lres.First().Response.ToString())
                    {
                        case "0":
                            ret = "Excepción Vigente para el cliente";
                            break;
                        case "1":
                            ret = null;
                            break;
                        case "3":
                            ret = "Datos no validos";
                            break;
                        case "5":
                            ret = "Codigo de Empleado no Valido";
                            break;
                        default:
                            return null;
                    }
                }

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public String UptExFni(IExcpDTO ex, String usr)
        {
            try
            {
                var ret = "";
                DateTime dt;
                var response = ObjCtxSCAdmIns.Instance.SCAdmEntity().Fn_U_Ex(idEx: ex.IdEx,
                                                                    idCliente: ex.IdCliente,
                                                                    idTEx: ex.TEx.IdTEx,
                                                                    idPais: ex.Pais.IdPais,
                                                                    idRol: ex.Rol.IdRol,
                                                                    dtVig: (DateTime.TryParse(ex.DtVig, out dt) ? ex.DtVig : null),
                                                                    idUsr: usr,
                                                                    msgEx: ex.MsgEx,
                                                                    idEmp: ex.IdEmp);
                var lres = response.ToList();
                if (lres.Count > 0)
                {
                    switch (lres.First().Response.ToString())
                    {
                        case "0":
                            ret = "Cliente con Excepción ya configurada";
                            break;
                        case "1":
                            ret = null;
                            break;
                        case "2":
                            ret = "La Excepción pertenece al Rol " + CEx(ex.IdCliente, ex.TEx.IdTEx, ex.DtVig).Rol.NRol;
                            break;
                        case "3":
                            ret = "Error..La excepción se encuentra configurada para un pais diferente";
                            break;
                        case "4":
                            ret = "Excepción Inexistente";
                            break;
                        case "5":
                            ret = "Codigo de Empleado no Valido";
                            break;
                        default:
                            return null;
                    }
                }
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IExcpDTO CEx(Int64 idCliente, String tEx, String dtVig)
        {
            List<IParamRequest> pR = new List<IParamRequest>();
            pR.Add(new Param("@IdCliente", "IN", idCliente));
            pR.Add(new Param("@IdTEx", "IN", tEx ?? ""));
            pR.Add(new Param("@dtVig", "IN", dtVig ?? ""));
            Int64 i64;
            Int32 i32;
            var res = (List<List<Object>>)SIUSP.ExSpSqlSrv("sysc.SPS_SYSSPI", "ex.SPS_ExToUpt", pR, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ConnectionString), 3);
            var exA = (from e in res
                       select new
                       {
                           IdEx = (Int32.TryParse(e[0].ToString(), out i32) ? Int32.Parse(e[0].ToString()) : 0),
                           IdCliente = (Int64.TryParse(e[1].ToString(), out i64) ? Int64.Parse(e[1].ToString()) : 0),
                           IdTEx = e[2].ToString(),
                           dtVig = e[3].ToString(),
                           IdPais = (Int32.TryParse(e[4].ToString(), out i32) ? Int32.Parse(e[4].ToString()) : 0),
                           IdRol = (Int32.TryParse(e[5].ToString(), out i32) ? Int32.Parse(e[5].ToString()) : 0),
                           MsgEx = e[6].ToString(),
                           IdEmp = (Int64.TryParse(e[7].ToString(), out i64) ? Int64.Parse(e[7].ToString()) : 0)
                       }).ToList();

            var ex = exA.Count == 0 ? null : exA.First();
            ITExcpDAO tE = new TExcpDAO();
            IPaisDAO pais = new PaisDAO();
            IRolDAO rol = new RolDAO();
            return ex == null ? null : new ExcpDTO(ex.IdEx, ex.IdCliente, tE.Load(ex.IdTEx), ex.dtVig, pais.Load(ex.IdPais), rol.Load(ex.IdRol), ex.MsgEx, ex.IdEmp);
        }
        public static IExcpDTO EtoDTO(tbExCli ex)
        {
            return (ex != null ? new ExcpDTO(ex.IdEx, ex.IdCliente, new TExcpDTO(ex.IdTEx, null), String.Format("{0:yyyy-MM-dd}", ex.dtVig), new PaisDAO().Load(ex.IdPais), new RolDAO().Load(ex.IdRol), ex.MsgEx, (ex.IdEmp ?? 0)) : null);
        }
        public tbExCli DTOtoE(IExcpDTO ex)
        {
            if (ex != null)
            {
                tbExCli excp = new tbExCli();
                excp.IdCliente = ex.IdCliente;
                excp.IdEx = ex.IdEx;
                excp.IdPais = ex.Pais.IdPais;
                excp.IdRol = ex.Rol.IdRol;
                excp.IdTEx = ex.TEx.IdTEx;
                excp.MsgEx = ex.MsgEx;
                return excp;
            }
            else
            {
                return null;
            }
        }

    }
}
