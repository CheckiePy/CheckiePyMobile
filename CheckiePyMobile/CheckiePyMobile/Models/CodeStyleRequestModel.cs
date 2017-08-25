using Newtonsoft.Json;

namespace CheckiePyMobile.Models
{
    public class CodeStyleRequestModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "repository")]
        public string Repository { get; set; }
    }
}
