using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using SizinIcinSectiklermiz.Data.Models;
using SizinIcinSectiklermiz.Data;
using System.Net;
using System;

namespace SizinIcinSectiklerimiz.UI.FactoryPattern
{
    class JsonData : FactoryData
    {
        public override void DataType()
        {
            string url = "";
            url = "http://s.hurriyet.com.tr/dinamik/mainpageservices/bigpara.json"; // for all data

            var webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:28.0) Gecko/20100101 Firefox/28.0";
            webRequest.ContentLength = 0;

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader reader = new StreamReader(webResponse.GetResponseStream());
            string jsonData = reader.ReadToEnd();
            var listNews = JsonConvert.DeserializeObject<List<Data>>(jsonData);
            foreach (var item in listNews)
            {
                item.Category = "Bigpara";
                item.Type = "Json";
            }
            reader.Close();
            webRequest.Abort();

            SqlHelper.InsertList(listNews);
        }
    }
}




//StreamReader _StreamReader = new StreamReader(@"C:\Users\Emre\Desktop\HurriyetApp\SizinIcinSectiklerimiz.UI\SizinIcinSectiklerimiz.UI\Data\bigpara.json");
//string jsonData = _StreamReader.ReadToEnd();
//var listNews = JsonConvert.DeserializeObject<List<Data>>(jsonData);
//foreach (var item in listNews)
//{
//    item.Category = "Bigpara";
//    item.Type = "Json";
//}


//string autorization = "USERNAME" + ":" + "PASSWORD";
//byte[] binaryAuthorization = System.Text.Encoding.UTF8.GetBytes(autorization);
//autorization = Convert.ToBase64String(binaryAuthorization);
//autorization = "Basic " + autorization;
//webRequest.Headers.Add("AUTHORIZATION", autorization);



