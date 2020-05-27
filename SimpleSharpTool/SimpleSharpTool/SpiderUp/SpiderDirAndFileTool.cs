using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools
{
    /// <summary>
    /// 升级版文件工具
    /// </summary>
    public class SpiderDirAndFileTool
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        public void CreateDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileBytes"></param>
        public void CreateFile(string filePath, byte[] fileBytes)
        {
            using (var stream = File.Create(filePath))
            {
                stream.Write(fileBytes, 0, fileBytes.Length);

                stream.Close();

                stream.Dispose();
            }
        }

        public void GetFiles(string dirPath)
        {
            var dirInfo = new DirectoryInfo(dirPath);

            var files = from f in dirInfo.EnumerateFiles("Com*,dll")
                orderby f.Length descending
                select (FileName: f.Name, FileSize: f.Length);
        }

        public void ReadFile(string filePath)
        {
            string[] files = File.ReadAllLines(filePath);
        }

        public void AppendContent(string filePath, string content)
        {
            File.AppendAllText(filePath, "\r\n" + content + "\r\n");
        }

        public void ClearWrite(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        public void FileDelete(string filePath)
        {
            File.Delete(filePath);
        }

        public void MSWrite()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = {230, 152, 122};

                int offset = 0;

                memoryStream.Write(buffer, offset, buffer.Length);

                //流移动当前位置
                memoryStream.Seek(5,SeekOrigin.Current);

                byte[] readBuffer = new byte[200];
                memoryStream.Read(readBuffer, offset, readBuffer.Length);
            }
        }

        public void StreamWriteFile(string filePath)
        {
            StreamWriter streamWriter = new StreamWriter(filePath);

            streamWriter.Write("Spider");

            StreamReader stream = new StreamReader(filePath);

            stream.ReadToEnd();

        }

        public void CreateZipFile(string zipPath)
        {
            FileStream zipStream = File.Create(zipPath);

            //利用文件流生成压缩文档(创建新的压缩文档)
            ZipArchive zip = new ZipArchive(zipStream,ZipArchiveMode.Create);

            //压缩文档添加压缩文件ENTRY
            ZipArchiveEntry zipArchiveEntryA = zip.CreateEntry("A.XML");

            Stream stream = zipArchiveEntryA.Open();

            StreamWriter streamWriter = new StreamWriter(stream);

            streamWriter.Write("Content");

            ZipArchiveEntry zipArchiveEntryB = zip.CreateEntry("A.XML");

            Stream streamB = zipArchiveEntryA.Open();

            StreamWriter streamWriterB = new StreamWriter(streamB);

            streamWriterB.Write("Content");

        }

        public void UnZip(string zipPath)
        {
            FileStream zipStream = File.OpenRead(zipPath);

            ZipArchive zip = new ZipArchive(zipStream);

            foreach (ZipArchiveEntry zipArchiveEntry in zip.Entries)
            {
                Stream stream = zipArchiveEntry.Open();

                FileStream fileStream = File.Create(zipArchiveEntry.Name);

                stream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        public void GZip(string inputPath,string outputPath)
        {
            FileStream inputStream = new FileStream(inputPath, FileMode.Open);

            FileStream outputStream = new FileStream(outputPath, FileMode.Create);

            GZipStream gZipStream = new GZipStream(outputStream, CompressionMode.Compress);

            inputStream.CopyTo(gZipStream);
        }

        /// <summary>
        /// 利用内存映射文件新建缓存区域
        /// </summary>
        /// <remarks>当所有进程指向该区域都被释放，则该区域会被回收。无法再访问到内存映射文件</remarks>
        public void MemoryWriteFile()
        {
            MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateNew("MemoMap",1024L);

            MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream();

            StreamWriter streamWriter = new StreamWriter(memoryMappedViewStream);

            streamWriter.Write("内存映射内容");
        }

        public void MemoryReadFile()
        {
            MemoryMappedFile memoryMappedFile = MemoryMappedFile.OpenExisting("MemoMap");

            MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream();

            StreamReader streamReader = new StreamReader(memoryMappedViewStream);

            string content = streamReader.ReadToEnd();
        }

        /// <summary>
        /// 从文件直接映射到内存
        /// </summary>
        public void FileMemory(string filePath)
        {
            MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(filePath,FileMode.OpenOrCreate,"MemoMap",1024L);

            MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream();

            BinaryWriter binaryWriter = new BinaryWriter(memoryMappedViewStream);

            binaryWriter.Write(160);
        }

        /// <summary>
        /// 管道通信
        /// </summary>
        /// <remarks>单向和双向管道 in/out</remarks>
        public void SyncPipe()
        {
            NamedPipeServerStream serverStream = new NamedPipeServerStream("SpiderPipe",PipeDirection.InOut);
            serverStream.WaitForConnection();

            StreamReader reader = new StreamReader(serverStream);

            var msg = reader.ReadToEnd();
        }

        public void ClientPipe()
        {
            NamedPipeClientStream clientStream = new NamedPipeClientStream("SpiderPipe");

            StreamWriter streamWriter = new StreamWriter(clientStream);

            streamWriter.WriteLine("消息内容");
        }
    }
}
