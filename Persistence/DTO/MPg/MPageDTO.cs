using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.MPg
{
    public class MPageDTO:IMPageDTO
    {
        public Int32 IdMPage { get; set; }
        public String DescMPage { get; set; }
        public String Url { get; set; }
        public String AppName { get; set; }

        public MPageDTO()
        {
        }
        public MPageDTO(Int32 idMPage, String descMPage, String url, String appName)
        {
            this.IdMPage = idMPage;
            this.DescMPage = descMPage;
            this.Url = url;
            this.AppName = appName;
        }
    }
}
