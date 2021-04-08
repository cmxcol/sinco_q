using DTO_Adapter.CTG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.CTG
{
    public interface ICmpCTG
    {
        
            IEnumerable<CtgDTO> getCtg(Int32 idCtg, Int32 idPais, Int32 idSupBCtg, Int32 isActive);
   }
}
