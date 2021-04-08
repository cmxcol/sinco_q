using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DAO.StaRg;
using Persistence.DTO.StaRg;
using Persistence.EntityDataModel;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;
using Persistence.DTO.Pais;

namespace Persistence.DAO.Pais
{
    public class PaisDAO : GenericDAO<catPais, Int32>, IPaisDAO
    {
        public PaisDAO()
            : base()
        {
            this.Context = ObjCtxSCAdmIns.Instance.SCAdmEntity();
        }
        public IEnumerable<IPaisDTO> LoadByState(int IdStaRg)
        {
            var pa = from p in ObjCtxSCAdmIns.Instance.SCAdmEntity().catPais
                     where p.IdStaRg == IdStaRg
                     select p;
            return EtoDTO(pa);
        }
        public new IPaisDTO Load(int id)
        {
            var e = base.Load(id);
            return e == null ? null : EtoDTO(e);
        }

        public static PaisDTO EtoDTO(catPais pais)
        {
            return (pais != null ? new PaisDTO(pais.IdPais, pais.NPais, pais.NPaisA, StaRgDAO.EtoDTO(pais.catStaRg)) : null);
        }
        public static IEnumerable<IPaisDTO> EtoDTO(IEnumerable<catPais> E)
        {
            IEnumerable<IPaisDTO> list = new List<IPaisDTO>();
            foreach (var p in E)
            {
                ((List<IPaisDTO>)list).Add(EtoDTO(p));
            }
            return list;
        }
        public static catPais DTOtoE(IPaisDTO E)
        {
            if (E != null)
            {
                catPais pais = new catPais();
                pais.IdPais  = E.IdPais;
                pais.NPais = E.NPais;
                pais.NPaisA = E.NPaisA;
                pais.IdStaRg = E.StaRg.IdStaRg;
                return pais;
            }
            else
            {
                return null;
            }
        }
    }
}
