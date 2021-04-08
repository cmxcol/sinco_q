using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Instance
{
    public sealed class ServiceI<T> where T:new()
    {
        private static T instance;
        private static readonly object padlock = new object();

        private ServiceI() { }

        public static T Instance
        {
            get
            {                
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }                        
                    }
                }
                return instance;
            }
        }
    }
}
