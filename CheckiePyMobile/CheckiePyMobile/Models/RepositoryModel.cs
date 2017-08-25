using Newtonsoft.Json;

namespace CheckiePyMobile.Models
{
    public class RepositoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "is_connected")]       
        public bool IsConnected { get; set; }
        public bool IsDisconnected => !IsConnected;
        [JsonProperty(PropertyName = "code_style_name")]
        public string CodeStyleName { get; set; }
    }
}
