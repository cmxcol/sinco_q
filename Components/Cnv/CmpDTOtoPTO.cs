using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.StaRg;
using Persistence.DAO.Pais;
using Persistence.DAO.Rol;
using Persistence.DAO.Excp;
using Persistence.DAO.Excp.TExcp;
using Persistence.DTO.Usr;
using Persistence.DTO.Pais;
using Persistence.DTO.Rol;
using Persistence.DTO.StaRg;
using Persistence.DTO.TPage;
using Persistence.DTO.Pg;
using Persistence.DTO.MPg;
using Persistence.DTO.Excp;
using Persistence.DTO.Excp.ExMsg;
using Components.PTO.Usr;
using Components.PTO.Pais;
using Components.PTO.Rol;
using Components.PTO.TPage;
using Components.PTO.Pg;
using Components.PTO.MPg;
using Components.PTO.Ex;
using Persistence.EntityDataModel;
using Components.PTO.Ex.ExMsg;


namespace Components.Cnv
{
    public class CmpDTOtoPTO : ICmpDTOtoPTO
    {
        #region DTOtoPTO Objects
        public IUsrPTO DTOtoPTO(IUsrDTO usr)
        {
            return (usr != null ? new UsrPTO(usr.CemexId, usr.NUsr, usr.EMail, DTOtoPTO(usr.Rol), DTOtoPTO(usr.Pais), DTOtoPTO(usr.StaRg)):null);
        }
        public IPaisPTO DTOtoPTO(IPaisDTO pais)
        {
            return (pais != null ? new PaisPTO(pais.IdPais, pais.NPais, pais.NPaisA, DTOtoPTO(pais.StaRg)) : null);
        }
        public IRolPTO DTOtoPTO(IRolDTO rol)
        {
            return (rol != null ? new RolPTO(rol.IdRol, rol.NRol):null);
        }
        public IStaRgPTO DTOtoPTO(IStaRgDTO staRg)
        {
            return (staRg != null ? new StaRgPTO(staRg.IdStaRg, staRg.NStaRg) : null);
        }
        public ITPagePTO DTOtoPTO(ITPageDTO tPage)
        {
            return (tPage != null ? new TPagePTO(tPage.IdTPage,tPage.DescTPage) : null);
        }
        public IPagePTO  DTOtoPTO(IPageDTO page)
        {
            return (page != null ? new PagePTO(page.IdPage, page.DescPage, page.Url, page.AppName, DTOtoPTO(page.TPage)) : null);
        }
        public IMPagePTO DTOtoPTO(IMPageDTO mPage)
        {
            return (mPage != null ? new MPagePTO(mPage.IdMPage,mPage.DescMPage,mPage.Url,mPage.AppName) : null);
        }

        public IExcpPTO DTOtoPTO(IExcpDTO ex)
        {
            return (ex != null ? new ExcpPTO(ex.IdCliente.ToString(), ex.TEx.IdTEx, ex.DtVig, ex.MsgEx, idEmp: ex.IdEmp) : null);
        }

        public IExMsgTPTO DTOtoPTO(IExMsgTDTO ex)
        {
            IExMsgTPTO exMsg = new ExMsgPTO();
            exMsg.Msg = ex.Msg;

            return (exMsg.Msg != null ? exMsg : null);
        }

        #endregion
        #region DTOtoPTO IEnumerable<Object>

        public IEnumerable<IPaisPTO> DTOtoPTO(IEnumerable<IPaisDTO> pais)
        {
            IEnumerable<IPaisPTO> lPais = new List<IPaisPTO>();
            foreach (var p in pais)
            {
                ((List<IPaisPTO>)lPais).Add(DTOtoPTO(p));
            }
            return lPais;
        }
        public IEnumerable<IRolPTO> DTOtoPTO(IEnumerable<IRolDTO> rol)
        {
            IEnumerable<IRolPTO> list = new List<IRolPTO>();
            foreach (var p in rol)
            {
                ((List<IRolPTO>)list).Add(DTOtoPTO(p));
            }
            return list;
        }
        public IEnumerable<IStaRgPTO> DTOtoPTO(IEnumerable<IStaRgDTO> rol)
        {
            IEnumerable<IStaRgPTO> list = new List<IStaRgPTO>();
            foreach (var p in rol)
            {
                ((List<IStaRgPTO>)list).Add(DTOtoPTO(p));
            }
            return list;
        }
        public IEnumerable<IPagePTO> DTOtoPTO(IEnumerable<IPageDTO> pages)
        {
            IEnumerable<IPagePTO> lPages = new List<IPagePTO>();
            foreach (var p in pages)
            {
                ((List<IPagePTO>)lPages).Add(DTOtoPTO(p));
            }
            return lPages;
        }

        public IEnumerable<IExMsgTPTO> DTOtoPTO(IEnumerable<IExMsgTDTO> exMsg)
        {
            IEnumerable<IExMsgTPTO> lexMsg = new List<IExMsgTPTO>();
            foreach (var eMsg in exMsg)
            {

                ((List<IExMsgTPTO>)lexMsg).Add(DTOtoPTO(eMsg));
            }
            return lexMsg;
        }

        #endregion
        #region PTOtoDTO Objects
        public IUsrDTO PTOtoDTO(IUsrPTO usr)
        {
            return (usr != null ? new UsrDTO(usr.CemexId, usr.NUsr, usr.EMail, PTOtoDTO(usr.Rol), PTOtoDTO(usr.Pais), PTOtoDTO(usr.StaRg)) : null);
        }
        public IPaisDTO PTOtoDTO(IPaisPTO pais)
        {
            return (pais != null ? new PaisDTO(pais.IdPais, pais.NPais, pais.NPaisA, PTOtoDTO(pais.StaRg)) : null);
        }
        public IRolDTO PTOtoDTO(IRolPTO rol)
        {
            return (rol != null ? new RolDTO(rol.IdRol, rol.NRol) : null);
        }
        public IStaRgDTO PTOtoDTO(IStaRgPTO staRg)
        {
            return (staRg != null ? new StaRgDTO(staRg.IdStaRg, staRg.NStaRg) : null);
        }
        public ITPageDTO PTOtoDTO(ITPagePTO tPage)
        {
            return (tPage != null ? new TPageDTO(tPage.IdTPage, tPage.DescTPage) : null);
        }
        public IPageDTO PTOtoDTO(IPagePTO page)
        {
            return (page != null ? new PageDTO(page.IdPage, page.DescPage, page.Url, page.AppName, PTOtoDTO(page.TPage)) : null);
        }
        public IMPageDTO PTOtoDTO(IMPagePTO mPage)
        {
            return (mPage != null ? new MPageDTO(mPage.IdMPage, mPage.DescMPage, mPage.Url, mPage.AppName) : null);
        }

        public IExcpDTO PTOtoDTO(IExcpPTO ex, Int32 idRol, Int32 idPais,Int32 idEx)
        {
            //return (ex != null ? new ExcpDTO(idEx, Int64.Parse(ex.IdCliente),TExcpDAO.EtoDTO(new TExcpDAO().Load(ex.TEx)), ex.DtVig, PaisDAO.EtoDTO(new PaisDAO().Load(idPais)), RolDAO.EtoDTO(new RolDAO().Load(idRol)), ex.MsgEx,ex.IdEmp) : null);
            return (ex != null ? new ExcpDTO(idEx, Int64.Parse(ex.IdCliente), new TExcpDAO().Load(ex.TEx), ex.DtVig, new PaisDAO().Load(idPais), new RolDAO().Load(idRol), ex.MsgEx, ex.IdEmp) : null);
        }

        #endregion
    }
}
