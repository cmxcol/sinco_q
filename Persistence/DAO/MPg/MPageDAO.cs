using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.MPg;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.DAO.MPg
{
    public class MPageDAO : GenericDAO<catMPage, Int32>, IMPageDAO
    {
        public MPageDAO(): base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IMPageDTO LoadAuthMPage(String cemexId, Int32 idPais)
        {
            var mPage = from relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol
                       join relPRMP in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaRolMPg on
                           new { iP = relPUR.IdPais, iR = relPUR.IdRol } equals
                           new { iP = relPRMP.IdPais, iR = relPRMP.IdRol }
                       join mP in ObjCtxSCAdmIns.Instance.SCAdmEntity().catMPage on relPRMP.IdMPage equals mP.IdMPage
                       where relPUR.CemexID == cemexId & relPUR.IdPais == idPais
                       select mP;
            return EtoDTO(mPage.First());
        }
        public static MPageDTO EtoDTO(catMPage MPage)
        {
            return (MPage != null ? new MPageDTO(MPage.IdMPage, MPage.DescMPage, MPage.Url, MPage.AppName) : null);
        }
        public static IEnumerable<IMPageDTO> EtoDTO(IEnumerable<catMPage> E)
        {
            IEnumerable<IMPageDTO> lPage = new List<IMPageDTO>();
            foreach (var p in E)
            {
                ((List<IMPageDTO>)lPage).Add(EtoDTO(p));
            }
            return lPage;
        }
    }
}
