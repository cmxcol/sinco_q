using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.StaRg;

namespace Components.PTO.Pais
{
    public interface IPaisPTO
    {
        int IdPais { get; set; }
        String NPais { get; set; }
        String NPaisA { get; set; }
        IStaRgPTO StaRg { get; set; }
    }
}
