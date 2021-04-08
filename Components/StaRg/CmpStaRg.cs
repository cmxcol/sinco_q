using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Cnv;
using Persistence.DAO.StaRg;
using Components.PTO.StaRg;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Components.StaRg
{
    public class CmpStaRg:ICmpStaRg
    {
        private IStaRgDAO _staRg = null;
        private ICmpDTOtoPTO cnv = null;

        public CmpStaRg()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            _staRg = container.Resolve<IStaRgDAO>();
            cnv = new CmpDTOtoPTO();
        }
        public IEnumerable<IStaRgPTO> LoadAll()
        {
            return cnv.DTOtoPTO(_staRg.LoadAll());
        }
    }
}
