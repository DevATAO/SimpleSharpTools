using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSharpTool.Core
{
    namespace Struct
    {
        /// <summary>
        /// 结构体为值类型 
        /// </summary>
        /// <remarks>值类型默认开辟内存空间，无需显示调用New关键字</remarks>
        public struct Struct
        {
            public int Width;
            public int Length;
        }
    }

    namespace Class
    {
        /// <summary>
        /// 类型为引用类型
        /// </summary>
        /// <remarks>值传递仅传递在托管堆上的地址引用</remarks>
        public class ClassService
        {
            /// <summary>
            /// 当仅设置Get方法就表示为只读属性
            /// </summary>
            public static string Name { get; }

            private int _age;

            public ref int Age
            {
                get => ref _age;
            }
        }
    }
}
