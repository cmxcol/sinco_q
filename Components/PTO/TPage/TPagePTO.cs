using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.TPage
{
    public class TPagePTO : ITPagePTO
    {
        public Int32 IdTPage { get; set; }
        public String DescTPage { get; set; }

        public TPagePTO()
        {
        }

        public TPagePTO(Int32 idTPage, String descTPage)
        {
            this.IdTPage = idTPage;
            this.DescTPage = descTPage;
        }
    }
}
