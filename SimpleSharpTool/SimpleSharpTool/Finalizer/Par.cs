using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSharpTool.Finalizer
{
    public class Par:IDisposable
    {
        public virtual void Dispose()
        {
            //释放资源
        }

        ~Par()
        {
            //释放资源
        }
    }
}
