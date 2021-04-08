using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Cnv;
using Components.PTO.Pg;
using Components.PTO.MPg;
using Persistence.DAO.Pg;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Components.Pg
{
    public class CmpPg:ICmpPg
    {
        private IPageDAO _page = null;
        private ICmpDTOtoPTO _cnv = null;
        public CmpPg()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            _page = container.Resolve<IPageDAO>();
            _cnv = new CmpDTOtoPTO();
        }
        public IEnumerable<IPagePTO> AuthPages(String cemexId, Int32 idPais)
        {
            return _cnv.DTOtoPTO(_page.LoadAuthPages(cemexId,idPais));            
        }
        public IEnumerable<IPagePTO> PtPage(int idTPage)
        {
            return _cnv.DTOtoPTO(_page.PageByTPage(idTPage));
        }
    }
}
