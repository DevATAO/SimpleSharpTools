using System;
using System.Collections.Generic;
using System.Text;
using System.Composition;
using System.Composition.Hosting;
using System.Reflection;

namespace SimpleSharpTool.SpiderUp
{
    public class SpiderComposition
    {
        public void Compos()
        {
            ContainerConfiguration config = new ContainerConfiguration();

            config.WithAssembly(Assembly.GetExecutingAssembly()); // 将DLL的所有的类型导入容器中

            CompositionHost container = config.CreateContainer();

            var car = container.GetExport<IMotol>("QiChe");

            var motol = container.GetExport<IMotol>("MoTuo");

            car.Name = motol.Name;


            //使用数组导入
            Accepter accepter = new Accepter();
            container.SatisfyImports(accepter);
            foreach (var motolTarget in accepter.Accept)
            {
                var s = motolTarget.Name;
            }

            //使用Lazy
            container.SatisfyImports(accepter);
            Car c = accepter.Compose.Value;
            var kvpair = accepter.Compose.Metadata;

        }

        //使用Lazy
      
    }

    public interface IMotol
    {
        string Name { get; set; }
    }

    /// <summary>
    /// 可导出类型
    /// </summary>
    [Export("QiChe", typeof(IMotol))]
    [ExportMetadata("Version",5.0)]
    public class Car:IMotol
    {
        private string _name;
        public string Name
        {
            get { return "Honda"; }
            set => _name = value;
        }
    }

    [Export("MoTuo",typeof(IMotol))]
    public class Motol:IMotol
    {
        public string Name { get; set; }
    }



    public class Accepter
    {
        //可以把类都导入到数组中
        [ImportMany]
        public List<IMotol> Accept { get; set; }

        [Import] 
        public Lazy<Car, IDictionary<string, object>> Compose { get; set; }
    }
}
