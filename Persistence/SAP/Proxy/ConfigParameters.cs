using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;

namespace Persistence.SAP.Proxy
{
    public class ConfigParameters : IDestinationConfiguration
    {
        private RfcConfigParameters parms;
        public ConfigParameters()
        {
            this.parms = new RfcConfigParameters();
        }

        public RfcConfigParameters GetParameters(String destinationName)
        {
            var config = System.Configuration.ConfigurationManager.GetSection(destinationName.Trim()) as NameValueCollection;
            if (config != null)
            {
                this.parms.Add(RfcConfigParameters.AppServerHost, config["ServerHost"]);
                this.parms.Add(RfcConfigParameters.SystemNumber, config["SystemNumber"]);
                this.parms.Add(RfcConfigParameters.SystemID, config["SystemID"]);
                this.parms.Add(RfcConfigParameters.Client, config["Client"]);
                this.parms.Add(RfcConfigParameters.Language, config["Language"]);
                this.parms.Add(RfcConfigParameters.PoolSize, config["PoolSize"]);
                this.parms.Add(RfcConfigParameters.PeakConnectionsLimit, config["PeakConnectionsLimit"]);
                this.parms.Add(RfcConfigParameters.ConnectionIdleTimeout, config["IdleTimeout"]);
                return parms;
            }
            return null;
        }

        public void Credentials(String user, String Password)
        {
            parms.Add(RfcConfigParameters.User, user);
            parms.Add(RfcConfigParameters.Password, Password);
        }

        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
    }
}
