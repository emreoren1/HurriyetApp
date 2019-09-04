using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SizinIcinSectiklermiz.Data;
using SizinIcinSectiklerimiz.UI.FactoryPattern;
using System.Xml;
using ServiceStack.Redis;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using SizinIcinSectiklermiz.Data.Models;
using System.Collections.Generic;
using ServiceStack;
using System.Linq;
using Newtonsoft.Json;
using SizinIicinSectiklerimiz.Cache;

namespace SizinIcinSectiklerimiz.UI
{
    class Program
    {
        private static IConfiguration _iconfiguration;

        static void Main(string[] args)
        {
            GetAppSettingsFile();

            #region NewsCountConfig
            var emlakCount = _iconfiguration["NewsCountConfig:emlakCount"];
                
            var aileCount = _iconfiguration.GetSection("NewsCountConfig").GetSection("aileCount").Value;
            var yeniBirIsCount = _iconfiguration.GetSection("NewsCountConfig").GetSection("yeniBirIsCount").Value;
            var bigparaCount = _iconfiguration.GetSection("NewsCountConfig").GetSection("bigparaCount").Value;
            var mahmureCount = _iconfiguration.GetSection("NewsCountConfig").GetSection("mahmureCount").Value;
            #endregion


            #region RedisConfig

            
            var redisKey = _iconfiguration.GetSection("RedisConfig").GetSection("Key").Value;
            var timeOut = _iconfiguration.GetSection("RedisConfig").GetSection("Timeout").Value;
            RedisHelper redisHelper = new RedisHelper();
            redisHelper.ReadData(redisKey);
            #endregion
            SqlHelper.TruncateDb();

            #region FactoryPattern
            Creater creater = new Creater();
            FactoryData jsonData = creater.FactoryMethod(Datas.Json);
            FactoryData xmlData = creater.FactoryMethod(Datas.Xml);
            jsonData.DataType();
            xmlData.DataType();
            #endregion

            var allDatas = SqlHelper.SelectDb();
            var list = SqlHelper.SelectedData(Convert.ToInt32(emlakCount), Convert.ToInt32(aileCount), Convert.ToInt32(yeniBirIsCount), Convert.ToInt32(bigparaCount), Convert.ToInt32(mahmureCount));
            var list2 = SqlHelper.SelectSomeData();

            redisHelper.SaveBigData(redisKey, timeOut, list);

            Console.WriteLine("Press to any key...");
            Console.ReadLine();
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












//Console.WriteLine("=========================");
//redisHelper.ReadData("RedisTest");
//redisHelper.SaveBigData("RedisTest");
//redisHelper.ReadData("RedisTest");


//ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect("localhost");
//IDatabase redDb = redisConn.GetDatabase(1);
//var keys = "key1";
//var value = JsonConvert.SerializeObject(list);
//redDb.StringSet(keys, value, TimeSpan.FromMinutes(60));
//var json = redDb.StringGet(keys);
//var result = JsonConvert.DeserializeObject<List<Data>>(json);
//Console.WriteLine(result);



//var cache = System.Runtime.Caching.MemoryCache.Default;
//var key = "denemelist";
//var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(2, 0, 0)};
//cache.Add(key, list , policy);

//string fileContents = cache["key1"] as Data;

//if (fileContents == null)
//{
//    CacheItemPolicy policy = new CacheItemPolicy();
//    List<Data> data = new List<Data>();
//     data.Add("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\bigpara.json");
//    policy.ChangeMonitors.Add(new HostFileChangeMonitor(data));

//}
//var tempData = (List<Data>)cache.Get(key);





//StreamReader _StreamReader = new StreamReader(@"C:\Users\Emre\Desktop\HurriyetApp\SizinIcinSectiklerimiz.UI\SizinIcinSectiklerimiz.UI\Data\bigpara.json");
//string jsonData = _StreamReader.ReadToEnd();
//var listNews = JsonConvert.DeserializeObject<List<Data>>(jsonData);
//foreach (var item in listNews)
//{
//    item.Category = "Bigpara";
//    item.Type = "Json";
//}
//SqlHelper.InsertList();

//XmlDocument xmlDocEmlak = new XmlDocument();
//xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");
//XmlDocument xmlDocMahmure = new XmlDocument();
//xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\mahmure.xml");
//XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
//XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");
//var listData = new List<Data>();

//foreach (XmlNode node in nodeListEmlak)
//{
//    listData.Add(
//        new Data
//        {
//            Title = node.SelectSingleNode("adv_title").InnerText,
//            Image = node.SelectSingleNode("adv_image").InnerText,
//            Description = node.SelectSingleNode("adv_text").InnerText,
//            Link = node.SelectSingleNode("adv_def_link").InnerText,
//            Category = "Hürriyet Emlak",
//            Type = "Xml",
//        }
//        );
//}

//foreach (XmlNode item in nodeListMahmure)
//{
//    listData.Add(
//        new Data
//        {
//            Title = item.SelectSingleNode("BASLIK").InnerText,
//            Image = item.SelectSingleNode("RESIM").InnerText,
//            Description = item.SelectSingleNode("METIN").InnerText,
//            Link = item.SelectSingleNode("LINK").InnerText,
//            Category = "Mahmure",
//            Type = "Xml",
//        }
//        );
//}
//SqlHelper.InsertList(listData);


//var castEmlak = new List<XmlNode>(nodeListEmlak.Cast<XmlNode>());
//var castMahmure = new List<XmlNode>(nodeListMahmure.Cast<XmlNode>());


//XmlSerializer serializer2 = new XmlSerializer(typeof(List<MahmureData>));

//        using (StreamReader stream = new StreamReader("C:\\Users\\Emre\\Desktop\\ConsoleXml\\SizinIcinSectiklerimizXml\\SizinIcinSectiklerimizXml\\XmlData\\mahmure.xml"))
//        {
//            var dezerializedList = (List<MahmureData>)serializer2.Deserialize(stream);

//            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=XmlDatabase;Integrated Security=True");
//            conn.Open();
//            string commandStrr = "Insert Into Mahmure(Baslik,Metin,ResimAdi,Resim,Link,Tarih) Values(@Baslik,@Metin,@ResimAdi,@Resim,@Link,@Tarih)";


//            if (conn.State == ConnectionState.Open)
//            {
//                foreach (var item in dezerializedList)
//                {
//                    SqlCommand command = new SqlCommand(commandStrr, conn);
//                    command.Parameters.AddWithValue("@Baslik", item.Baslik);
//                    command.Parameters.AddWithValue("Metin", item.Metin);
//                    command.Parameters.AddWithValue("@ResimAdi", item.ResimAdi);
//                    command.Parameters.AddWithValue("@Resim", item.Resim);
//                    command.Parameters.AddWithValue("@Link", item.Link);
//                    command.Parameters.AddWithValue("@Tarih", item.Tarih);
//                }
//            }
//            conn.Close();
//        }


//EncodingProvider ppp;
//ppp = CodePagesEncodingProvider.Instance;
//Encoding.RegisterProvider(ppp);

/*XmlSerializer serializer = new XmlSerializer(typeof(List<EmlakData>));

using (FileStream stream = File.OpenRead("C:\\Users\\Emre\\Desktop\\ConsoleXml\\SizinIcinSectiklerimizXml\\SizinIcinSectiklerimizXml\\XmlData\\emlak.xml"))
{
    List<EmlakData> dezerializedList = (List<EmlakData>)serializer.Deserialize(stream);

    SqlConnection conn = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=XmlDatabase;Integrated Security=True");
    conn.Open();
    string commandStr = "Insert Into Emlak(Adv_Text,Adv_Def_Link,Adv_Location,Adv_Price,Adv_Title,Adv_Imagename,Adv_Image,Adv_City_Id,Adv_Cityname) Values(@Adv_Text,@Adv_Def_Link,@Adv_Location,@Adv_Price,@Adv_Title,@Adv_Imagename,@Adv_Image,@Adv_City_Id,@Adv_Cityname)";


    if (conn.State == ConnectionState.Open)
    {
        foreach (var item in dezerializedList)
        {
            SqlCommand command = new SqlCommand(commandStr, conn);
            command.Parameters.AddWithValue("@Adv_Text", item.Adv_Text);
            command.Parameters.AddWithValue("Adv_Def_Link", item.Adv_Def_Link);
            command.Parameters.AddWithValue("@Adv_Location", item.Adv_Location);
            command.Parameters.AddWithValue("@Adv_Price", item.Adv_Price);
            command.Parameters.AddWithValue("@Adv_Title", item.Adv_Title);
            command.Parameters.AddWithValue("@Adv_Imagename", item.Adv_Imagename);
            command.Parameters.AddWithValue("@Adv_Image", item.Adv_Image);
            command.Parameters.AddWithValue("@Adv_City_Id", item.Adv_City_Id);
            command.Parameters.AddWithValue("@Adv_Cityname", item.Adv_Cityname);
            command.ExecuteNonQuery();
        }
    }
    conn.Close();
}*/


//var emlakDAL = new DataDal(_iconfiguration);
//var listEmlakModel = emlakDAL.AddEmlaks();
//listEmlakModel.ForEach(item =>
//{
//    Console.WriteLine(item.Adv_Text);
//    Console.WriteLine(item.Adv_Def_Link.ToString());
//    Console.WriteLine(item.Adv_Location);
//    Console.WriteLine(item.Adv_Price);
//    Console.WriteLine(item.Adv_Title);
//    Console.WriteLine(item.Adv_Imagename);
//    Console.WriteLine(item.Adv_Image);
//    Console.WriteLine(item.Adv_City_Id);
//    Console.WriteLine(item.Adv_Cityname);
//    Console.WriteLine("------------------------------------------------------------------------------------");
//});

//Console.WriteLine("Mahmure Haberleri");

//var mahmureDAL = new DataDal(_iconfiguration);
//var listMahmureModel = mahmureDAL.AddMahmures();
//listMahmureModel.ForEach(item =>
//{
//    Console.WriteLine(item.Baslik);
//    Console.WriteLine(item.Metin);
//    Console.WriteLine(item.ResimAdi);
//    Console.WriteLine(item.Resim);
//    Console.WriteLine(item.Link);
//    Console.WriteLine(item.Tarih);
//    Console.WriteLine("------------------------------------------------------------------------------------");
//});

//Console.WriteLine("Bigpara Haberleri");

//var newsDAL = new DataDal(_iconfiguration);
//var listNewsModel = newsDAL.AddNews();
//listNewsModel.ForEach(item =>
//{
//    Console.WriteLine(item.Title);
//    Console.WriteLine(item.Spot.ToString());
//    Console.WriteLine(item.Description);
//    Console.WriteLine(item.Link);
//    Console.WriteLine(item.ImagePath);
//    Console.WriteLine(item.Category);
//    Console.WriteLine(item.Order);
//    Console.WriteLine("------------------------------------------------------------------------------------");
//});