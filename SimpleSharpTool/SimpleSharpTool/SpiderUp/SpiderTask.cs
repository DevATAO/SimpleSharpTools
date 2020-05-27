using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.AnwiseGlobal.Spider.Kernel.MMS.Common.Tools.SpiderUp
{
    public class SpiderTask
    {
        /// <summary>
        /// 创建TASK异步操作（3中方式）
        /// </summary>
        /// <remarks>综合性能TASK 优于 THREAD</remarks>
        public void CreateTask()
        {
            //创建TASK
            Task TA = new Task(() =>
            {
                //执行的方法
                Console.WriteLine("要执行的函数");
            } );

            TA.Start();

            TA.Wait();

            //创建直接执行
            Task TB = Task.Run(() =>
            {
                //执行的方法
                Console.WriteLine("要执行的函数");
            });
            TB.Wait();

            //使用工厂创建
            TaskFactory factory = new TaskFactory();
            Task TC = factory.StartNew(() =>
            {
                //执行的方法
                Console.WriteLine("要执行的函数");
            });

            TC.Wait();
        }

        /// <summary>
        /// TASK带返回值
        /// </summary>
        public void ReTask()
        {
            Task<int> task = Task.Run(() => 50);

            task.Wait();

            var res = task.Result;
        }

        /// <summary>
        /// 带参数的任务
        /// </summary>
        public void TaskPara()
        {
            var info = (Name:"A",Age:15);//元祖

            var task = new Task((s) =>
            {
                (string name,int age) = ((string,int))s;
            }, info);

            //把info当做参数传入到task中

            task.Start();
            task.Wait();
        }

        /// <summary>
        /// 等待另一个任务执行完成
        /// </summary>
        public void WaitTask()
        {
            Task A = new Task(() => { Console.WriteLine("第一个先执行"); });

            Task B = new Task(() => { Console.WriteLine("第二个执行"); });

            B.ContinueWith(taskBefore =>
            {
                var input = taskBefore; // B执行的结果
                Console.WriteLine("B执行完成之后立即执行该任务");
            });
        }


        //异步等待使用方式
        public async void AsynAndAwait()
        {
            var x = await RunT();
        }

        public Task<int> RunT()
        {
            return Task.Run(() => { return 10;});
        }

        /// <summary>
        /// 异步上下文进行变量交换
        /// </summary>
        public async void TaskLocal()
        {
            AsyncLocal<int> PCBLocal = new AsyncLocal<int>();

            PCBLocal.Value = 50;

            await Task.Delay(500);

            var origin = PCBLocal.Value;
        }


        public Task<int> CancelRunTask(CancellationToken token)
        {
            TaskCompletionSource<int> res = new TaskCompletionSource<int>();

            //监听Token可以随时取消
            int value = 50;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                value = 50 + value;

                Task.Delay(200).Wait();
            }

            res.SetResult(value);
            var x = token.IsCancellationRequested;

            return res.Task;
        }

        public async void CallCancel()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            if (GetUseChoice())
            {
                source.Cancel();
            }

            var res = await CancelRunTask(source.Token);

            source.Dispose();
        }

        private bool GetUseChoice()
        {
            return false;
        }
    }
}
