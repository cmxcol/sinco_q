using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Rol
{
    public sealed class RolServI
    {
        private static volatile IRolServ instance = null;
        private static readonly object padlock = new object();

        private RolServI() { }

        public static IRolServ Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new RolServ();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
