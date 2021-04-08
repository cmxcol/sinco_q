using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Rol;
using Components.PTO.Rol;


namespace Services.Rol
{
    public class RolServ : IRolServ
    {
        private ICmpRol CmpRol = null;

        public RolServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            CmpRol = container.Resolve<ICmpRol>();
        }
        public IEnumerable<IRolPTO> LoadAll()
        {
            return CmpRol.LoadAll();
        }
        public IEnumerable<IRolPTO> LoadAsig(Int32 idRol)
        {
            return CmpRol.LoadAsig(idRol);
        }
    }
}
