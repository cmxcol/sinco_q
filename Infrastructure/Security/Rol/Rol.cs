using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Rol
{
    public class Rol : IRol
    {
        private int _idRol;
        private String _nRol;

        public Rol() { }

        public Rol(int idRol, String nRol)
        {
            this._idRol = idRol;
            this._nRol = nRol;
        }

        public int IdRol
        {
            get { return _idRol; }
            set { _idRol = value; }
        }
        public String NRol
        {
            get { return _nRol; }
            set { _nRol = value; }
        }
    }
}
