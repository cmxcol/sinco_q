using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Domain
{
    public interface IDomainServer
    {
        String DomainName { get; set; }
        String Path_LDAP { get; set; }
        bool IsAuthenticated(String usr, String password);
    }
}
