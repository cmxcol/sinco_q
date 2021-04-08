using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Usr;
using Components.Cnv;
using Persistence;
using Persistence.DAO.Usr;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Components.Usr
{
    public class CmpUsr : ICmpUsr
    {
        private IUsrDAO Usr = null;
        private ICmpDTOtoPTO cnv = null;

        public CmpUsr()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            Usr = container.Resolve<IUsrDAO>();
            cnv = new CmpDTOtoPTO();
        }
        public IUsrPTO UsrData(String cemexId,int idPais)
        {            
            return cnv.DTOtoPTO(Usr.LoadUsrR(cemexId, idPais));
        }
        public IUsrPTO LoadUsrAsig(String cemexId, Int32 idPais, Int32 idRol)
        {
            IUsrPTO usuario = new UsrPTO();
            usuario = cnv.DTOtoPTO(Usr.LoadUsrR(cemexId, idPais));
            if (usuario != null)
            {
                if (Usr.isRolAsig(idRol, usuario.Rol.IdRol) == 1)
                {
                    return usuario;
                }
                else
                    return null; 
            }
            else
                return null; 
        }
        public int InsertUsr(IUsrPTO usr)
        {
            return Usr.InsertUsr(cnv.PTOtoDTO(usr));
        }
        public int UpdateUsr(IUsrPTO usr)
        {
            return Usr.UpdateUsr(cnv.PTOtoDTO(usr));
        }
        public Boolean Exists(String cemexId)
        {
            return Usr.Exists(cemexId);
        }
        public Boolean IsActive(String cemexId, Int32 idPais)
        {
            return Usr.IsActive(cemexId,idPais);
        }
        public String DefaultPage(Int32 idRol, Int32 idPais)
        {
            return Usr.DefaultPage(idRol,idPais);
        }
    }
}
