using Newtonsoft.Json;

namespace SizinIcinSectiklermiz.Data.Models
{
    public class Data
    {
        public string Title { get; set; }
        [JsonProperty("ImagePath")]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
    }
}
