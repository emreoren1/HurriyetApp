using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SizinIcinSectiklermiz.Data
{
    public class SqlHelper
    {
        public static void InsertDb(Models.Data data)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("stpInsertDb", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Title", data.Title);
                command.Parameters.AddWithValue("@Image", data.Image);
                if (string.IsNullOrEmpty(data.Description))
                {
                    command.Parameters.AddWithValue("@Description", System.DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Description", data.Description);
                }
                command.Parameters.AddWithValue("@Link", data.Link);
                command.Parameters.AddWithValue("@Category", data.Category);
                if (string.IsNullOrEmpty(data.Type))
                {
                    command.Parameters.AddWithValue("@Type", System.DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Type", data.Type);
                }
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void TruncateDb()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand();
            string commandStr = string.Empty;
            commandStr = "TRUNCATE TABLE Data";
            command.Connection = con;
            command.CommandText = commandStr;
            command.ExecuteNonQuery();
            con.Close();
        }

        public static List<Models.Data> SelectDb()
        {
            var listModel = new List<Models.Data>();
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("stpGetAllDatas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listModel.Add(new Models.Data
                    {
                        Title = dr["Title"].ToString(),
                        Image = dr["Image"].ToString(),
                        Description = dr["Description"].ToString(),
                        Link = dr["Link"].ToString(),
                        Category = dr["Category"].ToString(),
                        Type = dr["Type"].ToString()
                    });
                }
                var hurriyetEmlak = from c in listModel where c.Category == "Hürriyet Emlak" select c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listModel;
        }

        public static IEnumerable<Models.Data> SelectSomeData()
        {
            var listModel = new List<Models.Data>();
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("stpGetAllDatas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listModel.Add(new Models.Data
                    {
                        Title = dr["Title"].ToString(),
                        Image = dr["Image"].ToString(),
                        Description = dr["Description"].ToString(),
                        Link = dr["Link"].ToString(),
                        Category = dr["Category"].ToString(),
                        Type = dr["Type"].ToString()
                    });
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var hurriyetEmlak = listModel.Where(c => c.Category == "Hürriyet Emlak").Take(4);
            var hurriyetAile = listModel.Where(c => c.Category == "Hürriyet Aile").Take(2);
            var bigpara = listModel.Where(c => c.Category == "Bigpara").Take(1);
            var mahmure = listModel.Where(c => c.Category == "Mahmure").Take(2);
            var yeniBirIs = listModel.Where(c => c.Category == "Yeni Bir İş").Take(2);
            var allData = hurriyetEmlak.Concat(hurriyetAile).Concat(bigpara).Concat(mahmure).Concat(yeniBirIs).ToList();
            
            return allData;
        }

        public static List<Models.Data> SelectedData(int emlakCount, int aileCount, int yeniBirIsCount, int bigparaCount, int mahmureCount)
        {
            var listModel = new List<Models.Data>();
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("stpGetSelectedDatas", con);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                
                //SABAH BAK BURAYA
                cmd.Parameters.Add("@emlakCount", SqlDbType.Int).Value = emlakCount;
                cmd.Parameters.Add("@aileCount", SqlDbType.Int).Value = aileCount;
                cmd.Parameters.Add("@yeniBirIsCount", SqlDbType.Int).Value = yeniBirIsCount;
                cmd.Parameters.Add("@bigparaCount", SqlDbType.Int).Value = bigparaCount;
                cmd.Parameters.Add("@mahmureCount", SqlDbType.Int).Value = mahmureCount;

                
                cmd.ExecuteNonQuery();
                
                //cmd.Parameters.Add(new SqlParameter("@emlakCount", 4));
                //cmd.Parameters.Add(new SqlParameter("@aileCount", 2));
                //cmd.Parameters.Add(new SqlParameter("@yeniBirIsCount", 1));
                //cmd.Parameters.Add(new SqlParameter("@bigparaCount", 2));
                //cmd.Parameters.Add(new SqlParameter("@mahmureCount", 2));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listModel.Add(new Models.Data
                    {
                        Title = dr["Title"].ToString(),
                        Image = dr["Image"].ToString(),
                        Description = dr["Description"].ToString(),
                        Link = dr["Link"].ToString(),
                        Category = dr["Category"].ToString(),
                        Type = dr["Type"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listModel;
        }

        public static void InsertList(List<Models.Data> list)
        {
            foreach (var item in list)
            {
                InsertDb(item);
            }
        }
    }
}



//SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");

//SqlCommand command = new SqlCommand();
//command.Connection = con;
//command.CommandText = "Select * from Data";
//con.Open();
//SqlDataReader reader = command.ExecuteReader();