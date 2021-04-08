using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.StaRg
{
    public class StaRgDTO:IStaRgDTO
    {
        public int IdStaRg { get; set; }
        public String NStaRg { get; set; }
        public StaRgDTO ()
        {
        }

        public StaRgDTO (int IdStaRg,String NStaRg)
        {
            this.IdStaRg = IdStaRg;
            this.NStaRg = NStaRg;
        }
    }
}
