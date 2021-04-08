using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pg;

namespace Services.Pg
{
    public interface IPgServ
    {
        IPagePTO LoginPage();
    }
}
