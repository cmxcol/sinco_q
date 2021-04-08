using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Pais
{
    public sealed class PaisServI
    {
        private static volatile IPaisServ instance = null;
        private static readonly object padlock = new object();

        private PaisServI() { }

        public static IPaisServ Instance
        {
            get
            {                
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new PaisServ();
                        }                        
                    }
                }
                return instance;
            }
        }
    }
}
