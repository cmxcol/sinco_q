using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.MPg
{
    public class MPagePTO:IMPagePTO
    {
        public Int32 IdMPage { get; set; }
        public String DescMPage { get; set; }
        public String Url { get; set; }
        public String AppName { get; set; }

        public MPagePTO()
        {
        }
        public MPagePTO(Int32 idMPage, String descMPage, String url, String appName)
        {
            this.IdMPage = idMPage;
            this.DescMPage = descMPage;
            this.Url = url;
            this.AppName = appName;
        }
    }
}
