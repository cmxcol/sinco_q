using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.TPage;

namespace Persistence.DTO.Pg
{
    public class PageDTO:IPageDTO
    {
        public Int32 IdPage { get; set; }
        public String DescPage { get; set; }
        public String Url { get; set; }
        public String AppName { get; set; }
        public ITPageDTO TPage { get; set; }

        public PageDTO()
        {
        }
        public PageDTO(Int32 idPage, String descPage, String url, String appName, ITPageDTO tPage)
        {
            this.IdPage = idPage;
            this.DescPage = descPage;
            this.Url = url;
            this.AppName = appName;
            this.TPage = tPage;
        }
    }
}
