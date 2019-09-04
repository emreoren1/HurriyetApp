using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SizinIcinSectiklermiz.Data;
using SizinIcinSectiklermiz.Data.Models;

namespace SizinIcinSectiklerimiz.WebUI.Controllers
{
    [Produces("application/json")]
    //[Route("api/Databases")]
    public class DatabasesController : Controller
    {
        private static IConfiguration _iconfiguration;

        // GET: api/GetSomeData
        [HttpGet]
        [Route("api/Databases/GetSomeData")]
        public IEnumerable<Data> GetSomeData()
        {
            GetAppSettingsFile();

            SqlHelper list = new SqlHelper();

            #region NewsCountConfig
            var emlakCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("emlakCount").Value);
            var aileCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("aileCount").Value);
            var yeniBirIsCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("yeniBirIsCount").Value);
            var bigparaCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("bigparaCount").Value);
            var mahmureCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("mahmureCount").Value);
            #endregion

            //return SqlHelper.SelectDb();
            return SqlHelper.SelectedData(emlakCount, aileCount, yeniBirIsCount, bigparaCount, mahmureCount);
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