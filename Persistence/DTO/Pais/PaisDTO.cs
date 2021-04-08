using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.StaRg;

namespace Persistence.DTO.Pais
{
    public class PaisDTO:IPaisDTO
    {
        public int IdPais { get; set; }
        public String NPais { get; set; }
        public String NPaisA { get; set; }
        public IStaRgDTO StaRg { get; set; }

        public PaisDTO()
        {
        }

        public PaisDTO(int idPais, String nPais, String nPaisA, IStaRgDTO staRg)
        {
            this.IdPais = idPais;
            this.NPais = nPais;
            this.NPaisA = nPaisA;
            this.StaRg = staRg;
        }
    }
}
