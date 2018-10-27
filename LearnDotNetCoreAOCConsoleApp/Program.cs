using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace LearnDotNetCoreAOCConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection service=new ServiceCollection();
            service.ConfigureDynamicProxy();
            service.AddTransient<IMySql, MySql>();
            var provider = service.BuildDynamicProxyServiceProvider();
            var mysql = provider.GetService<IMySql>();
            var msg = mysql.GetData(10);
            Console.WriteLine(msg);
            msg = mysql.GetData(10);
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }

    public class MyLogInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("开始记录日志......");
            var task = next(context);
            Console.WriteLine("结束记录日志......");
            return task;
        }
    }


    public class MyCacheInterceptorAttribute : AbstractInterceptorAttribute
    {
        private Dictionary<string,string> cacheDict=new Dictionary<string, string>();
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = string.Join(",", context.Parameters);
            if (cacheDict.ContainsKey(cacheKey))
            {
                context.ReturnValue = cacheDict[cacheKey];
                return Task.CompletedTask;
            }

            var task = next(context);

            var cacheValue = context.ReturnValue;

            cacheDict.Add(cacheKey,string.Format("from cache {0}......",cacheValue.ToString()));
           
            return task;
        }
    }

    public interface IMySql
    {
        string GetData(int id);
    }

    public class MySql : IMySql
    {
        [MyLogInterceptor]
        [MyCacheInterceptor]
        public string GetData(int id)
        {
            var msg = $"已经获取到数据Id={id}的数据";
            Console.WriteLine(msg);
            return msg;
        }
    }
}
