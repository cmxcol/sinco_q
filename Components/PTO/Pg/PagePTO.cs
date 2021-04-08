using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.TPage;

namespace Components.PTO.Pg
{
    public class PagePTO : IPagePTO
    {
        public Int32 IdPage { get; set; }
        public String DescPage { get; set; }
        public String Url { get; set; }
        public String AppName { get; set; }
        public ITPagePTO TPage { get; set; }

        public PagePTO()
        {
        }
        public PagePTO(Int32 idPage, String descPage, String url, String appName, ITPagePTO tPage)
        {
            this.IdPage = idPage;
            this.DescPage = descPage;
            this.Url = url;
            this.AppName = appName;
            this.TPage = tPage;
        }
    }
}
