using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.AppWS;

namespace Persistence.DAO.Ws
{
    public class WsGenericDao<TWs, TP, TR> where TWs : IWService<TP,TR>
    {
        public WsGenericDao()
        {

        }

        public virtual TR Execute(TWs ws,TP param,String usr, String password)
        {
            ws.RequestParameters(param);
            ws.Credentials(usr, password);
            return ws.Execute();
        }

    }
}
