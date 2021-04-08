using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Adapter.Log
{
    public class LogDTO
    {
        public string CemexId { get; set; }
        public Int64 CodObra { get; set; }
        public int Sector { get; set; }
        public string NombreObra { get; set; }
        public Int64 RazonSocial { get; set; }
        public string NombreSocial { get; set; }
        public string CondObra { get; set; }
        public string Comercial { get; set; }
        public int Cupo { get; set; }
        public int EstadoCta { get; set; }
        public int Cartera { get; set; }
        public int CompSAP { get; set; }
        public int SaldoTotal { get; set; }

    }
}
