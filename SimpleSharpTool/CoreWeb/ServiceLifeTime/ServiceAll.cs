using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.ServiceLifeTime
{
    public class ServiceAll
    {
        public ServiceAll(ServiceA A,ServiceB B,ServiceC C)
        {
            serviceA = A;
            serviceB = B;
            serviceC = C;
        }


        public ServiceA serviceA { get; }
        public ServiceB serviceB { get; }
        public ServiceC serviceC { get; }
    }
}
