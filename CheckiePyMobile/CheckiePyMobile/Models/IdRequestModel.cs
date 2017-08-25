using Newtonsoft.Json;

namespace CheckiePyMobile.Models
{
    public class IdRequestModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}
