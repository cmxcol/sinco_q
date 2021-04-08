using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Usr
{
    public sealed class UsrServI
    {
        private static volatile IUsrServ instance = null;
        private static readonly object padlock = new object();

        private UsrServI(){}

        public static IUsrServ Instance
        {
            get
            {                
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new UsrServ();
                        }                        
                    }
                }
                return instance;
            }
        }
    }
}
