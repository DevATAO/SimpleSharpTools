using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleSharpTool
{
    public class ToolServiceManager
    {
        public static Dictionary<string, IService> ServiceDict = new Dictionary<string, IService>();

        public static void RegisterService(string serviceName , IService service)
        {
            ServiceDict.Add(serviceName, service);
        }

        public static void UnloadService(string serviceName)
        {
            ServiceDict.Remove(serviceName);
        }

        public string GetServiceList()
        {
            return ServiceDict.Keys.ToString();
        }

        public IService GetService(string serviceName)
        {
            return ServiceDict[serviceName];
        }
    }
}
