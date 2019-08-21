using SizinIcinSectiklerimiz.UI.Models;
using SizinIcinSectiklerimizXml.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SizinIcinSectiklerimiz.UI
{
    public static class SqlHelper
    {
        public static void InsertDb(Data data) 
        {
            
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-6DFTQAA;Initial Catalog=NewsDb;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand();
            string commandStr = string.Empty;
            commandStr = "Insert Into Data(Title,Image,Description,Link) Values(@Title,@Image,@Description,@Link)";
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
            command.ExecuteNonQuery();
            con.Close();
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

        public static void InsertList(List<Data> list)
        {
            foreach (var item in list)
            {
                InsertDb(item);
            }
        }
    }
}
