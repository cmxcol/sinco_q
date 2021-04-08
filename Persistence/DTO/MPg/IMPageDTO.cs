using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DTO.MPg
{
    public interface IMPageDTO
    {
        Int32 IdMPage { get; set; }
        String DescMPage { get; set; }
        String Url { get; set; }
        String AppName { get; set; }
    }
}
