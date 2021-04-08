using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserAuthentication;

namespace Infrastructure.Security.Domain
{
    public class DomainServer:IDomainServer
    {
        private String _domainName;
        private String _path_LDAP;

        public DomainServer()
        {
            _domainName = String.Empty;
            _path_LDAP = String.Empty;
        }
        public DomainServer(String domainName, String Path_LDAP)
        {
            this._domainName = domainName;
            this._path_LDAP = Path_LDAP;
        }
        public String DomainName{get { return _domainName; }set { _domainName = value; } }
        public String Path_LDAP { get { return _path_LDAP; } set { _path_LDAP = value; } }
        public bool IsAuthenticated(String usr, String password)
        {
            try
            {
                var aDirV = new ActiveDirectoryValidator(_path_LDAP);
                return aDirV.IsAuthenticated(_domainName, usr, password);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
