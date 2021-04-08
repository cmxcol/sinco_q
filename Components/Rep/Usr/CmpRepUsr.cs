using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Components.Rep.Usr
{
    public class CmpRepUsr:ICmpRepUsr
    {
        public CmpRepUsr()
        {
        }
        public IEnumerable<Object> RepUsrs(Int32 tCom, Int32 idPais,Int32 idRol)
        {
            return ObjCtxSCAdmIns.Instance.SCAdmEntity().FIRepUsr(tCom,idPais,idRol);
        }
    }
}
