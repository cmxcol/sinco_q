using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Ex;
using Components.PTO.Ex.ExMsg;
using Components.Excp;
using Components.Excp.ExMsg;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Services.Excp
{
    public class ExS:IExS
    {
        private ICmpEx _cmpEx = null;
        private ICmpExMsg _cmpExMsg = null;

        public ExS()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            _cmpEx = container.Resolve<ICmpEx>();
            _cmpExMsg = container.Resolve<ICmpExMsg>();
        }

        public IEnumerable<IEnumerable<IExcpPTO>> InsExArch(IEnumerable<IExcpPTO> ex, String usr, Int32 idRol, Int32 idPais)
        {            
            var res = _cmpEx.ValIcExcp(ex);
            var resP = _cmpEx.InsExcpArch(res.ToList()[0], usr, idRol, idPais);
            var ret = res.ToList()[1].ToList();
            if (resP != null)
            {
                ret.AddRange(resP.ToList()[1]);
                return new List<List<IExcpPTO>>(){resP.ToList()[0].ToList(),ret};
            }
            return null;
        }
        public IEnumerable<IExMsgTPTO> ExMsg(String idTEx, int idStaRg)
        {
            return _cmpExMsg.TExMsg(idTEx, idStaRg);
        }
    }
}
