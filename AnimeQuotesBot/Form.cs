using Newtonsoft.Json;

namespace AnimeQuotesBot
{
    public partial class Form
    {
        [JsonProperty("anime")]
        public string Anime { get; set; }
    
        [JsonProperty("character")]
        public string Character { get; set; }
    
        [JsonProperty("quote")]
        public string Quote { get; set; }
    }
}
