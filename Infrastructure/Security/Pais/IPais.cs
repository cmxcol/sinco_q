using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Pais
{
    public interface IPais
    {
        int IdPais { get; set; }
        String NPais { get; set; }
    }
}
