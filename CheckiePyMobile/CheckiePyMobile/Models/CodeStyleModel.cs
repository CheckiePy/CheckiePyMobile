using Newtonsoft.Json;

namespace CheckiePyMobile.Models
{
    public class CodeStyleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Repository { get; set; }
        [JsonProperty(PropertyName = "calc_status")]
        public string CalculationStatus { get; set; }
    }
}
