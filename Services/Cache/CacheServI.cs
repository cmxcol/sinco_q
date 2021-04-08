using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Cache;

namespace Services.Cache
{
    public sealed class CacheServI
    {
        private static volatile ICacheServ instance = null;
        private static readonly object padlock = new object();

        private CacheServI() { }

        public static ICacheServ Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new CacheServ();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
