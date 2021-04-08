using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence.DAO.Proforma;
using System.Data;

namespace Components.CBloqueados
{
    public class CmpCBloqueados
    {
        CBloqueadosDAO _cmpCBlo = null;

        public CmpCBloqueados()
        {
            _cmpCBlo = new CBloqueadosDAO();
        }


        public string insertClient(string idCliente, string deudor, string user, string NIdTEx, string idPais,string Tipo) {
            return _cmpCBlo.insertClient(idCliente, deudor, user, NIdTEx, idPais,Tipo);
        }

        public string insertValidity(string idCliente, string vigencia, string idPais)
        {
            return _cmpCBlo.insertValidity(idCliente, vigencia, idPais);
        }
        

        public DataTable readClients(string idPais) {
            return _cmpCBlo.readClients(idPais);
        }

        public DataTable readValidities(string idPais)
        {
            return _cmpCBlo.readValidities(idPais);
        }

        public string updateClient(string id, string idCliente, string deudor, string user, string NIdTEx, string IdTEx, string idPais) {
            return _cmpCBlo.updateClient(id, idCliente, deudor, user, NIdTEx, IdTEx, idPais);
        }

        public string updateValidyty(string id, string idCliente, string vigencia, string idPais)
        {
            return _cmpCBlo.updateValidyty(id, idCliente, vigencia, idPais);
        }
        

        public string deleteClient(string idCliente, string idPais) {
            return _cmpCBlo.deleteClient(idCliente, idPais);
        }

        public string deleteValidity(string idCliente, string idPais)
        {
            return _cmpCBlo.deleteValidity(idCliente, idPais);
        }


        public string existClient(string idCliente, string IdTEx, string idPais)
        {
            return _cmpCBlo.existClient(idCliente, IdTEx, idPais);
        }

        public string validateLock(string idCliente, string IdEmp, string idPais) {
            return _cmpCBlo.validateLock(idCliente, IdEmp, idPais);
        }

        public DataTable findClient(string idCliente, string TCOM, string idPais) {
            return _cmpCBlo.findClient(idCliente, TCOM, idPais);
        }
        public DataTable findValidity(string idCliente, string TCOM, string idPais)
        {
            return _cmpCBlo.findValidity(idCliente, TCOM, idPais);
        }
        
        public DataTable getTypeExceptions() {
            return _cmpCBlo.getTypeExceptions();
        }

        public void truncateClients(string idpais) {
            _cmpCBlo.truncateClients( idpais);
        }

    }
}

