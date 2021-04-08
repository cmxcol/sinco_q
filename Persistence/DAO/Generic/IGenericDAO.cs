using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DAO.Generic
{
    public interface IGenericDAO<E, PK>
    {
        void Save(E entity);
        E Load(PK id);
        Boolean Exists(PK id);
        E Update(E entity);
        void Remove(PK id);
    }
}
