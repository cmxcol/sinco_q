using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Proforma;

namespace Services.Proforma
{
    public class ServProforma:IServProforma
    {
        CmpProforma cmpProf = null;

        public ServProforma()
        {
            cmpProf = new CmpProforma();
        }
        public int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, long Total)
        {
            return cmpProf.Save(Tipo, Obra, Cliente, Comercial, Sector, Total);
        }
        public void SaveMats(int Num, string Mat, double Vol, string Deno)
        {
            cmpProf.SaveMats(Num,Mat,Vol,Deno);
        }
        public void SaveSolici(int Num, string Name, string Cargo, string Tel, string Mail)
        {
            cmpProf.SaveSolici(Num,Name,Cargo,Tel,Mail);
        }
    }
}
