using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SizinIcinSectiklermiz.Data.Models;

namespace SizinIcinSectiklermiz.Data.FactoryPattern.Concrete
{
    public class SqlDataAccess : IDatabaseHandler
    {
        public SqlDataAccess()
        {
        }

        public void InsertDb(Models.Data data)
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

        public void TruncateDb()
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

        public void InsertList(List<Models.Data> list)
        {
            foreach (var item in list)
            {
                InsertDb(item);
            }
        }
    }
}
