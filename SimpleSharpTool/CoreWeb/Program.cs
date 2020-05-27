using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreWeb
{
    public class Program
    {
        /// <summary>
        /// �˴���Web��������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Kerstrel

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIIS()
                .UseIISIntegration() //��Ϊ�������
                .UseEnvironment("Preview")
                //.UseStartup<Startup>() //ʹ��StartUp

                .UseUrls("http://localhost:6500");

            //��BUILDER�ϲ������÷�����
            
            //�Զ���JSON�����ļ�
            
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(builder.GetSetting(WebHostDefaults.ContentRootKey));
            configurationBuilder.AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            var value = config["KEY"];

            builder.UseConfiguration(configurationBuilder.Build());

            //��StrartUp�Զ���congfig����
            builder.ConfigureServices(service =>
            {
                //Add Service
            });
            builder.Configure(app=>
            {
                app.Run(async context => { await context.Response.WriteAsync("Pipeline Break!"); });
            });

            var host = builder.Build();


            using(IServiceScope scope = host.Services.CreateScope())
            {
                //�˴���ȡ�����ʵ������
                IWebHostEnvironment environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                environment.ApplicationName = "�ҵ���MVC����APP";
            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
            
    }
}
