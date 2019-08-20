using Microsoft.Extensions.Configuration;
using SizinIcinSectiklerimizXml;
using SizinIcinSectiklerimizXml.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SizinIcinSectiklerimiz.UI.XmlDal
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
    }
}
