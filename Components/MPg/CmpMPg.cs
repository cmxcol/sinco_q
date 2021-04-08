using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Cnv;
using Components.PTO.MPg;
using Persistence.DAO.MPg;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Components.MPg
{
    public class CmpMPg : ICmpMPg
    {
        private IMPageDAO _mPage = null;
        private ICmpDTOtoPTO _cnv = null;

        public CmpMPg()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            _mPage = container.Resolve<IMPageDAO>();
            _cnv = new CmpDTOtoPTO();
        }

        public IMPagePTO AuthMPage(String cemexId, Int32 idPais)
        {
            return _cnv.DTOtoPTO(_mPage.LoadAuthMPage(cemexId,idPais));
        }
    }
}
