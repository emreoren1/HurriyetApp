using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                SqlCommand command = new SqlCommand();
                string commandStr = string.Empty;
                commandStr = "Insert Into Data(Title,Image,Description,Link,Category,Type) Values(@Title,@Image,@Description,@Link,@Category,@Type)";
                command.Connection = con;
                command.CommandText = commandStr;

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
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
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