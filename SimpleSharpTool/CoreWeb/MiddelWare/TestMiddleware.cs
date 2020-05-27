using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreWeb.MiddelWare
{
    public class TestMiddleware:IMiddleware
    {

        //使用需要在service中先注册，然后才能注册中间件

        public async Task InvokeAsync(HttpContext content, RequestDelegate next)
        {
            await content.Response.WriteAsync("调用中间件");
        }
    }
}
