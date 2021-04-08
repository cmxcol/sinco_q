using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.StaRg;

namespace Services.StaRg
{
    public interface IStaRgServ
    {
        IEnumerable<IStaRgPTO> LoadAll();
    }
}
