using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Generic;
using Persistence.DTO.Excp;
using Persistence.EntityDataModel;


namespace Persistence.DAO.Excp
{
    public interface IExCliDAO:IGenericDAO<tbExCli,Int32>
    {
        String InsExFni(IExcpDTO ex, String usr);
        String UptExFni(IExcpDTO ex, String usr);
        //IExcpDTO CEx(Int64 idCliente, String tEx);
        IExcpDTO CEx(Int64 idCliente, String tEx, String dtVig);
    }
}
