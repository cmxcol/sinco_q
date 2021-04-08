using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using SHUtil;

namespace Persistence.DAO.Emp
{
    public class EmpDAO:IEmpDAO
    {
        public EmpDAO()
        {
            
        }

        public bool Exist(Int64 idEmp)
        {
            try
            {
                var response = ObjCtxSCAdmIns.Instance.SCAdmEntity().Fn_S_Emp(idEmp, null, 0);
                var count = response.ToList().Count;
                return count > 0 ? true : false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //public bool lEmp(Int64 idEmp, String nEmp = null, int idCargo = 0)
        //{
        //    var response = ObjCtxSCAdmIns.Instance.SCAdmEntity().Fn_S_Emp(idEmp, nEmp, idCargo);

            

        //    return true;
        //}
    }
}
