using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_Adapter.DBCnn
{
    public class CargosCliDto
    {
        public String CCargaMin { get; set; }
        public String CMinBombeo { get; set; }

        public CargosCliDto()
        {

        }
    }

    public class VolMaxDto
    {
        public Decimal VolMaxVh { get; set; }
        public String UMVolMaxVh { get; set; }

        public VolMaxDto()
        {

        }
    }
}
