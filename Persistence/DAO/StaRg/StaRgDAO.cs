using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DTO.StaRg;


namespace Persistence.DAO.StaRg
{
    public class StaRgDAO:GenericDAO<catStaRg,Int32>,IStaRgDAO
    {
        public StaRgDAO():base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IEnumerable<IStaRgDTO> LoadAll()
        {
            var s = from sta in ObjCtxSCAdmIns.Instance.SCAdmEntity().catStaRg
                      select sta;
            return EtoDTO(s);
        }
        public static StaRgDTO EtoDTO(catStaRg staRg)
        {
            return (staRg != null ? new StaRgDTO(staRg.IdStaRg, staRg.NStaRg):null);
        }
        public static IEnumerable<IStaRgDTO> EtoDTO(IEnumerable<catStaRg> E)
        {
            IEnumerable<IStaRgDTO> list = new List<IStaRgDTO>();
            foreach (var p in E)
            {
                ((List<IStaRgDTO>)list).Add(EtoDTO(p));
            }
            return list;
        }
        public static catStaRg DTOtoE(IStaRgDTO sta)
        {
            if (sta != null)
            {
                catStaRg eSta = new catStaRg();
                eSta.IdStaRg = sta.IdStaRg;
                eSta.NStaRg = sta.NStaRg;
                return eSta;
            }
            else
            {
                return null;
            }
        }
    }
}
