using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components.Proforma;
using System.Data;
using Components.CBloqueados;

namespace Services.CBloqueados
{
    public class ServCBloqueados:IServCBloqueados
    {
        CmpCBloqueados cmpCBlo = null;


        public ServCBloqueados()
        {
            cmpCBlo = new CmpCBloqueados();
        }


        public string insertClient(string idCliente, string deudor, string user, string NIdTEx, string idPais, string Tipo)
        {
            return cmpCBlo.insertClient(idCliente, deudor, user, NIdTEx, idPais,Tipo);
        }

        public string insertValidity(string idCliente, string vigencia, string idPais)
        {
            return cmpCBlo.insertValidity(idCliente, vigencia, idPais);
        }

        public DataTable readClients(string idPais)
        {
            return cmpCBlo.readClients(idPais);
        }

        public DataTable readValidities(string idPais)
        {
            return cmpCBlo.readValidities(idPais);
        }
        
        public string updateClient(string id, string idCliente, string deudor, string user, string NIdTEx, string IdTEx, string idPais)
        {
            return cmpCBlo.updateClient(id, idCliente, deudor, user, NIdTEx,IdTEx, idPais);
        }

        public string updateValidyty(string id, string idCliente, string vigencia, string idPais)
        {
            return cmpCBlo.updateValidyty(id, idCliente, vigencia, idPais);
        }

        public string deleteClient(string idCliente, string idPais)
        {
            return cmpCBlo.deleteClient(idCliente, idPais);
        }

        public string deleteValidity(string idCliente, string idPais)
        {
            return cmpCBlo.deleteValidity(idCliente, idPais);
        }

        public string existClient(string idCliente, string IdTEx, string idPais)
        {
            return cmpCBlo.existClient(idCliente, IdTEx, idPais);
        }

        public string validateLock(string idCliente, string IdEmp, string idPais) {
            return cmpCBlo.validateLock(idCliente, IdEmp, idPais);
        }

        public DataTable findClient(string idCliente, string TCOM, string idPais)
        {
            return cmpCBlo.findClient(idCliente,TCOM, idPais);
        }

        public DataTable findValidity(string idCliente, string TCOM, string idPais)
        {
            return cmpCBlo.findValidity(idCliente, TCOM, idPais);
        }

        public DataTable getTypeExceptions() {
            return cmpCBlo.getTypeExceptions();
        }

        public void truncateClients(string idpais) {
            cmpCBlo.truncateClients(idpais);
        }
    }

    }
