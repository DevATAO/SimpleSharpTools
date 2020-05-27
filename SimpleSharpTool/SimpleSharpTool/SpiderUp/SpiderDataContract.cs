using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools
{
    /// <summary>
    /// 使用DataContract进行序列化
    /// </summary>
    public class SpiderDataContract
    {
        FileStream fileStream = new FileStream("A.xml", FileMode.OpenOrCreate);

        /// <summary>
        /// 序列化写入文件、读取
        /// </summary>
        public void DataContractSer()
        {
            //直接序列化成XML格式
            DataContractSerializer serializer = new DataContractSerializer(typeof(Communication));

            var content = new Communication { Name = "A" };

            //序列化写入文件流 
            serializer.WriteObject(fileStream, content);

            //从文件流反序列化
            var res = serializer.ReadObject(fileStream) as Communication;
        }

        /// <summary>
        /// DATACONTRACT序列化为JSON格式
        /// </summary>
        public void DataContractJsonSer()
        {
            //直接序列化成XML格式
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Communication));

            var content = new Communication { Name = "A" };

            //序列化写入文件流 
            serializer.WriteObject(fileStream, content);

            //从文件流反序列化
            var res = serializer.ReadObject(fileStream) as SerXmlModel;
        }

    }


    /// <summary>
    /// 只要DATACONTRACT一致就可以相互转换
    /// </summary>
    /// <remarks>JSON序列化是name namespace没有作用</remarks>
    [DataContract(Namespace ="Spider",Name ="Server")]
    public class Communication
    {
        [DataMember(Name = "Amount")]
        public string Name { get; set; }

        [DataMember(Name ="Total")]
        public string Age { get; set; }
    }

    [DataContract(Namespace = "Spider", Name = "Server")]
    public class DeCommunication
    {
        [DataMember(Name = "Amount")] 
        public string FamilyName { get; set; }

        [DataMember(Name = "Total",Order=2)]
        public string Account { get; set; }

        [IgnoreDataMember]//忽略
        public string Inner { get; set; }
    }
}
