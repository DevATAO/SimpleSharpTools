using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreWeb.MiddelWare
{
    public interface IMiddleware
    {
        /// <summary>
        /// Context是上下文 next是下一个中间件的引用
        /// </summary>
        /// <param name="content"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task InvokeAsync(HttpContext content,RequestDelegate next);
    }
}
