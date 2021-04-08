using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_Adapter.WS;
using Persistence.AppWS;

namespace Persistence.DAO.Ws
{
    public class CustomerStatementDao:WsGenericDao<CustomerStatement,CsParametersDto,CsResponseDto>
    {
        public CustomerStatementDao():base()
        {

        }

        public CsResponseDto CustomerStatement(CsParametersDto param)
        {
            return base.Execute(new CustomerStatement(""), param, "", "");
        }

    }
}
