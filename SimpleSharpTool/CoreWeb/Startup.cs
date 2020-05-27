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
            //��ӵ�Collection�е����з��񶼻�ͨ������ע���ṩ��������ʹ��

            services.AddMvc();

            //����Զ������ ֱ��ָ�����ͣ���ͬ�������ڣ�
            services.AddTransient<ServiceBase, ServiceA>(); // ˲ʱ����
            services.AddScoped<ServiceBase, ServiceB>();    // �ֲ�����
            services.AddSingleton<ServiceBase, ServiceC>(); // ��������

            services.AddTransient<TestMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// ֱ���ڲ����ϴ��룬ʵ������ע��
        ///
        /// HTTP�ܵ��м������������
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ISelfService selfService,SelfAbsService selfAbsService)
        {
            if (env.IsEnvironment("Preview"))
            {
                //����ע����е���
                selfService.AddFunc();
                selfAbsService.AddFunc();
            }

            //ע�ᶨ����м�� ���Դ��ݲ���
            app.UseMiddleware<SampleMiddleWare>("Para");

            app.UseMiddleware<TestMiddleware>();

            app.UseStaticFiles();

            app.Use(async (contex, next) =>
            {
                contex.Items["Middle"] = "�м䴦���һ��";
                await next();
            }).Use(async (contex, next) =>
            {
                contex.Items["Middle"] = "�м䴦��ڶ���";
                await next();
            }).Use(async (contex, next) =>
            {
                var res = contex.Items["Middle"].ToString();

                await contex.Response.WriteAsync(res); //�ܵ�������ɽ��з���

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

            //ӳ��
            app.Map("/home", _app => { _app.Run(async contex =>
            {
                await contex.Response.WriteAsync("��ҳ");
            }); });
        }
    }
}
