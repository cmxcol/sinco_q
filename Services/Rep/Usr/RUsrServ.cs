using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Rep.Usr;

namespace Services.Rep.Usr
{
    public class RUsrServ : IRUsrServ
    {
        private ICmpRepUsr CmpRUsr = null;

        public RUsrServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            CmpRUsr = container.Resolve<ICmpRepUsr>();
        }
        public IEnumerable<Object> RepUsrs(Int32 tCom, Int32 idPais,Int32 idRol)
        {
            return CmpRUsr.RepUsrs(tCom, idPais, idRol);
        }
    }
}
