using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.EntityDataModel;

namespace Persistence.EntityDataModelObjectContext.ObjectContextSCAdm
{
    public class ObjCtxSCAdm:IObjCtxSCAdm
    {
        private SCDBAdmEntities SCAdmEnt = null;
        public ObjCtxSCAdm()
        {
            SCAdmEnt = new SCDBAdmEntities();
            SCAdmEnt.CommandTimeout = 0;
        }

        public SCDBAdmEntities SCAdmEntity()
        {
            return SCAdmEnt;
        }
    }
}
