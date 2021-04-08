using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.TPage
{
    public class TPageDTO:ITPageDTO
    {
        public Int32 IdTPage { get; set; }
        public String DescTPage { get; set; }

        public TPageDTO()
        {
        }

        public TPageDTO(Int32 idTPage, String descTPage)
        {
            this.IdTPage = idTPage;
            this.DescTPage = descTPage;
        }
    }
}
