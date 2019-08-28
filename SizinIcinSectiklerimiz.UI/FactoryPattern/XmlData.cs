using SizinIcinSectiklermiz.Data;
using SizinIcinSectiklermiz.Data.Models;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace SizinIcinSectiklerimiz.UI.FactoryPattern
{
    class XmlData : FactoryData
    {
        public override void DataType()
        {
            XmlDocument xmlDocEmlak = new XmlDocument();
            xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");

            var client = new WebClient();
            string xmlData = client.DownloadString("http://mahmure.hurriyet.com.tr/hurriyet/anasayfa/xml/2/");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);

            
            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
            XmlNodeList nodeListMahmure = xml.DocumentElement.SelectNodes("/HABERLER/HABER");
            var listData = new List<Data>();

            foreach (XmlNode node in nodeListEmlak)
            {
                listData.Add(
                    new Data
                    {
                        Title = node.SelectSingleNode("adv_title").InnerText,
                        Image = node.SelectSingleNode("adv_image").InnerText,
                        Description = node.SelectSingleNode("adv_text").InnerText,
                        Link = node.SelectSingleNode("adv_def_link").InnerText,
                        Category = "Hürriyet Emlak",
                        Type = "Xml",
                    }
                    );
            }

            foreach (XmlNode item in nodeListMahmure)
            {
                listData.Add(
                    new Data
                    {
                        Title = item.SelectSingleNode("BASLIK").InnerText,
                        Image = item.SelectSingleNode("RESIM").InnerText,
                        Description = item.SelectSingleNode("METIN").InnerText,
                        Link = item.SelectSingleNode("LINK").InnerText,
                        Category = "Mahmure",
                        Type = "Xml",
                    }
                    );
            }

            SqlHelper.InsertList(listData);
        }
    }
}


// DOSYADAN OKUMA

//XmlDocument xmlDocMahmure = new XmlDocument();
//xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\mahmure.xml");

//XmlDocument xmlDocEmlak = new XmlDocument();
//xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");
//            XmlDocument xmlDocMahmure = new XmlDocument();
//xmlDocMahmure.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\mahmure.xml");
//            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");
//XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");