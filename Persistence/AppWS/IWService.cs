using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.AppWS
{
    public interface IWService<TP,TR>
    {
        void Credentials(String user, String password);
        void RequestParameters(TP parameters);
        TR Execute();
    }
}
