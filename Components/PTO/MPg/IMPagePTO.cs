using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.MPg
{
    public interface IMPagePTO
    {
        Int32 IdMPage { get; set; }
        String DescMPage { get; set; }
        String Url { get; set; }
        String AppName { get; set; }
    }
}
