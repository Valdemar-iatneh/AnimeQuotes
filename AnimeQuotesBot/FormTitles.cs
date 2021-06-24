using Newtonsoft.Json;
using System.Collections.Generic;

namespace AnimeQuotesBot
{
    class FormTitles
    {
        [JsonProperty("title")]
        public List<string> MyArray { get; set; }
    }
}
