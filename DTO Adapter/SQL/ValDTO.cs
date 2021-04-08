using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_Adapter.SQL
{
    public class ValDTO
    {
        public long Idcliente { get; set; }

        public string Cliente { get; set; }
        public string IdObra { get; set; }
        public string IdSector { get; set; }   
        public string Segmentacion { get; set; }
        public string SegmentacionCli { get; set; }
        public string SegmentacionReg { get; set; }
        public string Descuento_planta { get; set; }
        
        public string TipoObra{ get; set; }
        public string Logistica { get; set; }

        public string Max { get; set; }
    }
}
