using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Rep.Usr
{
    public sealed class RUsrServI
    {
        private static volatile IRUsrServ instance = null;
        private static readonly object padlock = new object();

        private RUsrServI() { }

        public static IRUsrServ Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new RUsrServ();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
