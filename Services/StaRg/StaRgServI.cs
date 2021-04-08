using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.StaRg
{
    public sealed class StaRgServI
    {
                private static volatile IStaRgServ instance = null;
        private static readonly object padlock = new object();

        private StaRgServI() { }

        public static IStaRgServ Instance
        {
            get
            {                
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new StaRgServ();
                        }                        
                    }
                }
                return instance;
            }
        }
    }
}
