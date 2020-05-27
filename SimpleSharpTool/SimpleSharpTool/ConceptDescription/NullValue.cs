using SimpleSharpTool.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSharpTool.ConceptDescription
{
    public class NullValue
    {
        public Nullable<int> Number { get; set; }

        public NullValue()
        {
            //针对对象可以使用?判断是否为空
            var cond = new CoAndContraVariance();
            cond?.Variance();
            
            //也可以对Nullable类型使用HasValue方法
            var res = Number.HasValue;
            Number = Number ?? 10;
        }
    }
}
