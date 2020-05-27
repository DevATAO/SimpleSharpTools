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
        /// 此处是Web程序的入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Kerstrel

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIIS()
                .UseIISIntegration() //作为反向代理
                .UseEnvironment("Preview")
                //.UseStartup<Startup>() //使用StartUp

                .UseUrls("http://localhost:6500");

            //在BUILDER上操作配置服务器
            
            //自定义JSON配置文件
            
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(builder.GetSetting(WebHostDefaults.ContentRootKey));
            configurationBuilder.AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            var value = config["KEY"];

            builder.UseConfiguration(configurationBuilder.Build());

            //无StrartUp自定义congfig配置
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
                //此处获取服务的实例对象
                IWebHostEnvironment environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                environment.ApplicationName = "我的新MVC服务APP";
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
