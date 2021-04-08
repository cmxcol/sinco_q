using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DAO.Generic;
using Persistence.DAO.Pais;
using Persistence.DAO.Rol;
using Persistence.DAO.StaRg;
using Persistence.DTO.Usr;
using Persistence.DTO.Usr.Rep;
using Persistence.DTO.Rol;
using Persistence.DTO.Pais;
using Persistence;

namespace Persistence.DAO.Usr
{
    public class UsrDAO : GenericDAO<catUsr, String>, IUsrDAO
    {
        public UsrDAO()
            : base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IUsrDTO LoadUsrR(String cemexId, int idPais)
        {
            try
            {
                catUsr usrD = Load(cemexId);
                
                IPaisDAO pDAO = new PaisDAO();
                catPais paisD = PaisDAO.DTOtoE(pDAO.Load(idPais));
                var idRol = from r in ObjCtxSCAdmIns.Instance.SCAdmEntity().catRol
                            join relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol on r.IdRol equals
                                relPUR.IdRol
                            where relPUR.CemexID == cemexId & relPUR.IdPais == idPais
                            select r.IdRol;                            

                var IdSta = from s in ObjCtxSCAdmIns.Instance.SCAdmEntity().catStaRg
                            join relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol on s.IdStaRg equals relPUR.IdStaRg
                            where relPUR.CemexID == cemexId & relPUR.IdPais == idPais
                            select s.IdStaRg;

                IRolDAO rDAO = new RolDAO();
                catRol rolD = (idRol.ToList().Count > 0 ? RolDAO.DTOtoE(rDAO.Load(idRol.First())) : null);

                IStaRgDAO sDAO = new StaRgDAO();
                catStaRg staD = (IdSta.ToList().Count > 0 ? sDAO.Load(IdSta.First()) : null);

                return EtoDTO(usrD, paisD, rolD, staD);
            }
            catch (InstanceNotFoundException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Int32 isRolAsig(Int32 idRol, Int32 idRolA)
        {
            try
            {
                var q = ObjCtxSCAdmIns.Instance.SCAdmEntity().FISID_R_RJ(1, idRol, idRolA);
                return q.ToList()[0].IdR;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        public int InsertUsr(IUsrDTO usr)
        {
            try
            {
                int r = 2;
                if (usr != null)
                {
                    if (!Exists(usr.CemexId))
                    {
                        //Save(DTOtoE(usr));
                        ObjCtxSCAdmIns.Instance.SCAdmEntity().FIIU_Usr(1, usr.CemexId, usr.NUsr, usr.EMail);
                        ObjCtxSCAdmIns.Instance.SCAdmEntity().FIIU_R_URP(1, usr.CemexId, usr.Pais.IdPais, usr.Rol.IdRol,usr.StaRg.IdStaRg);
                        r = 1;
                    }
                    else
                    {
                        var q = ObjCtxSCAdmIns.Instance.SCAdmEntity().FIIU_R_URP(1, usr.CemexId, usr.Pais.IdPais, usr.Rol.IdRol, usr.StaRg.IdStaRg);
                        switch (q.ToList()[0].IdR)
                        {
                            case 0:
                                r = 0;
                                break;
                            case 1:
                                r = 1;
                                break;
                        }

                    }
                }
                return r;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateUsr(IUsrDTO usr)
        {
            int r = 2;
            if (usr != null)
            {
                if (Exists(usr.CemexId))
                {
                    //catUsr u = DTOtoE(usr);
                    //u.EntityKey = CreateEntityKey(u.CemexID);
                    //Update(u);
                    //Update(DTOtoE(usr));
                    var q = ObjCtxSCAdmIns.Instance.SCAdmEntity().FIIU_R_URP(2, usr.CemexId, usr.Pais.IdPais, usr.Rol.IdRol,usr.StaRg.IdStaRg);
                    switch (q.ToList()[0].IdR)
                    {
                        case 0:
                            r = 0;
                            break;
                        case 1:
                            var u = ObjCtxSCAdmIns.Instance.SCAdmEntity().FIIU_Usr(2, usr.CemexId, usr.NUsr, usr.EMail);
                            r = 1;
                            break;
                    }
                }
                else
                    r = 0;
            }
            return r;
        }

        //public IEnumerable<IUsrsRepDTO> RepUsrs(Int32 idPais)
        //{
        //    try
        //    {
        //        var rUsrs = from relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol
        //                    join u in ObjCtxSCAdmIns.Instance.SCAdmEntity().catUsr on relPUR.CemexID equals u.CemexID
        //                    join r in ObjCtxSCAdmIns.Instance.SCAdmEntity().catRol on relPUR.IdRol equals r.IdRol
        //                    join sta in ObjCtxSCAdmIns.Instance.SCAdmEntity().catStaRg on u.IdStaRg equals sta.IdStaRg
        //                    where relPUR.IdPais == idPais
        //                    select new { u.CemexID, u.NUsuario, u.Email, r.NRol, sta.NStaRg };

        //        return EtoDTO();
        //    }
        //    catch (InstanceNotFoundException)
        //    {
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public String DefaultPage(Int32 idRol, Int32 idPais)
        {
            try
            {
                var dPage = from relPRP in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaRolPg
                            join cP in ObjCtxSCAdmIns.Instance.SCAdmEntity().catPage on relPRP.IdPage equals cP.IdPage
                            where relPRP.IdRol == idRol & relPRP.IdPais == idPais & cP.IdTPage == 2
                            select cP.Url;

                return dPage.First().ToString();
            }
            catch (Exception )
            {

                throw;
            }
        }        
        public Boolean IsActive(String cemexId, Int32 idPais)
        {
            try
            {
                var s = from relPUR in ObjCtxSCAdmIns.Instance.SCAdmEntity().relPaUsrRol
                        join u in ObjCtxSCAdmIns.Instance.SCAdmEntity().catUsr on relPUR.CemexID equals u.CemexID
                        where relPUR.CemexID == cemexId & relPUR.IdPais == idPais
                        select relPUR.IdStaRg;

                if (Int32.Parse(s.First().ToString()) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static UsrDTO EtoDTO(catUsr usr, catPais pais, catRol rol, catStaRg sta)
        {
            return (usr != null ? new UsrDTO(usr.CemexID, usr.NUsuario, usr.Email, RolDAO.EtoDTO(rol), PaisDAO.EtoDTO(pais), StaRgDAO.EtoDTO(sta)) : null);
        }
        public catUsr DTOtoE(IUsrDTO usr)
        {
            if (usr != null)
            {
                catUsr eUsr = new catUsr();
                eUsr.CemexID = usr.CemexId;
                eUsr.NUsuario = usr.NUsr;
                eUsr.Email = usr.EMail;
                return eUsr;
            }
            else
            {
                return null;
            }
        }
    }
}
