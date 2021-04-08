using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.SQL;

namespace Persistence.SQL
{
    public interface ISQLDAO
    {
        IEnumerable<ValDTO> ConsultarCondiciones(long Obra);
        String GetZTERM(long Obra,string Sector);
        String GetCentro(long Obra, string Sector);
        MasterMatDTO GetMatUM(string Familia, string Norma, string Res, string TamGra, string TipGra, string Edad, string Asen, string Bomb, string TipCem, string Var);
        string GetObraPriceList(string IdCliente, int IdSector);
    }
}
