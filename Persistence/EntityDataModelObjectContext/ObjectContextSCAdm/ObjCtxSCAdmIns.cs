using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.EntityDataModelObjectContext.ObjectContextSCAdm
{
    public sealed class ObjCtxSCAdmIns
    {
        private static volatile IObjCtxSCAdm instance = null;
        private static readonly object padlock = new object();
        private ObjCtxSCAdmIns()
        {

        }
        public static IObjCtxSCAdm Instance
        {
            get
            {                
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ObjCtxSCAdm();
                        }                        
                    }
                }
                return instance;
            }
        }
    }
}
