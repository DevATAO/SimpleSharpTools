using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSharpTool.Core
{
    /// <summary>
    /// LinQ使用
    /// </summary>
    public class LinQService
    {
        /// <summary>
        /// 常用扩展方法
        /// </summary>
        public void Ext()
        {
            List<int> arr = new List<int>();

            //最大最小值
            arr.Max();
            arr.Min();

            //传委托
            arr.Max(a=>a.ToString()+"x");
        }

        /// <summary>
        /// 合并两个List 并且排序
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="arrA"></param>
        /// <returns></returns>
        public List<int> HeBin(List<int>arr,List<int> arrA)
        {
            return arr.Concat(arrA).ToList();
        }

        /// <summary>
        /// 去除重复元素
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<int> Dist(List<int> arr)
        {
            return arr.Distinct().ToList();
        }


        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="arrA"></param>
        /// <returns></returns>
        public List<int> Minor(List<int> arr, List<int> arrA)
        {
            return arr.Except(arrA).ToList();
        }


        public void OriLinQ(List<int> arr, List<int> arrA)
        {
            var res = from a in arr
                      join b in arrA 
                      on a.ToString() equals b.ToString()
                      orderby a descending
                      where a>5
                      select new { Value = a, SecValue=b}; // 返回匿名类型
        }
    }
}
