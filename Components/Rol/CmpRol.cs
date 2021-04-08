using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Rol;
using Components.PTO.Rol;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Cnv;

namespace Components.Rol
{
    public class CmpRol : ICmpRol
    {
        private IRolDAO _rol = null;
        private ICmpDTOtoPTO cnv = null;

        public CmpRol()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            _rol = container.Resolve<IRolDAO>();
            cnv = new CmpDTOtoPTO();
        }
        public IEnumerable<IRolPTO> LoadAll()
        {
            return cnv.DTOtoPTO(_rol.LoadAll());
        }
        public IEnumerable<IRolPTO> LoadAsig(Int32 idRol)
        {
            return cnv.DTOtoPTO(_rol.LoadAsig(idRol));
        }
    }
}
