using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System.Xml.Serialization;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools
{
    /// <summary>
    /// 升级版序列化工具
    /// </summary>
    public class SpiderSerializeTool
    {
        FileStream fileStream = new FileStream("A.xml", FileMode.OpenOrCreate);

        public void BinarySer()
        { 
            BinaryFormatter serializer = new BinaryFormatter();

            var content = new SerXmlModel {Name = "A"};

            //加标签直接序列化对象
            serializer.Serialize(fileStream, content);

            //从流中直接读取反序列化
            var res = (SerXmlModel)serializer.Deserialize(fileStream);
        }

        /// <summary>
        /// XML序列化专用类
        /// </summary>
        public void XmlSer<T>() where T:class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SerXmlModel));

            var content = new SerXmlModel { Name = "A" };

            serializer.Serialize(fileStream, content);

            var res = (SerXmlModel)serializer.Deserialize(fileStream);
        }
    }

    /// <summary>
    /// 当前标签只支持二进制序列化
    /// </summary>
    [Serializable]
    [XmlRoot("CollageStudent",Namespace ="www.spider.com")]
    public class SerXmlModel
    {
        public string Name { get; set; }

        [NonSerialized] public string PrivateFiles;//序列化忽略

        [XmlArray("ALLRegisterStudent")]//修改数组节点名字
        [XmlArrayItem("EachStudent")]//修改下级节点的名称
        public List<string> Students { get; set; }

        [XmlElement("StudentAge", Namespace = "www.spider.com")]
        public string Age { get; set; }


        [XmlAttribute("ImportantInfo")] //变成根的一个属性
        public string MobInfo { get; set; }
    }
}
