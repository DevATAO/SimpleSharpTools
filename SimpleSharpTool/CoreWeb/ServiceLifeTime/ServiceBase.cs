using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.ServiceLifeTime
{
    public class ServiceBase
    {
        public Guid ID { get; }

        public ServiceBase()
        {
            ID = Guid.NewGuid();
        }
    }
}
