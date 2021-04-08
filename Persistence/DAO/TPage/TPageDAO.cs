using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.TPage;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.DAO.TPage
{
    public class TPageDAO : GenericDAO<catTPage, Int32>, ITPageDAO
    {
        public TPageDAO():base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public static TPageDTO EtoDTO(catTPage tPage)
        {
            return (tPage != null ? new TPageDTO(tPage.IdTPage, tPage.DescTPage) : null);
        }
        public static IEnumerable<TPageDTO> EtoDTO(IEnumerable<catStaRg> E)
        {
            return null;
        }
    }
}
