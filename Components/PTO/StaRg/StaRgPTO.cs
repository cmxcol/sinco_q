using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.StaRg
{
    public class StaRgPTO:IStaRgPTO
    {
        public int IdStaRg { get; set; }
        public String NStaRg { get; set; }
        public StaRgPTO ()
        {
        }

        public StaRgPTO(int IdStaRg, String NStaRg)
        {
            this.IdStaRg = IdStaRg;
            this.NStaRg = NStaRg;
        }
    }
}
