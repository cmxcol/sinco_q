using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.TPage;

namespace Persistence.DTO.Pg
{
    public interface IPageDTO
    {
        int IdPage { get; set; }
        String DescPage { get; set; }
        String Url { get; set; }
        String AppName { get; set; }
        ITPageDTO TPage { get; set; }
    }
}
