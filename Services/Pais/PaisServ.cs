using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Pais;
using Components.PTO.Pais;

namespace Services.Pais
{
    public class PaisServ : IPaisServ
    {
        private ICmpPais _cmpPais = null;

        public PaisServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            _cmpPais = container.Resolve<ICmpPais>();
        }
        public IEnumerable<IPaisPTO> LoadActive()
        {
            return _cmpPais.LoadActive();
        }
    }
}
