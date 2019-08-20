using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.Xml.Serialization;
using SizinIcinSectiklerimizXml.Models;
using SizinIcinSectiklerimizXml;

namespace SizinIcinSectiklerimiz.UI
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {

            //EncodingProvider ppp;
            //ppp = CodePagesEncodingProvider.Instance;
            //Encoding.RegisterProvider(ppp);

            // INSERT
            using (StreamReader _StreamReader = new StreamReader(@"C:\Users\Emre\Desktop\HurriyetApp\SizinIcinSectiklerimiz.UI\SizinIcinSectiklerimiz.UI\Data\bigpara.json"))
            {
                string jsonData = _StreamReader.ReadToEnd();
                List<NewsData> listCategory = JsonConvert.DeserializeObject<List<NewsData>>(jsonData);


                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
                connection.Open();
                string commandString = "Insert Into New(Title,Spot,Description,Link,ImagePath,Category,[Order]) Values(@Title,@Spot,@Description,@Link,@ImagePath,@Category,@Order)";
                string asd = "TRUNCATE TABLE New";

                var listNewsDelete = new List<NewsData>();
                using (SqlConnection conDeleteNews = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
                {
                    SqlCommand cmd = new SqlCommand(asd, conDeleteNews);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    conDeleteNews.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listNewsDelete.Remove(new NewsData
                        {
                            Title = dr["Title"].ToString(),
                            Spot = dr["Spot"].ToString(),
                            Description = dr["Description"].ToString(),
                            Link = dr["Link"].ToString(),
                            ImagePath = dr["ImagePath"].ToString(),
                            Category = dr["Category"].ToString(),
                            Order = Convert.ToInt32(dr["Order"]),
                        });
                    }
                }


                if (connection.State == ConnectionState.Open)
                {
                    foreach (var item in listCategory)
                    {
                        SqlCommand command = new SqlCommand(commandString, connection);
                        command.Parameters.AddWithValue("@Title", item.Title);
                        command.Parameters.AddWithValue("Spot", item.Spot);
                        if (string.IsNullOrEmpty(item.Description))
                        {
                            command.Parameters.AddWithValue("@Description", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Description", item.Description);
                        }

                        command.Parameters.AddWithValue("@Link", item.Link);
                        command.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                        command.Parameters.AddWithValue("@Category", item.Category);
                        command.Parameters.AddWithValue("@Order", item.Order);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            // INSERT END



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

            // EMLAK START

            XmlDocument xmlDocEmlak = new XmlDocument();
            xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");
            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
            string Adv_Text = "", Adv_Def_Link = "", Adv_Location = "", Adv_Price = "", Adv_Title = "", Adv_Imagename = "", Adv_Image = "", Adv_City_Id = "", Adv_Cityname = "";
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
            con.Open();
            string commandStr = "Insert Into Emlak(Adv_Text,Adv_Def_Link,Adv_Location,Adv_Price,Adv_Title,Adv_Imagename,Adv_Image,Adv_City_Id,Adv_Cityname) Values(@Adv_Text,@Adv_Def_Link,@Adv_Location,@Adv_Price,@Adv_Title,@Adv_Imagename,@Adv_Image,@Adv_City_Id,@Adv_Cityname)";


            var listEmlakDelete = new List<EmlakData>();
            using (SqlConnection conDeleteEmlaks = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("stpDeleteEmlaks", conDeleteEmlaks);
                cmd.CommandType = CommandType.StoredProcedure;
                conDeleteEmlaks.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listEmlakDelete.Remove(new EmlakData
                    {
                        Adv_Text = dr["Adv_Text"].ToString(),
                        Adv_Def_Link = dr["Adv_Def_Link"].ToString(),
                        Adv_Location = dr["Adv_Location"].ToString(),
                        Adv_Price = dr["Adv_Price"].ToString(),
                        Adv_Title = dr["Adv_Title"].ToString(),
                        Adv_Imagename = dr["Adv_Imagename"].ToString(),
                        Adv_Image = dr["Adv_Image"].ToString(),
                        Adv_City_Id = Convert.ToInt32(dr["Adv_City_Id"]),
                        Adv_Cityname = dr["Adv_Cityname"].ToString(),
                    });
                }
            }

            if (con.State == ConnectionState.Open)
            {
                foreach (XmlNode node in nodeListEmlak)
                {
                    Adv_Text = node.SelectSingleNode("adv_text").InnerText;
                    Adv_Def_Link = node.SelectSingleNode("adv_def_link").InnerText;
                    Adv_Location = node.SelectSingleNode("adv_location").InnerText;
                    Adv_Price = node.SelectSingleNode("adv_price").InnerText;
                    Adv_Title = node.SelectSingleNode("adv_title").InnerText;
                    Adv_Imagename = node.SelectSingleNode("adv_imagename").InnerText;
                    Adv_Image = node.SelectSingleNode("adv_image").InnerText;
                    Adv_City_Id = node.SelectSingleNode("adv_city_id").InnerText;
                    Adv_Cityname = node.SelectSingleNode("adv_cityname").InnerText;

                    SqlCommand command = new SqlCommand(commandStr, con);
                    command.Parameters.AddWithValue("@Adv_Text", Adv_Text);
                    command.Parameters.AddWithValue("Adv_Def_Link", Adv_Def_Link);
                    command.Parameters.AddWithValue("@Adv_Location", Adv_Location);
                    command.Parameters.AddWithValue("@Adv_Price", Adv_Price);
                    command.Parameters.AddWithValue("@Adv_Title", Adv_Title);
                    command.Parameters.AddWithValue("@Adv_Imagename", Adv_Imagename);
                    command.Parameters.AddWithValue("@Adv_Image", Adv_Image);
                    command.Parameters.AddWithValue("@Adv_City_Id", Adv_City_Id);
                    command.Parameters.AddWithValue("@Adv_Cityname", Adv_Cityname);
                    command.ExecuteNonQuery();
                }

                
            }

            XmlDocument xmlDocMahmure = new XmlDocument();
            xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\mahmure.xml");
            XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");
            string Baslik = "", Metin = "", ResimAdi = "", Resim = "", Link = "", Tarih = "";
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
            conn.Open();
            string commandStrr = "Insert Into Mahmure(Baslik,Metin,ResimAdi,Resim,Link,Tarih) Values(@Baslik,@Metin,@ResimAdi,@Resim,@Link,@Tarih)";

            var listMahmureDelete = new List<MahmureData>();
            using (SqlConnection conDeleteMahmures = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("stpDeleteMahmures", conDeleteMahmures);
                cmd.CommandType = CommandType.StoredProcedure;
                conDeleteMahmures.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listMahmureDelete.Remove(new MahmureData
                    {
                        Baslik = dr["Baslik"].ToString(),
                        Metin = dr["Metin"].ToString(),
                        ResimAdi = dr["ResimAdi"].ToString(),
                        Resim = dr["Resim"].ToString(),
                        Link = dr["Link"].ToString(),
                        Tarih = Convert.ToDateTime(dr["Tarih"]),
                    });
                }
            }

            if (conn.State == ConnectionState.Open)
            {
                foreach (XmlNode node in nodeListMahmure)
                {
                    Baslik = node.SelectSingleNode("BASLIK").InnerText;
                    Metin = node.SelectSingleNode("METIN").InnerText;
                    ResimAdi = node.SelectSingleNode("RESIMADI").InnerText;
                    Resim = node.SelectSingleNode("RESIM").InnerText;
                    Link = node.SelectSingleNode("LINK").InnerText;
                    Tarih = node.SelectSingleNode("TARIH").InnerText;

                    SqlCommand command = new SqlCommand(commandStrr, con);
                    command.Parameters.AddWithValue("@Baslik", Baslik);
                    command.Parameters.AddWithValue("Metin", Metin);
                    command.Parameters.AddWithValue("@ResimAdi", ResimAdi);
                    command.Parameters.AddWithValue("@Resim", Resim);
                    command.Parameters.AddWithValue("@Link", Link);
                    command.Parameters.AddWithValue("@Tarih", Convert.ToDateTime(Tarih));
                    command.ExecuteNonQuery();
                }
            }

            GetAppSettingsFile();

            Console.WriteLine("Bigpara Haberleri");

            var newsDAL = new XmlDal.DataDal(_iconfiguration);
            var listNewsModel = newsDAL.GetListNews();
            listNewsModel.ForEach(item =>
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Spot.ToString());
                Console.WriteLine(item.Description);
                Console.WriteLine(item.Link);
                Console.WriteLine(item.ImagePath);
                Console.WriteLine(item.Category);
                Console.WriteLine(item.Order);
                Console.WriteLine("------------------------------------------------------------------------------------");
            });

            Console.WriteLine("Hürriyet Emlak Haberleri");

            var emlakDAL = new XmlDal.DataDal(_iconfiguration);
            var listEmlakModel = emlakDAL.GetListEmlak();
            listEmlakModel.ForEach(item =>
            {
                Console.WriteLine(item.Adv_Text);
                Console.WriteLine(item.Adv_Def_Link.ToString());
                Console.WriteLine(item.Adv_Location);
                Console.WriteLine(item.Adv_Price);
                Console.WriteLine(item.Adv_Title);
                Console.WriteLine(item.Adv_Imagename);
                Console.WriteLine(item.Adv_Image);
                Console.WriteLine(item.Adv_City_Id);
                Console.WriteLine(item.Adv_Cityname);
                Console.WriteLine("------------------------------------------------------------------------------------");
            });

            Console.WriteLine("Mahmure Haberleri");

            var mahmureDAL = new XmlDal.DataDal(_iconfiguration);
            var listMahmureModel = mahmureDAL.GetListMahmure();
            listMahmureModel.ForEach(item =>
            {
                Console.WriteLine(item.Baslik);
                Console.WriteLine(item.Metin);
                Console.WriteLine(item.ResimAdi);
                Console.WriteLine(item.Resim);
                Console.WriteLine(item.Link);
                Console.WriteLine(item.Tarih);
                Console.WriteLine("------------------------------------------------------------------------------------");
            });

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
