using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DTO.Excp.ExMsg;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.DAO.Excp.ExMsg
{
    public class ExMsgDAO:IExMsgDAO
    {
        public ExMsgDAO()
        {
        }

        public IEnumerable<IExMsgTDTO> TExMsg(String idTEx,int idStaRg)
        {
            var lexMsg = ObjCtxSCAdmIns.Instance.SCAdmEntity().Fn_S_ExMsg(0, "", idTEx, idStaRg);

            var res = (from m in lexMsg
                      select m.Msg).ToList();
            return DAOtoDTO(res);
        }

        public static IExMsgTDTO DAOtoDTO(String tExMsg)
        {
            IExMsgTDTO ins = new ExMsgDTO();
            ins.Msg = tExMsg;
            return (tExMsg != null ? ins : null);
        }
        public static IEnumerable<IExMsgTDTO> DAOtoDTO(IEnumerable<String> E)
        {
            IEnumerable<IExMsgTDTO> list = new List<IExMsgTDTO>();
            foreach (var p in E)
            {
                ((List<IExMsgTDTO>)list).Add(DAOtoDTO(p));
            }
            return list;
        }
    }
}
