using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools.SpiderUp
{
    public class SpiderReflection
    {
        public void LoadAss()
        {
            Assembly assembly = Assembly.LoadFrom("x.DLL");

            var assemblyClasses = assembly.GetTypes();//获取其中所有的CLASS


            foreach (var eachClass in assemblyClasses)
            {
                var members = eachClass.GetMembers(BindingFlags.NonPublic|BindingFlags.Static);//获取类中成员

                //全名包含NameSpace 
                var combineName = eachClass.FullName +"|"+ eachClass.Name;

                foreach (var member in members)
                {
                    var res = member.Name;
                }


                //调用工造函数创建实例
                var consInfo = eachClass.GetConstructor(Type.EmptyTypes);
                var instance = consInfo.Invoke(null);
                foreach (var proper in eachClass.GetProperties())
                {
                    var defaultValue = proper.GetValue(instance);//通过反射获取实例对象的值
                }

                //通过反射直接调用方法
                var methodes = eachClass.GetMethods();
                foreach (var method in methodes)
                {
                    //获取参数信息
                    ParameterInfo[] paras = method.GetParameters();

                    var paraLenth = paras.Length;//参数个数
                    var paraName = paras[0].Name;//参数名称
                    var paraType = paras[0].ParameterType;//参数类型


                    //访问静态方法不依赖实例，第一个参数为NULL .实例方法需要传递实例进去
                    var s = method.Invoke(null,new object[]{});

                    var i = method.Invoke(instance, new object[] { });
                }


                //使用Activator创建对象
                var actInstance = Activator.CreateInstance(eachClass,"","","");

                //获取特性标签
                var customerAttr = eachClass.GetCustomAttribute<MyAttribute>();
                customerAttr.RunCheck(); // 调用特性方法
            }
        }


        public sealed class MyAttribute:Attribute
        {
            public void RunCheck()
            {

            }
        }
    }
}
