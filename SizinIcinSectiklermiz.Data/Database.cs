using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace SizinIcinSectiklermiz.Data
{
    public class Database
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DataConnStr"].ConnectionString);
        }
    }
}
