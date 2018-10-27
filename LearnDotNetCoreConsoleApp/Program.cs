using System;
using System.Collections.Generic;
using System.Net.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace LearnDotNetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var initData = new List<KeyValuePair<string, string>>();
            initData.Add(new KeyValuePair<string, string>("UserName", "abcd"));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddXmlFile("appSettings.xml")
                .AddInMemoryCollection(initData)
                .AddEnvironmentVariables()
                .Build();


            ConfigurationRoot root = null;
            ConfigurationSection section = null;
            JsonConfigurationSource jsonSource;

            ConfigurationBuilder builder = null;

            var info = configuration["UserName"];
            var path = configuration["path"];
            var host = configuration["MySql:Host"];
            var Id = configuration["Ids:0"];
            var shopid = configuration["shopidlist:0:shopid"];
            var shopid1 = configuration.GetSection("shopidlist").GetSection("1").GetSection("shopid").Value;
            var shopid2 = configuration.GetSection("shopidlist").GetSection("0")["shopid"];
            var port = configuration.GetValue<int>("MySql:Port");

            Rootobject rootobject=new Rootobject();
            configuration.Bind(rootobject);

            Rootobject rootobject1 = configuration.Get<Rootobject>();

            Console.WriteLine(info);
            Console.WriteLine(path);
            Console.WriteLine(host);
            Console.WriteLine(Id);
            Console.WriteLine(shopid);
            Console.WriteLine(shopid1);
            Console.WriteLine(shopid2);
            Console.WriteLine(port);

            Console.ReadKey();
        }
    }


    public class Rootobject
    {
        public string UserName { get; set; }
        public Mysql MySql { get; set; }
        public int[] Ids { get; set; }
        public Shopidlist[] shopidlist { get; set; }
    }

    public class Mysql
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class Shopidlist
    {
        public int shopid { get; set; }
    }

}
