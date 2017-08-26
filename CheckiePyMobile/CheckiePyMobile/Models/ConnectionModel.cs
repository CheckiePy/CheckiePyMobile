using Newtonsoft.Json;

namespace CheckiePyMobile.Models
{
    public class ConnectionModel
    {
        [JsonProperty(PropertyName = "code_style")]
        public int CodeStyle { get; set; }
        [JsonProperty(PropertyName = "repository")]
        public int Repository { get; set; }
    }
}
