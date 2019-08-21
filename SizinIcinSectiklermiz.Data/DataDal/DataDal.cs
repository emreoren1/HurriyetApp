using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SizinIcinSectiklermiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;

namespace SizinIcinSectiklermiz.Data.DataDal
{
    public class DataDal
    {
        private string _connectionString;
        public DataDal(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }

        public List<NewsData> GetListNews()
        {
            var listNewsModel = new List<NewsData>();
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
                {
                    SqlCommand cmd = new SqlCommand("stpGetAllNews", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listNewsModel.Add(new NewsData
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNewsModel;
        }


        public List<EmlakData> GetListEmlak()
        {
            var listEmlakModel = new List<EmlakData>();
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
                {
                    SqlCommand cmd = new SqlCommand("stpGetAllEmlaks", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listEmlakModel.Add(new EmlakData
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEmlakModel;
        }

        public List<MahmureData> GetListMahmure()
        {
            var listMahmureModel = new List<MahmureData>();
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True"))
                {
                    SqlCommand cmd = new SqlCommand("stpGetAllMahmures", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listMahmureModel.Add(new MahmureData
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMahmureModel;
        }

        public List<MahmureData> AddMahmures()
        {
            XmlDocument xmlDocMahmure = new XmlDocument();
            xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklermiz.Data\\Data\\mahmure.xml");
            XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");
            var nodes = new List<XmlNode>(nodeListMahmure.Cast<XmlNode>());
            var asdf = new List<MahmureData>(nodes.Cast<MahmureData>());
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
                }

                foreach (var item in asdf)
                {
                    SqlCommand command = new SqlCommand(commandStrr, conn);
                    command.Parameters.AddWithValue("@Baslik", Baslik);
                    command.Parameters.AddWithValue("Metin", Metin);
                    command.Parameters.AddWithValue("@ResimAdi", ResimAdi);
                    command.Parameters.AddWithValue("@Resim", Resim);
                    command.Parameters.AddWithValue("@Link", Link);
                    command.Parameters.AddWithValue("@Tarih", Convert.ToDateTime(Tarih));
                    command.ExecuteNonQuery();
                }
            }
            return asdf;
        }

        public List<EmlakData> AddEmlaks()
        {
            XmlDocument xmlDocEmlak = new XmlDocument();
            xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklermiz.Data\\Data\\emlak.xml");
            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
            var nodees = new List<XmlNode>(nodeListEmlak.Cast<XmlNode>());
            var asd = new List<EmlakData>(nodees.Cast<EmlakData>());
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
                foreach (XmlNode node in nodees)
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
                }

                foreach (var item in asd)
                {
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
            return asd;
        }

        public List<NewsData> AddNews()
        {
            using (StreamReader _StreamReader = new StreamReader(@"C:\Users\Emre\Desktop\HurriyetApp\SizinIcinSectiklerimiz.UI\SizinIcinSectiklermiz.Data\Data\bigpara.json"))
            {
                string jsonData = _StreamReader.ReadToEnd();
                List<NewsData> listNews = JsonConvert.DeserializeObject<List<NewsData>>(jsonData);


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
                    foreach (var item in listNews)
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
                return listNews;
            }
        }
    }
}
