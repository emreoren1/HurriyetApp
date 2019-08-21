using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SizinIcinSectiklerimiz.UI.Models
{
    public class Data
    {
        public string Title { get; set; }
        [JsonProperty("ImagePath")]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
