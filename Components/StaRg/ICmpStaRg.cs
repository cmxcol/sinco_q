using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.StaRg;

namespace Components.StaRg
{
    public interface ICmpStaRg
    {
        IEnumerable<IStaRgPTO> LoadAll();
    }
}
