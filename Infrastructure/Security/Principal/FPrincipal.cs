using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Infrastructure.Security.Usuario;

namespace Infrastructure.Security.Principal
{
    public class FPrincipal : IPrincipal, IFPrincipal
    {
        private IIdentity _identity;
        //private string[] _roles;
        //private string _Perfil;
        private int _idPais;


        //public FPrincipal(IIdentity identity, string[] roles)
        //{
        //    _identity = identity;
        //    _roles = roles;
        //}

        //public FPrincipal(IIdentity identity, string[] roles, string Perfil)
        //{
        //    _identity = identity;
        //    _roles = roles;
        //    _Perfil = Perfil;
        //}

        public FPrincipal(IIdentity identity, int idPais)
        {
            _identity = identity;
            _idPais = IdPais;
        }



        //Propiedad que utilizaremos para saber si el usuario tiene o no habilitado
        //el acceso a una determinada pagina
        public bool IsPageEnabled(String pName)
        {         
            return Usr.IsPageEnabled(pName, this._identity.Name,this._idPais);
        }

        /// <summary>
        /// Propiedad con el Perfil del Usuario
        /// </summary>
        public int IdPais
        {
            get
            {
                return _idPais;
            }
            set
            {
                _idPais = value;
            }
        }
        #region IPrincipal Members

        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        public bool IsInRole(string role)
        {
            // TODO:  Add FormsPrincipal.IsInRole implementation            
            return false;
        }


        #endregion

    }
}
