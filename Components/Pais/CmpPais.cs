using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.PTO.Pais;
using Components.PTO.Usr;
using Components.Cnv;
using Persistence;
using Persistence.DAO.Pais;
using Persistence.DTO.Pais;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Persistence.EntityDataModel;

namespace Components.Pais
{
    public class CmpPais:ICmpPais
    {
        private IPaisDAO pais = null;
        private ICmpDTOtoPTO cnv = null;

        public CmpPais()
        {
            UnityContainer container = new UnityContainer();
            container.LoadConfiguration("Dao");
            pais = container.Resolve<IPaisDAO>();
            cnv = new CmpDTOtoPTO();
        }
        public IEnumerable<IPaisPTO> LoadActive()
        {
            return cnv.DTOtoPTO(pais.LoadByState(1));
        }
        public Boolean Exists(int idPais)
        {
            return pais.Exists(idPais);
        }
        public Boolean IsActive(int idPais)
        {
            return (pais.Load(idPais).StaRg.IdStaRg == 1 ? true : false);
        }
    }
}
