using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SizinIcinSectiklermiz.Data.Models
{
    public class NewsData
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Spot")]
        public string Spot { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Link")]
        public string Link { get; set; }
        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("Order")]
        public int Order { get; set; }

        public class NewsList
        {
            public List<NewsData> News { get; set; }
        }
    }
}
