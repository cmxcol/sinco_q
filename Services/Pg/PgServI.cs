using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Pg
{
    public sealed class PgServI
    {
        private static volatile IPgServ instance = null;
        private static readonly object padlock = new object();

        public static IPgServ Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new PgServ();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
