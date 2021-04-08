using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pg;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Pg;

namespace Services.Pg
{
    public class PgServ:IPgServ
    {
        private ICmpPg cmpPg = null;

        public PgServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            cmpPg = container.Resolve<ICmpPg>();
        }

        public IPagePTO LoginPage()
        {
            var pg = cmpPg.PtPage(1).ToList();
            return pg.Count > 0 ? pg.First() : null;
        }
    }
}
