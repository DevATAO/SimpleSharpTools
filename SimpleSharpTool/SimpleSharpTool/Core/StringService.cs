using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SimpleSharpTool.Core
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public class StringService
    {
        public void UsingAt()
        {
            string str = @"我喜欢""你""";//忽略转移符
            string s = "我喜欢\"你\"";//正常转义
            string ss = $@"我喜欢""你""";//内插符
        }

        public void HtmlCode()
        {
            //利用Convert来转化所有格式和String之间的变化

            WebUtility.HtmlEncode("<a>www.baidu.com</a>");
        }
    }
}
