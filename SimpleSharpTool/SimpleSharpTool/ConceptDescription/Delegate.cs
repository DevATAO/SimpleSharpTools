using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleSharpTool.Core
{

    /// <summary>
    /// Covariance And Contravariance 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="para"></param>
    /// <returns></returns>
    /// <remarks>协变和逆变
    /// out :将子类对象赋值给父类 只能作为返回值
    /// in :将父类对象赋值给子类，只能作为参数
    /// </remarks>
    public delegate int DoSomethingGate<in T>(int para);

    public class Delegate
    {
        /// <summary>
        /// 利用事件对委托进行封装
        /// </summary>
        private event DoSomethingGate<int> _deleEvent;

        public event DoSomethingGate<int> Eve
        {
            //公开事件属性
            add { _deleEvent += value; }
            remove { _deleEvent -= value; }
        }

        public int GateMethod(int para)
        {
            return para * 2;
        }

        public int GateSecond(int para)
        {
            return para * 10;
        }

        public void BroadCast()
        {
            _deleEvent?.Invoke(100);

            Func<int, int> f = GateSecond;

            var s = $"{f(50)}";
        }

        public static void AddTwo(int A , int B)
        {
            var C = A + B;
           
        }

        Action<int, int> ADD = AddTwo;

    }
}
