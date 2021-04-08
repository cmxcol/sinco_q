using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_Adapter.Proforma
{
    public class ProformDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string Comercial { get; set; }
        public string Sector { get; set; }
        public double Total { get; set; }

    }
}
