using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_Adapter.SAP
{
    public class VA01_DTO
    {
        public string CPedido { get; set; }
        public int OrgVentas { get; set; }
        public string Canal { get; set; }
        public string Sector { get; set; }
        public Int64 CodObra { get; set; }
        public Int64 Material { get; set; }
        public int Vol { get; set; }
        public String Fecha { get; set; }
    }
}
