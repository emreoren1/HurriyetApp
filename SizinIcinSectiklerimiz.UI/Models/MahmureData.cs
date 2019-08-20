using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SizinIcinSectiklerimizXml.Models
{
    [Serializable()]
    public class MahmureData
    {
        [XmlElement(ElementName ="BASLIK")]
        public string Baslik { get; set; }
        [XmlElement(ElementName = "METIN")]
        public string Metin { get; set; }
        [XmlElement(ElementName = "RESIMADI")]
        public string ResimAdi { get; set; }
        [XmlElement(ElementName = "RESIM")]
        public string Resim { get; set; }
        [XmlElement(ElementName = "LINK")]
        public string Link { get; set; }
        [XmlElement(ElementName = "TARIH")]
        public DateTime Tarih { get; set; }

        [Serializable()]
        [XmlRoot("HABERLER")]
        public class MahmureList
        {
            public List<MahmureData> Mahmures { get; set; }
        }
    }
}
