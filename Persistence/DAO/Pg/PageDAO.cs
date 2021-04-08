using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Pg;
using Persistence.DAO.TPage;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.DAO.Pg
{
    public class PageDAO : GenericDAO<catPage, Int32>, IPageDAO
    {

        private static object _lock = new object();
        public PageDAO()
            : base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IEnumerable<IPageDTO> LoadAuthPages(String cemexId, Int32 idPais)
        {
            var page = from relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol
                       join relPRP in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaRolPg on
                           new {iP = relPUR.IdPais, iR = relPUR.IdRol} equals
                           new {iP = relPRP.IdPais, iR = relPRP.IdRol}
                       join p in ObjCtxSCAdmIns.Instance.SCAdmEntity().catPage on relPRP.IdPage equals p.IdPage
                       where relPUR.CemexID == cemexId & relPUR.IdPais == idPais
                       select p;
            return EtoDTO(page);
        }
        


            
        public IEnumerable<IPageDTO> PageByTPage(int idTPage)
        {
            lock (_lock)
            {
                var r = from p in ObjCtxSCAdmIns.Instance.SCAdmEntity().catPage
                    where p.IdTPage == idTPage
                    select p;
                 return EtoDTO(r.ToList());
            }
        }
        public static PageDTO EtoDTO(catPage Page)
        {
            return (Page != null ? new PageDTO(Page.IdPage, Page.DescPage, Page.Url, Page.AppName, TPageDAO.EtoDTO(Page.catTPage)) : null);
        }
        public static IEnumerable<IPageDTO> EtoDTO(IEnumerable<catPage> E)
        {
            IEnumerable<IPageDTO> lPage = new List<IPageDTO>();
            foreach (var p in E)
            {
                ((List<IPageDTO>)lPage).Add(EtoDTO(p));
            }
            return lPage;
        }
    }
}
