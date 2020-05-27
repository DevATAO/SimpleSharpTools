using CoreWeb.ServiceLifeTime;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.Controller
{
    public class LifeTimeController:Microsoft.AspNetCore.Mvc.Controller
    {
        public ServiceA serviceA = null;
        public ServiceB serviceB = null;
        public ServiceC serviceC = null;
        public ServiceAll serviceAll = null;
        public LifeTimeController(ServiceA A ,ServiceB B,ServiceC C,ServiceAll All)
        {
            serviceA = A;
            serviceB = B;
            serviceC = C;
            serviceAll = All;
        }


        public IActionResult GetID()
        {
            List<Guid> IDS = new List<Guid>();

            IDS.Add(serviceA.ID);
            IDS.Add(serviceB.ID);
            IDS.Add(serviceC.ID);


            IDS.Add(serviceAll.serviceA.ID);
            IDS.Add(serviceAll.serviceB.ID);
            IDS.Add(serviceAll.serviceC.ID);

            return View("~/GetId.cshtml",IDS);
        }
    }
}
