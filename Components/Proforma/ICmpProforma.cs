using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.Proforma
{
    public interface ICmpProforma
    {
        int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, long Total);
        void SaveMats(int Num, string Mat, double Vol, string Deno);
        void SaveSolici(int Num, string Name, string Cargo, string Tel, string Mail);
    }
}
