using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Proforma;

namespace Components.Proforma
{
    public class CmpProforma
    {
        ProformaDAO _cmpProf = null;

        public CmpProforma()
        {
            _cmpProf = new ProformaDAO();
        }
        public int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, long Total)
        {
            return _cmpProf.Save(Tipo, Obra, Cliente, Comercial, Sector, Total);
        }
        public void SaveMats(int Num, string Mat, double Vol, string Deno)
        {
            _cmpProf.SaveMats(Num, Mat, Vol, Deno);
        }
        public void SaveSolici(int Num, string Name, string Cargo, string Tel, string Mail)
        {
            _cmpProf.SaveSolici(Num, Name, Cargo, Tel, Mail);
        }
    }
}
