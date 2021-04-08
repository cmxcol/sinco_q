using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Persistence.DAO.Proforma
{
    public interface ICBloqueadosDAO
    {
        string insertClient(string idCliente, string deudor, string user, string NIdTEx, string idPais,string tipo);
        DataTable readClients(string idPais);
        string updateClient(string id, string idCliente, string deudor, string user, string NIdTEx, string IdTEx, string idPais);
        string deleteClient(string idCliente, string idPais);
        string existClient(string idCliente, string IdTEx, string idPais);
        string validateLock(string idCliente, string IdEmp, string idPais);
        DataTable findClient(string idCliente, string TCOM, string idPais);
        DataTable getTypeExceptions();
        void truncateClients(string idpais);

    }
}
