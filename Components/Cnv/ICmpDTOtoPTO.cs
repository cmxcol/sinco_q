using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Ex;
using Components.PTO.Ex.ExMsg;
using Components.PTO.MPg;
using Components.PTO.Pais;
using Components.PTO.Pg;
using Components.PTO.Rol;
using Components.PTO.StaRg;
using Components.PTO.TPage;
using Components.PTO.Usr;
using Persistence.DTO.Excp;
using Persistence.DTO.Excp.ExMsg;
using Persistence.DTO.MPg;
using Persistence.DTO.Pais;
using Persistence.DTO.Pg;
using Persistence.DTO.Rol;
using Persistence.DTO.StaRg;
using Persistence.DTO.TPage;
using Persistence.DTO.Usr;

namespace Components.Cnv
{
    public interface ICmpDTOtoPTO
    {
        #region DTOtoPTO Objects
        IUsrPTO DTOtoPTO(IUsrDTO usr);
        IPaisPTO DTOtoPTO(IPaisDTO pais);
        IRolPTO DTOtoPTO(IRolDTO rol);
        IStaRgPTO DTOtoPTO(IStaRgDTO staRg);
        ITPagePTO DTOtoPTO(ITPageDTO tPage);
        IPagePTO DTOtoPTO(IPageDTO page);
        IMPagePTO DTOtoPTO(IMPageDTO mPage);
        IExcpPTO DTOtoPTO(IExcpDTO ex);
        IExMsgTPTO DTOtoPTO(IExMsgTDTO ex);
        #endregion
        #region DTOtoPTO IEnumerable<Object>
        IEnumerable<IPaisPTO> DTOtoPTO(IEnumerable<IPaisDTO> pais);
        IEnumerable<IRolPTO> DTOtoPTO(IEnumerable<IRolDTO> rol);
        IEnumerable<IStaRgPTO> DTOtoPTO(IEnumerable<IStaRgDTO> rol);
        IEnumerable<IPagePTO> DTOtoPTO(IEnumerable<IPageDTO> Pages);
        IEnumerable<IExMsgTPTO> DTOtoPTO(IEnumerable<IExMsgTDTO> exMsg);
        #endregion
        #region PTOtoDTO Objects
        IUsrDTO PTOtoDTO(IUsrPTO usr);
        IPaisDTO PTOtoDTO(IPaisPTO pais);
        IRolDTO PTOtoDTO(IRolPTO rol);
        IStaRgDTO PTOtoDTO(IStaRgPTO staRg);
        ITPageDTO PTOtoDTO(ITPagePTO tPage);
        IPageDTO PTOtoDTO(IPagePTO page);
        IMPageDTO PTOtoDTO(IMPagePTO mPage);
        IExcpDTO PTOtoDTO(IExcpPTO ex, Int32 idRol, Int32 idPais, Int32 idEx);

        #endregion
    }
}
