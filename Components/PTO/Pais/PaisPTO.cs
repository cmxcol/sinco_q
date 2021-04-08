using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.StaRg;

namespace Components.PTO.Pais
{
    public class PaisPTO:IPaisPTO
    {
        public int IdPais { get; set; }
        public String NPais { get; set; }
        public String NPaisA { get; set; }
        public IStaRgPTO StaRg { get; set; }

        public PaisPTO()
        {
        }

        public PaisPTO(int idPais, String nPais, String nPaisA, IStaRgPTO staRg)
        {
            this.IdPais = idPais;
            this.NPais = nPais;
            this.NPaisA = nPaisA;
            this.StaRg = staRg;
        }
    }
}
