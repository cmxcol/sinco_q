using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Cnv;
using Components.PTO.Ex;
using Persistence.DAO.Excp;
using Persistence.DAO.Excp.TExcp;
using Persistence.DAO.Emp;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Components.Excp
{
    public class CmpEx : ICmpEx
    {
        private ITExcpDAO tEx = null;
        private IExCliDAO exCli = null;
        private ICmpDTOtoPTO cnv = null;
        private IEmpDAO _emp = null;
        public CmpEx()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            tEx = container.Resolve<ITExcpDAO>();
            exCli = container.Resolve<IExCliDAO>();
            _emp = container.Resolve<IEmpDAO>();
            cnv = new CmpDTOtoPTO();
        }

        public IExcpPTO ValIExcp(IExcpPTO eX)
        {
            Int64 i64;
            eX.EMsg += (Int64.TryParse(eX.IdCliente, out i64) ? null : "IdCliente.");
            DateTime dt;
            if (eX.TEx.Trim() == "PMT")
            {
                eX.EMsg += null;
                eX.DtVig = null;
            }
            else
            {
                eX.EMsg += (Int64.TryParse(eX.DtVig, out i64)
               ? (Int64.Parse(eX.DtVig) > 0 & Int64.Parse(eX.DtVig) <= 400000 ? null : "Fecha.")
               : (DateTime.TryParse(eX.DtVig, out dt)) ? (DateTime.Parse(eX.DtVig) >= DateTime.Today ? null : "Fecha.") : "Fecha.");
               eX.EMsg += (_emp.Exist(eX.IdEmp) ? null : "Id Empleado.");
            }
            eX.EMsg += (tEx.Exists(eX.TEx) ? null : "Tipo de Excepción.");
            //eX.EMsg += (_emp.Exist(eX.IdEmp) ? null : "Id Empleado.");

            if (eX.EMsg == "")
            {
                eX.EMsg = null;
            }
            else
            {
                eX.EMsg += (eX.EMsg == null ? null : "No Valido(a)");
            }
            if (Int64.TryParse(eX.DtVig, out i64))
            {
                if (Int64.Parse(eX.DtVig) > 0 & Int64.Parse(eX.DtVig) <= 400000)
                {
                    eX.DtVig = String.Format("{0:yyyy-MM-dd}", DateTime.FromOADate(Double.Parse(eX.DtVig)));
                }
            }            
            return eX;
        }

        public IEnumerable<IEnumerable<IExcpPTO>> ValIcExcp(IEnumerable<IExcpPTO> eX)
        {
            ICmpEx cmp = new CmpEx();
            var valid = new List<IExcpPTO>();
            var invalid = new List<IExcpPTO>();
            var ret = new List<List<IExcpPTO>>();
            foreach (var res in eX.Select(cmp.ValIExcp))
            {
                if (res.EMsg == null)
                {
                    valid.Add(res);
                }
                else
                {
                    invalid.Add(res);
                }
            }
            ret.Add(valid);
            ret.Add(invalid);
            return ret;
        }

        public IEnumerable<IEnumerable<IExcpPTO>> InsExcpArch(IEnumerable<IExcpPTO> eX, String usr, Int32 idRol, Int32 idPais)
        {
            var resE = new List<IExcpPTO>();
            var resOk = new List<IExcpPTO>();
            var res = new List<List<IExcpPTO>>();
            foreach (var v in eX)
            {
                String eMsg = null;
                var exDb = exCli.CEx(Int64.Parse(v.IdCliente), v.TEx,v.DtVig);                
                //eMsg = exDb != null ? exCli.UptExFni(cnv.PTOtoDTO(v, idRol, idPais, exDb.IdEx), usr) : exCli.InsExFni(cnv.PTOtoDTO(v, idRol, idPais, 0), usr);
                eMsg = exDb != null ? "Cliente con Excepción Configurada" : exCli.InsExFni(cnv.PTOtoDTO(v, idRol, idPais, 0), usr);
                if (eMsg != null)
                {
                    resE.Add(new ExcpPTO(v.IdCliente, v.TEx, v.DtVig, v.MsgEx, v.EMsg = eMsg,idEmp:v.IdEmp));
                }
                else
                {
                    resOk.Add(new ExcpPTO(v.IdCliente, v.TEx, v.DtVig, v.MsgEx, v.EMsg = eMsg, idEmp: v.IdEmp));
                }
            }
            res.Add(resOk);
            res.Add(resE);
            return res;
        }
    }
}
