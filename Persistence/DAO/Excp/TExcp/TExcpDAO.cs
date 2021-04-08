using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DAO.Generic;
using Persistence.DTO.Excp.TExcp;

namespace Persistence.DAO.Excp.TExcp
{
    public class TExcpDAO:GenericDAO<catTEx,String>,ITExcpDAO 
    {
        public TExcpDAO():base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IEnumerable<ITExcpDTO> LoadAll ()
        {
            var tex = from te in ObjCtxSCAdmIns.Instance.SCAdmEntity().catTEx
                      select te;

            return EtoDTO(tex);
        }
        public new ITExcpDTO Load(String id)
        {
            var e = base.Load(id);
            return e == null ? null : EtoDTO(e);
        }

        public static TExcpDTO EtoDTO(catTEx tEx)
        {
            return (tEx != null ? new TExcpDTO(tEx.IdTEx,tEx.NTEx) : null);
        }
        public static IEnumerable<ITExcpDTO> EtoDTO(IEnumerable<catTEx> E)
        {
            IEnumerable<ITExcpDTO> list = new List<ITExcpDTO>();
            foreach (var p in E)
            {
                ((List<ITExcpDTO>)list).Add(EtoDTO(p));
            }
            return list;
        }
        public static catTEx DTOtoE(TExcpDTO E)
        {
            if (E == null)
            {
                return null;
            }
            catTEx tEx = new catTEx();
            tEx.IdTEx = E.IdTEx;
            tEx.NTEx = E.NTEx;
            return tEx;
        }
    }
}
