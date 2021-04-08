using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.TPage;

namespace Components.PTO.Pg
{
    public interface IPagePTO
    {
        Int32 IdPage { get; set; }
        String DescPage { get; set; }
        String Url { get; set; }
        String AppName { get; set; }
        ITPagePTO TPage { get; set; }
    }
}
