using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.MiddelWare
{
    public class SampleMiddleWare
    {

        private readonly RequestDelegate _next;

        public SampleMiddleWare(RequestDelegate next,string parameter)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            await httpContext.Response.WriteAsync("");

            await _next(httpContext);
        }
    }
}
