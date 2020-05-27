using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CoreWeb.IService;
using CoreWeb.MiddelWare;
using CoreWeb.SelfService;
using CoreWeb.ServiceLifeTime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace CoreWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //添加到Collection中的所有服务都会通过依赖注入提供其他代码使用

            services.AddMvc();

            //添加自定义服务 直接指明类型（不同生命周期）
            services.AddTransient<ServiceBase, ServiceA>(); // 瞬时服务
            services.AddScoped<ServiceBase, ServiceB>();    // 局部服务
            services.AddSingleton<ServiceBase, ServiceC>(); // 单例服务

            services.AddTransient<TestMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 直接在参数上传入，实现依赖注入
        ///
        /// HTTP管道中间件在这里配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ISelfService selfService,SelfAbsService selfAbsService)
        {
            if (env.IsEnvironment("Preview"))
            {
                //依赖注入进行调用
                selfService.AddFunc();
                selfAbsService.AddFunc();
            }

            //注册定义的中间件 可以传递参数
            app.UseMiddleware<SampleMiddleWare>("Para");

            app.UseMiddleware<TestMiddleware>();

            app.UseStaticFiles();

            app.Use(async (contex, next) =>
            {
                contex.Items["Middle"] = "中间处理第一环";
                await next();
            }).Use(async (contex, next) =>
            {
                contex.Items["Middle"] = "中间处理第二环";
                await next();
            }).Use(async (contex, next) =>
            {
                var res = contex.Items["Middle"].ToString();

                await contex.Response.WriteAsync(res); //管道处理完成进行返回

                await Task.CompletedTask;
            });





            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseMvc();

            //映射
            app.Map("/home", _app => { _app.Run(async contex =>
            {
                await contex.Response.WriteAsync("主页");
            }); });
        }
    }
}
