using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SizinIcinSectiklermiz.Data;
using SizinIcinSectiklermiz.Data.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SizinIicinSectiklerimiz.Cache
{
    public class RedisHelper
    {
        //IConfiguration _configuration;
        private static IConfiguration _iconfiguration;

        //public RedisHelper(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        private readonly int databaseIndex;
        private static string host;
        public RedisHelper()
        {
            GetAppSettingsFile();
            host = _iconfiguration.GetSection("RedisConfig").GetSection("Host").Value;
            databaseIndex = Convert.ToInt32(_iconfiguration.GetSection("RedisConfig").GetSection("Database").Value);
        }
        static RedisHelper()
        {

            RedisHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(host);
            });
        }

        public static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public void ReadData(string key)
        {
            var cache = RedisHelper.Connection.GetDatabase(databaseIndex);
            var json = cache.StringGet(key);
      
            if (!string.IsNullOrEmpty(json))
            {
                var result = JsonConvert.DeserializeObject<List<Data>>(json);
                result.ForEach(res =>
                {
                    Console.WriteLine(res.ToString());
                });
            }
            else
            {
                Console.WriteLine("Key Not Found !");
            }
        }

        public void SaveBigData(string key, string timeout, List<Data> lists)
        {
            var cache = RedisHelper.Connection.GetDatabase(databaseIndex);
            var value = JsonConvert.SerializeObject(lists);
            if (!string.IsNullOrEmpty(key))
            {
                cache.StringSet(key, value, TimeSpan.FromMinutes(Convert.ToDouble(timeout)));
            }
            else
            {
                Console.WriteLine("Please Add Key");
            }
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            _iconfiguration = builder.Build();
        }
    }
}














//public void ReadData(string Key)
//{
//    if (KeyList.Exists(x => x == Key))
//    {
//        var cache = RedisHelper.Connection.GetDatabase(databaseIndex);
//        var keys = Key;
//        var json = cache.StringGet(keys);
//        var result = JsonConvert.DeserializeObject<List<Data>>(json);
//        result.ForEach(res =>
//        {
//            Console.WriteLine(res.ToString());
//        });
//    }
//    else
//    {
//        Console.WriteLine("Key Not Found!");
//    }
//}


//public void SaveBigData(string Key)
//{
//    if (KeyList.Exists(x => x == Key))
//    {
//        var list = SqlHelper.SelectDb();
//        var cache = RedisHelper.Connection.GetDatabase(databaseIndex);
//        var keys = Key;
//        var value = JsonConvert.SerializeObject(list);
//        cache.StringSet(keys, value, TimeSpan.FromMinutes(timeOut));
//    }
//    else
//    {
//        KeyList.Add(Key);
//        SaveBigData(Key);
//    }
//}