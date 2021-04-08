using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.StaRg;

namespace Persistence.DTO.Pais
{
    public interface IPaisDTO
    {
        int IdPais { get; set; }
        String NPais { get; set; }
        String NPaisA { get; set; }
        IStaRgDTO StaRg { get; set; }
    }
}
