using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSharpTool.Core
{
    /// <summary>
    /// 常用数学操作
    /// </summary>
    /// <remarks>利用Linq Math包进行 Min Max Avg Sum Sin Cos Power Sqrt计算</remarks>
    public class MathService
    {
        /// <summary>
        /// 返回二进制字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string NumberToBinary(long number)
        {
            return Convert.ToString(number,2);
        }

        /// <summary>
        /// 返回十六进制字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string NumberToHex(long number)
        {
            return number.ToString("X2");
        }

        /// <summary>
        /// 阶乘
        /// </summary>
        /// <returns></returns>
        public long Factorial(int end)
        {
            long res = 1;
            int temp = 1;

            while (temp <= end)
            {
                res = res * temp;
                temp++;
            }

            return res;
        }
    }
}
