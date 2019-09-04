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
using SizinIicinSectiklerimiz.Cache;

namespace SizinIcinSectiklerimiz.WebUI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private static IConfiguration _iconfiguration;
        // GET: api/Database
        [HttpGet]
        //[Route("api/Database")]
        public IEnumerable<Data> Get()
        {
            GetAppSettingsFile();

            #region NewsCountConfig
            var emlakCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("emlakCount").Value);
            var aileCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("aileCount").Value);
            var yeniBirIsCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("yeniBirIsCount").Value);
            var bigparaCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("bigparaCount").Value);
            var mahmureCount = Convert.ToInt32(_iconfiguration.GetSection("NewsCountConfig").GetSection("mahmureCount").Value);
            #endregion

            

            return SqlHelper.SelectedData(emlakCount,aileCount,yeniBirIsCount,bigparaCount,mahmureCount);
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Database/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Database
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Database/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
