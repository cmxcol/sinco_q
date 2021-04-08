using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_Adapter.DBCnn
{
    public class EmpDto
    {
        public Int64 IdEmp { get; set; }
        public String NEmp { get; set; }
        public Int32 IdCargo { get; set; }
        public Int64 IdEmpJE { get; set; }

        public EmpDto()
        {
            IdEmp = 0;
            NEmp = null;
            IdCargo = 0;
            IdEmpJE = 0;
        }

    }
}
