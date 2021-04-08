using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Pais
{
    public class Pais:IPais
    {
        private int _idPais;
        private String _nPais;

        public Pais() { }

        public Pais(int _idPais, String _nPais)
        {
            this._idPais = _idPais;
            this._nPais = _nPais;
        }

        public int IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }
        public String NPais
        {
            get { return _nPais; }
            set { _nPais = value; }
        }
        public static Pais GetPais(int idPais)
        {
            return new Pais();
        }
    }
}
