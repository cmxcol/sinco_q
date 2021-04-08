using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Components.Usr;
using Components.PTO.Usr;


namespace Services.Usr
{
    public class UsrServ : IUsrServ
    {
        private ICmpUsr CmpUsr = null;

        public UsrServ()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Cmp");
            CmpUsr = container.Resolve<ICmpUsr>();
        }
            public IUsrPTO UsrData(String cemexId,int idPais)
            {
                return CmpUsr.UsrData(cemexId,idPais);
            }
            public IUsrPTO LoadUsrAsig(String cemexId, Int32 idPais, Int32 idRol)
            {
                return CmpUsr.LoadUsrAsig(cemexId, idPais, idRol);
            }

            public int InsertUsr(IUsrPTO usr)
            {
                return CmpUsr.InsertUsr(usr);
            }
            public int UpdateUsr(IUsrPTO usr)
            {
                return CmpUsr.UpdateUsr(usr);
            }
            public String DefaultPage(Int32 idRol, Int32 idPais)
            {
                return CmpUsr.DefaultPage(idRol, idPais);
            }
        
    }
}
