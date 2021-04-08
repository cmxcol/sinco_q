using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.DAO.Proforma
{
    public interface IProformaDAO
    {
        int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, long Total);
        void SaveMats(int Num, string Mat, double Vol, string Deno);
        void SaveSolici(int Num, string Name, string Cargo, string Tel, string Mail);
    }
}
