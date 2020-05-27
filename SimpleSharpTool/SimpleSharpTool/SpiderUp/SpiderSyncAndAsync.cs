using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools
{
    public class SpiderSyncAndAsync
    {
        delegate void ParaDelegate(string parameter);
        public void UseThread()
        {
            //当前线程休息
            Thread.Sleep(200);

            ParaDelegate dele = new ParaDelegate(RunMethod);

            //开启新线程 “注意”:当前 P 是一个参数
            Thread newThread = new Thread(p =>
            {
                FileStream stream = new FileStream("",FileMode.OpenOrCreate);

                StreamWriter streamWriter = new StreamWriter(stream);

                streamWriter.Write(p as string);
            });

            newThread.Name="my name";

            newThread.Start("parameter");

            newThread.Join();
        }


        public void ManualSemaph()
        {
            ManualResetEvent semaphore = new ManualResetEvent(false);

            Thread newThread = new Thread(p =>
            {
                FileStream stream = new FileStream("", FileMode.OpenOrCreate);

                StreamWriter streamWriter = new StreamWriter(stream);

                streamWriter.Write(p as string);

                semaphore.Set();
            });


            //开始等待 当没有信号时，waitone会等待接收信号。若是有信号则直接继续执行。
            semaphore.WaitOne();
        }

        /// <summary>
        /// 自动设置的信号
        /// </summary>
        public void AutoSemaphore()
        {
            //分3阶段执行任务，要设置3个信号量
            AutoResetEvent semapA = new AutoResetEvent(false);
            AutoResetEvent semapB = new AutoResetEvent(false);
            AutoResetEvent semapC = new AutoResetEvent(false);

            Thread A = new Thread(() =>
            {
                Console.Write("第一阶段中");
                semapA.Set();
            });

            Thread B = new Thread(() =>
            {
                semapA.WaitOne();

                Console.Write("第二阶段中");
                semapB.Set();
            });

            Thread C = new Thread(() =>
            {
                semapB.WaitOne();

                Console.Write("第三阶段中");
                semapC.Set();
            });

            A.Start();
            B.Start();
            C.Start();

            semapC.WaitOne(); //等待第三个线程结束之后当前主线程才能继续执行
        }


        /// <summary>
        /// 信号量集合
        /// </summary>
        public void SemapArray()
        {
            AutoResetEvent[] arrs = new AutoResetEvent[]
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false)
            };

            for (int x = 0;x<3;x++)
            {
                Thread th = new Thread((para) =>
                {
                    int current = Convert.ToInt32(para);

                    using (FileStream stream = new FileStream("A.txt", FileMode.OpenOrCreate, FileAccess.Write,
                        FileShare.Write))
                    {
                        stream.Seek(current, SeekOrigin.Begin);
                        stream.Write(new byte[]{0xC2});

                        arrs[current].Set();
                    }
                });

                th.IsBackground = true;
                th.Start();
            }

            Console.WriteLine("等待所有任务完成后继续执行");
            WaitHandle.WaitAll(arrs);
           
        }

        public void UseLock()
        {
            List<int> arr = new List<int>();

            for (int x = 0; x < 3; x++)
            {
                Thread th = new Thread((para) =>
                {
                    lock (arr)
                    {
                        arr.RemoveAt(0);
                    }
                });

                th.Start();
            }
        }

        public void LocalVal()
        {
            ThreadLocal<int> local = new ThreadLocal<int>(true);

            Thread A = new Thread(() => { local.Value = 500; });

            Thread B = new Thread(() => { local.Value = 1000; });

            Thread C = new Thread(() => { local.Value = 2000; });

            A.Start();
            B.Start();
            C.Start();

            A.Join();
            B.Join();
            C.Join();


            foreach (var v in local.Values)
            {
                //V 的值为500，1000，2000
            }
        }
    }
}
