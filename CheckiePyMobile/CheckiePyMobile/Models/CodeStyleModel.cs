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
        [JsonIgnore]
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
