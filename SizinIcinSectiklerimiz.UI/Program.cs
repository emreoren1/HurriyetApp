using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SizinIcinSectiklermiz.Data;
using SizinIcinSectiklermiz.Data.Models;

namespace SizinIcinSectiklerimiz.UI
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            SqlHelper.TruncateDb();

            StreamReader _StreamReader = new StreamReader(@"C:\Users\Emre\Desktop\HurriyetApp\SizinIcinSectiklerimiz.UI\SizinIcinSectiklerimiz.UI\Data\bigpara.json");
            string jsonData = _StreamReader.ReadToEnd();
            var listNews = JsonConvert.DeserializeObject<List<Data>>(jsonData);
            foreach (var item in listNews)
            {
                item.Category = "Bigpara";
                item.Type = "Json";
            }
            SqlHelper.InsertList(listNews);

            XmlDocument xmlDocEmlak = new XmlDocument();
            xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");
            XmlDocument xmlDocMahmure = new XmlDocument();
            xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\mahmure.xml");
            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
            XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");
            var listData = new List<Data>();

            foreach (XmlNode node in nodeListEmlak)
            {
                listData.Add(
                    new Data
                    {
                        Title = node.SelectSingleNode("adv_title").InnerText,
                        Image = node.SelectSingleNode("adv_image").InnerText,
                        Description = node.SelectSingleNode("adv_text").InnerText,
                        Link = node.SelectSingleNode("adv_def_link").InnerText,
                        Category = "Hürriyet Emlak",
                        Type = "Xml",
                    }
                    );
            }

            foreach (XmlNode item in nodeListMahmure)
            {
                listData.Add(
                    new Data
                    {
                        Title = item.SelectSingleNode("BASLIK").InnerText,
                        Image = item.SelectSingleNode("RESIM").InnerText,
                        Description = item.SelectSingleNode("METIN").InnerText,
                        Link = item.SelectSingleNode("LINK").InnerText,
                        Category = "Mahmure",
                        Type = "Xml",
                    }
                    );
            }
            SqlHelper.InsertList(listData);

            Console.WriteLine("Press to any key...");
            Console.ReadLine();
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _iconfiguration = builder.Build();
        }
    }
}

















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