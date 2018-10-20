using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace LearnDotNetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var initData = new List<KeyValuePair<string, string>>();
            initData.Add(new KeyValuePair<string, string>("UserName","abcd"));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appSettings.json",optional:true,reloadOnChange:true)
                .AddXmlFile("appSettings.xml")
                .AddInMemoryCollection(initData)
                .AddEnvironmentVariables()
                .Build();


            ConfigurationRoot root = null;
            ConfigurationSection section = null;

            var info = configuration["UserName"];
            var path = configuration["path"];
            var host = configuration["MySql:Host"];
            var Id = configuration["Ids:0"];
            var shopid = configuration["shopidlist:0:shopid"];
            var shopid1 = configuration.GetSection("shopidlist").GetSection("1").GetSection("shopid").Value;
            var shopid2 = configuration.GetSection("shopidlist").GetSection("2")["shopid"];

            Console.WriteLine(info);
            Console.WriteLine(path);
            Console.WriteLine(host);
            Console.WriteLine(Id);
            Console.WriteLine(shopid);
            Console.WriteLine(shopid1);
            Console.WriteLine(shopid2);
            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
