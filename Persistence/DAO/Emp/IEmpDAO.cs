using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DAO.Emp
{
    public interface IEmpDAO
    {
        bool Exist(Int64 idEmp);
    }
}
