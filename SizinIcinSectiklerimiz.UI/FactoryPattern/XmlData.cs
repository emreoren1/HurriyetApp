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
            var listData = new List<Data>();
            XmlDocument xmlDocEmlak = new XmlDocument();
            xmlDocEmlak.Load("C:\\Users\\Emre\\Desktop\\HurriyetApp\\SizinIcinSectiklerimiz.UI\\SizinIcinSectiklerimiz.UI\\Data\\emlak.xml");

            var client = new WebClient();
            string xmlMahmure = client.DownloadString("http://mahmure.hurriyet.com.tr/hurriyet/anasayfa/xml/2/");
            string xmlYeniBirIs = client.DownloadString("http://www.yenibiris.com/service/articlewidget");
            string xmlHurriyetAile = client.DownloadString("http://www.hurriyetaile.com/feed/hurriyet-sizin-icin-sectiklerimiz.xml");

            XmlDocument xml = new XmlDocument();
            
            xml.LoadXml(xmlMahmure);
            XmlNodeList nodeListMahmure = xml.DocumentElement.SelectNodes("/HABERLER/HABER");

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
                        Type = "Xml"
                    }
                    );
            }

            xml.LoadXml(xmlYeniBirIs);
            XmlNodeList nodeListYeniBirIs = xml.DocumentElement.SelectNodes("/Items/Item");

            foreach (XmlNode item in nodeListYeniBirIs)
            {
                listData.Add(
                    new Data
                    {
                        Title = item.SelectSingleNode("Title").InnerText,
                        Image = item.SelectSingleNode("ImagePath").InnerText,
                        Link = item.SelectSingleNode("Link").InnerText,
                        Description = item.SelectSingleNode("Description").InnerText,
                        Category = "Yeni Bir İş",
                        Type = "Xml"
                    }
                    );
            }

            xml.LoadXml(xmlHurriyetAile);
            XmlNodeList nodeListHurriyetAile = xml.DocumentElement.SelectNodes("/Items/Item");

            foreach (XmlNode item in nodeListHurriyetAile)
            {
                listData.Add(
                    new Data
                    {
                        Title = item.SelectSingleNode("Title").InnerText,
                        Image = item.SelectSingleNode("ImagePath").InnerText,
                        Description = item.SelectSingleNode("Priority").InnerText,
                        Link = item.SelectSingleNode("Link").InnerText,
                        Category = "Hürriyet Aile",
                        Type = "Xml"
                    }
                    );
            }

            XmlNodeList nodeListEmlak = xmlDocEmlak.DocumentElement.SelectNodes("/Advertorial/adv");

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
                        Type = "Xml"
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


//XmlDocument xmlDocMahmure = new XmlDocument();
//XmlDocument xmlDocYeniBirIs = new XmlDocument();
//XmlDocument xmlDocHurriyetAile = new XmlDocument();
//xmlDocMahmure.LoadXml(xmlMahmure);
//XmlNodeList nodeListMahmure = xmlDocMahmure.DocumentElement.SelectNodes("/HABERLER/HABER");
//xmlDocYeniBirIs.LoadXml(xmlYeniBirIs);
//XmlNodeList nodeListYeniBirIs = xmlDocYeniBirIs.DocumentElement.SelectNodes("/Items/Item");
//xmlDocHurriyetAile.LoadXml(xmlHurriyetAile);
//XmlNodeList nodeListHurriyetAile = xmlDocHurriyetAile.DocumentElement.SelectNodes("/Items/Item");