using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DTO.Rol;

namespace Persistence.DAO.Rol
{
    public class RolDAO : GenericDAO<catRol, Int32>, IRolDAO
    {
        public RolDAO()
            : base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IEnumerable<IRolDTO> LoadAll()
        {
            try
            {
                var rol = from r in ObjCtxSCAdmIns.Instance.SCAdmEntity().catRol
                          select r;

                var a = rol.ToList();

                return EtoDTO(rol);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<IRolDTO> LoadAsig(Int32 idRol)
        {
            try
            {
                if (idRol == 3 | idRol == 4)
                {
                    Int32[] roles = new[] { 3, 4, 5, 6 };
                    var rAC = from r in ObjCtxSCAdmIns.Instance.SCAdmEntity().catRol
                              where (roles.Contains(r.IdRol))
                              select r;
                    return EtoDTO(rAC);
                }
                //if (idRol == 4)
                //{
                //    Int32[] roles = new[] { 1, 2, 3, 4 };
                //    var rACS = from r in ObjCtxSCAdmIns.Instance.SCAdmEntity().catRol
                //               where !(roles.Contains(r.IdRol))
                //               select r;
                //    return EtoDTO(rACS);
                //}
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public new IRolDTO Load(int id)
        {
            var r = base.Load(id);
            return r == null ? null : EtoDTO(r); 
        }

        public static RolDTO EtoDTO(catRol rol)
        {
            return (rol != null ? new RolDTO(rol.IdRol, rol.NRol) : null);
        }
        public static IEnumerable<IRolDTO> EtoDTO(IEnumerable<catRol> E)
        {
            IEnumerable<IRolDTO> list = new List<IRolDTO>();
            foreach (var p in E)
            {
                ((List<IRolDTO>)list).Add(EtoDTO(p));
            }
            return list;
        }
        public static catRol DTOtoE(IRolDTO rol)
        {
            if (rol != null)
            {
                catRol eRol = new catRol();
                eRol.IdRol = rol.IdRol;
                eRol.NRol = rol.NRol;
                return eRol;
            }
            else
            {
                return null;
            }
        }
    }
}
