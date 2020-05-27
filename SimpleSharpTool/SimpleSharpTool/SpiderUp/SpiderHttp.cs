using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools.SpiderUp
{
    public class SpiderHttp
    {

        public void Socket()
        {
            Socket socket = new Socket(SocketType.Stream,ProtocolType.Tcp);

            IPEndPoint iP = new IPEndPoint(IPAddress.Loopback,6666);

            socket.Bind(iP);

            socket.Listen(15);


            var client = socket.Accept();
            string msg = "Hello World!";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            client.Send(BitConverter.GetBytes(data.Length));
            client.Send(data);

            socket.Close();
            client.Close();
        }

        public void UseTcpClient()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5555);

            server.Start();

            TcpClient client = server.AcceptTcpClient();//等待连接

            using (NetworkStream stream = client.GetStream())
            {
                var buffer = new byte[256];
                stream.Read(buffer);

                string msg = Encoding.UTF8.GetString(buffer);
            }

            server.Stop();

            TcpClient clientA = new TcpClient();//向服务器发送消息

            clientA.Connect(IPAddress.Any, 5555);

            using (NetworkStream stream = clientA.GetStream())
            {
                var buffer = new byte[256];
                stream.Write(buffer);
            }

        }


        public void HttpDownload()
        {
            HttpWebRequest request = WebRequest.CreateHttp("url");

            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            Stream stream = response.GetResponseStream();

            FileStream fileStream = new FileStream("downloadFile.jpg",FileMode.Create);

            stream.CopyTo(fileStream);
        }

        /// <summary>
        /// 使用HTTPClient
        /// </summary>
        public async void HttpRequest()
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent("json");

            HttpResponseMessage responseMessage = await client.PostAsync("Url", content);

            var res = await responseMessage.Content.ReadAsStringAsync();//获取内容
        }
    }
}
