using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.StaRg;
using Components.PTO.StaRg;

namespace Services.StaRg
{
    public class StaRgServ : IStaRgServ
    {
        private ICmpStaRg CmpStaRg = null;

        public StaRgServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            CmpStaRg = container.Resolve<ICmpStaRg>();
        }
        public IEnumerable<IStaRgPTO> LoadAll()
        {
            return CmpStaRg.LoadAll();
        }
    }
}
