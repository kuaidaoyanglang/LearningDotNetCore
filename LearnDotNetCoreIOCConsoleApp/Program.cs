using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LearnDotNetCoreIOCConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceCollection service=new ServiceCollection();

            service.AddTransient<IFly, Pig>();

            service.AddLogging();

            var provider = service.BuildServiceProvider();

            var scope1 = provider.CreateScope();

            var scope2 = provider.CreateScope();

            scope1.ServiceProvider.GetService<IFly>();

            scope2.ServiceProvider.GetService<IFly>();

            provider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

            var fly = provider.GetService<IFly>();

            fly.Fly();

            Console.WriteLine("Hello World!"); 
            Console.ReadKey();
        }
    }

    public interface IFly
    {
        void Fly();
    }

    public class Pig : IFly
    {
        private ILogger<Pig> logger = null;

        public Pig(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<Pig>();
        }

        public void Fly()
        {
            logger.LogInformation("这是Console的日志......");
            Console.WriteLine("风口来了，猪也会飞！");
        }
    }
}
