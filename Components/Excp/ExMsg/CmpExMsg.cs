using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Cnv;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Persistence.DAO.Excp.ExMsg;
using Persistence.DTO.Excp.ExMsg;
using Components.PTO.Ex.ExMsg;

namespace Components.Excp.ExMsg
{
    public class CmpExMsg:ICmpExMsg
    {
        private IExMsgDAO _tEx = null;
        private ICmpDTOtoPTO _cnv = null;

        public CmpExMsg()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            container.LoadConfiguration("Cmp");
            _cnv = new CmpDTOtoPTO();
            _tEx = container.Resolve<IExMsgDAO>();
        }

        public IEnumerable<IExMsgTPTO> TExMsg(String idTEx, int idStaRg)
        {
            return _cnv.DTOtoPTO(_tEx.TExMsg(idTEx, idStaRg));
        }

    }
}
