using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools.SpiderUp
{
    public class SpiderEncript
    {
        /// <summary>
        /// 单向MD5加密（哈希）
        /// </summary>
        /// <param name="msg"></param>
        public void MDFive(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);

            MD5 trans = MD5.Create();

            var res = trans.ComputeHash(data);
        }

        /// <summary>
        /// 单向SHA算法(哈希)
        /// </summary>
        public void SHAONE(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);

            SHA1 trans = SHA1.Create();

            var res = trans.ComputeHash(data);
        }

        /// <summary>
        /// 双向对称加密AES
        /// </summary>
        public void AESAsym(string msg)
        {
            //提供秘钥，初始向量两个参数进行加密

            byte[] Key = new byte[]{};

            byte[] vector = new byte[]{};


            Aes trans = Aes.Create();

            MemoryStream stream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(stream,trans.CreateEncryptor(Key, vector),CryptoStreamMode.Write);

            StreamWriter writer = new StreamWriter(cryptoStream);

            writer.Write(msg);
        }


        public void AEsDec(byte[] content)
        {
            byte[] Key = new byte[] { };

            byte[] vector = new byte[] { };

            Aes trans = Aes.Create();

            trans.GenerateKey();

            Key = trans.Key;
            trans.Mode = CipherMode.ECB;//当设置ECB模式则不需要向量


            MemoryStream stream = new MemoryStream(content);//读取内容到流中

            var decoder = trans.CreateDecryptor(Key,vector);

            CryptoStream cryptoStream = new CryptoStream(stream, decoder, CryptoStreamMode.Read);

            StreamReader streamReader = new StreamReader(cryptoStream);

            streamReader.ReadToEnd();

        }


        public void AsymRSA(Byte[] msg)
        {

            byte[] pk = new byte[] { };

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            pk = RSA.ExportCspBlob(true);

            var EnData = RSA.Encrypt(msg,RSAEncryptionPadding.Pkcs1);


            RSA.ImportCspBlob(pk);//导入秘钥

            var res = RSA.Decrypt(EnData,RSAEncryptionPadding.Pkcs1);

            string tMsg = Encoding.UTF8.GetString(res);

        }


    }
}
